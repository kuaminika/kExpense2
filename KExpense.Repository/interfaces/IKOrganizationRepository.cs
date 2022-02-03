using System;
using System.Collections.Generic;

namespace KExpense.Repository.interfaces
{
    public interface IKOrganizationRepository
    {
         List<KExpense.Model.IOrganization> GetAll();

    }
}
