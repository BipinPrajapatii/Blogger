using System.Diagnostics;
using Blogger.Data.Entities;
using Blogger.Models;
using Blogger.Service;
using Blogger.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogService _blogService;
        private readonly IUserBlogService _userBlogService;


        public HomeController(ILogger<HomeController> logger, IBlogService blogService, IUserBlogService userBlogService)
        {
            _logger = logger;
            _blogService = blogService;
            _userBlogService = userBlogService;
        }

        public IActionResult Index(int blogPage = 1)
        {
            var viewModel = new Models.UserBlogViewModel();
            viewModel.Blogs = _userBlogService.GetUserBlogs(blogPage, 10);
            viewModel.PagingInfo = new PagingInfoModel
            {
                TotalItems = _userBlogService.GetUserBlogs()!.Count(),
                ItemsPerPage = 10
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchText)
        {
            var viewModel = new Models.UserBlogViewModel();
            viewModel.Blogs = _userBlogService.GetUserBlogs(searchText: searchText);
            viewModel.SearchFor = searchText;
            viewModel.PagingInfo = new PagingInfoModel
            {
                TotalItems = _userBlogService.GetSearchedUserBlogs(searchText)!.Count(),
                ItemsPerPage = 10
            };
            return View(viewModel);
        }

        public IActionResult Search(string searchText, int blogPage = 1)
        {
            var viewModel = new Models.UserBlogViewModel();
            viewModel.Blogs = _userBlogService.GetUserBlogs(blogPage, 10, searchText);
            viewModel.SearchFor = searchText;
            viewModel.PagingInfo = new PagingInfoModel
            {
                TotalItems = _userBlogService.GetSearchedUserBlogs(searchText)!.Count(),
                ItemsPerPage = 10
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}