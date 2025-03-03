using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace NMS_Razor.Pages.Staff
{
    public class MyProfileModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public MyProfileModel(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [BindProperty]
        public required AccountDTO Account { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userId = int.Parse(idClaim);
            Account = _mapper.Map<AccountDTO>(await _accountService.GetAccountByIdAsync(userId));
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var idClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var userId = int.Parse(idClaim);
                await _accountService.UpdateAccountAsync(userId, Account);
                TempData["SuccessMessage"] = "Your profile has been updated successfully!";
                return Page();
            }
            TempData["SuccessMessage"] = "Invalid data, please try again bro";
            return Page();
        }
    }
}
