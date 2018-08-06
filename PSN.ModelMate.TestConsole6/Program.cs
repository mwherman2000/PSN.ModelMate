using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PSN.ModelMate.EDM;
using PSN.ModelMate.Lib;

namespace PSN.ModelMate.TestConsole6
{
    class Program
    {
        static void Main(string[] args)
        {
            const string pnUniqueKey = "UniqueKey";
            const string pnFilename = "Filename";
            const string pnFQN = "FQN";
            const string pnType = "Type";
            const string pnName = "Name";
            const string pnSignature = "Signature";
            const string pnReturnType = "ReturnType";
            const string pnModuleFQN = "ModuleFQN";
            const string pnVersion = "Version";

            //var pdUniqueKey =   ModelFactory.NewPropertyDef(pnUniqueKey, ModelConst.PropertyDataType.stringType);
            //var pdFileName =    ModelFactory.NewPropertyDef(pnFilename, ModelConst.PropertyDataType.stringType);
            //var pdFQN =         ModelFactory.NewPropertyDef(pnFQN, ModelConst.PropertyDataType.stringType);
            //var pdType =        ModelFactory.NewPropertyDef(pnType, ModelConst.PropertyDataType.stringType);
            //var pdName =        ModelFactory.NewPropertyDef(pnName, ModelConst.PropertyDataType.stringType);
            //var pdSignature =   ModelFactory.NewPropertyDef(pnSignature, ModelConst.PropertyDataType.stringType);
            //var pdReturnType =  ModelFactory.NewPropertyDef(pnReturnType, ModelConst.PropertyDataType.stringType);
            //var pdModuleFQN =   ModelFactory.NewPropertyDef(pnModuleFQN, ModelConst.PropertyDataType.stringType);
            //var pdVersion =     ModelFactory.NewPropertyDef(pnVersion, ModelConst.PropertyDataType.stringType);

            //var propdefs0 =     ModelFactory.NewPropertyDefs(
            //    new propertydef[]
            //    {
            //        pdFileName,
            //        pdFQN,
            //        pdModuleFQN,
            //        pdName,
            //        pdReturnType,
            //        pdSignature,
            //        pdType,
            //        pdUniqueKey,
            //        pdVersion
            //    });

            //model.propertydefs.Add(propdefs0);

            //const string enameTest = "Test6 Element";
            //var element0 = ModelFactory.NewElement(enameTest, ModelConst.ElementType.Artifact);
            //var property0 = ModelFactory.NewProperty(ModelFactory.NewValue(Guid.NewGuid().ToString()), pdUniqueKey);
            //var properties0 = ModelFactory.NewProperties(new property[] { property0 });

            //element0.properties.Add(properties0);

            //var elements = ModelFactory.NewElements(element0);

            //model.elements.Add(elements);

            const string tnameTest = "Test6 Tenant";
            const string mnameTest = "Test6 Model";
            const string vnameTest = "Test6 View";

            ModelFactory.TenantName = tnameTest;
            ModelFactory.ModelName = mnameTest;

            var tenant = ModelFactory.NewTenantWithRootFolder(tnameTest);
            var folder0 = tenant.folders.ElementAt<folders>(0).folder.ElementAt(0);

            var model = ModelFactory.NewModel(mnameTest);
            var models = ModelFactory.NewModels(model);

            folder0.models.Add(models);


            var vView0 = ModelFactory.NewView(vnameTest);
            var views = ModelFactory.NewViews(new view[] { vView0 });

            model.views.Add(views);

            const string lApplication = "Application";
            const string lApplications = "Applications";
            const string lApplicationComponents = "Application Components";
            const string lApplicationInterfaces = "Application Interfaces";
            var iApplication = ModelFactory.NewItem(lApplication);
            var iApplications = ModelFactory.NewItem(lApplications);
            var iApplicationComponents = ModelFactory.NewItem(lApplicationComponents);
            var iApplicationInterfaces = ModelFactory.NewItem(lApplicationInterfaces);

            iApplication.item1.Add(iApplications);
            iApplications.item1.Add(iApplicationComponents);
            iApplications.item1.Add(iApplicationInterfaces);

            const string lTechnology = "Technology";
            const string lArtifacts = "Artifacts";
            const string lTechnologyInterfaces = "Technology Interfaces";
            const string lSystemSoftware = "System Software";
            var iTechnology = ModelFactory.NewItem(lTechnology);
            var iArtifacts = ModelFactory.NewItem(lArtifacts);
            var iTechnologyInterfaces = ModelFactory.NewItem(lTechnologyInterfaces);
            var iSystemSoftware = ModelFactory.NewItem(lSystemSoftware);

            iTechnology.item1.Add(iArtifacts);
            iTechnology.item1.Add(iTechnologyInterfaces);
            iTechnology.item1.Add(iSystemSoftware);

            const string lRelations = "Relations";
            var iRelations = ModelFactory.NewItem(lRelations);

            const string lViews = "Views";
            var iViews = ModelFactory.NewItem(lViews);
            var iView0 = ModelFactory.NewItem(vView0);

            iViews.item1.Add(iView0);

            //var item0 = ModelFactory.NewItem(element0);

            //iArtifacts.item1.Add(item0);

            var organization = ModelFactory.NewOrganization(new item[] { iApplication, iTechnology, iRelations, iViews });

            model.organization.Add(organization);

            using (var ctx = new ModelMateEFModel9Context())
            {
                ctx.Database.Log = Console.Write;

                ctx.tenant.Add(tenant);
                ctx.SaveChanges();
            }

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
