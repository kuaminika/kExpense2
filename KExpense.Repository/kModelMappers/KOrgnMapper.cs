using KExpense.Model;
using KExpense.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Repository.kModelMappers
{
    public class KOrgnMapper : IKModelMapper
    {
        private readonly string ID_COLUMN = "id";
        private readonly string ADDRESS_COLUMN = "address";
        private readonly string EMAIL_COLUMN = "email";
        private readonly string NAME_COLUMN = "name";
        private readonly string PHONE_COLUMN = "phone";

        public List<IOrganization> Models { get; private set; }

        public void map(IKDataReader kdt)
        {

            this.Models = new List<IOrganization>();


            while(kdt.Read())
            {
                Orgnanization model = new Orgnanization();
                model.id = kdt.GetInt(ID_COLUMN);
                model.Address = kdt.GetString(ADDRESS_COLUMN);
                model.Email = kdt.GetString(EMAIL_COLUMN);
                model.Name = kdt.GetString(NAME_COLUMN);
                model.Phone = kdt.GetString(PHONE_COLUMN);
                Models.Add(model);
            }
        }
          
    }
}
