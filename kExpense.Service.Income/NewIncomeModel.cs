﻿using kExpense.Service.Income.Source;
using System;



namespace kExpense.Service.Income
{
    public class NewIncomeModel : IIncomeModel
    {
        public int OrgId { get; set; }
        public DateTime IncomeDate { get; set; }
        public string BriefDescription { get; set; }
        public string InvestmentName { get; set; }
        public decimal Amount { get; set; }
        public IIncomeSourceModel Source { get; set; }
    }


}
