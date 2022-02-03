using KExpense.Model;
using KExpense.Repository.interfaces;
using KExpense.Repository.kModelMappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Repository
{
    public class OrgRepository : IKOrganizationRepository
    {
        private readonly AKDBAbstraction dbAbstraction;

        public OrgRepository(AKDBAbstraction db)
        {
            this.dbAbstraction = db;
        }
        public List<IOrganization> GetAll()
        {
            string query = @"SELECT d.id, 
                                    email,
                                    name,
                                    phone,
                                    address
                            from kOrgnDesc d 
                            inner join kOrgn o on d.id = o.`desc.id`";
            KOrgnMapper mapper = new KOrgnMapper();
            dbAbstraction.ExecuteReadTransaction(query, mapper);
            var result = mapper.Models;

            return result;
        }
    }
}
