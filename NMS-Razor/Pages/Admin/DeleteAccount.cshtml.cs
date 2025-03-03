using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Admin
{
    [Authorize(Policy = "Admin")]
    public class DeleteAccountModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DeleteAccountModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty(SupportsGet = true)]
        public required int Id {  get; set; }

        [BindProperty]
        public required SystemAccount Account { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Account = await _accountService.GetAccountByIdAsync(Id);
            if (Account == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var account = await _accountService.GetAccountByIdAsync(Id);
            if (account == null)
            {
                TempData["Error"] = "Account not found.";
                return RedirectToPage("/Admin/ManageAccounts");
            }

            // Perform the delete operation.
            await _accountService.DeleteAccountAsync(Id);

            TempData["Message"] = "Account deleted successfully.";
            return RedirectToPage("/Admin/ManageAccounts");
        }
    }
}
