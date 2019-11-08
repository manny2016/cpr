

using Newtonsoft.Json.Linq;

namespace Org.Joey.Common
{
    public class CompareGroup
    {
        public LogicGates LogicGate { get; set; }
        public ICompareExpression[] Expressions { get; set; }
        public CompareGroup Group { get; set; }
        public string OutStr { get; set; }
    }
}
