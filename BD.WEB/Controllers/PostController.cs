using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD.DATA.Entity;
using BD.WEB.Models;
using DB.SERVICE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BD.WEB.Controllers
{
    public class PostController : Controller
    {
        private IBloodGroupService bloodGroupService;
        private ICityService cityService;
        private IPostService postService;

        public PostController(IBloodGroupService bloodGroupService,ICityService cityService,IPostService postService)
        {
            this.bloodGroupService = bloodGroupService;
            this.cityService = cityService;
            this.postService = postService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var postList = postService.GetPosts().OrderByDescending(s=>s.AddDate).ToList();
            List<PostVM> posts = new List<PostVM>();
            foreach(var it in postList)
            {
                PostVM post = new PostVM();
                post.Headline = it.Headline;
                post.Description = it.Description;
                post.AddDate = it.AddDate;
                post.Location = it.Location;
                post.BloodGroup = bloodGroupService.GetBloodGroup(it.BloodGroupID.Value).Name;
            }
            return View(posts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.BloodGroup = new SelectList(bloodGroupService.GetBloodGroups().ToList(), "Id", "Name");

            return View();
        }
        [HttpPost]
        public IActionResult CreateConfirm(Post post)
        {
            post.AddDate = DateTime.Now;
            postService.InsertPost(post);
            return RedirectToAction(nameof(Index));
        }
    }
}
