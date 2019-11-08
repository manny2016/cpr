

namespace Org.Joey.Common
{
    using Newtonsoft.Json;
    public abstract class LogicalExpression<T> : ILogicalExpression
    {
        public LogicalExpression(CompareGates compare, string fieldName, params T[] values)
        {
            this.CompareGate = compare;
            this.FieldName = fieldName;
            this.Values = values;
        }
        public LogicalExpression() { }

        [JsonProperty("compare")]
        public CompareGates CompareGate { get; set; }

        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("values")]
        public virtual T[] Values { get; set; }

        public abstract string GenerateExpression();      

    }
}
