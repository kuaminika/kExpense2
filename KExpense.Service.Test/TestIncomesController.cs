using kExpense.Service.Income;
using kExpense.Service.Income.Source;
using kExpense2.Controllers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KExpense.Service.Test
{
    public class TestIncomesController
    {
        IncomesController specimen;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("kExpenseConfig.json", optional: false, reloadOnChange: false).Build();

            specimen = new IncomesController(config);
        }


        [Test]
        public void TestFetchingIncomesFromController()
        {
            List<RecordedIncomeModel> list = specimen.GetIncomes();
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }

        [Test]
        public void TestAddingIncomeFromController()
        {

            List<RecordedIncomeModel> list = specimen.GetIncomes();
            int beforeAmount = list.Count;
            NewIncomeModel newIncome = new NewIncomeModel { IncomeDate = DateTime.Now, BriefDescription = $"from controller {beforeAmount}", Amount = 10 * beforeAmount, ProductName = "Utwagwan", OrgId = 2 };

            newIncome.Source = new RecordedSource { Id = 12 };
            specimen.AddIncome(newIncome);

            list = specimen.GetIncomes();

            int after = list.Count;
            Assert.Greater(after, beforeAmount);
        }

        [Test]
        public void TestDeletingIncomeFromController()
        {
            List<RecordedIncomeModel> list = specimen.GetIncomes();

            int beforeAmount = list.Count;
            RecordedIncomeModel victim = list[list.Count - 1];
            specimen.Delete(victim);


            list = specimen.GetIncomes();

            int after = list.Count;
            Console.Out.WriteLine($"{beforeAmount}--{after}");
            Assert.Greater(beforeAmount, after);
        }
    }
}