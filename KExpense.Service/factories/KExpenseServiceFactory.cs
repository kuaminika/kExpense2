using kContainer;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace KExpense.Service.factories
{
    public class KExpenseServiceFactory
    {
        private string _prefix = "Create";
        private IKServiceConfig configs;

        public KExpenseServiceFactory(IKServiceConfig config)
        {
            this.configs = config;
        }

        public IKExpenseService Create(string creationType)
        {
            creationType =  _prefix + creationType;
            IKExpenseService result = runCreationType(creationType);

            return result;
        }

        private IKExpenseService runCreationType(string creationType)
        {
            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(creationType);

          //todo case when theMethod == null

            IKExpenseService result = (IKExpenseService)theMethod.Invoke(this, null);
            return result;
        }


        public IKExpenseService CreateMysql()
        {

            Repository.KMysql_KDBAbstraction db = new Repository.KMysql_KDBAbstraction(configs.connectionString);
            Repository.interfaces.IKExpenseRepository repo = new Repository.KExpenseRepository(db);
            IKExpenseService esult = new KExpenseService(repo);


            return esult;

        }
    }
}
