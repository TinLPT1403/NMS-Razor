using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using DAL.Entities;  // Your business logic layer

namespace NMS_Razor.Pages.Guest
{
    public class GuestIndexModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        public GuestIndexModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public IEnumerable<NewsArticle> NewsArticles { get; set; } = Enumerable.Empty<NewsArticle>();

        public async Task<IActionResult> OnGetAsync()
        {
            NewsArticles = await _newsArticleService.GetArticlesWithActiveCategories();
            return Page();
        }
    }

}
