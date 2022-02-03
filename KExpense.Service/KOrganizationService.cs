using KExpense.Model;
using KExpense.Repository.interfaces;
using KExpense.Service.abstracts;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Service
{
    public class KOrganizationService : KService, IKOrganizationService
    {
        private IKOrganizationRepository kOrganizationRepository;

        public KOrganizationService(IKOrganizationRepository kOrganizationRepository)
        {
            this.kOrganizationRepository = kOrganizationRepository;
        }


        public List<IOrganization> GetAll()
        {
            List<IOrganization> result = this.kOrganizationRepository.GetAll();
            return result;
        }


    }
}
