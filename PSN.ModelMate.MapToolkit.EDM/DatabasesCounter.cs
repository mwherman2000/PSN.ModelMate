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
    
    public partial class DatabasesCounter
    {
        public System.Guid DeviceNumber { get; set; }
        public string Instance { get; set; }
        public string DatabaseName { get; set; }
        public Nullable<long> DataFilesSizeKB { get; set; }
        public Nullable<long> LogFilesSizeKB { get; set; }
        public Nullable<long> LogFilesUsedSizeKB { get; set; }
        public Nullable<long> PercentLogUsed { get; set; }
        public Nullable<int> CreateCollectorId { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public Nullable<int> UpdateCollectorId { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
    
        public virtual Device Device { get; set; }
    }
}