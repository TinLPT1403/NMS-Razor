using AutoMapper;
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

        public async Task<IActionResult> OnGetAsync()
        {
            AccountList = await _accountService.GetAllAccountsForManageAsync();
            return Page();
        }
    }
}
