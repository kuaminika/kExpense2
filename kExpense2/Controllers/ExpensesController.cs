using System;

using System.Collections.Generic;
using KExpense.Model;
using kExpense2.kConfigs;
using kExpense2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace kExpense2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {//TODO: need to figure out how to return 500s
        private KxtensionsConfig _config;
        private KExpenseToolBox toolBox;
        public ExpensesController(IConfiguration Configuration)
        {
            _config = new KxtensionsConfig( Configuration);
            toolBox = new KExpenseToolBox(_config);
        }



        [HttpGet]
        public List<IKExpense> Get()
        {
            try
            {
                List<IKExpense> result =  toolBox.service.GetAll();
                return result;
            }
            catch(Exception ex)
            {
                List<IKExpense> result = new List<IKExpense>();
                var error = new ErrorModels.ErrorExpense { SpentOnName = "failed to return"+ this._config.connectionString, BriefDescription = ex.Message };
                result.Add(error);
                return result;
            }
        }

        [HttpPost]        
        public IKExpense Post( ExpenseModel newExpense)
        {
            try
            {
                IKExpense result =  toolBox.service.RecordExpense(newExpense);
                return result;
            }
            catch (Exception ex)
            {
                var error = new ErrorModels.ErrorExpense { SpentOnName = "failed to return", BriefDescription = ex.Message };
                return error;
            }
        }

        [HttpPost]
        [Route("whatISent")]
        public IKExpense whatISent(ExpenseModel newExpense)
        {
            try
            {
                return newExpense;
            }
            catch (Exception ex)
            {
                var error = new ErrorModels.ErrorExpense { SpentOnName = "failed to return", BriefDescription = ex.Message };
                return error;
            }
        }



        [HttpGet]
        [Route("Delete/{victimId}")]
        public IKResultModel Delete(int victimId)
        {
            try
            {
                //TODO : need to do situation where victimId = 0
                //TODO : need to do situation where victimID is non exisitent. perhaps confirm that 0 recordsdeleted
               int affectedRpwCount =  toolBox.service.DeleteExpenseWithId(victimId);


                var result =  new SuccessModel($"{affectedRpwCount} affected by change");
                //  HttpResponseMessage re = Request.CreateResponse<SuccessModel>(System.Net.HttpStatusCode.OK, result);
                return result;
            }
            catch (Exception ex)
            {
                var error = new ErrorModels.ErrorExpense { SpentOnName = "failed to return", BriefDescription = ex.Message };


              // HttpResponseMessage re = Request.CreateResponse<ErrorExpense>(System.Net.HttpStatusCode.InternalServerError, error);
                return error;
            }
        }

    }
}
