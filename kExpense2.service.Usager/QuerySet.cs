using Dapper;

namespace kExpense2.service.Usager
{
    public class QuerySet
    {
        string query = @"SELECT  ";

        public string GetUsagersLikeThis(DynamicParameters query)
        {
            UsagerModel m = scandQueryObj(query);

            string queryStr = $@"SELECT  p.id             `Id`,
                    p.`description`  `Description`,
                    p.`name`         `Name`,
                    p.`korgn_id`     `OrgId`
                FROM houseofm_kExpense_DEV.kOrgnProduct p
                WHERE (p.id = {m.Id} or (p.id <> {m.Id} and 0={m.Id}))
                AND (p.`name` = '{m.Name}' or (p.`name`<>'{m.Name}' and ''='{m.Name}'))
                AND (p.`description`= '{m.Description}' or (p.`description`<>''{m.Description}'' and ''='{m.Description}'))
                AND (p.kOrgn_id = {m.OrgId} or (p.kOrgn_id <>{m.OrgId} and 0 ={m.OrgId} ))";

            return queryStr;

        }
        private UsagerModel scandQueryObj(DynamicParameters parameters)
        {

            UsagerModel result = new UsagerModel();

            result.Id = parameters.Get<int>("Id");
            result.Name = parameters.Get<string>("Name");
            result.OrgId = parameters.Get<int>("OrgId");

            return result;
        }


    }
}
