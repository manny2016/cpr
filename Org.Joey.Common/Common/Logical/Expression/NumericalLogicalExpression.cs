


namespace Org.Joey.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class NumericalLogicalExpression : LogicalExpression<decimal>
    {
        public NumericalLogicalExpression(CompareGates compare, string fieldName, params decimal[] values)
            : base(compare, fieldName, values)
        {

        }

        public override decimal[] Values { get; set; }


        public override string GenerateExpression()
        {
            if (this.Values == null || this.Values.Length == 0) return string.Empty;
            switch (this.CompareGate)
            {
                case CompareGates.EqualTo:
                    return $"{this.FieldName}={this.Values[0]}";
                case CompareGates.Unequal:
                    return $"{this.FieldName}!={this.Values[0]}";
                case CompareGates.Contain:
                    return $"{this.FieldName} IN ({string.Join(",", this.Values)}";
                case CompareGates.Between:
                    return $"{this.FieldName} BETWEEN {this.Values[0]} AND {this.Values[1]}";
                case CompareGates.LessThan:
                    return $"{this.FieldName} < {this.Values[0]}";
                default:
                    throw new NotSupportedException($"The string type not suppport [{this.CompareGate.ToString()}] ");
            }
        }
       
    }
}
