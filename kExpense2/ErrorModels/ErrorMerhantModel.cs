using kExpense2.ViewModels;
using MerchantService.Models;

namespace kExpense2.ErrorModels
{
    public class ErrorMerhantModel : IMerchantModel,IKResultModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Error fetching merchant";
        public string Phone { get; set; }
        public string Address { get => Message; set => Message = value; } 
        public string Message { get; set; } = "Error fetching merchant";
    }

}
