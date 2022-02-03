using KExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Service
{
    public class KOrganizationServiceNotSpecified : IKOrganizationService
    {
        string queried;
        public KOrganizationServiceNotSpecified(string queried)
        {
            this.queried = queried;
        }
        public List<IOrganization> GetAll()
        {
            string tplt = "Orgnanization Service could not perform GetAll. Service type is not specified. could not find '{0}'";
            throw new Exception(string.Format(tplt,this.queried));
        }
    }
}
