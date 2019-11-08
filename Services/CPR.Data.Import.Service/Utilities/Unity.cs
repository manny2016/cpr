



namespace CPR.Data.Import
{
    using CPR.Data.Import.Models;
    using Microsoft.Extensions.Configuration;
    using Org.Joey.Common;
    using System.Linq;
    using System.Collections.Generic;
    public static class Unity
    {

        public static Descriptor GetDescriptor(DataSourceNames name)
        {
            return IoC.GetService<IConfiguration>()
                .GetSection("mappings")
                .Get<Dictionary<DataSourceNames, Descriptor>>()[name];
        }
        public static string GetWatchingFolder()
        {
            return IoC.GetService<IConfiguration>()
                .GetValue<string>("watchingFolder");
        }
    }
}
