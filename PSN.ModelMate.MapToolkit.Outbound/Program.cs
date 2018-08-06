using Newtonsoft.Json;
using PSN.ModelMate.Lib;
using PSN.ModelMate.MapToolkit.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace PSN.ModelMate.MapToolkit.Outbound
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new MAP_SampleDBContext2())
            {
                //ctx.Database.Log = Console.Write;

                ctx.Configuration.LazyLoadingEnabled = false;
                ctx.Configuration.ProxyCreationEnabled = false;

                ctx.Devices.Include(o => o.NetworkAdapters1).Load();

                foreach (Device d in ctx.Devices)
                { 
                    Console.WriteLine("Device: ====================");
                    ModelDump.DisplayDBPropertyValues(ctx.Entry(d).Entity.GetType().Name, ctx.Entry(d).CurrentValues, null);

                    Console.WriteLine("NetworkAdapter Linux: ====================");
                    foreach (NetworkAdapter na in d.NetworkAdapters) // Linux
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(na).Entity.GetType().Name, ctx.Entry(na).CurrentValues, null);
                    }
                    Console.WriteLine("NetworkAdapters1 Windows: ====================");
                    foreach (NetworkAdapters1 na in d.NetworkAdapters1) // Windows
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(na).Entity.GetType().Name, ctx.Entry(na).CurrentValues, null);
                    }
                    Console.WriteLine("NetworkAdapterConfiguration Windows: ====================");
                    foreach (NetworkAdapterConfiguration nac in d.NetworkAdapterConfigurations) // Windows
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(nac).Entity.GetType().Name, ctx.Entry(nac).CurrentValues, null);
                    }

                    Console.WriteLine("Inventory Oracle: ===================="); // Oracle
                    foreach (Inventory inv in d.Inventories) 
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(inv).Entity.GetType().Name, ctx.Entry(inv).CurrentValues, null);
                    }
                    Console.WriteLine("Inventory1 SQL Server: ===================="); // SQL Server
                    foreach (Inventory1 inv in d.Inventory1) 
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(inv).Entity.GetType().Name, ctx.Entry(inv).CurrentValues, null);
                    }

                    Console.WriteLine("IISApplicationPool: ====================");
                    foreach (IISApplicationPool iisap in d.IISApplicationPools)
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(iisap).Entity.GetType().Name, ctx.Entry(iisap).CurrentValues, null);
                    }
                    Console.WriteLine("IISEnabledService: ====================");
                    foreach (IISEnabledService iises in d.IISEnabledServices)
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(iises).Entity.GetType().Name, ctx.Entry(iises).CurrentValues, null);
                    }
                    Console.WriteLine("IISVirtualDirApplication: ===================="); 
                    foreach (IISVirtualDirApplication iisvda in d.IISVirtualDirApplications)
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(iisvda).Entity.GetType().Name, ctx.Entry(iisvda).CurrentValues, null);
                    }
                    Console.WriteLine("IISWebInfo: ====================");
                    foreach (IISWebInfo iiswi in d.IISWebInfoes)
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(iiswi).Entity.GetType().Name, ctx.Entry(iiswi).CurrentValues, null);
                    }
                    Console.WriteLine("IISWebServerSetting: ====================");
                    foreach (IISWebServerSetting iiswss in d.IISWebServerSettings)
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(iiswss).Entity.GetType().Name, ctx.Entry(iiswss).CurrentValues, null);
                    }
                    Console.WriteLine("IISWebStatu: ====================");
                    foreach (IISWebStatu iisws in d.IISWebStatus)
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(iisws).Entity.GetType().Name, ctx.Entry(iisws).CurrentValues, null);
                    }

                    Console.WriteLine("Service: ====================");
                    int nServices = 0;
                    foreach (Service s in d.Services)
                    {
                        ModelDump.DisplayDBPropertyValues(ctx.Entry(s).Entity.GetType().Name, ctx.Entry(s).CurrentValues, null);
                        if (nServices++ > 10) break;
                    }

                    Console.WriteLine("JSON: ====================");
                    //string json = JsonConvert.SerializeObject(d, Formatting.Indented);
                    string json = JsonConvert.SerializeObject(d, Formatting.Indented,
                                  new JsonSerializerSettings
                                  {
                                      //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                      ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                  });
                    Console.WriteLine(json);
                }
            }
        }
    }
}
