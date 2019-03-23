using LogInSystem.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static LogInSystem.Models.AccountModel;

namespace LogInSystem.Controllers
{
    public class SignInController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string query = "Select * from Account where Email= @Email";
                if (await Task.Run(() => Queries.Exist(query, "Email", model.Email)))
                {
                    query = "Select * from Account where Email= @Email AND Password= @Password";
                    string[] key = { "Email", "Password" };
                    string[] valus = { model.Email, model.Password };

                    if (model.RememberMe)
                    {
                        HttpCookie cookie = new HttpCookie("YourAppLogin");
                        cookie.Values.Add("email", model.Email);
                        cookie.Values.Add("password", model.Password);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(cookie);
                    }

                    if (await Task.Run(() => Queries.ExistParameters(query, key, valus)))
                    {
                        Session["Email"] = model.Email;
                        return RedirectToAction("Index", "Wellcom", model);
                    }
                    else
                    {
                        ViewData["Signin"] = false;
                        ViewData["error"] = "Password are not exist";
                        return View("Index");
                    }
                }
                else
                {
                    ViewData["Signin"] = false;
                    ViewData["error"] = "Email are not exist";
                    return View("Index");
                }
            }
            return View(model);
        }
    }
}