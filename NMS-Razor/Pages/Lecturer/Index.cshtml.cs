using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Lecturer
{
    [Authorize(Policy = "Lecturer")]
    public class IndexModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        public IndexModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        [BindProperty]
        public IEnumerable<NewsArticle> NewsArticles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            NewsArticles = await _newsArticleService.GetActiveNewsArticlesAsync();
            return Page();
        }
    }
}
