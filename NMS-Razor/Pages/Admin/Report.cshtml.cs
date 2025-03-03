using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Admin
{
    [Authorize(Policy = "Admin")]
    public class ReportModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        public ReportModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        [BindProperty]
        public required DateTime StartDate { get; set; }
        [BindProperty]
        public required DateTime EndDate { get; set; }
        [BindProperty]
        public required IEnumerable<NewsArticle> Articles { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Articles = await _newsArticleService.GetNewsArticlesReportAsync(StartDate, EndDate);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Articles = await _newsArticleService.GetNewsArticlesReportAsync(StartDate, EndDate);
            return Page();
        }
    }
}
