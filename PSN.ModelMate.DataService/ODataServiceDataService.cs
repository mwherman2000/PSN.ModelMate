using PSN.ModelMate.EDM;
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.Text;

namespace PSN.ModelMate.DataService
{
    [JSONPSupportBehavior]
    [System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class ODataServiceDataService : EntityFrameworkDataService<ModelMateEFModel9Context>
    {
        // This method is called only once to initialize service-wide policies. 
        public static void InitializeService(DataServiceConfiguration config)
        {
            // Explicitly define permissions on the exposed entity sets. 
            //config.SetEntitySetAccessRule("MyUsers", EntitySetRights.AllRead);
            //config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;

            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }
    }
}
