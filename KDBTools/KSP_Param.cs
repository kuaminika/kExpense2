using System;
using System.Collections.Generic;
using System.Text;

namespace KDBTools.Repository
{
    public enum KSP_ParamDirection {  NoDirection,In,Out }
    public enum KSP_ParamType { Int,Bool,Str,Decimal}
    public class KSP_Param
    {
        public KSP_Param()
        {
            this.Direction = KSP_ParamDirection.In;// default direction;
        }
        public KSP_ParamDirection Direction { get; set; }
        public KSP_ParamType Type { get; set; }
        public string Value { get; set; }
        public string Name { get; internal set; }
    }
}
