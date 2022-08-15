using kExpense.Service.Income.Utils;



namespace kExpense.Service.Income
{
    public class IncomeRepositoryToolBox
    {
        public IIncomeQueries QueryHolder { get; set; }
        public IDataGateway DataGateway { get; set; }
        public int OrgId { get;  set; }
        public DefaultLogger LogTool { get; set; }
    }
}
