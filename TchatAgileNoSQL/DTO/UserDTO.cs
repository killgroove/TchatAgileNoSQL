using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TchatAgileNoSQL
{
    public class UserDTO
    {
        public int id_usercl { get; set; }
        public string email_usercl { get; set; }
        public string pseudo_usercl { get; set; }
        public string password_usercl { get; set; }
        public string nom_usercl { get; set; }
        public string prenom_usercl { get; set; }
        public System.DateTime datenaissance_usercl { get; set; }
        public string pays_usercl { get; set; }
        public string langue_usercl { get; set; }
        public System.DateTime dateinscription_usercl { get; set; }
        public byte[] avatar_usercl { get; set; }
    }
}