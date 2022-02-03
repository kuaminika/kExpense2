using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KExpense.Model;
using kExpense2.kConfigs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace kExpense2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
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
            try {
            List<IKExpense> result =  toolBox.service.GetAll();
            return result;
            }
            catch(Exception ex)
            {
                List<IKExpense> result = new List<IKExpense>();
                var error = new ErrorModels.ErrorExpense { BriefDescription = "failed to return", Reason = ex.Message };
                result.Add(error);
                return result;
            }


        }
    }
}
