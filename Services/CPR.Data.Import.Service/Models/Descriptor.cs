
namespace CPR.Data.Import.Models
{

    public class Descriptor
    {

        public PropertyDescriptor[] Properties { get; set; }
    }
    public class PropertyDescriptor
    {
        public string Header { get; set; }
        public string FiledName { get; set; }

        [Newtonsoft.Json.JsonProperty("Length", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Length { get; set; }
        public System.Data.SqlDbType Type { get; set; }

    }
    public class PropertyObject
    {
        public PropertyDescriptor Descriptor { get; set; }
        public object Value { get; set; }
    }
}
