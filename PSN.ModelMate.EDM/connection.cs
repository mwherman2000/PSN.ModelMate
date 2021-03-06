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
    
    public partial class connection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public connection()
        {
            this.bendpoint = new HashSet<bendpoint>();
            this.documentation = new HashSet<documentation>();
            this.label = new HashSet<label>();
            this.properties = new HashSet<properties>();
            this.style = new HashSet<style>();
        }
    
        public string identifier { get; set; }
        public string relationshipref { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public string operation { get; set; }
        public int connection_Id { get; set; }
        public Nullable<int> view_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bendpoint> bendpoint { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<documentation> documentation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<label> label { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<properties> properties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<style> style { get; set; }
        public virtual view view { get; set; }
    }
}
