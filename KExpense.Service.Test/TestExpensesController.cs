using kExpense.Service.Income.Utils;
using KExpense.Model;
using kExpense2.Controllers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;

namespace KExpense.Service.Test
{
    public class TestExpensesController
    {
        ExpensesController specimen;
        IKLogTool logTool;

        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("kExpenseConfig.json", optional: false, reloadOnChange: false).Build();
            specimen = new ExpensesController(config);
            logTool = new DefaultLogger();
        }


        [Test]
        public void TestFetchingExpenses()
        {
            List<IKExpense> list = specimen.Get();
            logTool.logObject(list);
        }

        [Test]
        public void TestFetchingExpensesForMarch2022Elie()
        {
            int month = 3;
            int usagerId = 1;
            int year = 2022;

            var newEx = new ExpenseModel { SpendingOrgId = 2, BriefDescription = "test", ExpenseDate = new System.DateTime(2022, 03, 01), Cost = 1000, MerchantName = "STM", SpentOnName = "Elie Bertrand"};
            logTool.logObject(newEx);
            specimen.Post(newEx);


            List<IKExpense> list = specimen.GetExpensesForMonth(year,month,usagerId);
            logTool.logObject(list);
        }


    }
}