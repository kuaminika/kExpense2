using Dapper;
using kExpense.Service.Income.Source;
using kExpense.Service.Income.Utils;
using System;
using System.Collections.Generic;

namespace kExpense.Service.Income
{
    public interface IIncomeRepository
    {
        RecordedIncomeModel InsertIncome(IIncomeModel incomeModel);
        List<RecordedIncomeModel> FindIncomesLikeThis(IIncomeModel q = null);
        int DeleteIncomeById(RecordedIncomeModel victim);
    }


    public class IncomeRepository : IIncomeRepository
    {
        IIncomeQueries incomeQueries;
        IDataGateway dataGateway;
        int orgId;
        
        public IncomeRepository(IncomeRepositoryToolBox toolBox)
        {
            dataGateway = toolBox.DataGateway;
            orgId = toolBox.OrgId;
            incomeQueries = toolBox.QueryHolder;
        }

        public List<RecordedIncomeModel> FindIncomesLikeThis(IIncomeModel q = null)
        {
            IIncomeModel queryObj = q?? getBlankModel();

            DynamicParameters parameters = findDynamicParams(queryObj);
            string query = incomeQueries.FindIncomesLikeThis(parameters);
        Console.Out.WriteLine(query);
           List<RecordedIncomeModel> recordeds = dataGateway.ExecuteReadManyResult<RecordedIncomeModel>(query);

            return recordeds;
        }

        private DynamicParameters findDynamicParams<T>(T q)
        {
            var  map = KSqlMapper.Create;
            DynamicParameters reslt = new DynamicParameters();
            foreach ( var p  in q.GetType().GetProperties())
            {
                if (!map.Has(p.PropertyType)) continue;
                reslt.Add(p.Name, p.GetValue(q), map.Get(p.PropertyType));
            }

            return reslt;
        }

        private IIncomeModel getBlankModel()
        {
            IIncomeModel result = new NewIncomeModel { BriefDescription = "", IncomeDate = DateTime.MinValue, Amount = 0, Source = new RecordedSource(), ProductName = "", OrgId = orgId };
            return result;
        }


        int getProductId(IIncomeModel incomeModel)
        {
             //TODO: need to thing of scenario when reason not found
            try
            {
                string productSearchQuery = string.Format("SELECT id from kOrgnProduct p where p.name='{0}'", incomeModel.ProductName);
                int rslt =0;
                dataGateway.ExecuteScalar(productSearchQuery,new KDBAbstractions.AllMapper(reader=>{
                    if (!reader.Read()||!reader.YieldedResults) return ;
                    rslt = reader.GetInt("id");
                }));
                return rslt;
            }
            catch (Exception ex) {
               Console.Out.WriteLine(ex.Message);
               return 0;
            }// todo: need to log  error 
        }

        public RecordedIncomeModel InsertIncome(IIncomeModel incomeModel)
        {
            RecordedIncomeModel result = RecordedIncomeModel.Copy(incomeModel);
            result.SourceId = (incomeModel.Source as RecordedSource).Id;
            result.ProductId = getProductId(incomeModel);
            DynamicParameters parameters = findDynamicParams(result);     
            string query = incomeQueries.InsertQuery(parameters);
            var outcome = dataGateway.ExecuteInsert(query);          
            result.Id = (int)outcome.LastInsertedId;
            return result;
        }

        public int DeleteIncomeById(RecordedIncomeModel victim)
        {
            DynamicParameters parameters = findDynamicParams(victim);
           string query = incomeQueries.DeleteIncome(parameters);
            Console.Out.WriteLine(query);

            var outcome =      dataGateway.Execute(query);

            return outcome.AffectedRowCount;
        }
    }
}