using KExpense.Model;
using KExpense.Repository.interfaces;
using KExpense.Repository.kModelMappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KExpense.Repository
{
    public class KExpenseRepository:IKExpenseRepository
    {
        private readonly  AKDBAbstraction dbAbstraction;
        public int OrgId { get; set; } = 0;
        public KExpenseRepository(int orgId,AKDBAbstraction db)
        {
            this.OrgId = orgId;
            this.dbAbstraction = db;
        }

        private DateTime strToDate(string str)
        {

            string dateFmt = "yyyyMMdd";
            const DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;
           /* if (dateFmt == null)
            {
                var dateInfo = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat;
                dateFmt = dateInfo.GetAllDateTimePatterns();
            }*/
            var result = (DateTime.TryParseExact(str, dateFmt,
                 CultureInfo.InvariantCulture, style, out var dt)) ? dt : null as DateTime?;
            return result.GetValueOrDefault(DateTime.MinValue);

        }


        public List<IKExpense> GetAllKExpenses(int org_id = 0, string sortby = "id")
        {

            //TODO - test when org_id is something else
            List<IKExpense> result = new List<IKExpense>();
               string allExpenses =  @" SELECT * 
             FROM kExpense e 
             inner join  kForeignPartyOrgn o on e.kThirdPartyOrgn_id = o.id
             inner join  kOrgnProduct p on p.id = e.kOrgnProduct_id ";

            allExpenses += org_id == 0 ? string.Empty : string.Format(" where korgn_id={0}", OrgId);
            allExpenses += "order by e." + sortby + " desc";
            dbAbstraction.ExecuteReadTransaction(allExpenses, new AllMapper((kdt)=> {
                while (kdt.Read())
                {
                    var p = new ExpenseModel();
                    p.BriefDescription = kdt.GetString("reason");
                    p.ExpenseDate = strToDate(kdt.GetString("transactionDate"));
                    p.Cost = kdt.GetDecimal("amount");
                    p.MerchantName = kdt.GetString("name_denormed");
                    p.Id = kdt.GetInt("id");
                    p.SpendingOrgId = kdt.GetInt("korgn_id");
                    p.SpentOnName = kdt.GetString("name");
                    result.Add(p);
                 }
            }));
            return result;


        }





         DataExpenseModel validateProductAndMerchant(IKExpense expense)
        {
            DataExpenseModel result = new DataExpenseModel();
            result.copy(expense);

            //TODO: need to thing of scenario when reason not found
            try
            {
                string productSearchQuery = string.Format("SELECT id from kOrgnProduct p where p.name='{0}'", result.SpentOnName);
                dbAbstraction.ExecuteReadTransaction(productSearchQuery, new AllMapper(kdataReader =>
                {
                    if (!kdataReader.YieldedResults) return;
                    result.ForProductId = kdataReader.GetInt("id");
                }));
            }
            catch (Exception ex) {
                result.ForProductId = 0; 
            }// todo: need to log  error 

            result.MerchantId = 1;
            //TODO: the default merchant id should be a variable
            try
            {
                //todo: need to thing of scenario when merchant not found
                string merchantSearchQuery = string.Format("SELECT id from kForeignPartyOrgn p where p.name_denormed='{0}'", result.MerchantName);
                dbAbstraction.ExecuteReadTransaction(merchantSearchQuery, new AllMapper(kdataReader =>
                {
                    if (!kdataReader.YieldedResults) return;
                    result.MerchantId = kdataReader.GetInt("id");
                }));
            }
            catch {
                result.MerchantId = 1; 
            }// todo: need to log  error 

            return result;
        }

        public IKExpense RecordExpense(IKExpense newExpense)
        {
            DataExpenseModel newExpenseData = validateProductAndMerchant(newExpense);
        

            string insertQuery = @"insert into kExpense(reason, amount,transactionDate,kOrgn_id,kThirdPartyOrgn_id,kOrgnProduct_id)
                                    value ( '{0}',  {1},'{2}',{3},{4},{5});";
            long last_id = 0;
            insertQuery = string.Format(insertQuery, newExpenseData.BriefDescription, newExpenseData.Cost, newExpenseData.ExpenseDate.ToString("yyyyMMdd"), newExpenseData.SpendingOrgId, newExpenseData.MerchantId, newExpenseData.ForProductId);
            last_id =  dbAbstraction.ExecuteWriteTransaction(insertQuery).LastInsertedId;
            

           /*
            *
                //for some reason calling SP is not working so using manual insert instead
                string query = @"CALL `record_expense`({0},{1},{2},{3},{4},'{5}',{6},{7}); ";

                query = string.Format(query, product_id, newExpense.ExpenseDate.Year, newExpense.ExpenseDate.Month, newExpense.ExpenseDate.Day, newExpense.Cost, newExpense.BriefDescription, merchant_id, newExpense.SpendingOrgId);
                int last_id = 0;
                dbAbstraction.ExecuteReadTransaction(query, new AllMapper(kdataReader =>
                {
                    last_id = kdataReader.GetInt("id");
                }));
           */
            newExpense.Id = (int)last_id;
            return newExpense;

        }

        public List<IKExpense> GetAllKExpensesForMonth(int year, int month, int product_id)
        {
            List<IKExpense> result = new List<IKExpense>();
            List<KSP_Param> parameters = new List<KSP_Param>();
            parameters.Add(new KSP_Param { Name = "product_id",    Type = KSP_ParamType.Int, Value = product_id.ToString() }) ;
            parameters.Add(new KSP_Param { Name = "expense_year",  Type = KSP_ParamType.Int, Value = year.ToString() });
            parameters.Add(new KSP_Param { Name = "expense_month", Type = KSP_ParamType.Int, Value = month.ToString() });
            var mapper = new AllMapper((kdt) => {
                while (kdt.Read())
                {
                    var p = new ExpenseModel();
                    p.BriefDescription = kdt.GetString("reason");
                    p.ExpenseDate = strToDate(kdt.GetString("transactionDate"));
                    p.Cost = kdt.GetDecimal("amount");
                    p.MerchantName = kdt.GetString("name_denormed");
                    p.Id = kdt.GetInt("id");
                    p.SpendingOrgId = kdt.GetInt("korgn_id");
                    p.SpentOnName = kdt.GetString("name");
                    result.Add(p);
                }
            });
            dbAbstraction.ExecuteReadSPTranasaction("getExpensesForProduct", parameters, mapper);

            return result;
        }

        public bool DeleteExense(IKExpense victim)
        {
            DataExpenseModel victimData = validateProductAndMerchant(victim);
            /*string query = @"CALL `delete_expense`({0},{1},{2},{3},{4},'{5}',{6},{7}); ";

            query = string.Format(query, product_id, newExpense.ExpenseDate.Year, newExpense.ExpenseDate.Month, newExpense.ExpenseDate.Day, newExpense.Cost, newExpense.BriefDescription, merchant_id, newExpense.SpendingOrgId);
            int last_id = 0;*/


            string query = @" delete from kExpense k 
		    where k.id = {0}
		      and k.reason = '{1}' 
		      and k.amount = {2}
		      and k.transactionDate = '{3}'
		      and k.korgn_id = {4}
		      and k.kThirdPartyOrgn_id = {5}
		      and k.kOrgnProduct_id = {6};";
            query = string.Format(query, victimData.Id, victimData.BriefDescription, victimData.Cost, victimData.ExpenseDate.ToString("yyyyMMdd"), victimData.SpendingOrgId, victimData.MerchantId, victimData.ForProductId);
            long oneIfGood = dbAbstraction.ExecuteWriteTransaction(query);
            bool result =    oneIfGood == 1;
            return result;
        }

        /// <summary>
        /// returns amount of Rows affected
        /// </summary>
        /// <param name="victim"></param>
        /// <returns></returns>
        public int DeleteExense(IKExpense victim)
        {
            DataExpenseModel victimData = validateProductAndMerchant(victim);
            /*string query = @"CALL `delete_expense`({0},{1},{2},{3},{4},'{5}',{6},{7}); ";

            query = string.Format(query, product_id, newExpense.ExpenseDate.Year, newExpense.ExpenseDate.Month, newExpense.ExpenseDate.Day, newExpense.Cost, newExpense.BriefDescription, merchant_id, newExpense.SpendingOrgId);
            int last_id = 0;*/

            //TODO : note the k variable does not seem to be available in all versions of mysql ... perhaps it'll be good to specify in a readme.md to choose the right verison of mysql
            string query = @" delete from kExpense k 
		    where k.id = {0}
		      and k.reason = '{1}' 
		      and k.amount = {2}
		      and k.transactionDate = '{3}'
		      and k.korgn_id = {4}
		      and k.kThirdPartyOrgn_id = {5}
		      and k.kOrgnProduct_id = {6};";
            query = string.Format(query, victimData.Id, victimData.BriefDescription, victimData.Cost, victimData.ExpenseDate.ToString("yyyyMMdd"), victimData.SpendingOrgId, victimData.MerchantId, victimData.ForProductId);
            var result =  dbAbstraction.ExecuteWriteTransaction(query);
            return result.AffectedRowCount;
        }

        public int DeleteExenseById(IKExpense victim)
        {
            string query = $@" delete from kExpense where id = {victim.Id};";
            var  oneIfGood = dbAbstraction.ExecuteWriteTransaction(query);
            return  oneIfGood.AffectedRowCount;
        }

        public IKExpense GetById(int id)
        {
            //TODO - test when org_id is something else
            List<IKExpense> result = new List<IKExpense>();
            string allExpenses = $@" SELECT * 
             FROM kExpense e 
             inner join  kForeignPartyOrgn o on e.kThirdPartyOrgn_id = o.id
             inner join  kOrgnProduct p on p.id = e.kOrgnProduct_id 
             where e.id = {id}";

          
            dbAbstraction.ExecuteReadTransaction(allExpenses, new AllMapper((kdt) => {
                while (kdt.Read())
                {
                    var p = new ExpenseModel();
                    p.BriefDescription = kdt.GetString("reason");
                    p.ExpenseDate = strToDate(kdt.GetString("transactionDate"));
                    p.Cost = kdt.GetDecimal("amount");
                    p.MerchantName = kdt.GetString("name_denormed");
                    p.Id = kdt.GetInt("id");
                    p.SpendingOrgId = kdt.GetInt("korgn_id");
                    p.SpentOnName = kdt.GetString("name");
                    result.Add(p);
                }
            }));
            return result[0];
        }
    }
}
