using Dapper;
using KDBAbstractions.Repository.interfaces;
using kExpense.Service.Income.Utils;
using System.Collections.Generic;

namespace kExpense.Service.Income.Source
{
    public class IncomeSourceRepository : IIncomeSourceRepository
    {
        private IIncomeSourceQueries queryHolder;
        private IDataGateway dataGateway;
        public IncomeSourceRepository(IncomeSourceRepositoryToolBox toolbox)
        {
            dataGateway = toolbox.DataGateway;
            queryHolder = toolbox.QueryHolder;
        }

        public RecordedSource FindSourceLikeThis(IIncomeSourceModel potentialKnownSource)
        {

            DynamicParameters parameters = getDynamicParameters(potentialKnownSource);

            string query = queryHolder.FindSourceLikeThis(parameters);

            RecordedSource recordedSource = dataGateway.ExecuteReadOneQuery<RecordedSource>(query);

            return recordedSource;
        }
        private DynamicParameters getDynamicParameters(IIncomeSourceModel potentialKnownSource)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("Name", potentialKnownSource.Name, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("Email", potentialKnownSource.Email, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("Phone", potentialKnownSource.Phone, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("Address", potentialKnownSource.Address, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            return parameters;

        }
        public RecordedSource InsertIncomeSource(IIncomeSourceModel potentialKnownSource)
        {

            DynamicParameters parameters = getDynamicParameters(potentialKnownSource);


            RecordedSource details = findIncomeSourceDetails(parameters);
            if (details == null)
            {

                string query = queryHolder.InsertSourceDetails(parameters);
                dataGateway.ExecuteInsert(query);
               // parameters = getDynamicParameters(potentialKnownSource);
                details = findIncomeSourceDetails(parameters);
            }

            RecordedSource recordedSource = details;
            parameters.Add("SourceDetailsId", recordedSource.SourceDetailsId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            string insertQuery = queryHolder.InsertSource(parameters);
            KWriteResult kWriteResult = dataGateway.ExecuteInsert(insertQuery);
            recordedSource.Id = (int)kWriteResult.LastInsertedId;

            return recordedSource;


        }

        private RecordedSource findIncomeSourceDetails(DynamicParameters parameters)
        {
            string findDetailsQuery = queryHolder.FindSourceDetails(parameters);
            RecordedSource recordedSourceDetails = dataGateway.ExecuteReadOneQuery<RecordedSource>(findDetailsQuery);
            return recordedSourceDetails;
        }

        public List<IIncomeSourceModel> FindSourcesLikeThis(IIncomeSourceModel tmmp = null)
        {
            IIncomeSourceModel tmp = tmmp ??new NewIncomeSource { Address = "", Email = "", Name = "", Phone = "" };
            var args = getDynamicParameters(tmp);
            string query =  queryHolder.FindSourcesLikeThis(args);
            List<RecordedSource> recordedSources = dataGateway.ExecuteReadManyResult<RecordedSource>(query);

            List<IIncomeSourceModel> result = new List<IIncomeSourceModel>();

            recordedSources.ForEach(p => result.Add(p));

            return result;


        }
    }
}
