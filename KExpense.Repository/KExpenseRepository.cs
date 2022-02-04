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


        public List<IKExpense> GetAllKExpenses(int org_id )
        {

            //todo - test when org_id is something else
            List<IKExpense> result = new List<IKExpense>();
               string allExpenses =  @" SELECT * 
             FROM kExpense e 
             inner join  kForeignPartyOrgn o on e.kThirdPartyOrgn_id = o.id
             inner join  kOrgnProduct p on p.id = e.kOrgnProduct_id ";

            allExpenses += org_id == 0 ? string.Empty : string.Format(" where korgn_id={0}", OrgId);
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

        public List<IKExpense> GetAllKExpenses()
        {
            return this.GetAllKExpenses(0);
        }
        public IKExpense RecordExpense(IKExpense newExpense)
        {
            //todo: need to thing of scenario when reason not found
            int product_id =0;
            try
            {
                string productSearchQuery = string.Format("SELECT id from kOrgnProduct p where p.name='{0}'", newExpense.SpentOnName);
                dbAbstraction.ExecuteReadTransaction(productSearchQuery, new AllMapper(kdataReader =>
                {
                    if (!kdataReader.YieldedResults) return;
                    product_id = kdataReader.GetInt("id");
                }));
            }
            catch(Exception ex) { product_id = 0; }// todo: need to log  error 
            int merchant_id = 1 ;// todo: the default merchant id should be a variable
            try
            {
                //todo: need to thing of scenario when merchant not found
                string merchantSearchQuery = string.Format("SELECT id from kForeignPartyOrgn p where p.name_denormed='{0}'", newExpense.MerchantName);
                dbAbstraction.ExecuteReadTransaction(merchantSearchQuery, new AllMapper(kdataReader =>
                {
                    if (!kdataReader.YieldedResults) return;
                    merchant_id = kdataReader.GetInt("id");
                }));
            }
            catch { merchant_id = 1; }// todo: need to log  error 
           
            string query = @"CALL `record_expense`({0},{1},{2},{3},{4},'{5}',{6},{7}); ";

            query = string.Format(query, product_id, newExpense.ExpenseDate.Year, newExpense.ExpenseDate.Month, newExpense.ExpenseDate.Day, newExpense.Cost, newExpense.BriefDescription, merchant_id, newExpense.SpendingOrgId);
            int last_id = 0;
            dbAbstraction.ExecuteReadTransaction(query, new AllMapper(kdataReader =>
            {
                last_id = kdataReader.GetInt("id");
            }));
            newExpense.Id = last_id;
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

       
    }
}
