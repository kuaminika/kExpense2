using System;

namespace KExpense.Model
{
    public interface IKExpense
    {
         int Id { get; set; }
        int SpendingOrgId { get; set; }
         DateTime ExpenseDate { get; set; }
         string BriefDescription { get; set; }
         string SpentOnName { get; set; }
         decimal Cost { get; set; }
         string MerchantName { get; set; }
    }

    public class ExpenseModel: IKExpense
    {
        public int Id { get; set; }
        public int SpendingOrgId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string BriefDescription { get; set; }
        public string SpentOnName { get; set; }
        public decimal Cost { get; set; }
        public string MerchantName { get; set; }
    }
}
