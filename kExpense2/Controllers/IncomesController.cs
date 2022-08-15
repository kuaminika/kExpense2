using kExpense.Service.Income;
using kExpense.Service.Income.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using kExpense.Service.Income.Source;

namespace kExpense2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        IIncomeService service;

        public IncomesController(IConfiguration configuration)
        {
            service = Igniter.Ignite(configuration);
        }


        [HttpGet]
        public List<RecordedIncomeModel> GetIncomes()
        {
            try
            {
                List<RecordedIncomeModel> list = service.FindIncomes();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpPost]
        [Route("AddSource")]
        public RecordedSource AddSource(NewIncomeSource newSource)
        {
            try
            {

                RecordedSource recordedSource = service.AddSource(newSource);
                return recordedSource;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpGet]
        [Route("Sources")]
        public List<RecordedSource> GetIncomeSources()
        {
            try
            {
                IIncomeSourceModel blnk = new NewIncomeSource();
                List<IIncomeSourceModel> list = service.FindIncomeSources(blnk);
                List<RecordedSource> results = new List<RecordedSource>();
                list.ForEach(e=>results.Add(e as RecordedSource));
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Update")]
        public RecordedIncomeModel UpdateIncome(RecordedIncomeModel income)
        {
            string output = JsonConvert.SerializeObject(income);
            Console.Out.WriteLine(output);
            var result = service.UpdateIncome(income);
            return result;
        }

        [HttpPost]
        public RecordedIncomeModel AddIncome(NewIncomeModel newIncome)
        {
            string output = JsonConvert.SerializeObject(newIncome);
            Console.Out.WriteLine(output);
            var result =   service.InsertIncome(newIncome);
            return result;
        }

        [HttpPost]
        [Route("Delete")]
        public int Delete(RecordedIncomeModel victim)
        {
          int affectedRows =   service.DeleteIncomeById(victim);
            return affectedRows;
        }


    }
}
