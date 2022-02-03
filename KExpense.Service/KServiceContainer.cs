using kContainer;
using KExpense.Service.abstracts;
using KExpense.Service.factories;
using Org.BouncyCastle.Crypto.Agreement.Kdf;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace KExpense.Service
{
    public class KServiceContainer
    {
        private static KServiceContainer _instance;
        private IKServiceConfig _configs;

        public static KServiceContainer Instance
        {
            get
            {
                _instance = _instance ?? new KServiceContainer(KIgniter.Instance);
                return _instance;
            }
        }

        private KServiceContainer(KIgniter ig)
        {
            this._configs = ig.IgniteServiceConfig();
        }



        //     public KService 


        private class KServMapping
        {
            public string Name { get; set; }
            public KService Repo { get; set; }

            public Type RepoType { get; set; }
        }


    }
}
