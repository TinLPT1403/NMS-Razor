using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.NewsArticles
{
    [Authorize(Policy = "Staff")]
    public class DeleteModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        public DeleteModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        [BindProperty(SupportsGet = true)]
        public required string Id { get; set; }
        [BindProperty]
        public required NewsArticle Article { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Article = await _newsArticleService.GetNewsArticleByIdAsync(Id);
            if (Article == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _newsArticleService.DeactiveNewsArticleAsync(Id);
            TempData["Message"] = "Article deleted successfully.";
            return RedirectToPage("/NewsArticles/Index");
        }

    }
}
