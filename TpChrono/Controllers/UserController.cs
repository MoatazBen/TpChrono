using Microsoft.AspNetCore.Mvc;
using TpChrono.Models;
using TpChrono.Models.ModelViews;

namespace TpChrono.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Register(UserRegisterModelView mv)
        {
            if (ModelState.IsValid)
            {
                User u = new User();
                Register(mv);
                
                return RedirectToAction("Login");
            }
           
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginModelView mv)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Login");

        }
      

    }
}
 