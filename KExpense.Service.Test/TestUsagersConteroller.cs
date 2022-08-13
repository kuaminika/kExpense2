using kExpense2.Controllers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KExpense.Service.Test
{
    public class TestUsagersConteroller
    {
        UsagersController specimen;
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("kExpenseConfig.json", optional: false, reloadOnChange: false).Build();

            specimen = new UsagersController(config);
        }



        [Test]
        public void TestFetchingUsagers_NoConditions()
        {
            List<kExpense2.service.Usager.UsagerModel> list = specimen.Get();
            Console.Out.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0);
        }


    }
}