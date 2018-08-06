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
    
    public partial class DataBaseProperty
    {
        public System.Guid DeviceNumber { get; set; }
        public string ServerName { get; set; }
        public string DbName { get; set; }
        public string Size { get; set; }
        public string Owner { get; set; }
        public Nullable<int> Dbid { get; set; }
        public Nullable<System.DateTime> CreatedTimestamp { get; set; }
        public string Status { get; set; }
        public Nullable<int> CompatibilityLevel { get; set; }
        public Nullable<System.DateTime> LastBackupDate { get; set; }
        public Nullable<int> NumberTables { get; set; }
        public Nullable<int> NumberViews { get; set; }
        public Nullable<int> NumberSp { get; set; }
        public Nullable<int> NumberFunction { get; set; }
        public Nullable<int> CreateCollectorId { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public Nullable<int> UpdateCollectorId { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
    
        public virtual Device Device { get; set; }
    }
}