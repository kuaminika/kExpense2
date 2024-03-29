﻿using KExpense.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Service
{
    public interface IKExpenseService
    {
        List<IKExpense> GetAll();
        List<IKExpense> GetAllForMonth(int year, int month,int associatedProductId);
        IKExpense RecordExpense(IKExpense newExpense);
        int DeleteExpense(ExpenseModel newExpense);
        int DeleteExpenseWithId(int victimId);
        IKExpense UpdateExpense(ExpenseModel expenseToUpdate);
    }
}
