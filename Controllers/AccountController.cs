using MajesticAdminPanelTask.DataAccesLayer;
using MajesticAdminPanelTask.DataAccesLayer.Entities;
using MajesticAdminPanelTask.Models;
using MajesticAdminPanelTask.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MajesticAdminPanelTask.Controllers
{
    public class AccountController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EmailSender _emailSender;
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, EmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                ModelState.AddModelError("", "Bu adda istifadeci var");

                return View();
            }

            var createdUser = new AppUser
            {
                Fullname = model.Fullname,
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(createdUser, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View();

            }
            return RedirectToAction("index", "home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existUser = await _userManager.FindByNameAsync(model.Username);

            if (existUser == null)
            {
                ModelState.AddModelError("", "Information of username or password is not valid");

                return View();
            }

            //sign in managerin istifadesi password checking

            var result = await _signInManager.PasswordSignInAsync(existUser, model.Password, true, true);

            //lockedout

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "You are blocked with system");
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Information of username or password is not valid");

                return View();
            }


            return RedirectToAction("index", "home");

        }
        //logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        public IActionResult Forgetpassword()
        {
            return View();
        }
        //what is your problem
        //forgetpassword
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> ForgetPassword(ForgetViewModel model)

        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(model.Email))
                {

                    ModelState.AddModelError("", "Email address is required.");
                    return View(model);
                }

                var existUser = await _userManager.FindByEmailAsync(model.Email);

                if (existUser == null)
                {
                    ModelState.AddModelError("", "This email is not found with system");
                    return View(model);
                }

                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(existUser);

                var resetLink = Url.Action(nameof(ResetPassword), "Account", new { model.Email, resetToken }, Request.Scheme, Request.Host.ToString());

                //  return RedirectToAction(nameof(ResetPassword));
                //return View(nameof(EmailView), resetLink);

                _emailSender.SendEmail(model.Email, "Reset Password", $"Click <a href='{resetLink}'>here</a> to reset your password.");

                return View("Login");
            }
            return View(model);
        }


        public IActionResult EmailView()
        {
            return View();
        }

        public IActionResult ResetPassword(string email,string resetToken)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(resetToken))
                return BadRequest();

            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model, string email, string resetToken)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existUser = await _userManager.FindByEmailAsync(email);

            if (existUser == null) return BadRequest();

            var result = await _userManager.ResetPasswordAsync(existUser, resetToken, model.Password);


            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View();
            }


            return RedirectToAction(nameof(Login));
        }
    }
}

