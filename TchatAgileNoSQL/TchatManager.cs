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

        public UserClassique Login(string email, string password)
        {
            using(TchatNoSQLEntities db = new TchatNoSQLEntities())
            {
                return db.UserClassique
                    .Where(x => x.email_user == email && x.password_usercl == password)
                    .Select(x => new UserClassique
                    {
                        id_user = x.id_user,
                        avatar_usercl = x.avatar_usercl,
                        dateinscription_usercl = x.dateinscription_usercl,
                        password_usercl = x.password_usercl,
                        email_user = x.email_user,
                        datenaissance_usercl = x.datenaissance_usercl,
                        isActif_user = x.isActif_user,
                        langue_usercl = x.langue_usercl,
                        nom_usercl = x.nom_usercl,
                        pays_usercl = x.pays_usercl,
                        prenom_usercl = x.prenom_usercl,
                        pseudo_usercl = x.pseudo_usercl
                    }).Single();
            }
        }

        public List<UserClassique> GetAmis(int id)
        {
            List<UserClassique> contacts = new List<UserClassique>();

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
                        .Where(x => x.id_user == idContact)
                        .Select(x => new UserClassique
                        {
                            id_user = x.id_user,
                            avatar_usercl = x.avatar_usercl,
                            dateinscription_usercl = x.dateinscription_usercl,
                            password_usercl = x.password_usercl,
                            email_user = x.email_user,
                            datenaissance_usercl = x.datenaissance_usercl,
                            isActif_user = x.isActif_user,
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

    }
}