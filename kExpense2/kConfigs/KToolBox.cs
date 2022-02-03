using KExpense.Service;
using KExpense.Service.factories;

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


    }
}
