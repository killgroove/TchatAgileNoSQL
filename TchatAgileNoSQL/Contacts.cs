//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TchatAgileNoSQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contacts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contacts()
        {
            this.UserClassique = new HashSet<UserClassique>();
        }
    
        public int id_contact { get; set; }
        public int id_demandeur_contact { get; set; }
        public int id_accepteur_contact { get; set; }
        public System.DateTime date_demande_contact { get; set; }
        public System.DateTime date_acceptation_contact { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserClassique> UserClassique { get; set; }
    }
}
