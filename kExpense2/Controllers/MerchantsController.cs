using kExpense2.kConfigs;
using MerchantService;
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
                throw ex;
            }
        }


        [HttpPost]
        public IMerchantModel Post(MerchantModel record)
        {
            try
            {
                IMerchantModel result = toolBox.merchantService.AddMerchant(record);
                return result;
            }
            catch (Exception ex)
            {
                var error = ErrorModels.ErrorMerchantModelCreator.get().CreateFromException(ex);
                return error;
            }
        }

        [HttpPost]
        [Route("whatISent")]
        public IMerchantModel whatISent(MerchantModel record)
        {
            try
            {
                return record;
            }
            catch (Exception ex)
            {
                var error = ErrorModels.ErrorMerchantModelCreator.get().CreateFromException(ex);
                return error;
            }
        }


        [HttpPost]
        [Route("Update")]
        public int Update(MerchantModel merchant)
        {
            try
            {
                var updatedRecord = toolBox.merchantService.UpdateMerchant(merchant);
                return updatedRecord.RecordsAffectedCount;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }



        [HttpPost]
        [Route("Delete")]
        public int  Delete(MerchantModel merchant)
        {
            try
            {
                var updatedRecord = toolBox.merchantService.DeleteMerchant(merchant);
                return updatedRecord.RecordsAffectedCount;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

    }
}
