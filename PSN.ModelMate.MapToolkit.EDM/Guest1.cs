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
    
    public partial class Guest1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Guest1()
        {
            this.GuestFileinfoes = new HashSet<GuestFileinfo>();
            this.VirtualDisks = new HashSet<VirtualDisk>();
        }
    
        public System.Guid DeviceNumber { get; set; }
        public string MobRef { get; set; }
        public string SourceApiType { get; set; }
        public string VmwareName { get; set; }
        public string GuestGuestFamily { get; set; }
        public string GuestGuestFullName { get; set; }
        public string GuestGuestId { get; set; }
        public string GuestGuestState { get; set; }
        public string GuestHostName { get; set; }
        public string GuestIpAddress { get; set; }
        public string GuestToolsStatus { get; set; }
        public Nullable<int> GuestMemorySize { get; set; }
        public Nullable<int> GuestNumCpus { get; set; }
        public Nullable<int> GuestNumEthernetCards { get; set; }
        public Nullable<int> GuestNumVirtualDisks { get; set; }
        public string GuestToolsRunningStatus { get; set; }
        public string GuestToolsVersion { get; set; }
        public string GuestToolsVersionStatus { get; set; }
        public Nullable<System.DateTime> RuntimeBootTime { get; set; }
        public string RuntimeHost { get; set; }
        public string RuntimePowerState { get; set; }
        public string RuntimeFaultToleranceState { get; set; }
        public Nullable<bool> ConfigChangeTrackingEnabled { get; set; }
        public string ConfigGuestFullName { get; set; }
        public Nullable<bool> VmwareSnapshot { get; set; }
        public Nullable<int> CreateCollectorId { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public Nullable<int> UpdateCollectorId { get; set; }
        public Nullable<System.DateTime> UpdateDatetime { get; set; }
    
        public virtual Device Device { get; set; }
        public virtual Assessment Assessment { get; set; }
        public virtual GuestUnique GuestUnique { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuestFileinfo> GuestFileinfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VirtualDisk> VirtualDisks { get; set; }
    }
}
