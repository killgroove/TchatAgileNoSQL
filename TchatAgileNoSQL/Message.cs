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
    
    public partial class Message
    {
        public int id_message { get; set; }
        public string contenu_message { get; set; }
        public int fk_id_user_message { get; set; }
        public System.DateTime datetime_message { get; set; }
        public Nullable<int> id_usercl { get; set; }
        public int id_salon { get; set; }
        public Nullable<int> id_anonyme { get; set; }
    
        public virtual Anonyme Anonyme { get; set; }
        public virtual SalonTchat SalonTchat { get; set; }
        public virtual UserClassique UserClassique { get; set; }
    }
}
