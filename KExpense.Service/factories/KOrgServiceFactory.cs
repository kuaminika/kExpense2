using kContainer;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;

namespace KExpense.Service.factories
{
    public class KOrgServiceFactory
    {
        private  string _prefix = "Create";
        private IKServiceConfig configs;

        public KOrgServiceFactory(IKServiceConfig config)
        {
            this.configs = config;
        }



        public IKOrganizationService Create(string creationType)
        {
            creationType = _prefix + creationType;
            IKOrganizationService result =   runCreationType(creationType);

            return result;
        }

        private IKOrganizationService runCreationType(string creationType)
        {
          //  creationType = _prefix + creationType;
            Type thisType = this.GetType();
            MethodInfo theMethod = thisType.GetMethod(creationType);

            if (theMethod == null)
                return new KOrganizationServiceNotSpecified(creationType);

            IKOrganizationService result = (KOrganizationService)theMethod.Invoke(this, null);
            return result;
        }

        public KOrganizationService CreateMysql()
       {
          
            Repository.KMysql_KDBAbstraction db = new Repository.KMysql_KDBAbstraction(configs.connectionString);
            Repository.interfaces.IKOrganizationRepository repo = new Repository.OrgRepository(db);
            KOrganizationService esult = new KOrganizationService(repo);


            return esult;

       }
    }
}
