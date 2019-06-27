using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TchatAgileNoSQL.Models.HelperBLL;

namespace TchatAgileNoSQL.Controllers
{
    public class TchatController : Controller
    {
        TchatManager tchatManager = new TchatManager();
        // GET: Tchat
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                // Gérer la connexion en anonyme
                Session["UserID"] = "69004";
                Session["username"] = "alex69004";
                return View(); 
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult sendmsg(string message, string friend)
        {
            RabbitMQBLL obj = new RabbitMQBLL();
            IConnection con = obj.GetConnection();
            bool flag = obj.send(con, message, friend);
            return Json(null);
        }

        [HttpPost]
        public JsonResult receive()
        {
            try
            {
                RabbitMQBLL obj = new RabbitMQBLL();
                IConnection con = obj.GetConnection();
                string userqueue = Session["username"].ToString();
                string message = obj.receive(con, userqueue);
                return Json(message);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(FormCollection fc)
        {
            string email = fc["txtemail"].ToString();
            string password = fc["txtpassword"].ToString();
            var user = tchatManager.Login(email, password);
            if (user.id_usercl > 0)
            {
                ViewData["status"] = 1;
                ViewData["msg"] = "Connexion réussie...";
                Session["username"] = user.email_usercl;
                Session["userid"] = user.id_usercl.ToString();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["status"] = 2;
                ViewData["msg"] = "Email ou mot de passe invalie...";
                return View();
            }
        }

        [HttpPost]
        public JsonResult friendlist()
        {
            int id = Convert.ToInt32(Session["userid"].ToString());
            var amis = tchatManager.GetAmis(id);
            List<ListItem> userlist = new List<ListItem>();
            foreach (var ami in amis)
            {
                userlist.Add(new ListItem
                {
                    Value = ami.email_usercl.ToString(),
                    Text = ami.email_usercl.ToString()
                });
            }
            return Json(userlist);
        }


    }
}