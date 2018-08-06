using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PSN.ModelMate.Cecil
{
    public static class CecilDump
    {
        public static void DumpAll(DirectoryInfo di)
        {
            FileInfo[] fis = di.GetFiles("*.*");
            foreach (FileInfo fi in fis)
            {
                Console.WriteLine("File " + fi.FullName);
                if (fi.FullName.EndsWith(".dll") || fi.FullName.EndsWith(".exe"))
                {
                    //var module = ModuleDefinition.ReadModule(fi.FullName);
                    //AssemblyDefinition assembly = module.Assembly;

                    var assembly = AssemblyDefinition.ReadAssembly(fi.FullName);
                    Console.WriteLine("Assembly Name:\t" + assembly.Name);
                    Console.WriteLine("Assembly FullName:\t" + assembly.FullName);

                    var mainmodule = assembly.MainModule;
                    Console.WriteLine("MainModule Name:\t" + mainmodule.Name);
                    Console.WriteLine("MainModule FullyQualifiedName:\t" + mainmodule.FullyQualifiedName);
                    Console.WriteLine("MainModule RuntimeVersion:\t" + mainmodule.RuntimeVersion);

                    var ms = assembly.Modules;
                    foreach (var module in ms)
                    {
                        Console.WriteLine("Module Name:\t" + module.Name);
                        Console.WriteLine("Module FullyQualifiedName:\t" + module.FullyQualifiedName);
                        Console.WriteLine("Module RuntimeVersion:\t" + module.RuntimeVersion);
                    }

                    ms = assembly.Modules;
                    foreach (var module in ms)
                    {
                        Console.WriteLine("Module Name:\t" + module.Name);
                        Console.WriteLine("Module FullyQualifiedName:\t" + module.FullyQualifiedName);
                        Console.WriteLine("Module RuntimeVersion:\t" + module.RuntimeVersion);

                        var ars = module.AssemblyReferences;
                        foreach (AssemblyNameReference ar in ars)
                        {
                            Console.WriteLine("AssemblyNameReference Name:\t" + ar.Name);
                            Console.WriteLine("AssemblyNameReference FullName:\t" + ar.FullName);
                            Console.WriteLine("AssemblyNameReference Version:\t" + ar.Version.ToString());
                            Console.WriteLine("AssemblyNameReference IsWindowsRuntime:\t" + ar.IsWindowsRuntime.ToString());
                        }

                        var tds = module.Types;
                        foreach (TypeDefinition td in tds)
                        {
                            Console.WriteLine("TypeDefinition Name:\t" + td.Name);
                            Console.WriteLine("TypeDefinition FullName:\t" + td.FullName);
                            Console.WriteLine("TypeDefinition IsPublic:\t" + td.IsPublic.ToString());
                        }

                        tds = module.Types;
                        foreach (TypeDefinition td in tds)
                        {
                            //if (t.IsPublic)
                            {
                                Console.WriteLine("TypeDefinition FulleName:\t" + td.FullName);

                                var mds = td.Methods;
                                foreach (MethodDefinition m in mds)
                                {
                                    Console.WriteLine("    Method Name:\t" + m.Name);
                                    Console.WriteLine("      m.FullName:\t" + m.FullName);
                                    Console.WriteLine("      m.Module.FullyQualifiedName:\t" + m.Module.FullyQualifiedName);
                                    Console.WriteLine("      m.ReturnType:\t" + m.ReturnType.ToString());
                                    Console.WriteLine("      m.IsPublic:\t" + m.IsPublic.ToString());

                                    //if (m.IsPublic)
                                    {
                                        PrintMethodReferences(m);
                                        PrintFieldReferences(m);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void PrintMethodReferences(MethodDefinition method)
        {
            Console.WriteLine("      Methods called by " + method.Name);
            if (method.Body != null && method.Body.Instructions != null)
                foreach (var instruction in method.Body.Instructions)
                {
                    if (instruction.OpCode == OpCodes.Call)
                    {
                        MethodReference methodCall = instruction.Operand as MethodReference;
                        if (methodCall != null)
                        {
                            Console.WriteLine("\tMethodCall Name:\t" + methodCall.Name);
                            Console.WriteLine("\t  methodCall.FullName:\t" + methodCall.FullName);
                            Console.WriteLine("\t  methodCall.Module.FullyQualifiedName:\t" + methodCall.Module.FullyQualifiedName);
                            Console.WriteLine("\t  methodCall.ReturnType:\t" + methodCall.ReturnType.ToString());
                            Console.WriteLine("\t  instruction.Offset:\t" + instruction.Offset.ToString());
                        }
                    }
                }
        }

        public static void PrintFieldReferences(MethodDefinition method)
        {
            Console.WriteLine("      Fields referenced by " + method.Name);
            if (method.Body != null && method.Body.Instructions != null)
                foreach (var instruction in method.Body.Instructions)
                {
                    if (instruction.OpCode == OpCodes.Ldfld)
                    {
                        FieldReference fieldAccess = instruction.Operand as FieldReference;
                        if (fieldAccess != null)
                        {
                            Console.WriteLine("\tFieldAccess Name:\t" + fieldAccess.Name);
                            Console.WriteLine("\t  fieldAccess.FullName:\t" + fieldAccess.FullName);
                            Console.WriteLine("\t  fieldAccess.FieldType:\t" + fieldAccess.FieldType.ToString());
                            Console.WriteLine("\t  instruction.Offset:\t" + instruction.Offset.ToString());
                        }
                    }
                }
        }
    }
}
