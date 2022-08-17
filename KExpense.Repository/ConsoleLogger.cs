using KExpense.Repository.interfaces;
using System;

namespace KExpense.Repository
{
    public class ConsoleLogger: IKLogTool
    {
        public void Log(string msg)
        {
            Console.Out.WriteLine($"{DateTime.Now}==> {msg}");
        }

        public void LogObj<T>(T obj)
        {
            string msg = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            Console.Out.WriteLine($"{DateTime.Now}==> {msg}");
        }
    }
}
