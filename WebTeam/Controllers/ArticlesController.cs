using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebTeam.Data;
using WebTeam.Models;
using WebTeam.Services;

namespace WebTeam.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommentsService _commentsService;
        private readonly IArticleService _articleService;

        public ArticlesController(ApplicationDbContext context, IArticleService articleService, ICommentsService commentsService)
        {
            _context = context;
            _commentsService = commentsService;
            _articleService = articleService;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Articles.Include(a => a.Author).Include(a => a.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Like(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingLike = await _context.Likes
                .Where(l => l.ArticleID == id && l.UserID == userId)
                .FirstOrDefaultAsync();

            if (existingLike == null)
            {
                _context.Likes.Add(new Like { ArticleID = id, UserID = userId });
                article.LikesCount++;
            }
            else
            {
                _context.Likes.Remove(existingLike);
                article.LikesCount--;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { id });
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

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,Title,Content,MagazineCover,AuthorID,CategoryId,ArticleDate,NoOfLike")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", article.AuthorID);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", article.CategoryId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", article.AuthorID);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", article.CategoryId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,Title,Content,MagazineCover,AuthorID,CategoryId,ArticleDate,NoOfLike")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", article.AuthorID);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", article.CategoryId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articles == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Articles'  is null.");
            }
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
          return (_context.Articles?.Any(e => e.ArticleId == id)).GetValueOrDefault();
        }
    }
}
