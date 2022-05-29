using kExpense.Service.Income.Source;
using Microsoft.Extensions.Configuration;

namespace kExpense.Service.Income.Utils
{
    public class Igniter
    {
        public static IIncomeService Ignite(IConfiguration config)
        {

         
            IKonfig konfig = new Konfigs(config);
            IncomeSourceRepoFactory_A f = new IncomeSourceRepoFactory(new IncomeSourceRepositoryToolBox { DataGateway = new DataGateway(konfig.ConnectionString), QueryHolder = new IncomeSourceQueries() });
            IncomeRepositoryToolBox toolbox = new IncomeRepositoryToolBox { DataGateway = new DataGateway(konfig.ConnectionString) };
            toolbox.OrgId = konfig.GetIntValue("orgId");
            toolbox.QueryHolder = new IncomeQueries();
            IncomeRepositoryFactory_A ff = new IncomeRepositoryFactory(toolbox);
             IncomeServiceToolbox args = new Income.IncomeServiceToolbox();

            args.IncomeSourceRepo = f.Create();
            args.Repository = ff.Create();
            IncomeServiceFactory_A factory = new IncomeServiceFactory(args);
            IIncomeService service = factory.Create();
            return service;
        }
    }

}
