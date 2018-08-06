using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSN.ModelMate.EDM
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class ModelMateEFModel9ContextInitialier : CreateDatabaseIfNotExists<ModelMateEFModel9Context>
    {
        protected override void Seed(ModelMateEFModel9Context ctxTarget)
        {
            base.Seed(ctxTarget);

            //const string tnameTest = "Test6 Tenant";
            //const string mnameTest = "Test6 Model";
            //const string vnameTest = "Test6 View";

            //ModelFactory.TenantName = tnameTest;
            //ModelFactory.ModelName = mnameTest;

            //var tenant = ModelFactory.NewTenantWithRootFolder(tnameTest);
            //var folder0 = tenant.folders.ElementAt<folders>(0).folder.ElementAt(0);

            //var model = ModelFactory.NewModel(mnameTest);
            //var models = ModelFactory.NewModels(model);

            //folder0.models.Add(models);


            //var vView0 = ModelFactory.NewView(vnameTest);
            //var views = ModelFactory.NewViews(new view[] { vView0 });

            //model.views.Add(views);

            //const string lApplication = "Application";
            //const string lApplications = "Applications";
            //const string lApplicationComponents = "Application Components";
            //const string lApplicationInterfaces = "Application Interfaces";
            //var iApplication = ModelFactory.NewItem(lApplication);
            //var iApplications = ModelFactory.NewItem(lApplications);
            //var iApplicationComponents = ModelFactory.NewItem(lApplicationComponents);
            //var iApplicationInterfaces = ModelFactory.NewItem(lApplicationInterfaces);

            //iApplication.item1.Add(iApplications);
            //iApplications.item1.Add(iApplicationComponents);
            //iApplications.item1.Add(iApplicationInterfaces);

            //const string lTechnology = "Technology";
            //const string lArtifacts = "Artifacts";
            //const string lTechnologyInterfaces = "Technology Interfaces";
            //const string lSystemSoftware = "System Software";
            //var iTechnology = ModelFactory.NewItem(lTechnology);
            //var iArtifacts = ModelFactory.NewItem(lArtifacts);
            //var iTechnologyInterfaces = ModelFactory.NewItem(lTechnologyInterfaces);
            //var iSystemSoftware = ModelFactory.NewItem(lSystemSoftware);

            //iTechnology.item1.Add(iArtifacts);
            //iTechnology.item1.Add(iTechnologyInterfaces);
            //iTechnology.item1.Add(iSystemSoftware);

            //const string lRelations = "Relations";
            //var iRelations = ModelFactory.NewItem(lRelations);

            //const string lViews = "Views";
            //var iViews = ModelFactory.NewItem(lViews);
            //var iView0 = ModelFactory.NewItem(vView0);

            //iViews.item1.Add(iView0);

            ////var item0 = ModelFactory.NewItem(element0);

            ////iArtifacts.item1.Add(item0);

            //var organization = ModelFactory.NewOrganization(new item[] { iApplication, iTechnology, iRelations, iViews });

            //model.organization.Add(organization);

            //ctxTarget.tenant.Add(tenant);
            //ctxTarget.SaveChanges();
        }
    }
}
