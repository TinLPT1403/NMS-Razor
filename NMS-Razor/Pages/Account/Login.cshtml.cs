using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;  // Your business logic layer

namespace NMS_Razor.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public LoginModel(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [BindProperty]
        public LoginViewModel LoginData { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string adminEmail = _configuration["AdminCredentials:Email"];
            string adminPassword = _configuration["AdminCredentials:Password"];

            if (LoginData.Email == adminEmail && LoginData.Password == adminPassword)
            {
                var adminClaims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, "Admin"),
                    new(ClaimTypes.Name, "Administrator"),
                    new(ClaimTypes.Role, "3")
                };

                var adminIdentity = new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var adminPrincipal = new ClaimsPrincipal(adminIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, adminPrincipal);
                HttpContext.User = adminPrincipal;

                return RedirectToPage("/Admin/ManageAccounts");
            }

            var account = await _accountService.AuthenticateAsync(LoginData.Email, LoginData.Password);
            if (account == null)
            {
                TempData["Error"] = "Invalid email or password.";
                return RedirectToAction("Login", "Account");
            }

            // Define cookie options for the new token
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                IsPersistent = true
            };

            //// Retrieve user account details after authentication
            //var user = await _accountService.GetAccountByIdAsync(_userUtils.GetUserFromInputToken(token));

            var claims = new List<Claim>
                {
                new(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new(ClaimTypes.Name, account.AccountName),
                new(ClaimTypes.Role, account.AccountRole.ToString())
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
            HttpContext.User = principal;


            var role = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Redirect based on the user's role
            return role switch
            {
                "3" => RedirectToPage("/Admin/ManageAccounts"),
                "1" => RedirectToPage("/Categories/Index"),
                "2" => RedirectToPage("/Lecturer/Index"),
                _ => RedirectToPage("/NewsArticles/Index") // Default fallback
            };
        }

        public class LoginViewModel
        {
            [Required, EmailAddress]
            public string Email { get; set; } = string.Empty;

            [Required, DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

    }
}
