using LogInSystem.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static LogInSystem.Models.AccountModel;

namespace LogInSystem.Controllers
{
    public class WellcomController : Controller
    {
        public async Task<ActionResult> Index(LoginViewModel model)
        {
            if(Session["Email"] != null)
            {
                // Old user
                string email = (string)Session["Email"];

                string query = "SELECT * FROM Account WHERE Email=@Email";
                string[] key = { "Email" };
                dynamic[] values = { email };

                DataTable dt = await Task.Run(() => Queries.SELECT(query, key, values));
                User user = new User()
                {
                    FirstName = dt.Rows[0].ItemArray[3].ToString(),
                    LastName = dt.Rows[0].ItemArray[4].ToString()
                };
                return View(user);
            }
            return (RedirectToAction("Index", "SignIn"));
        }

        [HttpPost]
        public ActionResult LogOut(LoginViewModel model)
        {
            Session.Abandon();
            return (RedirectToAction("Index", "SignIn"));
        }
    }
}