using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Org.Joey.Common
{
    public class StringValueCompare : JObjectCompare<string>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(StringValueCompare));
        public StringValueCompare(LogicGates logic,
           CompareGates gate,
            ObjectChooseOptions options,
          Func<JObject, string> selectorA,
          Func<JObject, string> selectorB) : base(gate, options, selectorA, selectorB)
        {

        }
        public override bool Compare(JObject a, JObject b)
        {

            if (a == null || b == null)
            {

                this.OutStr = $"Compare String Value; Object A or B is null return default false value.";
                Logger.Warn(this.OutStr);
                return false;
            }
            var result = false;
            var va = this.SelectorA(a);
            var vb = this.SelectorB(b);
            if (va != null) va = va.ToUpper().Trim();
            if (vb != null) vb = vb.ToUpper().Trim();

            switch (this.CompareGate)
            {
                case CompareGates.EqualTo:
                    result = va == vb;
                    break;
                case CompareGates.Unequal:
                    result = va != vb;
                    break;
                case CompareGates.Format:
                default:
                    throw new NotSupportedException();
            }
            this.OutStr = $"Compare String Value A={va??"(Blank)"};B={vb??"(Blank)"};CompareGate={this.CompareGate}; Result:{result}";
            Logger.Warn(this.OutStr);
            return result;
        }
    }
}
