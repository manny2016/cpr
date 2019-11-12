



namespace CPR.Data.Import
{
    using CPR.Data.Import.Models;
    using Microsoft.Extensions.Configuration;
    using Org.Joey.Common;
    using System;
    using System.Collections.Generic;
    using Microsoft.SqlServer.Server;
    using Org.Joey.Common.Models;
    using System.Linq;
    public static class Unity
    {

        public static PropertyDescriptor[] GetDescriptor(DataSourceNames name)
        {
            return IoC.GetService<IConfiguration>()
                .GetSection("mappings")
                .Get<Dictionary<DataSourceNames, PropertyDescriptor[]>>()[name];
        }
        public static string GetWatchingFolder()
        {
            return IoC.GetService<IConfiguration>()
                .GetValue<string>("watchingFolder");
        }

        public static SqlDataRecord Convert(ExcelSignleRow row, DataSourceNames name)
        {
            try
            {
                if (row == null) throw new ArgumentNullException("data can't be null in convert");
                if (row.PrimaryKeyRequired(name) == false)
                {
                    throw new NullReferenceException($"Primary Key can't be null{name}");
                }
                var record = new SqlDataRecord(Constants.Structures[name]);                
                for (var index = 0; index < Constants.Structures[name].Length; index++)
                {
                    var fieldName = Constants.Structures[name][index].Name;
                    var property = row.Properties.TryFirst(o => o.Descriptor.FiledName.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
                    if (property == null)
                        record.SetDBNull(index);
                    else
                    {
                        if (property.Descriptor.Type == System.Data.SqlDbType.DateTime)
                        {
                            if (DateTime.TryParse(property.Value?.ToString(), out DateTime dt))
                            {
                                record.SetInt64(index, dt.ToUnixStampDateTime());
                            }
                            else
                            {
                                record.SetDBNull(index);
                            }
                        }
                        else if (property.Descriptor.Type == System.Data.SqlDbType.Int)
                        {
                            if (int.TryParse(property.Value?.ToString(), out int val))
                            {
                                record.SetInt32(index, val);
                            }
                            else
                            {
                                record.SetDBNull(index);
                            }
                        }
                        else if (property.Descriptor.Type == System.Data.SqlDbType.Money)
                        {
                            if (decimal.TryParse(property.Value?.ToString(), out decimal money))
                            {
                                record.SetSqlMoney(index, money);
                            }
                            else
                            {
                                record.SetDBNull(index);
                            }
                        }
                        else
                        {
                            record.SetStringIfNullOrEmpty(index, property.Value?.ToString());
                        }
                    }
                }
                return record;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        
    }
}
