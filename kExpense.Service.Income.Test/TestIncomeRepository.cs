using kExpense.Service.Income.Source;
using kExpense.Service.Income.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace kExpense.Service.Income.Test
{
    public class TestIncomeRepository
    {
        IIncomeSourceRepository incomeSourceRepo;
        IIncomeRepository repository;

        [SetUp]
        public void Setup()
        {

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("kExpenseConfig.json", optional: false, reloadOnChange: false).Build();

            IKonfig konfig = new Konfigs(config);
            IncomeSourceRepoFactory_A f = new IncomeSourceRepoFactory(new IncomeSourceRepositoryToolBox { DataGateway = new DataGateway(konfig.ConnectionString), QueryHolder = new IncomeSourceQueries() });
            IncomeRepositoryToolBox toolbox = new IncomeRepositoryToolBox { DataGateway = new DataGateway(konfig.ConnectionString) };
            toolbox.OrgId = konfig.GetIntValue("orgId");
            toolbox.QueryHolder = new IncomeQueries();
            IncomeRepositoryFactory_A ff = new IncomeRepositoryFactory(toolbox);
            incomeSourceRepo = f.Create();
            repository = ff.Create();
        }

        [Test]
        public void TestInsertingIncome()
        {
            IIncomeModel newIncome = new NewIncomeModel { IncomeDate = DateTime.Now, BriefDescription = "they gave it", Amount = 10, ProductName = "Utwagwan", OrgId = 2 };
            var sourceList = incomeSourceRepo.FindSourcesLikeThis();
            List<RecordedIncomeModel> list = repository.FindIncomesLikeThis();
            int buffer = 10;
            int beforeAmount = list.Count;

            if (sourceList.Count == 0)
            {
                RecordedSource incomeSourceModel = new RecordedSource { Name = $"Sandra{beforeAmount + buffer}", Address = "123 rue okap, No Okap Haiti", Email = $"no email for sandra{beforeAmount + buffer}", Phone = "514-123-1234" };

                newIncome.Source =incomeSourceRepo.InsertIncomeSource(incomeSourceModel);
            }
            else
                newIncome.Source = sourceList[0] as RecordedSource;



            repository.InsertIncome(newIncome);

            list = repository.FindIncomesLikeThis();


            int after = list.Count;
            list.ForEach(e => Console.Out.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(e)));

            Console.Out.WriteLine($"before {beforeAmount}, after {after}");

            Assert.Greater(after, beforeAmount);
        }

    }



}