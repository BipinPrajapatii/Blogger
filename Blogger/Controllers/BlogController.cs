using System.Security.Claims;
using Blogger.Models;
using Blogger.Service;
using Blogger.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IUserBlogService _userblogService;
        private readonly ILogger<BlogController> _logger;
        //private readonly string _appUserID;

        public BlogController(IBlogService blogService, IUserBlogService userblogService, ILogger<BlogController> logger)
        {
            _blogService = blogService;
            _userblogService = userblogService;
            _logger = logger;

        }

        // GET: Blog
        public ActionResult Index(int blogPage = 1)
        {
            var appUserID = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var viewModel = new UserBlogViewModel();
            var blogs = _userblogService.GetUserBlogs(appUserID, blogPage, 10);
            viewModel.Blogs = blogs;
            viewModel.PagingInfo = new PagingInfoModel
            {
                TotalItems = _userblogService.GetUserBlogs(appUserID)!.Count(),
                ItemsPerPage = 10
            };
            return View(viewModel);
        }

        [AllowAnonymous]
        // GET: Blog/Details/5
        public ActionResult Details(int id)
        {
            var model = _blogService.GetById(id);
            return View(model);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewData["Title"] = "Add Blog";
            return View("SaveBlog", new BlogSaveModel());
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogSaveModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.DatePublished = DateTime.Now;
                    model.AppUserID = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    _userblogService.Create(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("SaveBlog");
            }
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["Title"] = "Edit Blog";
            var model = _blogService.GetById(id);
            return View("SaveBlog", model);
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BlogEditModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.DatePublished = DateTime.Now;
                    model.AppUserID = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    _blogService.Update(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _blogService.GetById(id);
            ViewBag.IsDeleteAction = true;
            return View("Details", model);
        }

        // POST: Blog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, bool isConfirm)
        {
            try
            {
                if (isConfirm)
                {
                    var isDeleted = _blogService.DeleteById(id);
                    if (isDeleted)
                        TempData["Message"] = "Error while deleting blog.";
                    TempData["Message"] = "Blog Deleted";
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
