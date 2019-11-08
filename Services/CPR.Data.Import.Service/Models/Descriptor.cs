
namespace CPR.Data.Import.Models
{

    public class Descriptor
    {
        public string Prefix { get; set; }
        public Property[] Properties { get; set; }
    }
    public class Property
    {
        public string Header { get; set; }
        public string ColumnName { get; set; }
        public int? Length { get; set; }
        public System.Data.SqlDbType Type { get; set; }
    }
}
