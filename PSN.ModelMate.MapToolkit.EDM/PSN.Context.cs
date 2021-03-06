﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PSN.ModelMate.MapToolkit.EDM
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MAP_SampleDBContext2 : DbContext
    {
        public MAP_SampleDBContext2()
            : base("name=MAP_SampleDBContext2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CategorizedDevice> CategorizedDevices { get; set; }
        public virtual DbSet<HardwareInventoryCore> HardwareInventoryCores { get; set; }
        public virtual DbSet<HardwareInventoryEx> HardwareInventoryExes { get; set; }
        public virtual DbSet<AppConfig> AppConfigs { get; set; }
        public virtual DbSet<AppConfigDbSelection> AppConfigDbSelections { get; set; }
        public virtual DbSet<VMQualification> VMQualifications { get; set; }
        public virtual DbSet<IISApplicationPool> IISApplicationPools { get; set; }
        public virtual DbSet<IISEnabledService> IISEnabledServices { get; set; }
        public virtual DbSet<IISVirtualDirApplication> IISVirtualDirApplications { get; set; }
        public virtual DbSet<IISVirtualDirApplicationsSubdir> IISVirtualDirApplicationsSubdirs { get; set; }
        public virtual DbSet<IISWebInfo> IISWebInfoes { get; set; }
        public virtual DbSet<IISWebServerSetting> IISWebServerSettings { get; set; }
        public virtual DbSet<IISWebStatu> IISWebStatus { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<GuestMetric> GuestMetrics { get; set; }
        public virtual DbSet<Host> Hosts { get; set; }
        public virtual DbSet<HostGuest> HostGuests { get; set; }
        public virtual DbSet<HostMetric> HostMetrics { get; set; }
        public virtual DbSet<HypervFasttrackCustomization> HypervFasttrackCustomizations { get; set; }
        public virtual DbSet<Infra> Infras { get; set; }
        public virtual DbSet<InfraConfig> InfraConfigs { get; set; }
        public virtual DbSet<InfraHost> InfraHosts { get; set; }
        public virtual DbSet<InfraMetric> InfraMetrics { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<RoiStatu> RoiStatus { get; set; }
        public virtual DbSet<SqlApplianceCustomization> SqlApplianceCustomizations { get; set; }
        public virtual DbSet<AdDiscoveredDevice> AdDiscoveredDevices { get; set; }
        public virtual DbSet<CollectorClassesToCollect> CollectorClassesToCollects { get; set; }
        public virtual DbSet<CollectorClassStatu> CollectorClassStatus { get; set; }
        public virtual DbSet<DeviceConnectionError> DeviceConnectionErrors { get; set; }
        public virtual DbSet<DeviceInventoryStat> DeviceInventoryStats { get; set; }
        public virtual DbSet<DeviceQueueWatermark> DeviceQueueWatermarks { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<SCDiscoveredDevice> SCDiscoveredDevices { get; set; }
        public virtual DbSet<CollectorClassDetail> CollectorClassDetails { get; set; }
        public virtual DbSet<LocalizedString> LocalizedStrings { get; set; }
        public virtual DbSet<configuration> configurations { get; set; }
        public virtual DbSet<instance> instances { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Processor> Processors { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<HostGuestDetail> HostGuestDetails { get; set; }
        public virtual DbSet<BinaryFile> BinaryFiles { get; set; }
        public virtual DbSet<BrowserExtension> BrowserExtensions { get; set; }
        public virtual DbSet<Control> Controls { get; set; }
        public virtual DbSet<DeviceUserProfile> DeviceUserProfiles { get; set; }
        public virtual DbSet<InternetBrowser> InternetBrowsers { get; set; }
        public virtual DbSet<UserControl> UserControls { get; set; }
        public virtual DbSet<CdromDrive> CdromDrives { get; set; }
        public virtual DbSet<Distribution> Distributions { get; set; }
        public virtual DbSet<Filesystem> Filesystems { get; set; }
        public virtual DbSet<MemoryInfo> MemoryInfoes { get; set; }
        public virtual DbSet<NetworkAdapter> NetworkAdapters { get; set; }
        public virtual DbSet<PciDevice> PciDevices { get; set; }
        public virtual DbSet<Processors1> Processors1 { get; set; }
        public virtual DbSet<Products1> Products1 { get; set; }
        public virtual DbSet<SmbiosInfo> SmbiosInfoes { get; set; }
        public virtual DbSet<Office2010Assessment> Office2010Assessment { get; set; }
        public virtual DbSet<Office365Assessment> Office365Assessment { get; set; }
        public virtual DbSet<Databas> Databases { get; set; }
        public virtual DbSet<DatabaseSchema> DatabaseSchemas { get; set; }
        public virtual DbSet<DataFile> DataFiles { get; set; }
        public virtual DbSet<HomeName> HomeNames { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<InventoryVersion> InventoryVersions { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Products2> Products2 { get; set; }
        public virtual DbSet<SchemaObject> SchemaObjects { get; set; }
        public virtual DbSet<MetricAggregation> MetricAggregations { get; set; }
        public virtual DbSet<MetricPerTimeInterval> MetricPerTimeIntervals { get; set; }
        public virtual DbSet<ProcessorVelocityMap> ProcessorVelocityMaps { get; set; }
        public virtual DbSet<Statistic> Statistics { get; set; }
        public virtual DbSet<TimeInterval> TimeIntervals { get; set; }
        public virtual DbSet<MetricType> MetricTypes { get; set; }
        public virtual DbSet<RawMetric> RawMetrics { get; set; }
        public virtual DbSet<CandidateConnectionString> CandidateConnectionStrings { get; set; }
        public virtual DbSet<DataBaseFileGroup> DataBaseFileGroups { get; set; }
        public virtual DbSet<DataBaseProperty> DataBaseProperties { get; set; }
        public virtual DbSet<DatabasesCounter> DatabasesCounters { get; set; }
        public virtual DbSet<DataBaseServerConfiguration> DataBaseServerConfigurations { get; set; }
        public virtual DbSet<DataBaseServerProperty> DataBaseServerProperties { get; set; }
        public virtual DbSet<DataBaseUserGroup> DataBaseUserGroups { get; set; }
        public virtual DbSet<Inventory1> Inventory1 { get; set; }
        public virtual DbSet<SurveyHistory> SurveyHistories { get; set; }
        public virtual DbSet<SurveySummary> SurveySummaries { get; set; }
        public virtual DbSet<DnsConfiguration> DnsConfigurations { get; set; }
        public virtual DbSet<OsInfo> OsInfoes { get; set; }
        public virtual DbSet<AdDiscoveredUser> AdDiscoveredUsers { get; set; }
        public virtual DbSet<UsageTrackerSoftwareProduct> UsageTrackerSoftwareProducts { get; set; }
        public virtual DbSet<UsageDevice> UsageDevices { get; set; }
        public virtual DbSet<UsageDeviceSummary> UsageDeviceSummaries { get; set; }
        public virtual DbSet<UsageHour> UsageHours { get; set; }
        public virtual DbSet<UsageLogFile> UsageLogFiles { get; set; }
        public virtual DbSet<UsageLogFilesDevice> UsageLogFilesDevices { get; set; }
        public virtual DbSet<UsageRecordEvent> UsageRecordEvents { get; set; }
        public virtual DbSet<UsageRecord> UsageRecords { get; set; }
        public virtual DbSet<UsageServiceInstance> UsageServiceInstances { get; set; }
        public virtual DbSet<UsageUser> UsageUsers { get; set; }
        public virtual DbSet<ActiveSyncDevice> ActiveSyncDevices { get; set; }
        public virtual DbSet<AdActiveSync> AdActiveSyncs { get; set; }
        public virtual DbSet<AdElcFolder> AdElcFolders { get; set; }
        public virtual DbSet<AdForestDomain> AdForestDomains { get; set; }
        public virtual DbSet<AdMailboxDatabas> AdMailboxDatabases { get; set; }
        public virtual DbSet<AdMailboxItem> AdMailboxItems { get; set; }
        public virtual DbSet<AdPremiumJournaling> AdPremiumJournalings { get; set; }
        public virtual DbSet<AdRoot> AdRoots { get; set; }
        public virtual DbSet<AdServer> AdServers { get; set; }
        public virtual DbSet<ExchangeServer> ExchangeServers { get; set; }
        public virtual DbSet<MailboxContainer> MailboxContainers { get; set; }
        public virtual DbSet<PSMailboxItem> PSMailboxItems { get; set; }
        public virtual DbSet<ProductCode> ProductCodes { get; set; }
        public virtual DbSet<FepAccessLicense> FepAccessLicenses { get; set; }
        public virtual DbSet<CommonAreaPhone> CommonAreaPhones { get; set; }
        public virtual DbSet<FrontEndServer> FrontEndServers { get; set; }
        public virtual DbSet<Registrar> Registrars { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<ProviderLocation> ProviderLocations { get; set; }
        public virtual DbSet<SmsSite> SmsSites { get; set; }
        public virtual DbSet<SmsSiteSystemSummarizer> SmsSiteSystemSummarizers { get; set; }
        public virtual DbSet<SmsUTDevice> SmsUTDevices { get; set; }
        public virtual DbSet<SmsUTUser> SmsUTUsers { get; set; }
        public virtual DbSet<ConfigDbDsn> ConfigDbDsns { get; set; }
        public virtual DbSet<Edition> Editions { get; set; }
        public virtual DbSet<VdiConsolidationResult> VdiConsolidationResults { get; set; }
        public virtual DbSet<VdiWorkloadConsolidation> VdiWorkloadConsolidations { get; set; }
        public virtual DbSet<WindowsThinPcAssessment> WindowsThinPcAssessments { get; set; }
        public virtual DbSet<WorkLoadPerfMetric> WorkLoadPerfMetrics { get; set; }
        public virtual DbSet<WorkLoadProfile> WorkLoadProfiles { get; set; }
        public virtual DbSet<Assessment> Assessments { get; set; }
        public virtual DbSet<GuestUnique> GuestUniques { get; set; }
        public virtual DbSet<HostUnique> HostUniques { get; set; }
        public virtual DbSet<AboutInfo> AboutInfoes { get; set; }
        public virtual DbSet<Guest1> Guest1 { get; set; }
        public virtual DbSet<GuestFileinfo> GuestFileinfoes { get; set; }
        public virtual DbSet<Host1> Host1 { get; set; }
        public virtual DbSet<VCenter> VCenters { get; set; }
        public virtual DbSet<VirtualDisk> VirtualDisks { get; set; }
        public virtual DbSet<DeviceAssessment> DeviceAssessments { get; set; }
        public virtual DbSet<HardwareAssessment> HardwareAssessments { get; set; }
        public virtual DbSet<OsSkuInfo> OsSkuInfoes { get; set; }
        public virtual DbSet<WindowsInstalledSoftwareFull> WindowsInstalledSoftwareFulls { get; set; }
        public virtual DbSet<CdromDrives1> CdromDrives1 { get; set; }
        public virtual DbSet<ComputerSystemProduct> ComputerSystemProducts { get; set; }
        public virtual DbSet<DataFile1> DataFiles1 { get; set; }
        public virtual DbSet<DiskDrive> DiskDrives { get; set; }
        public virtual DbSet<InstalledDotnetFramework> InstalledDotnetFrameworks { get; set; }
        public virtual DbSet<LogicalDisk> LogicalDisks { get; set; }
        public virtual DbSet<MdtDeploymentTattoo> MdtDeploymentTattoos { get; set; }
        public virtual DbSet<NetworkAdapterConfiguration> NetworkAdapterConfigurations { get; set; }
        public virtual DbSet<NetworkAdapters1> NetworkAdapters1 { get; set; }
        public virtual DbSet<OemData> OemDatas { get; set; }
        public virtual DbSet<PhysicalMemory> PhysicalMemories { get; set; }
        public virtual DbSet<PnpEntity> PnpEntities { get; set; }
        public virtual DbSet<PowerShellComputerName> PowerShellComputerNames { get; set; }
        public virtual DbSet<Processors2> Processors2 { get; set; }
        public virtual DbSet<Products3> Products3 { get; set; }
        public virtual DbSet<ProductsSwid> ProductsSwids { get; set; }
        public virtual DbSet<ProductsUninstall> ProductsUninstalls { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<SoftwareLicensingProduct> SoftwareLicensingProducts { get; set; }
        public virtual DbSet<SoundDevice> SoundDevices { get; set; }
        public virtual DbSet<SspInstallTattoo> SspInstallTattoos { get; set; }
        public virtual DbSet<VideoController> VideoControllers { get; set; }
        public virtual DbSet<AssessmentSummary> AssessmentSummaries { get; set; }
        public virtual DbSet<LicensingAssessment> LicensingAssessments { get; set; }
        public virtual DbSet<Windows10Assessment> Windows10Assessment { get; set; }
        public virtual DbSet<Windows7Assessment> Windows7Assessment { get; set; }
        public virtual DbSet<Windows8Assessment> Windows8Assessment { get; set; }
        public virtual DbSet<AssessmentSummary1> AssessmentSummary1 { get; set; }
        public virtual DbSet<ServerAssessment> ServerAssessments { get; set; }
        public virtual DbSet<ServerRoleAssessment> ServerRoleAssessments { get; set; }
        public virtual DbSet<WS2008R2Assessment> WS2008R2Assessment { get; set; }
        public virtual DbSet<WS2012Assessment> WS2012Assessment { get; set; }
        public virtual DbSet<WS2012R2Assessment> WS2012R2Assessment { get; set; }
        public virtual DbSet<MSClusterAvailableDisk> MSClusterAvailableDisks { get; set; }
        public virtual DbSet<MSClusterCluster> MSClusterClusters { get; set; }
        public virtual DbSet<MSClusterClusterSharedVolume> MSClusterClusterSharedVolumes { get; set; }
        public virtual DbSet<MSClusterDisk> MSClusterDisks { get; set; }
        public virtual DbSet<MSClusterDiskPartition> MSClusterDiskPartitions { get; set; }
        public virtual DbSet<MSClusterNetwork> MSClusterNetworks { get; set; }
        public virtual DbSet<MSClusterNetworkInterface> MSClusterNetworkInterfaces { get; set; }
        public virtual DbSet<MSClusterNode> MSClusterNodes { get; set; }
        public virtual DbSet<MSClusterResource> MSClusterResources { get; set; }
        public virtual DbSet<MSClusterResourceGroup> MSClusterResourceGroups { get; set; }
        public virtual DbSet<MSClusterResourceType> MSClusterResourceTypes { get; set; }
        public virtual DbSet<MSClusterService> MSClusterServices { get; set; }
        public virtual DbSet<RDLicensingKeyPack> RDLicensingKeyPacks { get; set; }
        public virtual DbSet<RDLicensingServer> RDLicensingServers { get; set; }
        public virtual DbSet<RegistryAttribute> RegistryAttributes { get; set; }
        public virtual DbSet<ServerFeature> ServerFeatures { get; set; }
        public virtual DbSet<AppConfigSelectedConfig> AppConfigSelectedConfigs { get; set; }
        public virtual DbSet<DeviceHash> DeviceHashes { get; set; }
        public virtual DbSet<UniqueDevice> UniqueDevices { get; set; }
        public virtual DbSet<SqlInstance> SqlInstances { get; set; }
        public virtual DbSet<OsFamilyNameInfo> OsFamilyNameInfoes { get; set; }
        public virtual DbSet<UnixTypeInfo> UnixTypeInfoes { get; set; }
        public virtual DbSet<UsageTrackerFilterDate> UsageTrackerFilterDates { get; set; }
        public virtual DbSet<UsageLogSource> UsageLogSources { get; set; }
        public virtual DbSet<WorkLoadDevice> WorkLoadDevices { get; set; }
        public virtual DbSet<OsFamilyNameInfo1> OsFamilyNameInfo1 { get; set; }
        public virtual DbSet<OsVersionInfo> OsVersionInfoes { get; set; }
        public virtual DbSet<RDLicensingIssuedLicens> RDLicensingIssuedLicenses { get; set; }
    }
}
