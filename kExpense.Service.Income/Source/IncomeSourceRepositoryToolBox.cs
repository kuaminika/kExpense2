using kExpense.Service.Income.Utils;

namespace kExpense.Service.Income.Source
{
    public class IncomeSourceRepositoryToolBox
    {

        public IDataGateway DataGateway { get; set; }
        public IIncomeSourceQueries QueryHolder { get; set; }

    }
}
