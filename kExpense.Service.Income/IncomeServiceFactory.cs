using kExpense.Service.Income.Source;

namespace kExpense.Service.Income
{
    public struct IncomeServiceToolbox
    {
        public IIncomeSourceRepository IncomeSourceRepo { get; set; }
        public IIncomeRepository Repository { get; set; }
    }

    public abstract class IncomeServiceFactory_A
    {
        protected IncomeServiceToolbox args;

        public IncomeServiceFactory_A(IncomeServiceToolbox  args)
        {
            this.args = args;
        }
        public abstract IIncomeService Create();
    }

    public class IncomeServiceFactory : IncomeServiceFactory_A
    { 

        public IncomeServiceFactory(IncomeServiceToolbox args):base(args)
        {
            this.args = args;
        }
        public override IIncomeService Create()
        {
            IIncomeService reslt = new IncomeService(args);
            return reslt;
        }
    }
}