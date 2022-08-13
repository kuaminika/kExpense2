using kExpense2.kConfigs;
using kExpense2.service.Usager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace kExpense2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsagersController : ControllerBase
    {
        private IUsagerService service;
             

        public UsagersController(IConfiguration Configuration)
        {
            service = Igniter.Ignite(Configuration);
        }

        [HttpGet]
        public List<UsagerModel> Get()
        {
            List<UsagerModel> result = service.Get(new UsagerModel());

            return result;
        }
    }
}
