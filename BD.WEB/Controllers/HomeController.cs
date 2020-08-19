using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BD.WEB.Models;
using DB.SERVICE;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using BD.DATA.Entity;

namespace BD.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBloodGroupService bloodGroupService;
        private readonly ICityService cityService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ILogger<HomeController> logger,
            IBloodGroupService bloodGroupService,
            ICityService cityService,
            UserManager<ApplicationUser> userManager
            )
        {
            _logger = logger;
            this.bloodGroupService = bloodGroupService;
            this.cityService = cityService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.BloodGroup = new SelectList(bloodGroupService.GetBloodGroups().ToList(), "Id", "Name");
            ViewBag.City = new SelectList(cityService.GetCitys().ToList(), "Id", "Name");
            return View();
        }
        [HttpGet]
        public IActionResult Search(SearchVM search)
        {
            var donorList = userManager.Users.Where(s => s.CityID == search.City && s.BloodGroupID == search.BloodGroup).ToList();
            List<DonorVM> donors = new List<DonorVM>();
            foreach (var d in donorList)
            {
                DonorVM dn = new DonorVM();
                dn.ID = d.Id;
                dn.FullName = d.Fullname;
                dn.Contact = d.ContactNumber;
                dn.Contact2 = d.PhoneNumber;
                donors.Add(dn);
            }
            var bloodGroupName = bloodGroupService.GetBloodGroup(search.BloodGroup.Value).Name;
            var CityName = cityService.GetCity(search.City.Value).Name;
            ViewBag.Message = "Showing " + bloodGroupName + " blood donors in " + CityName +" City";
            return View(donors);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
