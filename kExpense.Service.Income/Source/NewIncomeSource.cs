namespace kExpense.Service.Income.Source
{
    public class NewIncomeSource : IIncomeSourceModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

}
