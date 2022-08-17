using System;
using System.Collections.Generic;
using System.Text;

namespace kExpense.Service.Income.Utils
{
    public interface IKLogTool
    {
         void Log(string msg);
        void logObject<T>(T list);
    }
}
