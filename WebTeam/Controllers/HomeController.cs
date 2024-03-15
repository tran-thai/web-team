using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTeam.Data;
using WebTeam.Models;
using WebTeam.Services;

namespace WebTeam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly ICommentsService _commentsService;
        private readonly IArticleService _articleService;




        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IArticleService articleService, ICommentsService commentsService)
        {
            _logger = logger;
            _context = context;
            _articleService = articleService;
            _commentsService = commentsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddComment([Bind("CommentID, CommentContent, UserID, ArticleID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                await _commentsService.Add(comment);
            }
            var listing = await _articleService.GetById(comment.ArticleID);
            return View("Details", listing);


        }

        public IActionResult Category()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}