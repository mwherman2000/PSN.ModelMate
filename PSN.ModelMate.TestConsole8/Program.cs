using Parallelspace.Content.Objects;
using PSN.ModelMate.Cecil;
using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.IO;

namespace PSN.ModelMate.TestConsole8
{
    class Program
    {
        static void Main(string[] args)
        {
            const string tTargetName = "Test6 Tenant";
            const string mTargetName = "Test6 Model";

            string tSourceName = tTargetName;
            string mSourceName = mTargetName;

            //DirectoryInfo di = new DirectoryInfo(@"..\..\MyUsers4ModelMate");
            DirectoryInfo di = new DirectoryInfo(@"..\..\Test Assemblies");
            //DirectoryInfo di = new DirectoryInfo(@"..\..\Web Assemblies");

            string test1 = "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

            //"System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

            //"System.Collections.Generic.IDictionary`2<System.String,System.Data.Entity.Infrastructure.Annotations.AnnotationValues> System.Data.Entity.Migrations.Model.AlterTableOperation::_annotations";

            //"System.Collections.Generic.IDictionary`2<System.String,System.Data.Entity.Infrastructure.Annotations.AnnotationValues> System.Data.Entity.Migrations.Model.AlterTableOperation::_annotations";

            //"System.Data.Entity.DbSet`1<VetContext1.DataModel1.AnimalType>";

            string test1Namespace = Cecil2ModelMate.GetNamespaceFromFQName(test1);
            string test1Name = Cecil2ModelMate.GetNameFromFQName(test1);
            string test1ObjectType = Cecil2ModelMate.DetermineArchiMateObjectType(test1).ToString();
            Console.WriteLine("test1: " + test1);
            Console.WriteLine("test1Namespace: " + test1Namespace);
            Console.WriteLine("test1Name: " + test1Name);
            Console.WriteLine("test1ObjectType: " + test1ObjectType);
            test1ObjectType = Cecil2ModelMate.DetermineArchiMateObjectType(test1Name).ToString();
            Console.WriteLine("test1ObjectType: " + test1ObjectType);
            test1ObjectType = Cecil2ModelMate.DetermineArchiMateObjectType(test1Namespace).ToString();
            Console.WriteLine("test1ObjectType: " + test1ObjectType);

            //test1 = "a.b.c.d";
            //test1Name = Cecil2ModelMate.GetNameFromFQName(test1);
            //test1Namespace = Cecil2ModelMate.GetNamespaceFromFQName(test1);
            //Console.WriteLine("test1: " + test1);
            //Console.WriteLine("test1Namespace: " + test1Namespace);
            //Console.WriteLine("test1Name: " + test1Name);

            //string test2 = "System.Data.Entity.DbSet`1<VetContext1.DataModel1.AnimalType>";
            //string test2Name = Cecil2ModelMate.DetermineArchiMateObjectType(test2).ToString();
            //Console.WriteLine("test2: " + test2);
            //Console.WriteLine("test2Name: " + test2Name);

            //test2 = "System.Data.Entity.DbSet`1";
            //test2Name = Cecil2ModelMate.DetermineArchiMateObjectType(test2).ToString();
            //Console.WriteLine("test2: " + test2);
            //Console.WriteLine("test2Name: " + test2Name);

            //string test3 = "System.Data.Entity.Infrastructure.Interception.MutableInterceptionContext`1<TResult>";
            //string test3Namespace = Cecil2ModelMate.GetNamespaceFromFQName(test3);
            //string test3Name = Cecil2ModelMate.DetermineArchiMateInterfaceType(test3).ToString();
            //Console.WriteLine("test3: " + test3);
            //Console.WriteLine("test3Namespace: " + test3Namespace);
            //Console.WriteLine("test3Name: " + test3Name);

            //test3 = "System.Data";
            //test3Namespace = Cecil2ModelMate.GetNamespaceFromFQName(test3);
            //test3Name = Cecil2ModelMate.DetermineArchiMateInterfaceType(test3).ToString();
            //Console.WriteLine("test3: " + test3);
            //Console.WriteLine("test3Namespace: " + test3Namespace);
            //Console.WriteLine("test3Name: " + test3Name);

            //test3 = "System";
            //test3Namespace = Cecil2ModelMate.GetNamespaceFromFQName(test3);
            //test3Name = Cecil2ModelMate.DetermineArchiMateInterfaceType(test3).ToString();
            //Console.WriteLine("test3: " + test3);
            //Console.WriteLine("test3Namespace: " + test3Namespace);
            //Console.WriteLine("test3Name: " + test3Name);

            //test3 = "S";
            //test3Namespace = Cecil2ModelMate.GetNamespaceFromFQName(test3);
            //test3Name = Cecil2ModelMate.DetermineArchiMateInterfaceType(test3).ToString();
            //Console.WriteLine("test3: " + test3);
            //Console.WriteLine("test3Namespace: " + test3Namespace);
            //Console.WriteLine("test3Name: " + test3Name);

            //test3 = "";
            //test3Namespace = Cecil2ModelMate.GetNamespaceFromFQName(test3);
            //test3Name = Cecil2ModelMate.DetermineArchiMateInterfaceType(test3).ToString();
            //Console.WriteLine("test3: " + test3);
            //Console.WriteLine("test3Namespace: " + test3Namespace);
            //Console.WriteLine("test3Name: " + test3Name);

            model mSource = Cecil2ModelMate.Migrate2ModelMate(tSourceName, mSourceName, di);

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();

            using (var ctxTarget = new ModelMateEFModel9Context())
            {
                //var oldLog = ctxTarget.Database.Log;
                //ctxTarget.Database.Log = Console.Write;

                Console.WriteLine("propertydefs.Count: " + ctxTarget.propertydefs.Local.Count().ToString());

                ModelMigrator.PreloadAllTables(ctxTarget);

                var tTarget = ModelFinder.FindTenant(ctxTarget, null, tTargetName);
                //var tenants = ModelFinder.FindTenants(ctxTarget, null, tnameTest);
                //var tenant = tenants.ElementAt<tenant>(0);
                Console.WriteLine("tenant.identifier: " + tTarget.identifier);

                folders fs0 = tTarget.folders.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("fs0", ctxTarget.Entry(fs0).CurrentValues, null);
                folder f0 = fs0.folder.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("f0", ctxTarget.Entry(f0).CurrentValues, null);

                var models = ModelFinder.FindModels(ctxTarget, tTarget, null, mTargetName);
                var mTarget = models.ElementAt<model>(0);
                Console.WriteLine("model.identifier: " + mTarget.identifier);
                var model0 = ModelFinder.FindModel(ctxTarget, tTarget, null, mTargetName);
                Console.WriteLine("model0.identifier: " + model0.identifier);

                ModelMigrator.MigrateModel(ctxTarget, tTarget, f0,
                                                mSource, PCOOperation.merge, PCOOperation.merge, true);

                string modelSchemaFile = @"..\..\..\PSN.ModelMate.Schema\Schema\ModelMateModel9.xsd";
                DataSet dsModel = new DataSet();
                dsModel.ReadXmlSchema(modelSchemaFile);
                var mmpModel = new ModelProcessor(ModelProcessor.ConvertModel2AMEFFDataSet);
                mmpModel.ProcessModel(dsModel, (DbContext)ctxTarget, new Collection<model> { model0 });
                //dsModel.WriteXml("model0ds.xml", XmlWriteMode.IgnoreSchema);
                string filename = "model8archi.xml";
                mmpModel.SaveAMEFFDataSetAsXML(dsModel, filename);

                //ctxTarget.Database.Log = oldLog;
                ctxTarget.SaveChanges();
            }

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
