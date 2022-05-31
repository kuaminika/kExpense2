namespace kExpense.Service.Income
{

 

    public abstract class IncomeRepositoryFactory_A
    {
        protected IncomeRepositoryToolBox args;

        public IncomeRepositoryFactory_A(IncomeRepositoryToolBox args)
        {
            this.args = args;
        }
        public abstract IIncomeRepository Create();
    }
    public class IncomeRepositoryFactory : IncomeRepositoryFactory_A
    {
        public IncomeRepositoryFactory(IncomeRepositoryToolBox t) : base(t) { }
        public override IIncomeRepository Create()
        {
            IIncomeRepository resuylt = new IncomeRepository(args);
            return resuylt;
        }
    }
}