using Company.DAL.Contexts;
using Company.DAL.Entities;
using Company.Presentation_Layer.Helper;
using Company.Presentation_Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.Presentation_Layer.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() // manual mapping 
                {
                    UserName = model.Email.Split('@')[0], 
                    Email = model.Email,
                    IsAgree = model.IsAgree,

                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var error in result.Errors) 
                    ModelState.AddModelError( string.Empty, error.Description);
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email); 
                if (user != null)
                {
                    var password = await UserManager.CheckPasswordAsync(user, model.Password);
                    if (password) 
                    {
                        var Result = await SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (Result.Succeeded)
                            return RedirectToAction("Index", "Home"); 
                    }
                }
            }
            return View(model);
        }

        public async new Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null) 
                {
                    var Token = await UserManager.GeneratePasswordResetTokenAsync(user); 
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email= user.Email , Token = Token  } , Request.Scheme);
                    var Email = new Email()
                    {
                        Title = "ResetPassword",
                        To = model.Email,
                        Body = passwordResetLink
                    };
                    EmailSetting.SendEmail(Email); // send mail
                    return RedirectToAction(nameof(CompleteForgetPassword));
                }
                ModelState.AddModelError(string.Empty, "This Email Is Not Existed");
            }
            return View(model);
        }
        public IActionResult CompleteForgetPassword()
        {
            return View();
        }
        public IActionResult ResetPassword( string email , string token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model) 
        {
            var user = await UserManager.FindByEmailAsync(model.Email); 
            if(ModelState.IsValid)
            {
                if (user != null)
                {
                    var result = await UserManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));
                    else
                    {
                        foreach (var error in result.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View(model);
        }
    }
}
