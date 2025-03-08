using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.NewsArticles
{
    [Authorize(Policy = "Staff")]
    public class DetailsModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly INewsTagService _newsTagService;
        public DetailsModel(INewsArticleService newsArticleService, INewsTagService newsTagService)
        {
            _newsArticleService = newsArticleService;
            _newsTagService = newsTagService;
        }

        [BindProperty(SupportsGet = true)]
        public required string Id { get; set; }
        [BindProperty]
        public required NewsArticle Article { get; set; }
        [BindProperty]
        public required IEnumerable<Tag> Tags { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // In Razor Page (OnGetAsync or OnPostAsync)
            Article = await _newsArticleService.GetNewsArticleByIdAsync(Id);
            if (Article == null) return NotFound();
            Tags = await _newsTagService.GetTagsOfArticleAsync(Id);
            return Page();
        }
    }
}
