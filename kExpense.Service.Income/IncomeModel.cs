using kExpense.Service.Income.Source;
using System;
using System.Globalization;

namespace kExpense.Service.Income
{


    public class RecordedIncomeModel : IIncomeModel
    {
        public int Id { get; set; }
        public int OrgId { get; set; }
        public DateTime IncomeDate { get => strToDate(RawDate); set { RawDate = value.ToString("yyyyMMdd"); } }
        public string BriefDescription { get; set; }
        public decimal Amount { get; set; }
        public RecordedSource Source { get { _source = _source ?? ghostSource();
                return _source;
            } set=>_source=value; }
        RecordedSource _source;
        private RecordedSource ghostSource()//TODO : need to make sure this ghost source works right
        {
            var result = new RecordedSource{ Id = 0, Name = "", Email = "", Phone="ghost", Address="ghost"};
            return result;
        }
        public ProductModel Product { get { _product = _product ?? ghostProduct();return _product; } set => _product = value; }
        ProductModel _product;
        private ProductModel ghostProduct() { return new ProductModel { Name = "ghost", Description = "ghost", OrgId = OrgId };  }
        
        public int SourceId { get => Source.Id; internal set => Source.Id = value; }
        public string RawDate { get; internal set; }
        public string ProductName { get=>this.InvestmentName; set=>InvestmentName=value;  }
        public string SourceEmail { get => Source.Email; internal set => Source.Email = value; }
        public string SourceName { get => Source.Name; internal set => Source.Name = value; }
        public string InvestmentName { get => Product.Name; internal set => Product.Name = value; }
        public int ProductId { get => Product.Id; internal set => Product.Id = value; }    

        private DateTime strToDate(string str)
        {

            string dateFmt = "yyyyMMdd";
            const DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;
       
            var result = (DateTime.TryParseExact(str, dateFmt,
                 CultureInfo.InvariantCulture, style, out var dt)) ? dt : null as DateTime?;
            return result.GetValueOrDefault(DateTime.MinValue);

        }

        public static RecordedIncomeModel Copy(IIncomeModel incomeModel)
        {

            var rslt = new RecordedIncomeModel();
            rslt.IncomeDate = incomeModel.IncomeDate;
            rslt.BriefDescription = incomeModel.BriefDescription;
            rslt.Source = incomeModel.Source;
            rslt.OrgId = incomeModel.OrgId;
            rslt.Product = incomeModel.Product;
            rslt.Amount = incomeModel.Amount;
           
            return rslt;
        }

    }


}
