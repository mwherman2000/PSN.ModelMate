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
    
    public partial class CdromDrives1
    {
        public System.Guid DeviceNumber { get; set; }
        public System.Guid Uid { get; set; }
        public Nullable<int> Availability { get; set; }
        public string Capabilities { get; set; }
        public string CapabilityDescriptions { get; set; }
        public string Caption { get; set; }
        public string CompressionMethod { get; set; }
        public Nullable<int> CreateCollectorId { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public Nullable<long> DefaultBlockSize { get; set; }
        public string Description { get; set; }
        public string DeviceId { get; set; }
        public string Drive { get; set; }
        public Nullable<byte> DriveIntegrity { get; set; }
        public Nullable<long> FilesystemFlagsex { get; set; }
        public string Id { get; set; }
        public Nullable<System.DateTime> InstallDate { get; set; }
        public string Manufacturer { get; set; }
        public Nullable<long> MaxBlockSize { get; set; }
        public Nullable<long> MaxMediaSize { get; set; }
        public Nullable<long> MaximumComponentLength { get; set; }
        public string MediaType { get; set; }
        public string MfrAssignedRevisionLevel { get; set; }
        public Nullable<long> MinBlockSize { get; set; }
        public string Name { get; set; }
        public Nullable<byte> NeedsCleaning { get; set; }
        public Nullable<long> NumberOfMediaSupported { get; set; }
        public string PnpDeviceId { get; set; }
        public string RevisionLevel { get; set; }
        public Nullable<long> ScsiBus { get; set; }
        public Nullable<int> ScsiLogicalUnit { get; set; }
        public Nullable<int> ScsiPort { get; set; }
        public Nullable<int> ScsiTargetId { get; set; }
        public Nullable<long> Size { get; set; }
        public Nullable<double> TransferRate { get; set; }
        public Nullable<int> UpdateCollectorId { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
        public string VolumeName { get; set; }
        public string VolumeSerialNumber { get; set; }
    
        public virtual Device Device { get; set; }
    }
}