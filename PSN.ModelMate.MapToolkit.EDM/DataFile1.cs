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
    
    public partial class DataFile1
    {
        public System.Guid DeviceNumber { get; set; }
        public System.Guid Uid { get; set; }
        public Nullable<long> AccessMask { get; set; }
        public Nullable<bool> Archive { get; set; }
        public string Caption { get; set; }
        public Nullable<bool> Compressed { get; set; }
        public string CompressionMethod { get; set; }
        public Nullable<int> CreateCollectorId { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string Description { get; set; }
        public string Drive { get; set; }
        public string EightDotThreeFileName { get; set; }
        public Nullable<bool> Encrypted { get; set; }
        public string EncryptionMethod { get; set; }
        public string Extension { get; set; }
        public string FileName { get; set; }
        public Nullable<long> FileSize { get; set; }
        public string FileType { get; set; }
        public string FileSystemName { get; set; }
        public Nullable<bool> Hidden { get; set; }
        public Nullable<System.DateTime> InstallDate { get; set; }
        public Nullable<long> InUseCount { get; set; }
        public Nullable<System.DateTime> LastAccessed { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Nullable<bool> Readable { get; set; }
        public string Status { get; set; }
        public Nullable<bool> System { get; set; }
        public string Version { get; set; }
        public Nullable<int> UpdateCollectorId { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
        public Nullable<bool> Writeable { get; set; }
    
        public virtual Device Device { get; set; }
    }
}