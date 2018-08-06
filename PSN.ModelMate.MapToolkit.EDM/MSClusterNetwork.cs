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
    
    public partial class MSClusterNetwork
    {
        public System.Guid DeviceNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string AddressMask { get; set; }
        public Nullable<bool> AutoMetric { get; set; }
        public string Caption { get; set; }
        public Nullable<long> Characteristics { get; set; }
        public string Description { get; set; }
        public Nullable<long> Flags { get; set; }
        public Nullable<System.DateTime> InstallDate { get; set; }
        public string IPv4Addresses { get; set; }
        public string IPv4PrefixLengths { get; set; }
        public string IPv6Addresses { get; set; }
        public string IPv6PrefixLengths { get; set; }
        public Nullable<long> Metric { get; set; }
        public Nullable<long> Role { get; set; }
        public Nullable<long> State { get; set; }
        public string Status { get; set; }
        public Nullable<int> CreateCollectorId { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public Nullable<int> UpdateCollectorId { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
    
        public virtual Device Device { get; set; }
    }
}