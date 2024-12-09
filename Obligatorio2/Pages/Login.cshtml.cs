using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Obligatorio2.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Obligatorio2.Pages
    {
    public class LoginModel : PageModel
        {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
            {
            _context = context;
            }

        [BindProperty]
        [Required(ErrorMessage = "Necesita ingresar un email.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Debe ingresar una dirección de email válida.")]
        public string EmailString { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Necesita ingresar una contraseña.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Formato de contraseña incorrecto.")]
        public string ContraseniaString { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public string? NombreHotel { get; set; }

        public async Task OnGet()
            {
            var hotel = await _context.Hoteles!
                .FindAsync(1);

            NombreHotel = hotel!.NombreHotel;
            }

        public async Task<IActionResult> OnPost()
            {
            if (!ModelState.IsValid)
                {
                return Page();
                }
            try
                {
                var usuario = await _context.Usuarios!
                    .FirstOrDefaultAsync(user => user.Email!.ToLower() == EmailString.ToLower());

                if (usuario == null || usuario.Contrasenia != ContraseniaString)
                    {
                    ErrorMessage = "Email o contraseña incorrectos";
                    return Page();
                    }

                var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Email!)
                    };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");

                await HttpContext.SignInAsync("MyCookieAuth", new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                        {
                        IsPersistent = true
                        });

                return RedirectToPage("/Index");
                }
            catch (Exception ex)
                {
                ErrorMessage = "Error inesperado.";
                Console.WriteLine(ex);
                return Page();
                }
            }
        }
    }