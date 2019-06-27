using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TchatAgileNoSQL
{
    public class TchatManager
    {

        public void GetId()
        {
            //using(TchatNoSQLEntities db = new TchatNoSQLEntities())
            //{
            //    return db.UserClassique.
            //        Where(x => x.)
            //}
        }

        public UserDTO Login(string email, string password)
        {
            using(TchatNoSQLEntities db = new TchatNoSQLEntities())
            {
                var user = db.UserClassique
                    .Where(x => x.email_usercl == email && x.password_usercl == password)
                    .Select(x => new UserDTO
                    {
                        id_usercl = x.id_usercl,
                        password_usercl = x.password_usercl,
                        email_usercl = x.email_usercl,
                        avatar_usercl = x.avatar_usercl,
                        dateinscription_usercl = x.dateinscription_usercl,
                        datenaissance_usercl = x.datenaissance_usercl,
                        langue_usercl = x.langue_usercl,
                        nom_usercl = x.nom_usercl,
                        pays_usercl = x.pays_usercl,
                        prenom_usercl = x.prenom_usercl,
                        pseudo_usercl = x.pseudo_usercl
                    }).Single();

                return user;
            }
        }


        public List<UserDTO> GetAmis(int id)
        {
            List<UserDTO> contacts = new List<UserDTO>();

            using(TchatNoSQLEntities db = new TchatNoSQLEntities())
            {
                //On récupère les id de tous ceux amis avec l'utilisateur, dans une liste
                var idContacts = db.Contacts
                    .Where(x => x.id_accepteur_contact == id || x.id_demandeur_contact == id)
                    .SelectMany(x => new[] { x.id_accepteur_contact, x.id_demandeur_contact })
                    .ToList();

                // On supprime l'id utilisateur, dans cette liste d'id.
                idContacts.RemoveAll(x => x == id);
                // Distinct pour que chaque id soit unique
                idContacts.Distinct();

                foreach (var idContact in idContacts)
                {
                    var contact = db.UserClassique
                        .Where(x => x.id_usercl == idContact)
                        .Select(x => new UserDTO
                        {
                            id_usercl = x.id_usercl,
                            avatar_usercl = x.avatar_usercl,
                            dateinscription_usercl = x.dateinscription_usercl,
                            password_usercl = x.password_usercl,
                            email_usercl = x.email_usercl,
                            datenaissance_usercl = x.datenaissance_usercl,
                            langue_usercl = x.langue_usercl,
                            nom_usercl = x.nom_usercl,
                            pays_usercl = x.pays_usercl,
                            prenom_usercl = x.prenom_usercl,
                            pseudo_usercl = x.pseudo_usercl
                        }).Single();

                    contacts.Add(contact);
                }

                return contacts;

            }
        }

        public UserClassique Register(string email, string mdp)
        {
            using (TchatNoSQLEntities db = new TchatNoSQLEntities())
            {

                var user = new UserClassique()
                {
                    email_usercl = email,
                    password_usercl = mdp
                };

                db.UserClassique.Add(user);
                db.SaveChanges();

                return user;
            }
        }

    }
}