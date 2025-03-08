using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.NewsArticles
{
    [Authorize(Policy = "Staff")]
    public class EditModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly INewsTagService _newsTagService;
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public EditModel(INewsArticleService newsArticleService, ICategoryService categoryService, INewsTagService newsTagService, ITagService tagService, IMapper mapper)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _newsTagService = newsTagService;
            _tagService = tagService;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public required string Id { get; set; }
        [BindProperty]
        public required NewsArticleUpdateDTO Article { get; set; }
        [BindProperty]
        public required IEnumerable<Category> Categories { get; set; }
        [BindProperty]
        public required IEnumerable<Tag> NewsTags { get; set; }
        [BindProperty]
        public required IEnumerable<Tag> Tags { get; set; }
        [BindProperty]
        public required List<int>? TagIdList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var article = await _newsArticleService.GetNewsArticleByIdAsync(Id);
            if (article == null) return NotFound();

            var model = new NewsArticleUpdateDTO
            {
                NewsTitle = article.NewsTitle,
                Headline = article.Headline,
                NewsContent = article.NewsContent,
                NewsSource = article.NewsSource,
                NewsStatus = article.NewsStatus,
                CategoryId = article.CategoryId,
                NewsTagIds = _newsTagService.GetTagsOfArticleAsync(Id).Result.Select(t => t.TagId).ToList()
            };

            Article = model;

            Categories = await _categoryService.GetActiveCategoriesAsync();
            NewsTags = await _newsTagService.GetTagsOfArticleAsync(Id);
            Tags = await _tagService.GetAllTagsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = _mapper.Map<NewsArticleUpdateDTO>(Article);
            dto.NewsTagIds = TagIdList;           

            if (ModelState.IsValid)
            {
                await _newsArticleService.UpdateNewsArticleAsync(Id, dto, HttpContext);
                TempData["Message"] = "Article updated successfully.";
                return RedirectToPage("/NewsArticles/Index");
            }
            TempData["Error"] = "Failed to update article.";
            Categories = await _categoryService.GetActiveCategoriesAsync();
            NewsTags = await _newsTagService.GetTagsOfArticleAsync(Id);
            Tags = await _tagService.GetAllTagsAsync();
            return Page();
        }
    }
}
