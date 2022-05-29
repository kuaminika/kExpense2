using System.Collections.Generic;

namespace kExpense.Service.Income.Source
{
    public interface IIncomeSourceRepository
    {
        RecordedSource FindSourceLikeThis(IIncomeSourceModel potentialKnownSource);
        RecordedSource InsertIncomeSource(IIncomeSourceModel potentialKnownSource);
        List<IIncomeSourceModel> FindSourcesLikeThis(IIncomeSourceModel tmmp= null);
    }





}
