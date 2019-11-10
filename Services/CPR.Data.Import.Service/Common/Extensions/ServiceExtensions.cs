namespace Org.Joey.Common
{
    using CPR.Data.Import;
    using CPR.Data.Import.Models;
    using CPR.Data.Import.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Org.Joey.Common;
    using Org.Joey.Common.Models;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddImportService(this IServiceCollection collection)
        {
            collection.Add(new ServiceDescriptor(typeof(IDataImportDirectly), typeof(DataImportDirectly), ServiceLifetime.Singleton));
            collection.AddScoped<ImportSettings>();
            collection.AddScoped<ImportState>();
            collection.AddScoped<IWorkItem, WorkItemWithDataflow<ImportState, ExcelSignleRow>>();
            return collection;
        }
    }
}
