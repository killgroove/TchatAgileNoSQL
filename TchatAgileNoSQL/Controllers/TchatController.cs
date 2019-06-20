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
            UserClassique user = tchatManager.Login(email, password);
            if (user.id_user > 0)
            {
                ViewData["status"] = 1;
                ViewData["msg"] = "login Successful...";
                Session["username"] = user.email_user;
                Session["userid"] = user.id_user.ToString();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["status"] = 2;
                ViewData["msg"] = "invalid Email or Password...";
                return View();
            }
        }

        [HttpPost]
        public JsonResult friendlist()
        {
            int id = Convert.ToInt32(Session["userid"].ToString());
            List<UserClassique> amis = tchatManager.GetAmis(id);
            List<ListItem> userlist = new List<ListItem>();
            foreach (var ami in amis)
            {
                userlist.Add(new ListItem
                {
                    Value = ami.pseudo_usercl.ToString(),
                    Text = ami.email_user.ToString()
                });
            }
            return Json(userlist);
        }


    }
}