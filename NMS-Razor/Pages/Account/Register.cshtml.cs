using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;
        public RegisterModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountCreateDTO Account { get; set; } = new()
        {
            AccountName = string.Empty,
            AccountEmail = string.Empty,
            Password = string.Empty
        };


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _accountService.CreateAccountAsync(Account);
                return RedirectToPage("/Account/Login");
            }
            return Page();
        }

    }
}
