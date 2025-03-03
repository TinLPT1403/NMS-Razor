using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.NewsArticles
{
    [Authorize(Policy = "Staff")]
    public class IndexModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public IndexModel(INewsArticleService newsArticleService, ICategoryService categoryService, ITagService tagService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        [BindProperty]
        public required IEnumerable<Category> Categories { get; set; }
        [BindProperty]
        public required int? CategoryId { get; set; }
        [BindProperty]
        public required int? TagId { get; set; }
        [BindProperty]
        public required IEnumerable<Tag> Tags { get; set; }
        [BindProperty]
        public required IEnumerable<NewsArticle> Articles { get; set; }
        [BindProperty]
        public required string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //On Page load
            Categories = await _categoryService.GetAllCategoriesAsync();
            Tags = await _tagService.GetAllTagsAsync();

            // Fetch filtered articles in a single optimized database query
            Articles = await _newsArticleService.SearchArticlesAsync(SearchTerm, CategoryId, TagId);
            return Page();
        }

    }
}
