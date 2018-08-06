//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Host1
    {
        public System.Guid DeviceNumber { get; set; }
        public string MobRef { get; set; }
        public string SourceApiType { get; set; }
        public string VmwareName { get; set; }
        public Nullable<int> HardwareCpuMhz { get; set; }
        public string HardwareCpuModel { get; set; }
        public Nullable<long> HardwareMemorySize { get; set; }
        public string HardwareModel { get; set; }
        public Nullable<int> HardwareNumCpuCores { get; set; }
        public Nullable<int> HardwareNumCpuPkgs { get; set; }
        public Nullable<int> HardwareNumCpuThreads { get; set; }
        public string HardwareVendor { get; set; }
        public string DnsConfigDomainName { get; set; }
        public string DnsConfigHostName { get; set; }
        public string ProductApiType { get; set; }
        public string ProductFullName { get; set; }
        public string ProductName { get; set; }
        public string ProductOsType { get; set; }
        public string ProductProductLineId { get; set; }
        public string ProductVersion { get; set; }
        public Nullable<int> CreateCollectorId { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public Nullable<int> UpdateCollectorId { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
    
        public virtual Device Device { get; set; }
        public virtual HostUnique HostUnique { get; set; }
    }
}