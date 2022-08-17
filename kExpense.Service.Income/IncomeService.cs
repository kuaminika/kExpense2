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
        RecordedSource AddSource(NewIncomeSource newSource);
        RecordedIncomeModel UpdateIncome(RecordedIncomeModel income);
        List<RecordedIncomeModel> GetIncomesForMonth(int year, int month, int usagerId);
    }


    public class IncomeService : IIncomeService
    {
        IIncomeRepository incomeRepository;
        IIncomeSourceRepository incomeSourceRepository;
        private IncomeServiceToolbox args;
        Utils.IKLogTool logTool;


        public IncomeService(IncomeServiceToolbox args)
        {
            this.args = args;
            this.logTool = args.LogTool;
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

        public RecordedSource AddSource(NewIncomeSource newSource)
        {
            logTool.Log($"inside {GetType().Name}.AddSource");

            var result =   incomeSourceRepository.InsertIncomeSource(newSource);
            return result;
        }

        public int DeleteIncomeById(RecordedIncomeModel victim)
        {
            logTool.Log($"inside {GetType().Name}.DeleteIncomeById");
            int affectedRows =   incomeRepository.DeleteIncomeById(victim);
            return affectedRows;
        }

        public List<RecordedIncomeModel> FindIncomes()
        {
            logTool.Log($"inside {GetType().Name}.FindIncomes");
            var result =  incomeRepository.FindIncomesLikeThis();
            return result;
        }

        public List<RecordedIncomeModel> GetIncomesForMonth(int year, int month, int usagerId)
        {
            logTool.Log($"inside {GetType().Name}.GetIncomesForMonth");
            List<RecordedIncomeModel> result = incomeRepository.GetIncomesForMonth(year,month,usagerId);
            return result;
        }


        public List<IIncomeSourceModel> FindIncomeSources(IIncomeSourceModel source)
        {
            logTool.Log($"inside {GetType().Name}.FindIncomeSources");
            List<IIncomeSourceModel> sourceList = incomeSourceRepository.FindSourcesLikeThis(source);
            return sourceList;
        }

        public RecordedIncomeModel InsertIncome(IIncomeModel newIncome)
        {
            logTool.Log($"inside {GetType().Name}.InsertIncome");
            var sourceList = incomeSourceRepository.FindSourcesLikeThis(newIncome.Source);
            if (sourceList.Count == 0)
            {
                newIncome.Source = incomeSourceRepository.InsertIncomeSource(newIncome.Source);
            }
            else
                newIncome.Source = sourceList[0] as RecordedSource;
            RecordedIncomeModel  result = incomeRepository.InsertIncome(newIncome);

            return result;
        }

        public RecordedIncomeModel UpdateIncome(RecordedIncomeModel income)
        {
            logTool.Log($"inside {GetType().Name}.UpdateIncome");
            logTool.Log($"before:{Newtonsoft.Json.JsonConvert.SerializeObject(income)}");
            logTool.Log($"looking for:{Newtonsoft.Json.JsonConvert.SerializeObject(income.Source)}");
            var sourceList = incomeSourceRepository.FindSourcesLikeThis(income.Source);
            
            if (sourceList.Count == 0)
            {
                logTool.Log("no source found");
                income.Source = incomeSourceRepository.InsertIncomeSource(income.Source);
            }
            else
                income.Source = sourceList[0] as RecordedSource;
            RecordedIncomeModel result = incomeRepository.UpdateIncome(income);

            return result;
        }
    }
}