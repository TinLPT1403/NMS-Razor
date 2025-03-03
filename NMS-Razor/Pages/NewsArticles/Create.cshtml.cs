using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.NewsArticles
{
    [Authorize(Policy = "Staff")]
    public class CreateModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        public CreateModel(INewsArticleService newsArticleService, ICategoryService categoryService, ITagService tagService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        [BindProperty]
        public IEnumerable<Category> Categories { get; set; }
        [BindProperty]
        public IEnumerable<Tag> Tags { get; set; }
        [BindProperty]
        public required NewsArticleCreateDTO Article { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _categoryService.GetActiveCategoriesAsync();
            Tags = await  _tagService.GetAllTagsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _newsArticleService.CreateNewsArticleAsync(Article, HttpContext);
                TempData["Message"] = "Article created successfully.";
                return RedirectToPage("/NewsArticles/Index");
            }
            TempData["Error"] = "Failed to create article.";
            return Page();
        }
    }
}
