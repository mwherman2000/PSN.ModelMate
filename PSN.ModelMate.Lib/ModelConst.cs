using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSN.ModelMate.Lib
{
    public static class ModelConst
    {
        public const int RGB_MIN = 0;
        public const int RGB_MAX = 255;
        public const int ALPHA_MIN = 0;
        public const int ALPHA_MAX = 100;

        public const string GENERICNAME_PREFIX = "generic";
        public const string NAME_PREFIX = "name";
        public const string LABEL_PREFIX = "label";
        public const string DOCUMENTATION_PREFIX = "documentation";
        public const string VALUE_PREFIX = "value";

        public const string TENANT_PREFIX = "tenant";
        public const string TENANT_VERSION = "6.6";
        public const string TENANT_TESTVERSION = "6.7";

        public const string FOLDER_PREFIX = "folder";
        public const string ROOTFOLDERNAME = "/";

        public const string MODEL_PREFIX = "model";
        public const string MODEL_VERSION = "6.0";

        public const string PROPERTYDEF_PREFIX = "propertydef";

        public const string ITEM_PREFIX = "item";

        public const string CONNECTION_PREFIX = "connection";

        public const string NODE_PREFIX = "node";

        public const string ELEMENT_PREFIX = "element";

        public enum ElementType
        {
            ApplicationCollaboration,
            ApplicationComponent,
            ApplicationFunction,
            ApplicationInteraction,
            ApplicationInterface,
            ApplicationService,
            Artifact,
            Assessment,
            BusinessActor,
            BusinessCollaboration,
            BusinessEvent,
            BusinessFunction,
            BusinessInteraction,
            BusinessInterface,
            BusinessObject,
            BusinessProcess,
            BusinessRole,
            BusinessService,
            CommunicationPath,
            Constraint,
            Contract,
            DataObject,
            Deliverable,
            Device,
            Driver,
            Gap,
            Goal,
            InfrastructureFunction,
            InfrastructureInterface,
            InfrastructureService,
            Junction,
            Location,
            Meaning,
            Network,
            Node,
            Plateau,
            Principle,
            Product,
            Representation,
            Requirement,
            Stakeholder,
            SystemSoftware,
            Value,
            WorkPackage,
            AllElementTypes,
            OtherOrUnknownOrUndefinedElementType,
        };

        public enum EADomain
        {
            Business,
            Application,
            Technology,
            Physical,
            Strategy,
            Motivation,
            ImplementationAndMigration,
            General,
            Other,
        }

        public static EADomain[] EADomains = new EADomain[]
{
            EADomain.Application,
            EADomain.Application,
            EADomain.Application,
            EADomain.Application,
            EADomain.Application,
            EADomain.Application,
            EADomain.Technology,
            EADomain.Motivation,
            EADomain.Business,
            EADomain.Business,
            EADomain.Business,
            EADomain.Business,
            EADomain.Business,
            EADomain.Business,
            EADomain.Business,
            EADomain.Business,
            EADomain.Business,
            EADomain.Business,
            EADomain.Technology,
            EADomain.Motivation,
            EADomain.Business,
            EADomain.Application,
            EADomain.ImplementationAndMigration,
            EADomain.Technology,
            EADomain.Motivation,
            EADomain.ImplementationAndMigration,
            EADomain.Motivation,
            EADomain.Technology,
            EADomain.Technology,
            EADomain.Technology,
            EADomain.Business,
            EADomain.General,
            EADomain.Business,
            EADomain.Technology,
            EADomain.Technology,
            EADomain.ImplementationAndMigration,
            EADomain.Motivation,
            EADomain.Business,
            EADomain.Business,
            EADomain.Motivation,
            EADomain.Motivation,
            EADomain.Technology,
            EADomain.Business,
            EADomain.ImplementationAndMigration,
            EADomain.Other,
            EADomain.Other,
        };

        public static string[] EADomainNames = new string[] 
        {
            EADomain.Application.ToString(),
            EADomain.Application.ToString(),
            EADomain.Application.ToString(),
            EADomain.Application.ToString(),
            EADomain.Application.ToString(),
            EADomain.Application.ToString(),
            EADomain.Technology.ToString(),
            EADomain.Motivation.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Technology.ToString(),
            EADomain.Motivation.ToString(),
            EADomain.Business.ToString(),
            EADomain.Application.ToString(),
            EADomain.ImplementationAndMigration.ToString(),
            EADomain.Technology.ToString(),
            EADomain.Motivation.ToString(),
            EADomain.ImplementationAndMigration.ToString(),
            EADomain.Motivation.ToString(),
            EADomain.Technology.ToString(),
            EADomain.Technology.ToString(),
            EADomain.Technology.ToString(),
            EADomain.Business.ToString(),
            EADomain.General.ToString(),
            EADomain.Business.ToString(),
            EADomain.Technology.ToString(),
            EADomain.Technology.ToString(),
            EADomain.ImplementationAndMigration.ToString(),
            EADomain.Motivation.ToString(),
            EADomain.Business.ToString(),
            EADomain.Business.ToString(),
            EADomain.Motivation.ToString(),
            EADomain.Motivation.ToString(),
            EADomain.Technology.ToString(),
            EADomain.Business.ToString(),
            EADomain.ImplementationAndMigration.ToString(),
            EADomain.Other.ToString(),
            EADomain.Other.ToString(),
        };

        public enum PropertyDataType
        {
            booleanType,
            currencyType,
            dateType,
            numberType,
            stringType,
            timeType,
            OtherOrUnknownOrUndefinedDataType,
        }

        public const string RELATIONSHIP_PREFIX = "relationship";

        public enum RelationshipType
        {
            AccessRelationship,
            AggregationRelationship,
            AssignmentRelationship,
            AssociationRelationship,
            CompositionRelationship,
            FlowRelationship,
            InfluenceRelationship,
            RealisationRelationship,
            SpecialisationRelationship,
            TriggeringRelationship,
            UsedByRelationship,
            DerivedRelationship,
            OtherOrUnknownOrUndefinedRealtionship,
            AllRelationshipTypes
        }

        public const string TIMESNAP_PREFIX = "timesnap";

        public enum TimesnapCategory
        {
            processinghistory,
            usage,
            performance,
            management,
            OtherOrUnknownCategory,
        }

        public const string VIEW_PREFIX = "view";

        public enum ViewType
        {
            Introductory,
            Organization,
            ActorCooperation,
            BusinessFunction,
            BusinessProcess,
            BusinessProcessCooperation,
            Product,
            ApplicationBehavior,
            ApplicationCooperation,
            ApplicationStructure,
            ApplicationUsage,
            Infrastructure,
            InfrastructureUsage,
            ImplementationandDeployment,
            InformationStructure,
            ServiceRealization,
            Layered,
            LandscapeMap,
            Stakeholder,
            GoalRealization,
            GoalContribution,
            Principles,
            RequirementsRealization,
            Motivation,
            Project,
            Migration,
            ImplementationandMigration,
            OtherOrUnknownOrUndefinedViewType,
            AllViewTypes,
        }

        public const string LANG_EN = "en"; // TODO - change to an enumeration
        public const string LANG_FR = "fr"; // TODO - change to an enumeration
        public const string LANG_NL = "nl"; // TODO - change to an enumeration
        public const string LANG_DE = "de"; // TODO - change to an enumeration
        public const string LANG_ES = "es"; // TODO - change to an enumeration

        public const string ID_DATETIMEFORMAT = "yyyy'-'MM'-'dd'T'HH'-'mm'-'ss'-'fff";  // no colons and no dots

        public const string METADATA_XPREFIX = "metadata";
        public const string PROPERTIES_XPREFIX = "properties";
        public const string PROPERTY_XPREFIX = "property";
        public const string PROPERTYDEFS_XPREFIX = "propertydefs";
        public const string ELEMENTS_XPREFIX = "elements";
        public const string RELATIONSHIPS_XPREFIX = "releationships";
        public const string VIEWS_XPREFIX = "views";
        public const string ORGANIZATION_XPREFIX = "organization";
        public const string ITEM_XPREFIX = "item";
        public const string BENDPOINT_XPREFIX = "bendpoint";
        public const string STYLE_XPREFIX = "style";
        public const string FILLCOLOR_XPREFIX = "fillcolor";
        public const string LINECOLOR_XPREFIX = "linecolor";
        public const string FONT_XPREFIX = "font";
        public const string COLOR_XPREFIX = "color"; 
    }
}
