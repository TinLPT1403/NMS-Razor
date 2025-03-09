using BLL.DTOs;
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
        public IEnumerable<Category>? Categories { get; set; }
        [BindProperty]
        public int? CategoryId { get; set; }
        [BindProperty]
        public  int? TagId { get; set; }
        [BindProperty]
        public  IEnumerable<Tag>? Tags { get; set; }
        [BindProperty]
        public  IEnumerable<NewsArticle>? Articles { get; set; }
        [BindProperty]
        public string? SearchTerm { get; set; }
        [BindProperty]
        public NewsArticleCreateDTO Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _newsArticleService.CreateNewsArticleAsync(Article, HttpContext);
                TempData["Message"] = "Article created successfully.";
                return RedirectToPage();
            }
            TempData["Error"] = "Failed to create article.";
            return RedirectToPage();
        }

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
