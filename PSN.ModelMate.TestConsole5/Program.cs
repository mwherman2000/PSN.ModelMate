using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSN.ModelMate.Cecil;

namespace PSN.ModelMate.TestConsole5
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo(@"..\..\Test Assemblies");
            //DirectoryInfo di = new DirectoryInfo(@"..\..\Web Assemblies");

            CecilDump.DumpAll(di);

            //var type = module.Types.First(x => x.Name == "A");
            //var method = type.Methods.First(x => x.Name == "test");

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
