using kExpense.Service.Income.Source;
using System;



namespace kExpense.Service.Income
{
    public class NewIncomeModel : IIncomeModel
    {
        public int OrgId { get; set; }
        public DateTime IncomeDate { get; set; }
        public string BriefDescription { get; set; }
        public string ProductName { get => Product.Name; set => Product.Name = value; }
        public decimal Amount { get; set; }
        public RecordedSource Source { get; set; }
        public ProductModel Product { get { product = product ?? new ProductModel(); return product; } set => product = value; }


        private ProductModel product ;


    }


}
