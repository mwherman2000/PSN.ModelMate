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
    
    public partial class InfraHost
    {
        public System.Guid ResultId { get; set; }
        public System.Guid InfraNumber { get; set; }
        public System.Guid HostNumber { get; set; }
    
        public virtual Host Host { get; set; }
        public virtual Infra Infra { get; set; }
        public virtual Result Result { get; set; }
    }
}