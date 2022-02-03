using KExpense.Model;
using System.Collections.Generic;

namespace KExpense.Service
{
    public interface IKOrganizationService
    {
        List<IOrganization> GetAll();
    }
}