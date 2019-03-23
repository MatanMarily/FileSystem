using LogInSystem.DB;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using static LogInSystem.Models.AccountModel;

namespace LogInSystem.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                string query = "Select * from Account where Email= @Email";
                if (await Task.Run(() => Queries.Exist(query, "Email", model.Email)))
                {
                    // Email exist
                    MailAddress mailfrom = new MailAddress("ResetPassword@gmail.com");
                    MailAddress mailto = new MailAddress(model.Email);
                    MailMessage newmsg = new MailMessage(mailfrom, mailto);

                    string patemporaryPassword = RandomString(6);
                    newmsg.Subject = "Reset Password";
                    newmsg.Body = "Your password is: " + patemporaryPassword;

                    SmtpClient smtps = new SmtpClient("smtp.gmail.com", 587);
                    smtps.UseDefaultCredentials = false;
                    smtps.Credentials = new NetworkCredential("testforgetprojectma@gmail.com", "123456Matan");
                    smtps.EnableSsl = true;

                    try
                    {
                        smtps.Send(newmsg);
                        query = "UPDATE Account set Password=@Password where Email=@Email";
                        string[] key = { "Password", "Email" };
                        dynamic[] values = { patemporaryPassword, model.Email };
                        bool result = await Task.Run(() => Queries.INSERT_UPDATE_DELETE(query, key, values));
                        if (result)
                        {
                            ViewData["ForgotPassword"] = true;
                            ViewData["success"] = "Password are reset successfully";
                            return View("Index");
                        }
                    }
                    catch
                    {
                        ViewData["ForgotPassword"] = false;
                        ViewData["error"] = "Not successfully";
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
            return View("Index"); 
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}