
using System;
using System.Collections.Generic;
using System.Text;

namespace kContainer
{
    public class KIgniter
    {
        private static KIgniter _instance;
        public static KIgniter Instance { get {

                _instance = _instance ?? new KIgniter();
                return _instance;
            
            }  }

        public IKServiceConfig IgniteServiceConfig()
        {
            IKServiceConfig result = new kSericeConfig();
            return result;
        }
    }
}
