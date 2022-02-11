using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kExpense2.ViewModels
{
    public struct SuccessModel: IKResultModel
    {
        private string msg;

        public SuccessModel(string msg)
        {
            this.msg = msg;
        }
        public string Message=> $"it worked {msg}";
    }
}
