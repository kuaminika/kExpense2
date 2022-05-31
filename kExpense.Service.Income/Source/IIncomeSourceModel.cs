namespace kExpense.Service.Income.Source
{
    public interface IIncomeSourceModel
    {
        string Name { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Address { get; set; }
    }



}
