using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Org.Joey.Common
{
    public class DateValueCompare : JObjectCompare<DateTime?>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DateValueCompare));
        public DateValueCompare(CompareGates gate,
           ObjectChooseOptions options,
        Func<JObject, DateTime?> selectorA,
        Func<JObject, DateTime?> selectorB) : base(gate, options, selectorA, selectorB)
        {

        }
        public override bool Compare(JObject a, JObject b)
        {
            if (a == null || b == null)
            {
                this.OutStr = $"Compare Date Value; Object A or B is null return default false value.";
                Logger.Warn(this.OutStr);
                return false;
            }
                

            var va = this.SelectorA(a);
            var vb = this.SelectorB(b);
            if (va != null) va = va.Value.Date;
            if (vb != null) vb = vb.Value.Date;
            bool result = false;

            switch (this.CompareGate)
            {
                case CompareGates.EqualTo:
                    result= va == vb;
                    break;
                case CompareGates.Unequal:
                    result= va != vb;
                    break;
                case CompareGates.LessThan:
                    result= va < vb;
                    break;
                case CompareGates.GreaterThan:
                    result=va > vb;
                    break;
                default:
                    throw new NotSupportedException();
            }
            
            this.OutStr = $"Compare String Value A={va};B={vb};CompareGate={this.CompareGate}; Result:{result}";
            Logger.Warn(this.OutStr);
            return result;
        }
    }
}
