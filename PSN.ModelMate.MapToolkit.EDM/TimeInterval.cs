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
    
    public partial class TimeInterval
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeInterval()
        {
            this.MetricPerTimeIntervals = new HashSet<MetricPerTimeInterval>();
        }
    
        public int TimeInterval1 { get; set; }
        public System.DateTime StartDatetime { get; set; }
        public System.DateTime EndDatetime { get; set; }
        public int DateIndex { get; set; }
        public int HourIndex { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetricPerTimeInterval> MetricPerTimeIntervals { get; set; }
    }
}