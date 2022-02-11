using KExpense.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Service.factories
{
    public class ToolBox
    {
       
        public IKExpenseRepository ExpenseReo { get; internal set; }
    }
}
