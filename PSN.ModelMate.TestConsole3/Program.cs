using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;

namespace ModelMateTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            var models1 = new models
            {
                models_Id = Util.MakeIdInt32()
            };
            for (int iTimes = 0; iTimes < 10; iTimes++)
            {
                var model = new model
                {
                    identifier = Util.MakeIdentifierTimestamped(ModelConst.MODEL_PREFIX, DateTime.Now),
                    version = ModelConst.MODEL_VERSION,
                    model_Id = Util.MakeIdInt32()
                };
                models1.model.Add(model);
            }

            var models2 = new models
            {
                models_Id = Util.MakeIdInt32()
            };
            for (int iTimes = 0; iTimes < 10; iTimes++)
            {
                var model = new model
                {
                    identifier = Util.MakeIdentifierTimestamped(ModelConst.MODEL_PREFIX, DateTime.Now),
                    version = ModelConst.MODEL_VERSION,
                    model_Id = Util.MakeIdInt32()
                };
                models2.model.Add(model);
            }

            folder[] folderArray = new folder[2];
            folderArray[0] = new folder
            {
                identifier = Util.MakeIdentifierTimestamped(ModelConst.MODEL_PREFIX, DateTime.Now),
                folder_Id = Util.MakeIdInt32()
            };
            folderArray[0].models.Add(models1); // NOTE: Models

            folderArray[1] = new folder
            {
                identifier = Util.MakeIdentifierTimestamped(ModelConst.MODEL_PREFIX, DateTime.Now),
                folder_Id = Util.MakeIdInt32()
            };
            folderArray[1].models.Add(models2); // NOTE: Model Templates

            var folders = new folders
            {
                folders_Id = Util.MakeIdInt32()
            };

            for (int iTimes = 0; iTimes < 2; iTimes++)
            {
                var folder = folderArray[iTimes];
                folders.folder.Add(folder);
            }

            var tenant = new tenant
            {
                identifier = Util.MakeIdentifierTimestamped(ModelConst.TENANT_PREFIX, DateTime.Now),
                version = ModelConst.TENANT_VERSION,
                tenant_Id = Util.MakeIdInt32()
            };

            name name;

            name = new name();
            name.name_Id = Util.MakeIdInt32();
            name.lang = "en";
            name.name_text = Util.MakeIdentifierTimestamped(ModelConst.TENANT_PREFIX);
            tenant.name.Add(name);

            name = new name();
            name.name_Id = Util.MakeIdInt32();
            name.lang = "en";
            name.name_text = Util.MakeIdentifierTimestamped(ModelConst.TENANT_PREFIX);
            tenant.name.Add(name);

            tenant.folders.Add(folders);

            using (var context = new ModelMateEFModel9Context())
            {
                context.tenant.Add(tenant);
                context.SaveChanges();

                tenant.version = ModelConst.TENANT_TESTVERSION;

                context.SaveChanges();

                object[] keys = { 1724588879 };
                tenant tenantDelete = context.tenant.Find(keys);
                context.tenant.Remove(tenantDelete);
                context.SaveChanges();
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
