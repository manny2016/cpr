


namespace Org.Joey.Common
{
    using Microsoft.SqlServer.Server;
    public static class SqlDataRecordExtension
    {

        

        public static void SetInt64(this SqlDataRecord record, int index, long? value)
        {
            if (value == null)
                record.SetDBNull(index);
            else
                record.SetInt64(index, value ?? 0);
        }
        public static void SetInt32(this SqlDataRecord record, int index, int? value)
        {
            if (value == null)
                record.SetDBNull(index);
            else
                record.SetInt32(index, value ?? 0);
        }
        public static void SetStringIfNullOrEmpty(this SqlDataRecord record, int index, string value)
        {
            if (string.IsNullOrEmpty(value))
                record.SetDBNull(index);
            else
                record.SetString(index, value);
        }
        public static void SetBoolean(this SqlDataRecord record, int index, bool? value)
        {
            if (value == null)
                record.SetDBNull(index);
            else
                record.SetSqlBoolean(index, value ?? false);
        }

        public static void SetDecimal(this SqlDataRecord record, int index, decimal? value)
        {
            if (value == null)
                record.SetDBNull(index);
            else
                record.SetDecimal(index, value ?? 0);

        }
    }
}
