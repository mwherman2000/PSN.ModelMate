using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSN.ModelMate.TestConsole2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new PSN.ModelMate.EDM.ModelMateEFModel9Context())
            {
                ctx.Database.Log = Console.Write;
                var tenant2 = ctx.tenant.Find(new object[] { 1173654396 });
                Console.WriteLine("tenant.name.count: " + tenant2.name.Count);
                Console.WriteLine("tenant.folders.count: " + tenant2.folders.Count);
                ctx.SaveChanges();
            }

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
    }
}
