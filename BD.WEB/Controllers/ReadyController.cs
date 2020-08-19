using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.DATA.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BD.WEB.Controllers
{
    public class ReadyController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public ReadyController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (roleManager.Roles.Count() == 0)
            {
                IdentityRole model = new IdentityRole();
                IdentityRole model1 = new IdentityRole();
                model.Name = "Admin";
                model1.Name = "Donor";
                await roleManager.CreateAsync(model);
                await roleManager.CreateAsync(model1);
            }
            var check = userManager.FindByEmailAsync("asif.shikder.ar@gmail.com");
            if (check.Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.PhoneNumber = "01743881164";
                user.UserName = "AsifShikderAR";
                user.Email = "asif.shikder.ar@gmail.com";
                var password = "PasswordISNEW";
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            return View();
        }
    }
}
