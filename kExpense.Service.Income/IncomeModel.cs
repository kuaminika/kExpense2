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
        public IIncomeSourceModel Source { get; set; }
        public int SourceId { get; internal set; }
        public string RawDate { get; internal set; }

        private DateTime strToDate(string str)
        {

            string dateFmt = "yyyyMMdd";
            const DateTimeStyles style = DateTimeStyles.AllowWhiteSpaces;
       
            var result = (DateTime.TryParseExact(str, dateFmt,
                 CultureInfo.InvariantCulture, style, out var dt)) ? dt : null as DateTime?;
            return result.GetValueOrDefault(DateTime.MinValue);

        }

        internal static RecordedIncomeModel Copy(IIncomeModel incomeModel)
        {

            var rslt = new RecordedIncomeModel();
            rslt.IncomeDate = incomeModel.IncomeDate;
            rslt.BriefDescription = incomeModel.BriefDescription;
            rslt.Source = incomeModel.Source;
            rslt.OrgId = incomeModel.OrgId;
            rslt.Amount = incomeModel.Amount;
           
            return rslt;
        }

    }


}
