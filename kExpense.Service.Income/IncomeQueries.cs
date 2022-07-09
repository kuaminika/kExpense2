using Dapper;

namespace kExpense.Service.Income
{
    public interface IIncomeQueries
    {
        string FindIncomesLikeThis<T>(T m);
        string InsertQuery<T>(T m);
        string DeleteIncome<T>(T m);
    }
    public class IncomeQueries : IIncomeQueries
    {
        public string DeleteIncome<T>(T m)
        {
            DynamicParameters parameters = m as DynamicParameters;

            string result = $@"update `kIncome` set deletedTimne=NOW() where id = {parameters.Get<int>("Id")}"; //$@"delete from `kIncome` where id ={parameters.Get<int>("Id")}";

            return result;

        }

        public string FindIncomesLikeThis<T>(T m)
        { 
            RecordedIncomeModel queryObj = getRecordedModel(m);
            string date = queryObj.IncomeDate.ToString("yyyyMMdd");
            string defaultDate = System.DateTime.MinValue.ToString("yyyyMMdd");
            string result = $@"SELECT i.id                   `Id`
                                    , i.reason               `BriefDescription`
                                    , i.amount               `Amount`
                                    , i.transactionDate      `RawDate` 
                                    , p.id                   `SourceId`
                                    , p.email_denormed       `SourceEmail`
                                    , p.name_denormed        `SourceName`
                                    , ifnull(pr.`name`,'')   `InvestmentName`
                                    , i.kOrgnid              `OrgId`
                                    , ifnull(pr.id,0)	     `ProductId`
                              FROM `kIncome` i 
                        INNER JOIN `kForeignIncomePartyOrgn` p on i.kThirdPartyOrgnid = p.id
                         LEFT JOIN `kOrgnProduct` pr on pr.id = i.kProductId
                             WHERE ( i.transactionDate = {date} or (i.transactionDate <>{date} and {date}={defaultDate}))
                               and ( i.id = {queryObj.Id}       or (i.id<>{queryObj.Id} and {queryObj.Id} = 0))
                               and ( i.reason = '{queryObj.BriefDescription}'       or (i.reason<>'{queryObj.BriefDescription}' and '{queryObj.BriefDescription}' = ''))
                               and ( i.deletedTimne is null)
                          ORDER BY i.id DESC     ;";


            return result;           

        }

        public string InsertQuery<T>(T m)
        {
            RecordedIncomeModel queryObj = getRecordedModel(m);

            string result = $@"insert into `kIncome` (`reason`,`amount`,`transactionDate`,`kThirdPartyOrgnid`,`kOrgnid`,`kProductId`) 
                                        values('{queryObj.BriefDescription}',{queryObj.Amount},{queryObj.RawDate},{queryObj.SourceId},{queryObj.OrgId},{queryObj.ProductId})";
            return result;
        }

        private RecordedIncomeModel getRecordedModel<T>(T m)
        {
            DynamicParameters parameters = m as DynamicParameters;
            var result = new RecordedIncomeModel();


            result.BriefDescription = parameters.Get<string>("BriefDescription");
            result.IncomeDate = parameters.Get<System.DateTime>("IncomeDate");
            result.Amount = parameters.Get<decimal>("Amount");
            result.Id = parameters.ParameterNames.AsList().Contains("Id")? parameters.Get<int>("Id"):0;
            result.OrgId = parameters.Get<int>("OrgId");
            result.SourceId = parameters.ParameterNames.AsList().Contains("SourceId")? parameters.Get<int>("SourceId"):0;
            result.ProductId = parameters.ParameterNames.AsList().Contains("ProductId")?parameters.Get<int>("ProductId"):0;
            return result;

        }

    }
}