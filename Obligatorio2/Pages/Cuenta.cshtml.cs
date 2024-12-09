using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Obligatorio2.Pages
    {
    [Authorize]
    public class CuentaModel : PageModel
        {
        public void OnGet()
            {
            }

        public async Task<IActionResult> OnPostAsync()
            {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("/Login");
            }
        }
    }