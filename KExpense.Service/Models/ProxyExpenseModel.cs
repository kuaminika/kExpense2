using KExpense.Model;
using KExpense.Repository.interfaces;
using KExpense.Service.factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Service.Models
{
    public class ProxyExpenseModel: IKExpense
    {
        private IKExpenseRepository expenseRepo;

        //TODO finalize ProxyExpenseModel class such that it uses selects and updates 
        internal ProxyExpenseModel(int id, ToolBox toolBox)
        {
            Id = id;
            this.expenseRepo = toolBox.ExpenseReo;
        }

        public int Id { get; set; }
        IKExpense _cache;
        IKExpense cache { get {
                if (_cache != null) return _cache;

                _cache  = this.expenseRepo.GetById(this.Id);
                return _cache;
            } }

        public int SpendingOrgId { get => cache.SpendingOrgId; set { cache.SpendingOrgId = value; } }
        public DateTime ExpenseDate { get => cache.ExpenseDate; set { cache.ExpenseDate = value; } }
        public string BriefDescription { get => cache.BriefDescription; set { cache.BriefDescription = value; } }
        public string SpentOnName { get => cache.SpentOnName; set { cache.SpentOnName = value; } }
        public decimal Cost { get => cache.Cost; set { cache.Cost = value; } }
        public string MerchantName { get => cache.MerchantName; set { cache.MerchantName = value; } }

    }
}
