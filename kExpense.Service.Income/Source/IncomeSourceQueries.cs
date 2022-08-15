using Dapper;

namespace kExpense.Service.Income.Source
{
    public class IncomeSourceQueries : IIncomeSourceQueries
    {
        public string FindSourceDetails<T>(T m)
        {
            DynamicParameters parameters = m as DynamicParameters;
            string result = string.Format(@" SELECT id    `SourceDetailsId`,
                                                    email `Email`,
                                                    name  `Name`,
                                                    phone `Phone`,
                                                    address `Address` from kOrgnDesc p where p.name = '{0}' and email = '{1}'",parameters.Get<string>("Name"),parameters.Get<string>("Email"));
            return result;
        }

        public string FindSourceLikeThis<T>(T m,bool ignorePhoneAndAddress = true)
        {
            string ign = ignorePhoneAndAddress ? "ign" : "";
            DynamicParameters parameters = m as DynamicParameters;
            string name = parameters.Get<string>("Name");
            string email = parameters.Get<string>("Email");
            string phone = parameters.Get<string>("Phone");
            string address = parameters.Get<string>("Address");
            string result = $@"	 select f.id  `Id`,
                                  d.`email`   `Email`,
                                  d.`name`    `Name`,
                                  d.`phone`   `Phone`,
                                  d.`address` `Address`
                               from `kForeignIncomePartyOrgn` f
                         inner join `kOrgnDesc` d on f.`desc.id` = d.id
                              where  (d.`email` = '{email}' or (d.`email` <> '{email}' and '{email}' =''  ))           
                                and  (d.`name` = '{name}' or (d.`name`<>'{name}' and '{name}'=''))      
                                and  (d.`phone` = '{phone}' or (d.`name`<>'{phone}' and '{phone}'='' or ('ign'='{ign}')))      
                                and  (d.`address` = '{address}' or (d.`name`<>'{address}' and '{address}'=''or ('ign'='{ign}')))
                        ";
            return result;

        }

        public string FindSourcesLikeThis<T>(T args)
        {
            return FindSourceLikeThis(args);
        }

        public string InsertSource<T>(T m)
        {
            DynamicParameters parameters = m as DynamicParameters;
            string name = parameters.Get<string>("Name");
            string email = parameters.Get<string>("Email");
            int sourceId = parameters.Get<int>("SourceDetailsId");
           string result =  $@"insert into kForeignIncomePartyOrgn ( name_denormed, email_denormed,`desc.id`) 
                                              values ('{name}','{email}',{sourceId});";
            return result;
        }

        public string InsertSourceDetails<T>(T m)
        {
            DynamicParameters parameters = m as DynamicParameters;
            string name = parameters.Get<string>("Name");
            string email = parameters.Get<string>("Email");
            string phone = parameters.Get<string>("Phone");
            string address = parameters.Get<string>("Address");
            string result = $@" insert into `kOrgnDesc` (`name`,`phone`,`address`,`email`) 
                                                values('{name}','{phone}','{address}','{email}');";
            return result;
        }
    }

  



}
