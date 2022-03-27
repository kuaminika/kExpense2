using KDBAbstractions.Repository.interfaces;
using KExpense.Service;
using KExpense.Service.factories;
using MerchantService;
using MerchantService.Models;

namespace kExpense2.kConfigs
{
    public class KExpenseToolBox
    {
        private KxtensionsConfig _config;

        public KExpenseToolBox(KxtensionsConfig config)
        {
            _config = config;
        }


        public IKExpenseService service
        {
            get
            {
                KExpenseServiceFactory expenseFactory = new KExpenseServiceFactory(_config);
                IKExpenseService result =  expenseFactory.Create(_config.Get("repositoryType"));
                return result;
            }
        }

        public MerchantService.IMerchantService merchantService
        {
            get
            {
                //TODO create serviceFactory
                MerchantRepositoryCreator repoCreator = MerchantRepositoryCreator.Instanciate;
                var args = new MerchantRepositoryArgs();
                args.RepositoryType =_config.Get("repositoryType");
                args.ConnectionString = _config.connectionString;

                IKRepository<IMerchantModel>  kRepo = repoCreator.Create(args);
                IMerchantService face = new ServiceFacade(kRepo);
                return face;
            }
        }
    }
}
