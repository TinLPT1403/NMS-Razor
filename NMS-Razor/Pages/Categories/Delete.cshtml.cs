using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        public DeleteModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty(SupportsGet = true)]
        public required int Id { get; set; }

        [BindProperty]
        public required Category Category { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Category = await _categoryService.GetCategoryByIdAsync(Id);
            if (Category == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _categoryService.DeactiveCategoryAsync(Id);
                return RedirectToPage("/Categories/Index");

            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = "Category has Articles, cannot delete";
                return RedirectToPage("/Categories/Delete");
            }
        }
    }
}
