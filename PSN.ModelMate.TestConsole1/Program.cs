using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSN.ModelMate.EDM;

namespace PSN.ModelMate.TestConsole1
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = (Int32)DateTime.Now.Ticks;
            Console.WriteLine("id: " + id.ToString());

            var tenant1 = new tenant();
            tenant1.tenant_Id = id++;
            var tname = new name();
            tname.name_Id = id++;
            tname.lang = "en-us";
            tname.name_text = "tenant " + DateTime.Now.ToString();
            tenant1.name.Add(tname);

            var folders = new folders();
            folders.folders_Id = id++;
            var fname = new name();
            fname.lang = "en-us";
            fname.name_Id = id++;
            fname.name_text = "folders " + DateTime.Now.ToString();

            tenant1.folders.Add(folders);

            using (var ctx = new PSN.ModelMate.EDM.ModelMateEFModel9Context())
            {
                ctx.Database.Log = Console.Write;
                ctx.tenant.Add(tenant1);
                ctx.SaveChanges();
            }

            using (var ctx = new PSN.ModelMate.EDM.ModelMateEFModel9Context())
            {
                ctx.Database.Log = Console.Write;
                var tenant2 = ctx.tenant.Find(new object[] { tenant1.tenant_Id });
                Console.WriteLine("tenant.name.count: " + tenant2.name.Count);
                Console.WriteLine("tenant.folders.count: " + tenant2.folders.Count);
                ctx.SaveChanges();
            }

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

        }
    }
}
