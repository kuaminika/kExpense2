using kExpense.Service.Income.Source;
using kExpense.Service.Income.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace kExpense.Service.Income.Test
{
    public class TestIncomeService
    {
        IIncomeService service;
        [SetUp]
        public void Setup()
        {

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("kExpenseConfig.json", optional: false, reloadOnChange: false).Build();

            service = Igniter.Ignite(config);
        }

        [Test]
        public void TestAddingIncome()
        {
            List<RecordedIncomeModel> list = service.FindIncomes();
            int beforeAmount = list.Count;
            IIncomeModel newIncome = new NewIncomeModel { IncomeDate = DateTime.Now, BriefDescription = $"they gave it {beforeAmount}", Amount = 10*beforeAmount, InvestmentName = "Utwagwan", OrgId = 2 };
   
            newIncome.Source = new RecordedSource { Id = 12 };
            service.InsertIncome(newIncome);

            list = service.FindIncomes();

            int after = list.Count;
            Assert.Greater(after, beforeAmount);
        }
        [Test]
        public void TestDeletingIncome()
        {
            List<RecordedIncomeModel> list = service.FindIncomes();


            int beforeAmount = list.Count;
            RecordedIncomeModel victim = list[list.Count - 1];

            service.DeleteIncomeById(victim);
            

            list = service.FindIncomes();

            int after = list.Count;

            Console.Out.WriteLine($"{beforeAmount}--{after}");
            Assert.Greater( beforeAmount,after);
        }
    }
}