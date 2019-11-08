
namespace CPR.Data.Import.Models
{

    public class Descriptor
    {
        public string Header { get; set; }
        public string ColumnName { get; set; }

        public System.Data.SqlDbType Type { get; set; }
    }
}
