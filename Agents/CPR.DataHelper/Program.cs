

namespace CPR.DataHelper
{

    using Org.Joey.Common;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CPR.Data.Import;
    using System.Collections.Generic;
    using CPR.Data.Import.Models;

    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    class Program
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            IoC.ConfigureServiceProvider(null, (collection) =>
            {
                collection.AddJsonConfiguration();
                collection.AddImportService();
                collection.AddMemoryCache();
            });

            var team = @"D:\Manny\online\Repos\CPR Reporting\8-10data\8-10data\Team Level\Chat,TQL,DirectVolumeTeamLevel(8-10).xlsx";
            var phone = @"D:\Manny\online\Repos\CPR Reporting\8-10data\8-10data\Team Level\PhoneVolumeTeamLevel(8-10).xlsx";
            ExcelHelper.ReadExcel(team);
            ExcelHelper.ReadExcel<object>(team, (index, headers, array) =>
            {
                var header = string.Join("\t", headers);
                var values = string.Join("\t", array);
                Logger.Info($"\r\n{header}\r\n{values}");
                return new object();
            }, 0, 19, 2).ToList();

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

        static void LoginOneDrive()
        {
            var clientId = "4d2e2592-1703-46b3-99fd-82f22c732fd5";
            var tenantId = "e4c9ab4e-bd27-40d5-8459-230ba2a757fb";
            var objectId = "4226923e-fb03-4ccc-a25e-6fcb816945a4";
            var secret = "W_jZ=t257AsB-xnCjDEQtE3dQRi.Eqx2";

            var url = $"https://login.microsoftonline.com/e4c9ab4e-bd27-40d5-8459-230ba2a757fb/adminconsent?client_id={clientId}";


        }
        static void GenernateFieldMappingConfiguration()
        {
            var mapping = new Dictionary<DataSourceNames, PropertyDescriptor[]>()
            {
                { DataSourceNames.Avaya, new PropertyDescriptor[]{
                    new PropertyDescriptor(){ Header  ="Agent",         FiledName="Agent", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Split/Skill",   FiledName="SkillSet", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Date",          FiledName="CreatedDateTime", Type= System.Data.SqlDbType.DateTime  },
                    new PropertyDescriptor(){ Header  ="ACD CALL",      FiledName="Volume", Length =50,  Type= System.Data.SqlDbType.NVarChar  }
                } },
                { DataSourceNames.Chat, new PropertyDescriptor[]{
                    new PropertyDescriptor(){ Header  ="Start (UTC)", FiledName="CreatedDateTime",   Type= System.Data.SqlDbType.DateTime  },
                    new PropertyDescriptor(){ Header  ="Skill",       FiledName="SkillSet", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Agent Name",  FiledName="Agent", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                } },
                { DataSourceNames.MSXSQO, new PropertyDescriptor[]{
                    new PropertyDescriptor(){ Header  ="Opportunity Id",        FiledName="OpportunityId", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Created By",         FiledName="Agent", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Status",         FiledName="Status", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Est. Revenue",         FiledName="EstRevenue", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Currency",         FiledName="Currency", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Created On",         FiledName="CreatedDateTime",   Type= System.Data.SqlDbType.DateTime  },
                    new PropertyDescriptor(){ Header  ="Source Campaign",         FiledName="SourceCampaign",Length=200,   Type= System.Data.SqlDbType.DateTime  },


                } },
                { DataSourceNames.MSXTQL, new PropertyDescriptor[]{
                    new PropertyDescriptor(){ Header  ="Lead Id",         FiledName="LeadId", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Created By",         FiledName="Agent", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Created On",         FiledName="CreatedDateTime",   Type= System.Data.SqlDbType.DateTime  },
                    new PropertyDescriptor(){ Header  ="Status",         FiledName="Status", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Est. Value",         FiledName="EstValue", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Currency",         FiledName="Currency", Length =50,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="CSS Disposition",         FiledName="CSSDispostion", Length =200,  Type= System.Data.SqlDbType.NVarChar  },
                    new PropertyDescriptor(){ Header  ="Source Campaign",         FiledName="SourceCampaign",Length=200,   Type= System.Data.SqlDbType.DateTime  }
                } },
            };
            var configuration = mapping.SerializeToJson();
        }
    }
}
