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
        string ProductName {get;set;}
        RecordedSource Source { get; set; }
    }
}
