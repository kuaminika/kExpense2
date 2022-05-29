namespace kExpense.Service.Income.Source
{
    public class RecordedSource : IIncomeSourceModel
    {
        public int Id { get; set; }
        public int SourceDetailsId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public static RecordedSource Convert(IIncomeSourceModel original)
        {
            return new RecordedSource { Address = original.Address, Name = original.Name, Phone = original.Phone, Email = original.Email };
        }
    }

}
