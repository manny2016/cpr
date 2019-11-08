

using System;
using System.Collections.Generic;
using System.Linq;

namespace Org.Joey.Common
{
    public class GivenStringCompare : IObjectCompare<string, IEnumerable<string>>
    {
        private CompareGates Gate { get; set; }
        private IEnumerable<string> CompareTo { get; set; }
        public string RefFieldName { get; private set; }
        public GivenStringCompare(
            IEnumerable<string> compareto,
            string refFieldName = null,
            string outStr = null,
            CompareGates gate = CompareGates.EqualTo)
        {
            this.Gate = gate;
            this.CompareTo = compareto;
            this.OutStr = outStr;
            this.RefFieldName = refFieldName;
        }
        //public string OutStr { get; set; }

        public ObjectChooseOptions Option { get; set; }
        public string OutStr { get; private set; }

        public bool Compare(string a, IEnumerable<string> b)
        {
            if (b == null) b = this.CompareTo;
            switch (this.Gate)
            {
                case CompareGates.EqualTo:
                    return b.Any(o => o.Equals(a, StringComparison.OrdinalIgnoreCase));
                case CompareGates.Unequal:
                    return @b.Any(o => o.Equals(a, StringComparison.OrdinalIgnoreCase));
                default:
                    throw new NotSupportedException();
            }

        }

        public bool Compare(object a, object b)
        {
            return Compare(a as string, b as IEnumerable<string>);
        }
    }
}
