using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using spalpuGitHub.Models;

namespace spalpuGitHub.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Action()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount model)
        {
            if (ModelState.IsValid)
            {
                UserAccount user = null;
                using (UserDbContext db = new UserDbContext())
                {
                    user = db.UserAccounts.FirstOrDefault(u => u.Email == model.Email);

                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (UserDbContext db = new UserDbContext())
                    {
                        db.UserAccounts.Add(new UserAccount() { Id = Guid.NewGuid().ToString(), UserName = model.UserName, Password = model.Password, Email = model.Email, ConfirmEmail = false, Tasks = null });
                        db.SaveChanges();

                        user = db.UserAccounts.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    }
                }

            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccount model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                UserAccount user = null;
                using (UserDbContext db = new UserDbContext())
                {
                    user = db.UserAccounts.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}