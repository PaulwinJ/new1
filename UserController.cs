using System.Web.Mvc;
using crud.DAL;
using crud.Models;

namespace crud.Controllers
{
    public class UserController : Controller
    {
        User_DAL _userDAL = new User_DAL();

        // GET: User/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = _userDAL.RegisterUser(model.Username, model.Password, model.Email, model.Gender, model.Country);

                if (isRegistered)
                {
                    TempData["SuccessMessage"] = "Registration successful.";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["ErrorMessage"] = "Registration failed.";
                }
            }
            return View(model);
        }

        // GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            bool isAuthenticated = _userDAL.AuthenticateUser(model.Username, model.Password);

            if (isAuthenticated)
            {
                Session["Username"] = model.Username;
                return RedirectToAction("Index", "Product");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid username or password.";
            }
            return View(model);
        }



        // GET: User/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
