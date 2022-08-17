using System;
using System.Collections.Generic;
using System.Text;

namespace kExpense.Service.Income.Utils
{
    public class DefaultLogger : IKLogTool
    {
        public void Log(string msg)
        {
           Console.Out.WriteLine($"{DateTime.Now}->{msg}");
        }

        public void logObject<T>(T list)
        {
           string msg =    Newtonsoft.Json.JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);

            Console.Out.WriteLine($"{DateTime.Now}->{msg}");
        }
    }
}
