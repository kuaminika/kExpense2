using kExpense2.kConfigs;
using MerchantService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace kExpense2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private KxtensionsConfig _config;
        private KExpenseToolBox toolBox;

        public MerchantsController(IConfiguration Configuration)
        {
            _config = new KxtensionsConfig(Configuration);
            toolBox = new KExpenseToolBox(_config);

        }

        [HttpGet]
        public List<IMerchantModel> Get()
        {
            try 
            {
                List<IMerchantModel> result =  toolBox.merchantService.GetAll();
                return result;
            }
            catch(Exception ex)
            {
                List<IMerchantModel> errList = new List<IMerchantModel>();
                errList.Add(ErrorModels.ErrorMerchantModelCreator.get().CreateFromException(ex));
                return errList;
            }
        }

    }
}
