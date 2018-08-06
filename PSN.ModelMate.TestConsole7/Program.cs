using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using System.Data;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace PSN.ModelMate.TestConsole7
{
    class Program
    {
        static void Main(string[] args)
        {
            const string tnameTest = "Test6 Tenant";
            const string mnameTest = "Test6 Model";

            using (var ctxTarget = new ModelMateEFModel9Context())
            {
                ctxTarget.Database.Log = Console.Write;
                Console.WriteLine("LazyLoadingEnabled: " + ctxTarget.Configuration.LazyLoadingEnabled.ToString());
                //ctx.Configuration.LazyLoadingEnabled = false;
                //Console.WriteLine("LazyLoadingEnabled: " + ctx.Configuration.LazyLoadingEnabled.ToString());

                ModelMigrator.PreloadAllTables(ctxTarget);

                var tTarget = ModelFinder.FindTenant(ctxTarget, null, tnameTest);
                //var tenants = ModelFinder.FindTenants(ctx, null, tnameTest);
                //var tenant = tenants.ElementAt<tenant>(0);
                Console.WriteLine("tenant.identifier: " + tTarget.identifier);

                var models = ModelFinder.FindModels(ctxTarget, tTarget, null, mnameTest);
                var mTarget = models.ElementAt<model>(0);
                Console.WriteLine("model.identifier: " + mTarget.identifier);
                var model0 = ModelFinder.FindModel(ctxTarget, tTarget, null, mnameTest);
                Console.WriteLine("model0.identifier: " + model0.identifier);

                string modelSchemaFile = @"..\..\..\PSN.ModelMate.Schema\Schema\ModelMateModel9.xsd";
                DataSet dsModel = new DataSet();
                dsModel.ReadXmlSchema(modelSchemaFile);
                dsModel.Tables["element"].MinimumCapacity = ctxTarget.element.Count();
                dsModel.Tables["properties"].MinimumCapacity = ctxTarget.properties.Count();
                dsModel.Tables["property"].MinimumCapacity = ctxTarget.property.Count();
                dsModel.Tables["value"].MinimumCapacity = ctxTarget.value.Count();
                dsModel.Tables["relationship"].MinimumCapacity = ctxTarget.relationship.Count();

                var mmpModel = new ModelProcessor(ModelProcessor.ConvertModel2AMEFFDataSet);
                mmpModel.ProcessModel(dsModel, (DbContext)ctxTarget, new Collection<model> { model0 });
                //dsModel.WriteXml("model0ds.xml", XmlWriteMode.IgnoreSchema);
                string filename = "model7archi.xml";
                mmpModel.SaveAMEFFDataSetAsXML(dsModel, filename);

                ctxTarget.SaveChanges();
            }

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
