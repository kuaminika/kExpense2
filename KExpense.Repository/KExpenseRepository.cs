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
       
        public KExpenseRepository(AKDBAbstraction db)
        {
            this.dbAbstraction = db;
        }

        // MySql.Data.MySqlClient.


        /*
         public static DateTime? ToDate(this string dateTimeStr, params string[] dateFmt)
	{
		// example: var dt = "2011-03-21 13:26".ToDate(new string[]{"yyyy-MM-dd HH:mm", 
		//                                                  "M/d/yyyy h:mm:ss tt"});
		const DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;
		if (dateFmt == null)
		{
			var dateInfo = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat;
			dateFmt=dateInfo.GetAllDateTimePatterns();
		}
		var result = (DateTime.TryParseExact(dateTimeStr, dateFmt,
	 		CultureInfo.InvariantCulture, style, out var dt)) ? dt : null as DateTime?;
		return result;
	}
         
         */
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


        public List<IKExpense> GetAllKExpenses()
        {
            List<IKExpense> result = new List<IKExpense>();
               string allExpenses = @" SELECT * 
             FROM kExpense e 
             inner join  kForeignPartyOrgn o on e.kThirdPartyOrgn_id = o.id
             inner join  kOrgnProduct p on p.id = e.kOrgnProduct_id ";
            dbAbstraction.ExecuteReadTransaction(allExpenses, new AllMapper((kdt)=> {


                while (kdt.Read())
                {
                    var p = new ExpenseModel();
                    p.BriefDescription = kdt.GetString("reason");
                    p.ExpenseDate = strToDate(kdt.GetString("transactionDate"));
                    p.Cost = kdt.GetDecimal("amount");
                    p.Id = kdt.GetInt("id");
                    p.Reason = kdt.GetString("name");
                    result.Add(p);
                 }
            }));
            return result;


        }
        public IKExpense RecordExpense(IKExpense newExpense)
        {
            // MySqlConnection
            return new ExpenseModel()
          { Id =0 , BriefDescription ="not implemented yet"};

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
                    p.Id = kdt.GetInt("id");
                    p.Reason = kdt.GetString("name");
                    result.Add(p);
                }
            });
            dbAbstraction.ExecuteReadSPTranasaction("getExpensesForProduct", parameters, mapper);

            return result;
        }
    }
}
