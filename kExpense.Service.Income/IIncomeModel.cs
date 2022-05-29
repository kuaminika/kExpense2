using kExpense.Service.Income.Source;
using System;



namespace kExpense.Service.Income
{
    public interface IIncomeModel
    {
        int OrgId { get; set; }
        DateTime IncomeDate { get; set; }
        string BriefDescription { get; set; } 
        decimal Amount { get; set; }
        IIncomeSourceModel Source { get; set; }
    }
}
