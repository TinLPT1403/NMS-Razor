using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Admin
{
    public class DetailsAccountModel : PageModel
    {
        private readonly IAccountService _accountService;
        public DetailsAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public required SystemAccount Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Account = await _accountService.GetAccountByIdAsync(id);
            if (Account == null) return NotFound();
            return Page();
        }
    }
}
