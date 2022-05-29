using kExpense.Service.Income.Source;
using System.Collections.Generic;

namespace kExpense.Service.Income
{



    public interface IIncomeService
    {
        List<IIncomeSourceModel> FindIncomeSources(IIncomeSourceModel source);
        List<RecordedIncomeModel> FindIncomes();
        RecordedIncomeModel InsertIncome(IIncomeModel newIncome);
        int DeleteIncomeById(RecordedIncomeModel victim);
    }


    public class IncomeService : IIncomeService
    {
        IIncomeRepository incomeRepository;
        IIncomeSourceRepository incomeSourceRepository;
        private IncomeServiceToolbox args;

        
        public IncomeService(IncomeServiceToolbox args)
        {
            this.args = args;
            incomeRepository = args.Repository;
            incomeSourceRepository = args.IncomeSourceRepo;
        }

        public void AddIncome(IIncomeModel incomeModel)
        {
            IIncomeSourceModel potentialKnownSource = incomeModel.Source;
            RecordedSource recordedSource = incomeSourceRepository.FindSourceLikeThis(potentialKnownSource);

            if (recordedSource == null)
            {
                recordedSource = incomeSourceRepository.InsertIncomeSource(potentialKnownSource);
            }

            incomeModel.Source = recordedSource;

            incomeRepository.InsertIncome(incomeModel);

        }

        public int DeleteIncomeById(RecordedIncomeModel victim)
        {
          int affectedRows =   incomeRepository.DeleteIncomeById(victim);
            return affectedRows;
        }

        public List<RecordedIncomeModel> FindIncomes()
        {
           var result =  incomeRepository.FindIncomesLikeThis();
            return result;
        }

        public List<IIncomeSourceModel> FindIncomeSources(IIncomeSourceModel source)
        {
            List<IIncomeSourceModel> sourceList = incomeSourceRepository.FindSourcesLikeThis(source);
            return sourceList;
        }

        public RecordedIncomeModel InsertIncome(IIncomeModel newIncome)
        {
           var sourceList = incomeSourceRepository.FindSourcesLikeThis(newIncome.Source);
            if (sourceList.Count == 0)
            {               
                newIncome.Source = incomeSourceRepository.InsertIncomeSource(newIncome.Source);
            }

            RecordedIncomeModel  result = incomeRepository.InsertIncome(newIncome);

            return result;
        }
    }
}