using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Admin
{
    [Authorize(Policy = "Admin")]
    public class CreateAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public required AccountCreateAdminDTO Account { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                await _accountService.CreateAccountAsync(Account);
                TempData["Message"] = "Account created successfully.";
                return RedirectToPage("/Admin/ManageAccounts");
            }
            TempData["Error"] = "Failed to create account.";
            return Page();
        }
    }
}
