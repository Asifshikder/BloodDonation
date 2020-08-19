using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.DATA.Entity;
using BD.WEB.Models;
using DB.SERVICE;
using DB.SERVICE.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;

namespace BD.WEB.Controllers
{
    public class ManageController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private IPostService postService;
        private IBloodGroupService bloodGroupService;

        public ManageController(UserManager<ApplicationUser> userManager, IPostService postService,IBloodGroupService bloodGroupService)
        {
            this.userManager = userManager;
            this.postService = postService;
            this.bloodGroupService = bloodGroupService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeletePost(int id)
        {
          var post = postService.GetPost(id);
            return View(post);
        }
        [HttpGet]
        public IActionResult Post()
        {
            var postList = postService.GetPosts().OrderByDescending(s => s.AddDate).ToList();
            List<PostVM> posts = new List<PostVM>();
            foreach (var it in postList)
            {
                PostVM post = new PostVM();
                post.ID = it.Id;
                post.Headline = it.Headline;
                post.Description = it.Description;
                post.AddDate = it.AddDate;
                post.Location = it.Location;
                post.BloodGroup = bloodGroupService.GetBloodGroup(it.BloodGroupID.Value).Name;
            }
            return View(posts);
        }

    }
}
