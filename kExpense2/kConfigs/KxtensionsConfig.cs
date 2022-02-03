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
        public string connectionString { get { return Get("connectionString"); } }

        public string Get(string key)
        {
          string result =   _config[key];
            return result?? string.Empty;
        }
    }
}
