

namespace Org.Joey.Common
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ExpressionGroup
    {
        [JsonProperty("logic")]
        public LogicGates Logic { get; set; }

        [JsonProperty("expressions")]
        public JObject[] Expressions { get; set; }

        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public ExpressionGroup Group { get; set; }
    }
}
