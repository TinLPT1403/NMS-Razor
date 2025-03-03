using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        public DetailsModel(ICategoryService categoryService)
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
    }
}
