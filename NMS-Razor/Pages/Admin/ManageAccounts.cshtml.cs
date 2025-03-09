using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Admin
{
    [Authorize(Policy = "Admin")]
    public class ManageAccountsModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public ManageAccountsModel(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [BindProperty]  
        public required IEnumerable<SystemAccount> AccountList {  get; set; }
        [BindProperty]
        public required AccountCreateAdminDTO Account { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            AccountList = await _accountService.GetAllAccountsForManageAsync();
            return Page();
        }

        // Named handler for creating a new account (called via partial form post)
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _accountService.CreateAccountAsync(Account);
                TempData["Message"] = "Account created successfully.";
                return RedirectToPage();
            }
            TempData["Error"] = "Validation failed!";
            // Reload accounts in case we want to display them again
            AccountList = await _accountService.GetAllAccountsForManageAsync();
            return RedirectToPage();
        }
    }
}
