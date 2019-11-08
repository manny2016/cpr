using CPR.Data.Import.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CPR.Data.Import
{
    public static class Constants
    {
        public static readonly Dictionary<DataSourceNames, Descriptor[]> ExcelDataStructureMapping =
            new Dictionary<DataSourceNames, Descriptor[]>()
            {
                { DataSourceNames.Chat,new Descriptor[]{ } },
                { DataSourceNames.Avaya,new Descriptor[]{ } },
                { DataSourceNames.MSXWon,new Descriptor[]{ } },
                { DataSourceNames.MSXTQL,new Descriptor[]{ } },
            };

        public static readonly Dictionary<DataSourceNames, string[]> DataSourceMapping = new Dictionary<DataSourceNames, string[]>()
        {

        };
    }
}
