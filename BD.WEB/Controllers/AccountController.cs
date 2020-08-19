using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.DATA.Entity;
using BD.WEB.Models;
using BD.WEB.Models.ACCVM;
using BD.WEB.Service;
using DB.SERVICE;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace BD.WEB.Controllers
{
    public class AccountController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IBloodGroupService bloodGroupService;
        private ICityService cityService;

        public AccountController(IBloodGroupService bloodGroupService,ICityService cityService,RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.bloodGroupService = bloodGroupService;
            this.cityService = cityService;
        }
        // login start
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (signInManager.IsSignedIn(User))
            {

                return RedirectToAction("Index", "MyEscrow");

            }
            //returnUrl = returnUrl.Replace("%2F", "/");

            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);

            if (user != null)
            {

                var result = await signInManager.PasswordSignInAsync(user, login.Password, false, false);
                if (result.Succeeded)
                {
                    if (login.ReturnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(login.ReturnUrl);
                    }
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("IncorrectInput", "Email is not verified.");
                    return View(login);

                }
            }
            ModelState.AddModelError("IncorrectInput", "Username or Password is incorrect");
            return View(login);
        }

        ////login end

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            if (signInManager.IsSignedIn(User) && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Manage");
            }
            if (signInManager.IsSignedIn(User) && !User.IsInRole("Donor"))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            ViewBag.BloodGroup = new SelectList(bloodGroupService.GetBloodGroups().ToList(), "Id", "Name");
            ViewBag.City = new SelectList(cityService.GetCitys().ToList(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register, [FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration)
        {
            var isValid = IsValidEmail(register.Email);
            if (isValid == false)
            {
                ModelState.AddModelError("Email", "Email is not valid!");
            }
            else
            {
                var usercheck = await userManager.FindByEmailAsync(register.Email ?? "");
                if (usercheck != null)
                {
                    ModelState.AddModelError("Email", "Email is already exists!");
                }
                else
                {
                    ApplicationUser user = new ApplicationUser();
                    SetDataToApplicationUser(ref user, ref register);
                    var result = await userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Donor");
                        var result2 = await signInManager.PasswordSignInAsync(user, register.Password, false, false);
                        //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var linkas = Url.Action(nameof(VerifyEmail), "Account", new { userID = user.Id, code = token }, Request.Scheme, Request.Host.ToString());


                        //var title = "Blood Donation: Email confimation.";

                        //var mailbody = "Please click the link to verify your account " + linkas +"";
                        //emailSender.Post(
                        //   subject: title,
                        //   body: mailbody,
                        //   recipients: user.Email,
                        //   sender: configuration["AdminContact"]);
                        //return RedirectToAction(nameof(EmailVerification), user);
                        return Redirect("/Home/Index");       
                    }
                }
            }
            return View(register);
        }
        private object SetDataToApplicationUser(ref ApplicationUser user, ref RegisterViewModel register)
        {
            user.Email = register.Email;
            user.UserName = register.Email;
            user.Fullname = register.FullName;
            user.CityID = register.CityID;
            user.BloodGroupID = register.BloodGroupID;
            user.ContactNumber = register.COntactNumber;
            user.PhoneNumber = register.PhoneNumber;
            return user;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return View(usr);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Profiles()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return View(usr);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            PasswordVM model = new PasswordVM();
            var user = await userManager.GetUserAsync(HttpContext.User);
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            model.UserID = user.Id;
            model.Email = user.Email;
            model.BaseCode = token;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordConfirm(PasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserID);
                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    model.Errormessage = "Password did not match";
                    return View("ChangePassword", model);
                }
            }
            return View("ChangePassword", model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            ViewBag.BloodGroup = new SelectList(bloodGroupService.GetBloodGroups().ToList(), "Id", "Name");
            ViewBag.City = new SelectList(cityService.GetCitys().ToList(), "Id", "Name");
            ApplicationUser usr = await GetCurrentUserAsync();
            DonorVM model = new DonorVM();
            model.FullName = usr.Fullname;
            model.Address = usr.Address;
            model.Contact = usr.PhoneNumber;
            model.Contact2 = usr.ContactNumber;
            model.City =usr.CityID!=null? cityService.GetCity(usr.CityID.Value).Name :"Not set";
            model.BloodGroup = usr.BloodGroupID != null ? bloodGroupService.GetBloodGroup(usr.BloodGroupID.Value).Name : "Not set";
            model.BloodGroupID = usr.BloodGroupID != null ? usr.BloodGroupID.Value:0;
            model.CityID = usr.CityID != null ? usr.CityID.Value:0;
            return View(model);
        }
        [HttpGet]
        public IActionResult EmailVerification(ApplicationUser user)
        {
            return View(user);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfileConfirm(DonorVM model)
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            usr.Fullname = model.FullName;
            usr.Address = model.Address;
            usr.PhoneNumber = model.Contact;
            usr.ContactNumber = model.Contact2;
            usr.CityID = model.CityID;
            usr.BloodGroupID = model.BloodGroupID;
            var result = await userManager.UpdateAsync(usr);
            if (result.Succeeded)
            {
                ViewBag.message = "Successfully Updated";
            }
            return View(nameof(EditProfile));
        }
        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string userID, string code)
        {
            var user = await userManager.FindByIdAsync(userID);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return View();
            }
            return BadRequest();
        }


        [HttpGet]
        public IActionResult ForgotPassword(LoginViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordMail([FromServices] IEmailSender emailSender, [FromServices] IConfiguration configuration, LoginViewModel model)
        {
            var validatemail = IsValidEmail(model.Email);
            if (validatemail == false)
            {
                ModelState.AddModelError("Email", "Email is not valid");
            }
            else
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "There is no account assosiated with this email.");
                }
                else
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var link = Url.Action(nameof(ResetPassword), "Account", new { userID = user.Id, code = token }, Request.Scheme, Request.Host.ToString());
                    emailSender.Post(
                       subject: "Pay guardex: Reset Password",
                       body: $"<div><p>Please click on the link to reset your password.</p><br/><p>{link} </p><br/><p> or <button  class=\"btn btn-success\"><a href=\"{link}\">Click Here</a></button></p></div>",
                       recipients: user.Email,
                       sender: configuration["AdminContact"]);
                    return RedirectToAction(nameof(ResetMailSent));
                }
            }
            return View(nameof(ForgotPassword), model);
        }
        [HttpGet]
        public IActionResult ResetMailSent()
        {
            return View();
        }
        public IActionResult ResetPassword(string userID, string code)
        {
            PasswordVM recover = new PasswordVM();
            recover.UserID = userID;
            recover.BaseCode = code;
            return View(recover);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(PasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserID);
                var result = await userManager.ResetPasswordAsync(user, model.BaseCode, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                }
                return RedirectToAction(nameof(Profile));
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (signInManager.IsSignedIn(User))
            {
                await signInManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

