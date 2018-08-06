using PSN.ModelMate.MapToolkit.EDM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static PSN.ModelMate.Lib.ModelConst;

//TODO
//    MERGE(n:Person { name: { value} })
//ON CREATE SET n.created = timestamp()
//ON MATCH SET
//    n.counter = coalesce(n.counter, 0) + 1,
//    n.accessTime = timestamp()

namespace PSN.Modelmate.Neo4j.DBGen
{
    // https://ruijarimba.wordpress.com/2012/03/18/entity-framework-get-mapped-table-name-from-an-entity/
    public static class ContextExtensions
    {
        public static string GetSchemaTableName<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetSchemaTableName<T>();
        }
        public static string GetSchemaTableName<T>(this ObjectContext context) where T : class
        {
            ObjectSet<T> os = context.CreateObjectSet<T>();

            string sql = context.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex("FROM (?<schematable>.*) AS");
            Match match = regex.Match(sql);

            string table = match.Groups["schematable"].Value;
            return table;
        }

        public static string[] GetSchemaTableNames<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetSchemaTableNames<T>();
        }
        public static string GetSchemaName<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetSchemaTableNames<T>()[0];
        }
        public static string GetTableName<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetSchemaTableNames<T>()[1];
        }
        public static string[] GetSchemaTableNames<T>(this ObjectContext context) where T : class
        {
            string[] names = { "", "" };

            string sql = context.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex(@"FROM \[(?<schema>.*)\]\.\[(?<table>.*)\] AS");
            Match match = regex.Match(sql);

            string schema = match.Groups["schema"].Value;
            string table = match.Groups["table"].Value;

            names[0] = schema;
            names[1] = table;

            return names;
        }

        // http://stackoverflow.com/questions/7253943/entity-framework-code-first-find-primary-key
        static public IEnumerable<string> GetKeysNames<T>(this DbContext context) where T : class
        {
            //var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this.Context).ObjectContext;
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return GetKeysPropertyNames(typeof(T), objectContext.MetadataWorkspace);
        }
        static public string[] GetKeysNamesArray<T>(this DbContext context) where T : class
        {
            //var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this.Context).ObjectContext;
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return GetKeysPropertyNames(typeof(T), objectContext.MetadataWorkspace).ToArray<string>();
        }

        private static IEnumerable<string> GetKeysPropertyNames(Type type, MetadataWorkspace workspace)
        {
            EdmType edmType;

            if (workspace.TryGetType(type.Name, type.Namespace, DataSpace.OSpace, out edmType))
            {
                return edmType.MetadataProperties.Where(mp => mp.Name == "KeyMembers")
                    .SelectMany(mp => mp.Value as ReadOnlyMetadataCollection<EdmMember>)
                    .OfType<EdmProperty>().Select(edmProperty => edmProperty.Name);
            }

            return null;
        }
    }

    class Program
    {
        //static public Type[] EntityTypes =
        //{

        //    typeof(HardwareInventoryCore),
        //    typeof(HardwareInventoryEx),

        //    typeof(IISApplicationPool),
        //    typeof(IISEnabledService),
        //    typeof(IISVirtualDirApplication),
        //    typeof(IISVirtualDirApplicationsSubdir),
        //    typeof(IISWebInfo),
        //    typeof(IISWebServerSetting),
        //    typeof(IISWebStatu),





        //    typeof(Device),

        //    typeof(Processor),
        //    typeof(Product),
        //    typeof(HostGuestDetail),









        //    typeof(MemoryInfo),
        //    typeof(NetworkAdapter),

        //    typeof(Processors1),
        //    typeof(Products1),

        //    typeof(Databas),
        //    typeof(DatabaseSchema),


        //    typeof(Inventory),
        //    typeof(Option),
        //    typeof(Products2),

        //    typeof(MetricAggregation),
        //    typeof(MetricPerTimeInterval),
        //    typeof(Statistic),
        //    typeof(RawMetric),
        //    typeof(SqlInstance),


        //    typeof(DataBaseProperty),
        //    typeof(DatabasesCounter),
        //    typeof(DataBaseServerConfiguration),
        //    typeof(DataBaseServerProperty),

        //    typeof(Inventory1),
        //    typeof(DnsConfiguration),
        //    typeof(OsInfo),















        //    typeof(AboutInfo),
        //    typeof(Guest),
        //    typeof(Host),
        //    typeof(VCenter),


        //    typeof(WindowsInstalledSoftwareFull),

        //    typeof(ComputerSystemProduct),





        //    typeof(NetworkAdapterConfiguration),
        //    typeof(NetworkAdapters1),

        //    typeof(PhysicalMemory),


        //    typeof(Processors2),
        //    typeof(Products3),


        //    typeof(Service),


        //    typeof(ServerFeature),

        //};

        public class TypeInfo
        {
            public TypeInfo(Type tIn, ElementType archimateTypeIn, string databaseNameIn, string schemaNameIn, string tableNameIn, string[] primaryKeysIn, string[] connStringPartsIn)
            {
                t = tIn;
                archimateType = archimateTypeIn;
                databaseName = databaseNameIn;
                schemaName = schemaNameIn;
                tableName = tableNameIn;
                primaryKeys = primaryKeysIn;
                entityLabelParts = new string[] { tableNameIn, schemaNameIn, databaseNameIn };
                connStringParts = connStringPartsIn;
            }

            public Type t { get; set; }
            public ElementType archimateType { get; set; } 
            public string databaseName { get; set; }
            public string schemaName { get; set; }
            public string tableName { get; set; }
            public string[] primaryKeys { get; set; }
            //public string schemaTableName { get; set; }
            public string[] entityLabelParts { get; set; } // table, schema, database
            public string[] connStringParts { get;  set; } // serverport, databasename, user, password
        };

        static void Main(string[] args)
        {
            const string DEBUGTYPE = null;  // "DateTime";
            const string DEBUGPROPERTY = "SqlInstance";

            const string databaseName = "MAP_SampleDB";
            string[] connStringParts = new string[] { "PSN-W12R2-003:1433", "databaseName=" + databaseName, "user=sa", "password=P@ssword1", "" };

            const string comment1 = "// FROM DataTable:   [{0}].[{1}].[{2}]";
            const string comment2 = "// TO   EntityLabel: {2}_{1}_{0}";
            const string comment3 = "// FOR  EntityLabel: {2}_{1}_{0}";
            const string delete1 =       "MATCH (n:{0}) DELETE n RETURN count(*);";
            // NetworkAdapterConfiguration_Win_Inventory_MapToolkit
            const string dropindex1 =    "DROP   CONSTRAINT ON (d:{0}) ASSERT d.{1} IS UNIQUE;";
            // NetworkAdapterConfiguration_Win_Inventory_MapToolkit
            // DeviceNumber_Uid
            const string createindex1 =   "CREATE CONSTRAINT ON (d:{0}) ASSERT d.{1} IS UNIQUE;";
            // NetworkAdapterConfiguration_Win_Inventory_MapToolkit
            // DeviceNumber_Uid
            const string createindex2 =  "CREATE INDEX ON:{0}({1});";
            const string createindex2convert = "CREATE INDEX ON:{0}({1}String);";
            // NetworkAdapterConfiguration_Win_Inventory_MapToolkit
            // DeviceName
            const string createindex3 =  "CREATE INDEX ON:{0}({1});";
            // NetworkAdapterConfiguration_Win_Inventory_MapToolkit
            // Uid

            //const string periodicommit1 = "USING  PERIODIC COMMIT 1000"; // only for Load.CSV - use apoc.periodic.interate instead
            const string with1 =    "WITH   \"jdbc:sqlserver://{0}\" AS ConnString";
            // PSN-W12R2-003:1433;databaseName=MAP_SampleDB;user=sa;password=P@ssword1;
            const string call1 =    "CALL   apoc.load.jdbc(ConnString,";
            const string select1stmt =           "\"SELECT [{0}]";
            // DeviceNumber
            // CONVERT style code 121 = yyyy-mm-dd hh:mi:ss.mmm (24h)	ODBC canonical (with milliseconds)
            // http://www.w3schools.com/sql/func_convert.asp
            const string select1stmtconvert =    "\"SELECT CONVERT(VARCHAR,[{0}],121) AS {0}String";
            // CreateDatetime
            const string selectNclause =         "\t\t,[{0}]";
            // DeviceNumber
            const string selectNclauseconvert =  "\t\t,CONVERT(VARCHAR,[{0}],121) AS {0}String";
            // CreateDatetime
            const string from1 =    "FROM   [{0}].[{1}].[{2}]\") YIELD row AS DataRow ";
            // MAP_SampleDB
            // Win_Inventory
            // NetworkAdapterConfigurations
            const string with2 =    "WITH   DataRow ";
            const string merge1 =   "MERGE  (d:{0} {{"; // double {{ escape
            // NetworkAdapterConfiguration_Win_Inventory_MapToolkit
            const string merge2a =    "\t\t_DatabaseVersion: \"0.94\",";
            const string merge2b =    "\t\t_DatabaseSource:  \"[{0}].[{1}].[{2}]\",";
            // MAP_SampleDB
            // Win_Inventory
            // NetworkAdapterConfigurations

            const string merge3 =     "\t\t_ArchiMate2Type:   \"{0}_ArchiMate2\",";
            const string addlabel1 = "MATCH (n:{0})";
            // NetworkAdapterConfiguration_Win_Inventory_MapToolkit
            const string addlabel2 = "CALL apoc.create.addLabels([ id(n) ], [ n._ArchiMate2Type ]) YIELD node";
            const string addlabel3 = "RETURN count(*);";

            const string merge4key2 = "\t\t{0}: coalesce(DataRow.{1}, \"NULL\") + \"_\" + coalesce(DataRow.{2}, \"NULL\"),";
            const string merge4key3 = "\t\t{0}: coalesce(DataRow.{1}, \"NULL\") + \"_\" + coalesce(DataRow.{2}, \"NULL\") + \"_\" + coalesce(DataRow.{3}, \"NULL\"),";
            const string merge4key4 = "\t\t{0}: coalesce(DataRow.{1}, \"NULL\") + \"_\" + coalesce(DataRow.{2}, \"NULL\") + \"_\" + coalesce(DataRow.{3}, \"NULL\") + \"_\" + coalesce(DataRow.{4}, \"NULL\"),";
            const string merge4key5 = "\t\t{0}: coalesce(DataRow.{1}, \"NULL\") + \"_\" + coalesce(DataRow.{2}, \"NULL\") + \"_\" + coalesce(DataRow.{3}, \"NULL\") + \"_\" + coalesce(DataRow.{4}, \"NULL\") + \"_\" + coalesce(DataRow.{5}, \"NULL\"),";
            // DeviceNumber_Uid
            // DeviceNumber
            // Uid
            const string merge5 =   "\t\t{0}: coalesce(DataRow.{1}, \"NULL\"),";
            const string merge5convert = "\t\t{0}String: coalesce(DataRow.{1}String, \"NULL\"),";
            // DeviceNumber
            // DeviceNumber
            //const string merge6 =   "\t\t_Modified: timestamp(), _Removed: 0";
            const string merge6 =   "\t\t_Removed: 0";
            const string merge7 =   "})"; 
            const string oncreate1 = "ON CREATE SET d._Created = timestamp(), d._Modified = timestamp(), d._Removed = 0";
            const string returncount1 = "RETURN count(*);";

            const string exit1 = "exit";

            using (System.IO.StreamWriter FILE = new System.IO.StreamWriter(databaseName + ".ModelMate2Neo4j.cql", false))
            {
                using (var ctx = new MAP_SampleDBContext2())
                {
                    ctx.Database.Log = Console.Write;

                    //string sql = "select * from [Core_Inventory].[Devices]";
                    //DbParameter[] parms = new DbParameter[] { };
                    //var devices = ctx.Devices.SqlQuery(sql, parms).ToList<Device>();
                    //var d1 = devices[0];
                    //Type d1Type = d1.GetType();
                    //var d1Properties = d1Type.GetProperties();

                    //string tableName = ContextExtensions.GetSchemaTableName<Device>(ctx);
                    //Console.WriteLine("tablename: " + tableName);

                    //string[] names = ContextExtensions.GetSchemaTableNames<Device>(ctx);
                    //Console.WriteLine("schema: " + names[0]);
                    //Console.WriteLine("tale: " + names[1]);

                    //names = ContextExtensions.GetSchemaTableNames<Products2>(ctx);
                    //Console.WriteLine("schema: " + names[0]);
                    //Console.WriteLine("tale: " + names[1]);

                    //Console.WriteLine("Device: ");
                    //var pi2 = ContextExtensions.GetKeyPropertyNames<Device>(ctx);
                    //foreach (string s in pi2) Console.WriteLine("key: " + s);

                    //Console.WriteLine("Products2: ");
                    //pi2 = ContextExtensions.GetKeyPropertyNames<Products2>(ctx);
                    //foreach (string s in pi2) Console.WriteLine("key: " + s);

                    Dictionary<string, TypeInfo> typeInfo = new Dictionary<string, TypeInfo>();
                    //foreach (Type t in EntityTypes)
                    //{
                    //    string tName = t.Name;
                    //    string schemaName = ContextExtensions.GetSchemaName<NetworkAdapterConfiguration>(ctx);
                    //    string tableName = ContextExtensions.GetTableName<NetworkAdapterConfiguration>(ctx);
                    //    string[] primaryKeys = ContextExtensions.GetKeysNamesArray<NetworkAdapterConfiguration>(ctx);

                    //    TypeInfo tInfo = new TypeInfo(t, databaseName, schemaName, tableName, primaryKeys, connStringParts);
                    //    typeInfo.Add(tName, tInfo);
                    //}

                    typeInfo.Add(typeof(HardwareInventoryCore).Name, new TypeInfo(typeof(HardwareInventoryCore), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<HardwareInventoryCore>(ctx), ContextExtensions.GetTableName<HardwareInventoryCore>(ctx), ContextExtensions.GetKeysNamesArray<HardwareInventoryCore>(ctx), connStringParts));
                    typeInfo.Add(typeof(HardwareInventoryEx).Name, new TypeInfo(typeof(HardwareInventoryEx), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<HardwareInventoryEx>(ctx), ContextExtensions.GetTableName<HardwareInventoryEx>(ctx), ContextExtensions.GetKeysNamesArray<HardwareInventoryEx>(ctx), connStringParts));

                    typeInfo.Add(typeof(IISApplicationPool).Name, new TypeInfo(typeof(IISApplicationPool), ElementType.InfrastructureFunction, databaseName, ContextExtensions.GetSchemaName<IISApplicationPool>(ctx), ContextExtensions.GetTableName<IISApplicationPool>(ctx), ContextExtensions.GetKeysNamesArray<IISApplicationPool>(ctx), connStringParts));
                    typeInfo.Add(typeof(IISEnabledService).Name, new TypeInfo(typeof(IISEnabledService), ElementType.InfrastructureService, databaseName, ContextExtensions.GetSchemaName<IISEnabledService>(ctx), ContextExtensions.GetTableName<IISEnabledService>(ctx), ContextExtensions.GetKeysNamesArray<IISEnabledService>(ctx), connStringParts));
                    typeInfo.Add(typeof(IISVirtualDirApplication).Name, new TypeInfo(typeof(IISVirtualDirApplication), ElementType.InfrastructureFunction, databaseName, ContextExtensions.GetSchemaName<IISVirtualDirApplication>(ctx), ContextExtensions.GetTableName<IISVirtualDirApplication>(ctx), ContextExtensions.GetKeysNamesArray<IISVirtualDirApplication>(ctx), connStringParts));
                    typeInfo.Add(typeof(IISVirtualDirApplicationsSubdir).Name, new TypeInfo(typeof(IISVirtualDirApplicationsSubdir), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<IISVirtualDirApplicationsSubdir>(ctx), ContextExtensions.GetTableName<IISVirtualDirApplicationsSubdir>(ctx), ContextExtensions.GetKeysNamesArray<IISVirtualDirApplicationsSubdir>(ctx), connStringParts));
                    typeInfo.Add(typeof(IISWebInfo).Name, new TypeInfo(typeof(IISWebInfo), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<IISWebInfo>(ctx), ContextExtensions.GetTableName<IISWebInfo>(ctx), ContextExtensions.GetKeysNamesArray<IISWebInfo>(ctx), connStringParts));
                    typeInfo.Add(typeof(IISWebServerSetting).Name, new TypeInfo(typeof(IISWebServerSetting), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<IISWebServerSetting>(ctx), ContextExtensions.GetTableName<IISWebServerSetting>(ctx), ContextExtensions.GetKeysNamesArray<IISWebServerSetting>(ctx), connStringParts));
                    typeInfo.Add(typeof(IISWebStatu).Name, new TypeInfo(typeof(IISWebStatu), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<IISWebStatu>(ctx), ContextExtensions.GetTableName<IISWebStatu>(ctx), ContextExtensions.GetKeysNamesArray<IISWebStatu>(ctx), connStringParts));





                    typeInfo.Add(typeof(Device).Name, new TypeInfo(typeof(Device), ElementType.Node, databaseName, ContextExtensions.GetSchemaName<Device>(ctx), ContextExtensions.GetTableName<Device>(ctx), ContextExtensions.GetKeysNamesArray<Device>(ctx), connStringParts));

                    typeInfo.Add(typeof(Processor).Name, new TypeInfo(typeof(Processor), ElementType.Device, databaseName, ContextExtensions.GetSchemaName<Processor>(ctx), ContextExtensions.GetTableName<Processor>(ctx), ContextExtensions.GetKeysNamesArray<Processor>(ctx), connStringParts));
                    typeInfo.Add(typeof(Product).Name, new TypeInfo(typeof(Product), ElementType.Product, databaseName, ContextExtensions.GetSchemaName<Product>(ctx), ContextExtensions.GetTableName<Product>(ctx), ContextExtensions.GetKeysNamesArray<Product>(ctx), connStringParts));
                    typeInfo.Add(typeof(HostGuestDetail).Name, new TypeInfo(typeof(HostGuestDetail), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<HostGuestDetail>(ctx), ContextExtensions.GetTableName<HostGuestDetail>(ctx), ContextExtensions.GetKeysNamesArray<HostGuestDetail>(ctx), connStringParts));









                    typeInfo.Add(typeof(MemoryInfo).Name, new TypeInfo(typeof(MemoryInfo), ElementType.Device, databaseName, ContextExtensions.GetSchemaName<MemoryInfo>(ctx), ContextExtensions.GetTableName<MemoryInfo>(ctx), ContextExtensions.GetKeysNamesArray<MemoryInfo>(ctx), connStringParts));
                    typeInfo.Add(typeof(NetworkAdapter).Name, new TypeInfo(typeof(NetworkAdapter), ElementType.Device, databaseName, ContextExtensions.GetSchemaName<NetworkAdapter>(ctx), ContextExtensions.GetTableName<NetworkAdapter>(ctx), ContextExtensions.GetKeysNamesArray<NetworkAdapter>(ctx), connStringParts));

                    typeInfo.Add(typeof(Processors1).Name, new TypeInfo(typeof(Processors1), ElementType.Device, databaseName, ContextExtensions.GetSchemaName<Processors1>(ctx), ContextExtensions.GetTableName<Processors1>(ctx), ContextExtensions.GetKeysNamesArray<Processors1>(ctx), connStringParts));
                    typeInfo.Add(typeof(Products1).Name, new TypeInfo(typeof(Products1), ElementType.Product, databaseName, ContextExtensions.GetSchemaName<Products1>(ctx), ContextExtensions.GetTableName<Products1>(ctx), ContextExtensions.GetKeysNamesArray<Products1>(ctx), connStringParts));

                    typeInfo.Add(typeof(Databas).Name, new TypeInfo(typeof(Databas), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<Databas>(ctx), ContextExtensions.GetTableName<Databas>(ctx), ContextExtensions.GetKeysNamesArray<Databas>(ctx), connStringParts));
                    typeInfo.Add(typeof(DatabaseSchema).Name, new TypeInfo(typeof(DatabaseSchema), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<DatabaseSchema>(ctx), ContextExtensions.GetTableName<DatabaseSchema>(ctx), ContextExtensions.GetKeysNamesArray<DatabaseSchema>(ctx), connStringParts));


                    typeInfo.Add(typeof(Inventory).Name, new TypeInfo(typeof(Inventory), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<Inventory>(ctx), ContextExtensions.GetTableName<Inventory>(ctx), ContextExtensions.GetKeysNamesArray<Inventory>(ctx), connStringParts));
                    typeInfo.Add(typeof(Option).Name, new TypeInfo(typeof(Option), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<Option>(ctx), ContextExtensions.GetTableName<Option>(ctx), ContextExtensions.GetKeysNamesArray<Option>(ctx), connStringParts));
                    typeInfo.Add(typeof(Products2).Name, new TypeInfo(typeof(Products2), ElementType.Product, databaseName, ContextExtensions.GetSchemaName<Products2>(ctx), ContextExtensions.GetTableName<Products2>(ctx), ContextExtensions.GetKeysNamesArray<Products2>(ctx), connStringParts));

                    typeInfo.Add(typeof(MetricAggregation).Name, new TypeInfo(typeof(MetricAggregation), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<MetricAggregation>(ctx), ContextExtensions.GetTableName<MetricAggregation>(ctx), ContextExtensions.GetKeysNamesArray<MetricAggregation>(ctx), connStringParts));
                    typeInfo.Add(typeof(MetricPerTimeInterval).Name, new TypeInfo(typeof(MetricPerTimeInterval), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<MetricPerTimeInterval>(ctx), ContextExtensions.GetTableName<MetricPerTimeInterval>(ctx), ContextExtensions.GetKeysNamesArray<MetricPerTimeInterval>(ctx), connStringParts));
                    typeInfo.Add(typeof(Statistic).Name, new TypeInfo(typeof(Statistic), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<Statistic>(ctx), ContextExtensions.GetTableName<Statistic>(ctx), ContextExtensions.GetKeysNamesArray<Statistic>(ctx), connStringParts));
                    typeInfo.Add(typeof(RawMetric).Name, new TypeInfo(typeof(RawMetric), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<RawMetric>(ctx), ContextExtensions.GetTableName<RawMetric>(ctx), ContextExtensions.GetKeysNamesArray<RawMetric>(ctx), connStringParts));
                    typeInfo.Add(typeof(SqlInstance).Name, new TypeInfo(typeof(SqlInstance), ElementType.InfrastructureService, databaseName, ContextExtensions.GetSchemaName<SqlInstance>(ctx), ContextExtensions.GetTableName<SqlInstance>(ctx), 
                        new string[] { "DeviceNumber", "Servicename", "Instanceid" }, connStringParts));


                    typeInfo.Add(typeof(DataBaseProperty).Name, new TypeInfo(typeof(DataBaseProperty), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<DataBaseProperty>(ctx), ContextExtensions.GetTableName<DataBaseProperty>(ctx), ContextExtensions.GetKeysNamesArray<DataBaseProperty>(ctx), connStringParts));
                    typeInfo.Add(typeof(DatabasesCounter).Name, new TypeInfo(typeof(DatabasesCounter), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<DatabasesCounter>(ctx), ContextExtensions.GetTableName<DatabasesCounter>(ctx), ContextExtensions.GetKeysNamesArray<DatabasesCounter>(ctx), connStringParts));
                    typeInfo.Add(typeof(DataBaseServerConfiguration).Name, new TypeInfo(typeof(DataBaseServerConfiguration), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<DataBaseServerConfiguration>(ctx), ContextExtensions.GetTableName<DataBaseServerConfiguration>(ctx), ContextExtensions.GetKeysNamesArray<DataBaseServerConfiguration>(ctx), connStringParts));
                    typeInfo.Add(typeof(DataBaseServerProperty).Name, new TypeInfo(typeof(DataBaseServerProperty), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<DataBaseServerProperty>(ctx), ContextExtensions.GetTableName<DataBaseServerProperty>(ctx), ContextExtensions.GetKeysNamesArray<DataBaseServerProperty>(ctx), connStringParts));

                    typeInfo.Add(typeof(Inventory1).Name, new TypeInfo(typeof(Inventory1), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<Inventory1>(ctx), ContextExtensions.GetTableName<Inventory1>(ctx), ContextExtensions.GetKeysNamesArray<Inventory1>(ctx), connStringParts));
                    typeInfo.Add(typeof(DnsConfiguration).Name, new TypeInfo(typeof(DnsConfiguration), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<DnsConfiguration>(ctx), ContextExtensions.GetTableName<DnsConfiguration>(ctx), ContextExtensions.GetKeysNamesArray<DnsConfiguration>(ctx), connStringParts));
                    typeInfo.Add(typeof(OsInfo).Name, new TypeInfo(typeof(OsInfo), ElementType.SystemSoftware, databaseName, ContextExtensions.GetSchemaName<OsInfo>(ctx), ContextExtensions.GetTableName<OsInfo>(ctx), ContextExtensions.GetKeysNamesArray<OsInfo>(ctx), connStringParts));















                    typeInfo.Add(typeof(AboutInfo).Name, new TypeInfo(typeof(AboutInfo), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<AboutInfo>(ctx), ContextExtensions.GetTableName<AboutInfo>(ctx), ContextExtensions.GetKeysNamesArray<AboutInfo>(ctx), connStringParts));
                    //typeInfo.Add(typeof(Guest).Name, new TypeInfo(typeof(Guest), ElementType.Node, databaseName, ContextExtensions.GetSchemaName<Guest>(ctx), ContextExtensions.GetTableName<Guest>(ctx), ContextExtensions.GetKeysNamesArray<Guest>(ctx), connStringParts));
                    //typeInfo.Add(typeof(Host).Name, new TypeInfo(typeof(Host), ElementType.Device, databaseName, ContextExtensions.GetSchemaName<Host>(ctx), ContextExtensions.GetTableName<Host>(ctx), ContextExtensions.GetKeysNamesArray<Host>(ctx), connStringParts));
                    typeInfo.Add(typeof(VCenter).Name, new TypeInfo(typeof(VCenter), ElementType.InfrastructureService, databaseName, ContextExtensions.GetSchemaName<VCenter>(ctx), ContextExtensions.GetTableName<VCenter>(ctx), ContextExtensions.GetKeysNamesArray<VCenter>(ctx), connStringParts));


                    typeInfo.Add(typeof(WindowsInstalledSoftwareFull).Name, new TypeInfo(typeof(WindowsInstalledSoftwareFull), ElementType.Product, databaseName, ContextExtensions.GetSchemaName<WindowsInstalledSoftwareFull>(ctx), ContextExtensions.GetTableName<WindowsInstalledSoftwareFull>(ctx), ContextExtensions.GetKeysNamesArray<WindowsInstalledSoftwareFull>(ctx), connStringParts));

                    typeInfo.Add(typeof(ComputerSystemProduct).Name, new TypeInfo(typeof(ComputerSystemProduct), ElementType.Product, databaseName, ContextExtensions.GetSchemaName<ComputerSystemProduct>(ctx), ContextExtensions.GetTableName<ComputerSystemProduct>(ctx), ContextExtensions.GetKeysNamesArray<ComputerSystemProduct>(ctx), connStringParts));





                    typeInfo.Add(typeof(NetworkAdapterConfiguration).Name, new TypeInfo(typeof(NetworkAdapterConfiguration), ElementType.Artifact, databaseName, ContextExtensions.GetSchemaName<NetworkAdapterConfiguration>(ctx), ContextExtensions.GetTableName<NetworkAdapterConfiguration>(ctx), ContextExtensions.GetKeysNamesArray<NetworkAdapterConfiguration>(ctx), connStringParts));
                    typeInfo.Add(typeof(NetworkAdapters1).Name, new TypeInfo(typeof(NetworkAdapters1), ElementType.Device, databaseName, ContextExtensions.GetSchemaName<NetworkAdapters1>(ctx), ContextExtensions.GetTableName<NetworkAdapters1>(ctx), ContextExtensions.GetKeysNamesArray<NetworkAdapters1>(ctx), connStringParts));

                    typeInfo.Add(typeof(PhysicalMemory).Name, new TypeInfo(typeof(PhysicalMemory), ElementType.Device, databaseName, ContextExtensions.GetSchemaName<PhysicalMemory>(ctx), ContextExtensions.GetTableName<PhysicalMemory>(ctx), ContextExtensions.GetKeysNamesArray<PhysicalMemory>(ctx), connStringParts));


                    typeInfo.Add(typeof(Processors2).Name, new TypeInfo(typeof(Processors2), ElementType.Device, databaseName, ContextExtensions.GetSchemaName<Processors2>(ctx), ContextExtensions.GetTableName<Processors2>(ctx), ContextExtensions.GetKeysNamesArray<Processors2>(ctx), connStringParts));
                    typeInfo.Add(typeof(Products3).Name, new TypeInfo(typeof(Products3), ElementType.Product, databaseName, ContextExtensions.GetSchemaName<Products3>(ctx), ContextExtensions.GetTableName<Products3>(ctx), ContextExtensions.GetKeysNamesArray<Products3>(ctx), connStringParts));


                    typeInfo.Add(typeof(Service).Name, new TypeInfo(typeof(Service), ElementType.InfrastructureService, databaseName, ContextExtensions.GetSchemaName<Service>(ctx), ContextExtensions.GetTableName<Service>(ctx), ContextExtensions.GetKeysNamesArray<Service>(ctx), connStringParts));





























                    typeInfo.Add(typeof(ServerFeature).Name, new TypeInfo(typeof(ServerFeature), ElementType.InfrastructureFunction, databaseName, ContextExtensions.GetSchemaName<ServerFeature>(ctx), ContextExtensions.GetTableName<ServerFeature>(ctx), ContextExtensions.GetKeysNamesArray<ServerFeature>(ctx), connStringParts));

                    //typeInfo.Add(typeof(MetricAggregation).Name, new TypeInfo(typeof(MetricAggregation), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<MetricAggregation>(ctx), ContextExtensions.GetTableName<MetricAggregation>(ctx), ContextExtensions.GetKeysNamesArray<MetricAggregation>(ctx), connStringParts));
                    //typeInfo.Add(typeof(MetricPerTimeInterval).Name, new TypeInfo(typeof(MetricPerTimeInterval), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<MetricPerTimeInterval>(ctx), ContextExtensions.GetTableName<MetricPerTimeInterval>(ctx), ContextExtensions.GetKeysNamesArray<MetricPerTimeInterval>(ctx), connStringParts));
                    typeInfo.Add(typeof(MetricType).Name, new TypeInfo(typeof(MetricType), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<MetricType>(ctx), ContextExtensions.GetTableName<MetricType>(ctx), ContextExtensions.GetKeysNamesArray<MetricType>(ctx), connStringParts));
                    typeInfo.Add(typeof(ProcessorVelocityMap).Name, new TypeInfo(typeof(ProcessorVelocityMap), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<ProcessorVelocityMap>(ctx), ContextExtensions.GetTableName<ProcessorVelocityMap>(ctx), ContextExtensions.GetKeysNamesArray<ProcessorVelocityMap>(ctx), connStringParts));
                    typeInfo.Add(typeof(TimeInterval).Name, new TypeInfo(typeof(TimeInterval), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<TimeInterval>(ctx), ContextExtensions.GetTableName<TimeInterval>(ctx), ContextExtensions.GetKeysNamesArray<TimeInterval>(ctx), connStringParts));

                    typeInfo.Add(typeof(HostGuest).Name, new TypeInfo(typeof(HostGuest), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<HostGuest>(ctx), ContextExtensions.GetTableName<HostGuest>(ctx), ContextExtensions.GetKeysNamesArray<HostGuest>(ctx), connStringParts));
                    typeInfo.Add(typeof(Host).Name, new TypeInfo(typeof(Host), ElementType.Device, databaseName, ContextExtensions.GetSchemaName<Host>(ctx), ContextExtensions.GetTableName<Host>(ctx), ContextExtensions.GetKeysNamesArray<Host>(ctx), connStringParts));
                    typeInfo.Add(typeof(Guest).Name, new TypeInfo(typeof(Guest), ElementType.Node, databaseName, ContextExtensions.GetSchemaName<Guest>(ctx), ContextExtensions.GetTableName<Guest>(ctx), ContextExtensions.GetKeysNamesArray<Guest>(ctx), connStringParts));
                    typeInfo.Add(typeof(HostMetric).Name, new TypeInfo(typeof(HostMetric), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<HostMetric>(ctx), ContextExtensions.GetTableName<HostMetric>(ctx), ContextExtensions.GetKeysNamesArray<HostMetric>(ctx), connStringParts));
                    typeInfo.Add(typeof(GuestMetric).Name, new TypeInfo(typeof(GuestMetric), ElementType.Value, databaseName, ContextExtensions.GetSchemaName<GuestMetric>(ctx), ContextExtensions.GetTableName<GuestMetric>(ctx), ContextExtensions.GetKeysNamesArray<GuestMetric>(ctx), connStringParts));

                    FILE.WriteLine("// Parallelspace ModelMate");
                    FILE.WriteLine("// ModelMate SQL Server to Neo4j Loader (ModelMate2Neo4j)");
                    FILE.WriteLine("// Version 1.0.1001");
                    FILE.WriteLine("//") ;
                    FILE.WriteLine("// Server:   " + connStringParts[0] + ";" + connStringParts[1] + ";");
                    FILE.WriteLine("// Database: " + databaseName);
                    FILE.WriteLine("// Created:  " + DateTime.Now.ToString());

                    // DELETE RELATIONSHIPS
                    const string reldelete1 = "MATCH path = (source)-[r:{0}__{1}]->(target) DELETE r RETURN count(*);";
                    // Device_Core_Inventory_MapToolkit
                    // NetworkAdapterConfiguration_Win_Inventory_MapToolkit

                    FILE.WriteLine();
                    FILE.WriteLine("////////////////////////////////////////////////////");
                    FILE.WriteLine("// Delete Relationships");
                    FILE.WriteLine();

                    foreach (TypeInfo ti in typeInfo.Values)
                    {
                        //if (ti.t != typeof(Device) &&
                        //    ti.t != typeof(NetworkAdapterConfiguration)) continue;

                        bool firstProperty = true;
                        foreach (PropertyInfo pi in ti.t.GetProperties())
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Type.Name: " + ti.t.Name);
                            Console.WriteLine("PropertyInfo.Name: " + pi.Name);
                            Console.WriteLine("PropertyInfo.PropertyType: " + pi.PropertyType.Name);
                            Console.WriteLine("PropertyInfo.PropertyType: " + pi.PropertyType.FullName);
                            Console.WriteLine("PropertyInfo.ReflectedType: " + pi.ReflectedType.FullName);
                            if (pi.PropertyType.GetGenericArguments().Count() >= 1) // Nullable`1, ICollection`1
                            {
                                Console.WriteLine("PropertyInfo.PropertyType.GetGenericArguments: " + pi.PropertyType.GetGenericArguments()[0].Name);
                                Console.WriteLine("PropertyInfo.PropertyType.GetGenericArguments: " + pi.PropertyType.GetGenericArguments()[0].FullName);

                                switch (pi.PropertyType.Name)
                                {
                                    case "Nullable`1":
                                        {
                                            break;
                                        }
                                    case "ICollection`1":
                                        {
                                            string tname = pi.PropertyType.GetGenericArguments()[0].Name;
                                            if (typeInfo.Keys.Contains(tname))
                                            {
                                                TypeInfo tiTarget = typeInfo[tname];
                                                switch (ti.primaryKeys.Length)
                                                {
                                                    case 1:
                                                        {
                                                            FILE.WriteLine(reldelete1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            FILE.WriteLine(reldelete1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                            break;
                                                        }
                                                }
                                            }
                                            else
                                            {
                                                // do nothing - the target is not a table we're interested in
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            throw new NotImplementedException(pi.PropertyType.FullName);
                                            break;
                                        }
                                }
                                //if (!String.IsNullOrEmpty(DEBUGTYPE) && pi.PropertyType.FullName.Contains(DEBUGTYPE)) System.Diagnostics.Debugger.Break(); // delete relationships
                                //if (!String.IsNullOrEmpty(DEBUGPROPERTY) && pi.Name.Contains(DEBUGPROPERTY)) System.Diagnostics.Debugger.Break(); // delete relationships
                            }
                            else
                            {
                                if (pi.GetAccessors()[0].IsVirtual)
                                {
                                    // do nothing - a reference to another table - not a property
                                }
                                else
                                {
                                    // do nothing - because all relationships result from virtual properties (fields with virtual accessors)
                                    // TODO: Add delete relationships for single virtual Device
                                }
                                //if (!String.IsNullOrEmpty(DEBUGTYPE) && pi.PropertyType.FullName.Contains(DEBUGTYPE)) System.Diagnostics.Debugger.Break();  // delete relationships
                                //if (!String.IsNullOrEmpty(DEBUGPROPERTY) && pi.Name.Contains(DEBUGPROPERTY)) System.Diagnostics.Debugger.Break();  // delete relationships
                            }
                            firstProperty = false;
                        }
                    }
                    
                    // ENTITIES
                    foreach (TypeInfo ti in typeInfo.Values)
                    {
                        //if (ti.t != typeof(Device) &&
                        //    ti.t != typeof(NetworkAdapterConfiguration)) continue;

                        FILE.WriteLine();
                        FILE.WriteLine("////////////////////////////////////////////////////");
                        FILE.WriteLine(comment1, ti.databaseName, ti.schemaName, ti.tableName);
                        FILE.WriteLine(comment2, ti.databaseName, ti.schemaName, ti.tableName);
                        FILE.WriteLine();
                        FILE.WriteLine(delete1, String.Join("_", ti.entityLabelParts));
                        FILE.WriteLine();
                        FILE.WriteLine(dropindex1, String.Join("_", ti.entityLabelParts), String.Join("_", ti.primaryKeys));
                        FILE.WriteLine(createindex1, String.Join("_", ti.entityLabelParts), String.Join("_", ti.primaryKeys));
                        if (ti.primaryKeys.Length > 1)
                        {
                            foreach (string key in ti.primaryKeys)
                            {
                                PropertyInfo piMatch = null;
                                foreach (PropertyInfo pi in ti.t.GetProperties()) if (pi.Name == key) { piMatch = pi;  break; }
                                if (piMatch.PropertyType.FullName == "System.DateTime")
                                {
                                    FILE.WriteLine(createindex2convert, String.Join("_", ti.entityLabelParts), key);
                                }
                                else
                                {
                                    FILE.WriteLine(createindex2, String.Join("_", ti.entityLabelParts), key);
                                }
                            }
                        }
                        FILE.WriteLine(createindex2, String.Join("_", ti.entityLabelParts), "_ArchiMate2Type");
                        FILE.WriteLine();
                        //FILE.WriteLine(periodicommit1);
                        FILE.WriteLine(with1, String.Join(";", ti.connStringParts));
                        FILE.WriteLine(call1);

                        bool firstProperty = true;
                        foreach (PropertyInfo pi in ti.t.GetProperties())
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Type.Name: " + ti.t.Name);
                            Console.WriteLine("PropertyInfo.Name: " + pi.Name);
                            Console.WriteLine("PropertyInfo.PropertyType: " + pi.PropertyType.Name);
                            Console.WriteLine("PropertyInfo.PropertyType: " + pi.PropertyType.FullName);
                            Console.WriteLine("PropertyInfo.ReflectedType: " + pi.ReflectedType.FullName);
                            if (pi.PropertyType.GetGenericArguments().Count() >= 1) // Nullable`1, ICollection`1
                            {
                                Console.WriteLine("PropertyInfo.PropertyType.GetGenericArguments: " + pi.PropertyType.GetGenericArguments()[0].Name);
                                Console.WriteLine("PropertyInfo.PropertyType.GetGenericArguments: " + pi.PropertyType.GetGenericArguments()[0].FullName);

                                switch (pi.PropertyType.Name)
                                {
                                    case "Nullable`1":
                                        {
                                            switch (pi.PropertyType.GetGenericArguments()[0].FullName)
                                            {
                                                case "System.DateTime":
                                                    {
                                                        if (firstProperty)
                                                        {
                                                            FILE.WriteLine(select1stmtconvert, pi.Name);
                                                        }
                                                        else
                                                        {
                                                            FILE.WriteLine(selectNclauseconvert, pi.Name);
                                                        }
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        if (firstProperty)
                                                        {
                                                            FILE.WriteLine(select1stmt, pi.Name);
                                                        }
                                                        else
                                                        {
                                                            FILE.WriteLine(selectNclause, pi.Name);
                                                        }
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case "ICollection`1":
                                        {
                                            break;
                                        }
                                    default:
                                        {
                                            throw new NotImplementedException(pi.PropertyType.FullName);
                                            break;
                                        }
                                }
                                if (!String.IsNullOrEmpty(DEBUGTYPE) && pi.PropertyType.FullName.Contains(DEBUGTYPE)) System.Diagnostics.Debugger.Break(); // select stmt subtype
                                if (!String.IsNullOrEmpty(DEBUGPROPERTY) && pi.Name.Contains(DEBUGPROPERTY)) System.Diagnostics.Debugger.Break(); // select stmt sub type
                            }
                            else
                            {
                                if (pi.GetAccessors()[0].IsVirtual)
                                {
                                    // do nothing - a reference to another table - not a property
                                }
                                else
                                {
                                    string piName = pi.Name;
                                    if (piName == "MetricType1") piName = "MetricType";
                                    if (piName == "Domain_Workgroup") piName = "Domain/Workgroup";
                                    if (pi.PropertyType.FullName == "System.DateTime")
                                    {
                                        if (firstProperty)
                                        {
                                            FILE.WriteLine(select1stmtconvert, piName);
                                        }
                                        else
                                        {
                                            FILE.WriteLine(selectNclauseconvert, piName);
                                        }
                                    }
                                    else
                                    {
                                        if (firstProperty)
                                        {
                                            FILE.WriteLine(select1stmt, piName);
                                        }
                                        else
                                        {
                                            FILE.WriteLine(selectNclause, piName);
                                        }
                                    }
                                }
                                //if (!String.IsNullOrEmpty(DEBUGTYPE) && pi.PropertyType.FullName.Contains(DEBUGTYPE)) System.Diagnostics.Debugger.Break(); // select stmt
                                //if (!String.IsNullOrEmpty(DEBUGPROPERTY) && pi.Name.Contains(DEBUGPROPERTY)) System.Diagnostics.Debugger.Break(); // select stmt
                            }

                            firstProperty = false;
                        }

                        FILE.WriteLine(from1, ti.databaseName, ti.schemaName, ti.tableName);
                        FILE.WriteLine(with2);
                        FILE.WriteLine(merge1, String.Join("_", ti.entityLabelParts));
                        FILE.WriteLine(merge2a);
                        FILE.WriteLine(merge2b, ti.databaseName, ti.schemaName, ti.tableName);
                        FILE.WriteLine(merge3, ti.archimateType.ToString());

                        // MERGE CLAUSE
                        foreach (PropertyInfo pi in ti.t.GetProperties())
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Type.Name: " + ti.t.Name);
                            Console.WriteLine("PropertyInfo.Name: " + pi.Name);
                            Console.WriteLine("PropertyInfo.PropertyType: " + pi.PropertyType.Name);
                            Console.WriteLine("PropertyInfo.PropertyType: " + pi.PropertyType.FullName);
                            Console.WriteLine("PropertyInfo.ReflectedType: " + pi.ReflectedType.FullName);
                            if (pi.PropertyType.GetGenericArguments().Count() >= 1) // System.Nullable'1, ICollection'1
                            {
                                Console.WriteLine("PropertyInfo.PropertyType.GetGenericArguments: " + pi.PropertyType.GetGenericArguments()[0].Name);
                                Console.WriteLine("PropertyInfo.PropertyType.GetGenericArguments: " + pi.PropertyType.GetGenericArguments()[0].FullName);

                                switch (pi.PropertyType.Name)
                                {
                                    case "Nullable`1":
                                        {
                                            switch (pi.PropertyType.GetGenericArguments()[0].FullName)
                                            {
                                                case "System.DateTime":
                                                    {
                                                        FILE.WriteLine(merge5convert, pi.Name, pi.Name);
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        FILE.WriteLine(merge5, pi.Name, pi.Name);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case "ICollection`1":
                                        {
                                            break;
                                        }
                                    default:
                                        {
                                            throw new NotImplementedException(pi.PropertyType.FullName);
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                if (pi.GetAccessors()[0].IsVirtual)
                                {
                                    // do nothing - a reference to another table - not a property
                                }
                                else
                                {
                                    string piName = pi.Name;
                                    if (piName == "MetricType1") piName = "MetricType";
                                    if (piName == "Domain_Workgroup") piName = "Domain/Workgroup";
                                    if (pi.PropertyType.FullName == "System.DateTime")
                                    {
                                        FILE.WriteLine(merge5convert, pi.Name, pi.Name);
                                    }
                                    else
                                    {
                                        FILE.WriteLine(merge5, pi.Name, pi.Name);
                                    }
                                }
                                // if (!String.IsNullOrEmpty(DEBUGTYPE) && pi.PropertyType.FullName.Contains(DEBUGTYPE)) System.Diagnostics.Debugger.Break(); // merge clause
                                // if (!String.IsNullOrEmpty(DEBUGPROPERTY) && pi.Name.Contains(DEBUGPROPERTY)) System.Diagnostics.Debugger.Break(); // merge clause
                            }
                        }

                        switch(ti.primaryKeys.Length)
                        {
                            case 1:
                                {
                                    // do nothing
                                    break;
                                }
                            case 2:
                                {
                                    string key = ti.primaryKeys[1];
                                    PropertyInfo piMatch = null;
                                    foreach (PropertyInfo pi in ti.t.GetProperties()) if (pi.Name == key) { piMatch = pi; break; }
                                    if (piMatch.PropertyType.FullName == "System.DateTime")
                                    {
                                        FILE.WriteLine(merge4key2, String.Join("_", ti.primaryKeys), ti.primaryKeys[0], key + "String");
                                    }
                                    else
                                    {
                                        FILE.WriteLine(merge4key2, String.Join("_", ti.primaryKeys), ti.primaryKeys[0], key);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    FILE.WriteLine(merge4key3, String.Join("_", ti.primaryKeys), ti.primaryKeys[0], ti.primaryKeys[1], ti.primaryKeys[2]);
                                    break;
                                }
                            case 4:
                                {
                                    FILE.WriteLine(merge4key4, String.Join("_", ti.primaryKeys), ti.primaryKeys[0], ti.primaryKeys[1], ti.primaryKeys[2], ti.primaryKeys[3]);
                                    break;
                                }
                            case 5:
                                {
                                    FILE.WriteLine(merge4key5, String.Join("_", ti.primaryKeys), ti.primaryKeys[0], ti.primaryKeys[1], ti.primaryKeys[2], ti.primaryKeys[3], ti.primaryKeys[4]);
                                    break;
                                }
                            default:
                                {
                                    throw new NotImplementedException("ti.primaryKeys.Length");
                                    break;
                                }
                        }

                        FILE.WriteLine(merge6);
                        FILE.WriteLine(merge7);
                        FILE.WriteLine(oncreate1);
                        FILE.WriteLine(returncount1);

                        FILE.WriteLine(addlabel1, String.Join("_", ti.entityLabelParts));
                        FILE.WriteLine(addlabel2);
                        FILE.WriteLine(addlabel3);
                    }

                    // ADD RELATIONSHIPS
                    const string relmatch1 = "MATCH (source:{0}), (target:{1})";
                    // Device_Core_Inventory_MapToolkit
                    // NetworkAdapterConfiguration_Win_Inventory_MapToolkit
                    const string relwhere1 = "WHERE source.{0} = target.{1}";
                    // DeviceNumber
                    // DeviceNumber
                    const string relmerge1 = "MERGE path = (source)-[r:{0}__{1}]->(target)";
                    // Device_Core_Inventory_MapToolkit
                    // NetworkAdapterConfiguration_Win_Inventory_MapToolkit
                    const string relreturn1 = "RETURN count(*);";

                    foreach (TypeInfo ti in typeInfo.Values)
                    {
                        //if (ti.t != typeof(Device) &&
                        //    ti.t != typeof(NetworkAdapterConfiguration)) continue;

                        FILE.WriteLine();
                        FILE.WriteLine("////////////////////////////////////////////////////");
                        FILE.WriteLine(comment1, ti.databaseName, ti.schemaName, ti.tableName);
                        FILE.WriteLine(comment3, ti.databaseName, ti.schemaName, ti.tableName);

                        bool firstProperty = true;
                        foreach (PropertyInfo pi in ti.t.GetProperties())
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Type.Name: " + ti.t.Name);
                            Console.WriteLine("PropertyInfo.Name: " + pi.Name);
                            Console.WriteLine("PropertyInfo.PropertyType: " + pi.PropertyType.Name);
                            Console.WriteLine("PropertyInfo.PropertyType: " + pi.PropertyType.FullName);
                            Console.WriteLine("PropertyInfo.ReflectedType: " + pi.ReflectedType.FullName);
                            if (pi.PropertyType.GetGenericArguments().Count() >= 1) // Nullable`1, ICollection`1
                            {
                                Console.WriteLine("PropertyInfo.PropertyType.GetGenericArguments: " + pi.PropertyType.GetGenericArguments()[0].Name);
                                Console.WriteLine("PropertyInfo.PropertyType.GetGenericArguments: " + pi.PropertyType.GetGenericArguments()[0].FullName);

                                switch (pi.PropertyType.Name)
                                {
                                    case "Nullable`1":
                                        {
                                            break;
                                        }
                                    case "ICollection`1":
                                        {
                                            string tname = pi.PropertyType.GetGenericArguments()[0].Name;
                                            if (typeInfo.Keys.Contains(tname))
                                            {
                                                TypeInfo tiTarget = typeInfo[tname];
                                                switch (ti.primaryKeys.Length)
                                                {
                                                    case 1:
                                                        {
                                                            FILE.WriteLine();
                                                            FILE.WriteLine(relmatch1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                            FILE.WriteLine(relwhere1, String.Join("_", ti.primaryKeys), String.Join("_", ti.primaryKeys));
                                                            FILE.WriteLine(relmerge1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                            FILE.WriteLine(relreturn1);
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            // e.g. HostGuest
                                                            FILE.WriteLine();
                                                            FILE.WriteLine(relmatch1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                            FILE.WriteLine(relwhere1, String.Join("_", ti.primaryKeys), String.Join("_", ti.primaryKeys));
                                                            FILE.WriteLine(relmerge1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                            FILE.WriteLine(relreturn1);
                                                            break;
                                                        }
                                                }
                                            }
                                            else
                                            {
                                                // do nothing - the target is not a table we're interested in
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            throw new NotImplementedException(pi.PropertyType.FullName);
                                            break;
                                        }
                                }
                                if (!String.IsNullOrEmpty(DEBUGTYPE) && pi.PropertyType.FullName.Contains(DEBUGTYPE)) System.Diagnostics.Debugger.Break(); // add relationship
                                if (!String.IsNullOrEmpty(DEBUGPROPERTY) && pi.Name.Contains(DEBUGPROPERTY)) System.Diagnostics.Debugger.Break(); // add relationship
                            }
                            else
                            {
                                if (pi.GetAccessors()[0].IsVirtual)
                                {
                                    // single (virtual) reference to the target type
                                    string tname = pi.Name;
                                    if (typeInfo.Keys.Contains(tname))
                                    {
                                        TypeInfo tiTarget = typeInfo[tname];
                                        switch (ti.primaryKeys.Length)
                                        {
                                            case 1:
                                                {
                                                    FILE.WriteLine();
                                                    FILE.WriteLine(relmatch1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                    FILE.WriteLine(relwhere1, String.Join("_", ti.primaryKeys), String.Join("_", ti.primaryKeys));
                                                    FILE.WriteLine(relmerge1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                    FILE.WriteLine(relreturn1);
                                                    break;
                                                }
                                            default:
                                                {
                                                    if (tiTarget.primaryKeys.Length == 1)
                                                    {
                                                        // e.g. IisApplicationPools and Device
                                                        FILE.WriteLine();
                                                        FILE.WriteLine(relmatch1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                        FILE.WriteLine(relwhere1, String.Join("_", tiTarget.primaryKeys), String.Join("_", tiTarget.primaryKeys));
                                                        FILE.WriteLine(relmerge1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                        FILE.WriteLine(relreturn1);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        // e.g. Consolidation_Assesment.Guest
                                                        FILE.WriteLine();
                                                        FILE.WriteLine(relmatch1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                        FILE.WriteLine(relwhere1, String.Join("_", tiTarget.primaryKeys), String.Join("_", tiTarget.primaryKeys));
                                                        FILE.WriteLine(relmerge1, String.Join("_", ti.entityLabelParts), String.Join("_", tiTarget.entityLabelParts));
                                                        FILE.WriteLine(relreturn1);
                                                        break;
                                                    }
                                                    break;
                                                }
                                        }
                                    }
                                }
                                else
                                {
                                    // do nothing - because all relationships result from virtual properties (fields with virtual accessors)
                                }
                                if (!String.IsNullOrEmpty(DEBUGTYPE) && pi.PropertyType.FullName.Contains(DEBUGTYPE)) System.Diagnostics.Debugger.Break(); // add relationship
                                if (!String.IsNullOrEmpty(DEBUGPROPERTY) && pi.Name.Contains(DEBUGPROPERTY)) System.Diagnostics.Debugger.Break(); // add relationship
                            }
                            firstProperty = false;
                        }
                    }

                    FILE.WriteLine();
                    FILE.WriteLine(exit1);
                }
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
