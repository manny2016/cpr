


namespace Org.Joey.Common
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class StringLogicalExpression : LogicalExpression<string>
    {
        public StringLogicalExpression(CompareGates compare, string fieldName, params string[] values)
           : base(compare, fieldName, values)
        {

        }
        

        public override string GenerateExpression()
        {
            var value = string.Join(",", this.Values.Select((ctx) =>
            {
                var text = ctx.Replace("'", "''");
                return $"'{text}'";
            }));
            switch (this.CompareGate)
            {

                case CompareGates.EqualTo:
                    return $"{this.FieldName}={value}";
                case CompareGates.Unequal:
                    return $"{this.FieldName}!={value}";
                case CompareGates.Contain:
                    return $"{this.FieldName} IN ({value}";
                default:
                    throw new NotSupportedException($"The string type not suppport [{this.CompareGate.ToString()}] CompareGates.");
            }
        }
    }
}
