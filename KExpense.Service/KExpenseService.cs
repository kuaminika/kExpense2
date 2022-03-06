using KExpense.Repository.interfaces;
using KExpense.Model;
using System;
using System.Collections.Generic;
using KExpense.Repository;
using KExpense.Service.factories;

namespace KExpense.Service
{
    public class  KExpenseService: IKExpenseService
    {
        IKExpenseRepository kexpenseRepository;
        private ToolBox toolBox;

        public KExpenseService(ToolBox toolBox)
        {
            this.toolBox = toolBox;
            this.kexpenseRepository = toolBox.ExpenseReo;
        }

        public int DeleteExpense(ExpenseModel newExpense)
        {
            int result =  this.kexpenseRepository.DeleteExense(newExpense);
            return result;
        }

        public int DeleteExpenseWithId(int victimId)
        {
            Models.ProxyExpenseModel victim = new Models.ProxyExpenseModel(victimId,this.toolBox);
            int result = this.kexpenseRepository.DeleteExenseById(victim);
            return result;
        }

        public List<IKExpense> GetAll()
        {
            List <IKExpense> result =  this.kexpenseRepository.GetAllKExpenses();
            return result;
        }

        public List<IKExpense> GetAllForMonth(int year, int month,int productId)
        {
            List<IKExpense> result = this.kexpenseRepository.GetAllKExpensesForMonth(year, month,productId);
            return result;
        }

        public IKExpense RecordExpense(IKExpense newExpense)
        {
            IKExpense savedRecord = this.kexpenseRepository.RecordExpense(newExpense);
            return savedRecord;
        }

        public IKExpense UpdateExpense(ExpenseModel expenseToUpdate)
        {
            try
            {
                bool updateWentWell = this.kexpenseRepository.UpdateExpense(expenseToUpdate) == 1;
                if (updateWentWell) return expenseToUpdate;

                throw new Exception("Failed to update");//TODO: shouldn't be harcoding error message
            }
            catch(Exception ex)
            {
                throw ex;
            
            }

        }
    }
}
