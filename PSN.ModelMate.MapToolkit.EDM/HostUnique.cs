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
    
    public partial class HostUnique
    {
        public System.Guid DeviceNumber { get; set; }
        public string MobRef { get; set; }
        public Nullable<System.Guid> HostDeviceNumber { get; set; }
    
        public virtual Host1 Host1 { get; set; }
    }
}