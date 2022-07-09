using kExpense2.Controllers;
using MerchantService.Models;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KExpense.Service.Test
{
    public class TestMerchantController
    {
        MerchantsController specimen;

        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("kExpenseConfig.json", optional: false, reloadOnChange: false).Build();
            specimen = new MerchantsController(config);

        }

        [Test]
        public void TestGetAllMerchants()
        {
            try
            {

                List<IMerchantModel> merchants = specimen.Get();
            }
            catch (Exception ex)
            {
                string m = ex.Message;

                while(ex.InnerException!=null)
                {
                    m += "-->" + ex;
                    ex = ex.InnerException;
                }

                Assert.Fail(m);
                throw;
            }


        }




    }
}