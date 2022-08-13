using System;
using System.Collections.Generic;
using System.Text;

namespace KDBTools.interfaces
{
    public interface IKonfig
    {
        string ConnectionString { get; }

        int GetIntValue(string v);
        string GetOrDefault(string v, string name);
    }
}
