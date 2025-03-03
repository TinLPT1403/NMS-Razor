using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NMS_Razor.Pages.Admin
{
    [Authorize(Policy = "Admin")]
    public class EditAccountModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public EditAccountModel(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public required int Id { get; set; }

        [BindProperty]
        public required AccountUpdateAdminDTO Account { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var account = await _accountService.GetAccountByIdAsync(Id);
            if (account == null) return NotFound();
            Account = _mapper.Map<AccountUpdateAdminDTO>(account);
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _accountService.UpdateAccountAsync(Id, Account);
                TempData["Message"] = "Account updated successfully.";
                return RedirectToPage("/Admin/ManageAccounts");
            }
            TempData["Error"] = "Failed to update account.";
            return Page();
        }
    }
}
