using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace PSN.ModelMate.TestConsole4c
{
    class Program
    {
        static void Main(string[] args)
        {
            const string tTargetName = "Tenant 2 Test";
            string tTargetIdentifier = Util.MakeIdentifierRootIdentifier(typeof(tenant).Name, Util.TrimWhitespace(tTargetName));

            const string mTargetName = "Model 2 Test";
            string mTargetIdentifier = Util.MakeIdentifierFromParentIdentifier(
                    Util.MakeIdentifierRootIdentifier(typeof(tenant).Name, tTargetName),
                    typeof(model).Name, Util.TrimWhitespace(mTargetName));

            tenant tTarget = null;
            model mTarget = null;

            using (var ctxTarget = new ModelMateEFModel9Context())
            {
                ctxTarget.Database.Log = Console.Write;

                tTarget = ModelFinder.FindTenant(ctxTarget, tTargetIdentifier, "");
                ModelDump.DisplayDBPropertyValues("tTarget", ctxTarget.Entry(tTarget).CurrentValues, null);

                folders fs0 = tTarget.folders.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("fs0", ctxTarget.Entry(fs0).CurrentValues, null);
                folder f0 = fs0.folder.ElementAt(0);
                ModelDump.DisplayDBPropertyValues("f0", ctxTarget.Entry(f0).CurrentValues, null);

                mTarget = ModelFinder.FindModel(ctxTarget, tTarget, mTargetIdentifier, null);
                ModelDump.DisplayDBPropertyValues("mTarget", ctxTarget.Entry(mTarget).CurrentValues, null);

                string modelSchemaFile = @"..\..\..\PSN.ModelMate.Schema\Schema\ModelMateModel9.xsd";
                DataSet dsModel = new DataSet();
                dsModel.ReadXmlSchema(modelSchemaFile);
                //DataSet ds3 = dsModel.Copy();
                //var mmpModel = new ModelMateProcessor();
                var mmpModel = new ModelProcessor(ModelProcessor.ConvertModel2AMEFFDataSet);
                mmpModel.ProcessModel(dsModel, (DbContext)ctxTarget, new Collection<model> { mTarget });
                //dsModel.WriteXml("modelds.xml", XmlWriteMode.IgnoreSchema);
                string filename = "model4archi.xml";
                mmpModel.SaveAMEFFDataSetAsXML(dsModel, filename);

                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
        }
    }
}
