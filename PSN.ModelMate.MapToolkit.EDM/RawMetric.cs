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
    
    public partial class RawMetric
    {
        public System.Guid DeviceNumber { get; set; }
        public System.DateTime CollectionDatetime { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<double> CpuPercentage { get; set; }
        public Nullable<double> MemoryUsedBytes { get; set; }
        public Nullable<double> MemoryAvailableBytes { get; set; }
        public Nullable<double> DiskTotalTransfersPerSec { get; set; }
        public Nullable<double> DiskWriteTransfersPerSec { get; set; }
        public Nullable<double> DiskReadTransfersPerSec { get; set; }
        public Nullable<double> DiskUsedBytes { get; set; }
        public Nullable<double> PercentageFreeSpace { get; set; }
        public Nullable<double> NetworkioTotalBytesPerSec { get; set; }
        public Nullable<double> NetworkioSentBytesPerSec { get; set; }
        public Nullable<double> NetworkioRecvBytesPerSec { get; set; }
        public Nullable<byte> SourceTypeId { get; set; }
        public Nullable<double> CpuUserPercentage { get; set; }
        public Nullable<double> MemoryPoolNonpagedBytes { get; set; }
        public Nullable<double> MemoryCacheBytes { get; set; }
        public Nullable<double> DiskBytesPerSec { get; set; }
        public Nullable<double> DiskQueueLength { get; set; }
        public Nullable<double> DiskReadQueueLength { get; set; }
        public Nullable<double> DiskWriteQueueLength { get; set; }
    
        public virtual Device Device { get; set; }
    }
}
