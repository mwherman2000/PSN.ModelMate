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
    
    public partial class folders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public folders()
        {
            this.folder = new HashSet<folder>();
        }
    
        public int folders_Id { get; set; }
        public Nullable<int> tenant_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<folder> folder { get; set; }
        public virtual tenant tenant { get; set; }
    }
}
