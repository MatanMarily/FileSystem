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

                    if (await Task.Run(() => Queries.ExistParameters(query, key, valus)))
                    {
                        ViewData["Signin"] = true;
                        ViewData["success"] = "Sign in successfully";
                        return View("Index");
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