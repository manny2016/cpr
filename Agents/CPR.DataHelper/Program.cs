

namespace CPR.DataHelper
{
    using Microsoft.Extensions.Logging;
    using Org.Joey.Common;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CPR.Data.Import;
    

    class Program
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            IoC.ConfigureServiceProvider(null, (collection) =>
            {
                collection.AddJsonConfiguration();
                collection.AddImportService();
            });
            StartAuto();
        }
        static void StartAuto()
        {


            var workitems = IoC.GetServices<IWorkItem>();
            var scheduler = new WebJobScheduler((cancellation) =>
            {
                Parallel.ForEach(workitems, new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 5
                }, (workitem) =>
                {
                    while (cancellation.IsCancellationRequested == false)
                    {

                        try
                        {
                            var offset = 60D * 10;//1 hour     
                            workitem.Execute();
                            for (var i = 0; ((cancellation.IsCancellationRequested == false) && (i < offset)); i++)
                            {
                                Thread.Sleep(1000);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex.Message, ex);
                        }
                    }
                });
            });
            scheduler.Shutdown += (sender, args) =>
            {
                foreach (var workitem in workitems)
                {
                    workitem.Abort();
                }
            };
            scheduler.Start();
        }
    }
}
