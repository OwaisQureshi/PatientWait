//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webmap.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DOCTOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOCTOR()
        {
            this.DOCTORSUSERRATINGS = new HashSet<DOCTORSUSERRATING>();
        }
    
        public int DOCTORID { get; set; }
        public string DOCTORNAME { get; set; }
        public string DOCTORADDRESS { get; set; }
        public string DOCTOREmail { get; set; }
        public string DOCTORTelePhone { get; set; }
        public string DOCTORWorkingHours { get; set; }
        public Nullable<int> ClinicID { get; set; }
        public string CLINICGoogleUID { get; set; }
        public string ClinicName { get; set; }
        public Nullable<int> USERID { get; set; }
        public string USERNAME { get; set; }
        public System.DateTime USERSearchDATETIME { get; set; }
    
        public virtual CLINIC CLINIC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCTORSUSERRATING> DOCTORSUSERRATINGS { get; set; }
    }
}
