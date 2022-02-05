using kContainer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kExpense2.kConfigs
{
    public class KxtensionsConfig : IKServiceConfig
    {
        private IConfiguration _config;

        public KxtensionsConfig(IConfiguration innerConfig)
        {
            _config = innerConfig;
        }
        //todo: need to find a way to make this work without hardocding string
        public string connectionString { get { return string.IsNullOrEmpty(Get("connectionString"))? "server=mi3-ts9.a2hosting.com;user id=houseofm_kExpense;persistsecurityinfo=True;database=houseofm_kExpense;password=kExpense1000": Get("connectionString"); } }

        public int orgId { get => GetInt("orgId"); }

        public string Get(string key)
        {
          string result =   _config[key];
            return result?? string.Empty;
        }

        public int GetInt(string key)
        {
            string result = _config[key];
            int r;
            bool itsGood = int.TryParse(result, out r);

            return itsGood ? r : 0;
        }
    }
}
