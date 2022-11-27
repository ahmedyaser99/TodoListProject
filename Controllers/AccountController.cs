using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoProject.Models;
using ToDoProject.ViewModel;

namespace ToDoProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }



        //action to open registration link
        public IActionResult Registration()
        {
            return View();
        }
        // action form ==> Database
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterAccountViewModel newAccount)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = newAccount.Username;
                user.Email = newAccount.Email;
                //save & create cookie
                IdentityResult result = await userManager.CreateAsync(user, newAccount.Password);
                if (result.Succeeded)
                {//create cookie
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(newAccount);
        }
        public IActionResult AddAdmin()
        {
            return View("Registration");
        }
        // action form ==> Database
        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterAccountViewModel newAccount)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = newAccount.Username;
                user.Email = newAccount.Email;
                //save & create cookie
                IdentityResult result = await userManager.CreateAsync(user, newAccount.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    //create cookie
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View("Registration", newAccount);
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = await userManager.FindByNameAsync(loginUser.UserName);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await signInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.RememberMe, false);
                    if (result.Succeeded) { return RedirectToAction("Index", "Home"); }
                    else ModelState.AddModelError("", "Incorrect UserName Or PassWord");
                }
                else { ModelState.AddModelError("", "Invalid UserName,PassWord"); }
            }
            return View(loginUser);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

