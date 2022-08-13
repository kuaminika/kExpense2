using KDBTools.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace KDBTools
{
    public class Konfigs_default:IKonfig
    {
        private IConfiguration config;
        private string connectionstringName;

        public Konfigs_default(IConfiguration config)
        {
            this.config = config;
            this.connectionstringName = config["connectionstringName"] ?? "default";
        }

        public string ConnectionString => config.GetConnectionString(this.connectionstringName);

        public int GetIntValue(string v) => int.Parse(this.config[v]);

        public string GetOrDefault(string key, string defaultValue)
        {
            string result = config[key] ?? defaultValue;
            return result;
        }
    }



    /// <summary>
    /// Instances of this class only use Console.out.Writeline to log certain things.
    /// </summary>
    public class KLogTool : IKLogTool
    {
        public void Log(string msg)
        {
            string fullMsg = $"{DateTime.Now}: {msg}";

            Console.Out.WriteLine(fullMsg);
        }
    }
}
