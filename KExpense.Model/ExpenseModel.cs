using System;

namespace KExpense.Model
{
    public interface IKExpense
    {
         int Id { get; set; }
         DateTime ExpenseDate { get; set; }
         string BriefDescription { get; set; }
         string Reason { get; set; }
         decimal Cost { get; set; }
    }

    public class ExpenseModel: IKExpense
    {
        public int Id { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string BriefDescription { get; set; }
        public string Reason { get; set; }

        public decimal Cost { get; set; }
    }
}
