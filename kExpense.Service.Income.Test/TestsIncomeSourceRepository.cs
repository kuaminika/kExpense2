using kExpense.Service.Income.Source;
using kExpense.Service.Income.Utils;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Collections.Generic;

namespace kExpense.Service.Income.Test
{
    public class TestsIncomeSourceRepository
    {
        IIncomeSourceRepository repository;
        [SetUp]
        public void Setup() 
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("kExpenseConfig.json", optional: false, reloadOnChange: false).Build();

            IKonfig konfig = new Konfigs(config);

            IncomeSourceRepoFactory_A f = new IncomeSourceRepoFactory(new IncomeSourceRepositoryToolBox {LogTool=new DefaultLogger(), DataGateway = new DataGateway(konfig.ConnectionString), QueryHolder = new IncomeSourceQueries() });

            repository = f.Create();
        }

        [Test]
        public void TestAddingIncomeSource()
        {

            try
            {
                int buffer = 9;
                List<IIncomeSourceModel> list = repository.FindSourcesLikeThis();
                int beforeAmount = list.Count+buffer;
                IIncomeSourceModel incomeSourceModel = new NewIncomeSource { Name = $"Sandra{beforeAmount}", Address = "123 rue okap, No Okap Haiti", Email = $"no email for sandra{beforeAmount}", Phone = "514-123-1234" };

                repository.InsertIncomeSource(incomeSourceModel);

                list = repository.FindSourcesLikeThis();
                int afterAmount = list.Count+buffer;


                Assert.IsTrue(beforeAmount < afterAmount);
            }
            catch (System.Exception ex)
            {
                string err = ex.Message;


                if (ex.InnerException == null)
                    Assert.Fail(err);


                while (ex.InnerException!=null)
                {
                    err += $"-->\n{ex.Message}";
                    ex = ex.InnerException;
                }

                Assert.Fail(err);
            }
        }

        
    }
}