//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PSN.ModelMate.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class bendpoint
    {
        public long x { get; set; }
        public long y { get; set; }
        public Nullable<long> z { get; set; }
        public string operation { get; set; }
        public Nullable<int> connection_Id { get; set; }
        public int bendpoint_Id { get; set; }
    
        public virtual connection connection { get; set; }
    }
}
