using kExpense2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kExpense2.ErrorModels
{
    public class KError : IKResultModel
    {
        //TODO this shouldnt be hardocoded
        public string Message => "it just didnt work";
    }
}
