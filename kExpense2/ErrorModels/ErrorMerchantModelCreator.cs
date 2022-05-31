using System;

namespace kExpense2.ErrorModels
{
    public class ErrorMerchantModelCreator
    {
        private ErrorMerhantModel result = new ErrorMerhantModel();



        public ErrorMerhantModel CreateFromException(Exception ex)
        {
            result.Name = ex.GetType().Name;
            result.Message = ex.Message;


            digIn(ex);

            return result;

        }
        private void digIn(Exception x)
        {
            if (x == null) return;
            digIn(x.InnerException);

            result.Name += $"{x.GetType().Name}";
            result.Message += $"-->{x.Message}";

        }
        public static ErrorMerchantModelCreator get() => new ErrorMerchantModelCreator();
    }

}
