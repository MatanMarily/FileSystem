using LogInSystem.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static LogInSystem.Models.AccountModel;

namespace LogInSystem.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string query = "Select * from Account where Email= @Email";
                if (await Task.Run(() => !Queries.Exist(query, "Email", model.Email)))
                {
                    query = "INSERT INTO Account(Email, Password, FirstName, LastName, Phone)" +
                                   "VALUES (@Email, @Password, @FirstName, @LastName, @Phone)";

                    string[] key = { "Email", "Password", "FirstName", "LastName", "Phone" };
                    dynamic[] values = { model.Email, model.Password, model.FirstName, model.LastName, model.Phone };

                    bool result = await Task.Run( () => Queries.INSERT_UPDATE_DELETE(query, key, values));
                    if (result)
                    {
                        ViewData["InsertDB"] = true;
                        ViewData["success"] = "Register complete successfully";
                        return View("Index");
                    }
                    else
                    {
                        ViewData["InsertDB"] = false;
                        ViewData["error"] = "Register are not complete successfully";
                        return View("Index");
                    }
                }
                else //Email are exist!
                {
                    ViewData["InsertDB"] = false;
                    ViewData["error"] = "Email are exist";
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        }
    }
}