using Microsoft.AspNetCore.Mvc;
using QnA_app.Models;

namespace QnA_app.Controllers
{
    public class AuthController : Controller
    {
        public QnA_DBContext Context { get; }

        public AuthController(QnA_DBContext context)
        {
            Context = context;
        }

        public IActionResult Login_Signup()
        {

            return View();
        }


      //SIGN UP >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public IActionResult Signup(SignupTbl userdata)
        {
            if (ModelState.IsValid){
                var isCreated=Context.SignupTbls.FirstOrDefault(item=>item.Username ==  userdata.Username);
                if (isCreated == null)
                {
                    Context.SignupTbls.Add(userdata);
                    Context.SaveChanges();
                    HttpContext.Session.SetString("username", userdata.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "username is already taken");
                }
            }
            return RedirectToAction("Login_Signup", "Auth");
        }


        //SIGN IN >>>>>>>>>>>>>>>>>>>>>

        public IActionResult Signin(SigninModel data)
        {
            if (ModelState.IsValid)
            {
                var isUser = Context.SignupTbls.FirstOrDefault
                    (item => item.Username == data.Username && item.Password == data.Password);
                if (isUser == null)
                {
                    ModelState.AddModelError(string.Empty, "user name or password not found");
                }
                else
                {
                    HttpContext.Session.SetString("username", data.Username);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login_Signup", "Auth");
        }
        public IActionResult Signout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login_Signup", "Auth");
        }

    }
}
