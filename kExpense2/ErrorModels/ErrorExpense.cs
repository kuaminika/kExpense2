using KExpense.Model;
using kExpense2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kExpense2.ErrorModels
{
    public class ErrorExpense : IKExpense, IKResultModel
    {
        public int Id { get; set; } = 0;
        public DateTime ExpenseDate { get; set; } = DateTime.Now;
        public string BriefDescription { get; set; } = string.Empty;
        public string SpentOnName { get; set; } = string.Empty; 
        public string MerchantName { get; set; } = string.Empty;
        public decimal Cost { get; set; } = 0;
        public int SpendingOrgId { get; set; } = 0;
        
        public string Message { get { return this.BriefDescription; } }
    }
}
