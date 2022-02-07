using KExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Repository.interfaces
{
    public interface IKExpenseRepository
    {
        List<IKExpense> GetAllKExpenses(int org_id =0,string sortby="id");
     
        IKExpense RecordExpense(IKExpense newExpense);
        List<IKExpense> GetAllKExpensesForMonth(int year, int month, int productId);
    }
}
