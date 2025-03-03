using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Lecturer
{
    [Authorize(Policy = "Lecturer")]
    public class DetailsModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        public DetailsModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        [BindProperty(SupportsGet = true)]
        public required string Id {  get; set; }

        [BindProperty]
        public required NewsArticle Article { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Article = await _newsArticleService.GetNewsArticleByIdAsync(Id);
            if (Article == null) return NotFound();
            return Page();
        }
    }
}
