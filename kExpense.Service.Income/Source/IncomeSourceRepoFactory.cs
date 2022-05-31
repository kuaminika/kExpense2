using kExpense.Service.Income.Utils;

namespace kExpense.Service.Income.Source
{
    public abstract class IncomeSourceRepoFactory_A
    {
        protected IncomeSourceRepositoryToolBox toolBx;
        public IncomeSourceRepoFactory_A(IncomeSourceRepositoryToolBox toolbox)
        {
            this.toolBx = toolbox;
        }
        public abstract IIncomeSourceRepository Create();
    }

    public class IncomeSourceRepoFactory : IncomeSourceRepoFactory_A
    {

        public IncomeSourceRepoFactory(IncomeSourceRepositoryToolBox toolbox) : base(toolbox) { }
        public  override IIncomeSourceRepository Create()
        {
            IIncomeSourceRepository result = new IncomeSourceRepository(toolBx);
            return result;
        }
    }





}
