using KExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Repository
{
     class DataExpenseModel :  ExpenseModel
    {

        public void copy(IKExpense original)
        {            
            Id = original.Id;
            SpendingOrgId = original.SpendingOrgId;
            ExpenseDate = original.ExpenseDate;
            BriefDescription = original.BriefDescription;
            SpentOnName = original.SpentOnName;
            Cost = original.Cost;
            MerchantName = original.MerchantName;
        }
        public int ForProductId { get; set; } = 5;
        public int MerchantId { get; set; } = 0;
    }
}
