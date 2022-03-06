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

    public  class ErrorExpenseCreator
    {
        private ErrorExpense result = new ErrorExpense();

     
        public  ErrorExpense CreateFromException(Exception ex)
        {
            result.SpentOnName = ex.GetType().Name;
            result.BriefDescription = ex.Message;
            result.MerchantName = this.GetType().Name;
            digIn(ex);
            return result;
        }

        private  void digIn(Exception x)
        {
            if (x == null) return;
            digIn(x.InnerException);

            result.SpentOnName = $"{result.SpentOnName} --> {x.GetType().Name}";
            result.BriefDescription = $@"{result.BriefDescription} --
                    {x.Message}";

        }

        public static ErrorExpenseCreator get() => new ErrorExpenseCreator();
    }
}
