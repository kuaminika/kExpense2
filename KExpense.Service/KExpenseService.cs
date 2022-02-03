using KExpense.Repository.interfaces;
using KExpense.Model;
using System;
using System.Collections.Generic;

namespace KExpense.Service
{
    public class  KExpenseService: IKExpenseService
    {
        IKExpenseRepository kexpenseRepository;

        public KExpenseService(IKExpenseRepository kexpenseRepository)
        {
            this.kexpenseRepository = kexpenseRepository;
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
    }
}
