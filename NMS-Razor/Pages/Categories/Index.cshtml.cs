using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public IEnumerable<Category> Categories { get; set; }

        [BindProperty]
        public Category Category { get; set; }
        public IEnumerable<Category> ParentCategoryList { get; set; }
        //public async Task<IActionResult> OnGetAsync()
        //{
        //    ParentCategoryList = await _categoryService.GetParentCategoriesAsync();
        //    return Page();
        //}
        [BindProperty]
        public CategoryDTO categoryDTO { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(Category);
                return RedirectToPage();
            }
            ParentCategoryList = await _categoryService.GetParentCategoriesAsync();
            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            ParentCategoryList = await _categoryService.GetParentCategoriesAsync();
            Categories = await _categoryService.GetAllCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEditCategoryAsync( Category category)
        {
            Console.WriteLine("Category: " + category.IsActive);
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(category.CategoryId, category);
                TempData["Message"] = "Category updated successfully.";
                return RedirectToPage();
            }
            ParentCategoryList = await _categoryService.GetParentCategoriesAsync();
            return RedirectToPage();
        }
    }
}
