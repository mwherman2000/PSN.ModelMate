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
    
    public partial class AdDiscoveredUser
    {
        public System.Guid DeviceNumber { get; set; }
        public string ObjectSid { get; set; }
        public Nullable<System.DateTime> AccountExpires { get; set; }
        public string Cn { get; set; }
        public string DisplayName { get; set; }
        public string DistinguishedName { get; set; }
        public string GivenName { get; set; }
        public Nullable<System.DateTime> LastLogon { get; set; }
        public Nullable<System.DateTime> LastLogonTimestamp { get; set; }
        public Nullable<int> LogonCount { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }
        public string NetBIOsDomain { get; set; }
        public Nullable<System.Guid> ObjectGuid { get; set; }
        public Nullable<int> PrimaryGroupID { get; set; }
        public Nullable<System.DateTime> PwdLastSet { get; set; }
        public string SAMAccountName { get; set; }
        public Nullable<int> SAMAccountType { get; set; }
        public string Sn { get; set; }
        public Nullable<int> UserAccountControl { get; set; }
        public Nullable<System.DateTime> WhenChanged { get; set; }
        public Nullable<System.DateTime> WhenCreated { get; set; }
        public int IsDisabled { get; set; }
        public int IsNormalAccount { get; set; }
        public string LoginName { get; set; }
        public Nullable<int> CreateCollectorId { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public Nullable<int> UpdateCollectorId { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
    
        public virtual Device Device { get; set; }
    }
}
