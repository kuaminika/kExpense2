using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Model
{
    public class Orgnanization:IOrganization
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

    }
}
