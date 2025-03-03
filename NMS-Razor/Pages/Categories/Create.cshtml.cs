using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NMS_Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; }

        public IEnumerable<Category> ParentCategoryList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            ParentCategoryList = await _categoryService.GetParentCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(Category);
                return RedirectToPage("/Categories/Index");
            }
            ParentCategoryList = await _categoryService.GetParentCategoriesAsync();
            return Page();
        }
    }
}
