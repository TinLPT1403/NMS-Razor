using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        public EditModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty(SupportsGet = true)]
        public required int Id { get; set; }
        [BindProperty]
        public required Category Category { get; set; }
        [BindProperty]
        public IEnumerable<Category> ParentCategoryList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Category = await _categoryService.GetCategoryByIdAsync(Id);
            if (Category == null) return NotFound();
            ParentCategoryList = await _categoryService.GetParentCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var category = await _categoryService.GetCategoryByIdAsync(Id);
            if (category == null) return NotFound();
            if(ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(Id, Category);
            }

            ParentCategoryList = await _categoryService.GetParentCategoriesAsync();
            return Page();
        }
    }
}
