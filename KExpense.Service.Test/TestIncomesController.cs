using kExpense.Service.Income;
using kExpense.Service.Income.Source;
using kExpense.Service.Income.Utils;
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
            NewIncomeModel newIncome = new NewIncomeModel { IncomeDate = DateTime.Now, BriefDescription = $"from controller {beforeAmount}", Amount = 10 * beforeAmount, ProductName = "Utwagan Emir", OrgId = 2 };

            newIncome.Source = new RecordedSource { Id = 12 };
            specimen.AddIncome(newIncome);

            list = specimen.GetIncomes();

            int after = list.Count;
            Assert.Greater(after, beforeAmount);
        }
        [Test]
        public void TestUpdatingIncomeFromController()
        {

            List<RecordedIncomeModel> list = specimen.GetIncomes();
            int beforeAmount = list.Count;
            NewIncomeModel newIncome = new NewIncomeModel { IncomeDate = DateTime.Now, BriefDescription = $"from controller {beforeAmount}", Amount = 10 * beforeAmount, ProductName = "Utwagan Emir", OrgId = 2 };

            newIncome.Source = new RecordedSource { Id = 12 };
            specimen.AddIncome(newIncome);
            list = specimen.GetIncomes();
            RecordedIncomeModel victim = list[0];
            RecordedIncomeModel before =  RecordedIncomeModel.Copy(victim);
            ProductModel beforeP = before.Product;
            victim.Product = new ProductModel { Name = "Elie Bertrand", Id = 1 };
            victim = specimen.UpdateIncome(victim);

            list = specimen.GetIncomes();
            Console.Out.WriteLine($"comparing before:{beforeP.Id} and after {victim.Product.Id}");
            Assert.AreNotEqual(beforeP.Id, victim.Product.Id);
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