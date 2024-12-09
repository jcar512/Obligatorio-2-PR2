using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Obligatorio2.Data;
using Obligatorio2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obligatorio2.Pages
    {
    public class NuevoUsuarioModel : PageModel
        {
        private readonly ApplicationDbContext _context;

        public NuevoUsuarioModel(ApplicationDbContext context)
            {
            _context = context;
            }

        [BindProperty]
        [Required(ErrorMessage = "Debe ingresar un nombre.")]
        [MaxLength(150)]
        public string? Nombre { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe ingresar un apellido.")]
        [MaxLength(150)]
        public string? Apellido { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe ingresar una fecha de nacimiento.")]
        [Column(TypeName = "Date")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        public DateTime EdadMinima { get; set; } = DateTime.Now.AddYears(-18);

        public DateTime EdadMaxima { get; set; } = DateTime.Now.AddYears(-80);

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar un tipo de documento.")]
        public string? TipoDocumento { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe ingresar un número de documento.")]
        [RegularExpression(@"^[A-Z0-9]{6,9}$",
            ErrorMessage = "El formato del CI/pasaporte es inválido.")]
        public string? NumeroDocumento { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar un país de origen.")]
        public int PaisId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
        [RegularExpression(@"^\d{9}$",
            ErrorMessage = "Debe ingresar un número de teléfono válido.")]
        public string? Telefono { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe ingresar un email.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Formato de email no válido.")]
        public string? Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe ingresar una contraseña.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "Formato de contraseña no válido.")]
        public string? Contrasenia { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        public string? ConfirmarContrasenia { get; set; }

        public Usuario? Usuario { get; set; }

        public IEnumerable<Pais> Paises { get; set; } = Enumerable.Empty<Pais>();

        public string? ErrorMessage { get; set; }

        public async Task OnGet()
            {
            Paises = await _context.Paises!.ToListAsync();
            }

        public async Task<IActionResult> OnPost()
            {
            if (!ModelState.IsValid)
                {
                ErrorMessage = "Error inesperado.";
                Console.WriteLine("Error de modelo");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                    Console.WriteLine(error.ErrorMessage);
                    }
                await OnGet();
                return Page();
                }
            try
                {
                var usuarioRepetido = await _context.Usuarios!
                    .FirstOrDefaultAsync(u => u.Email == Email);

                if (usuarioRepetido != null)
                    {
                    ErrorMessage = "El email ya esta registrado";
                    await OnGet();
                    return Page();
                    }

                if (Contrasenia != ConfirmarContrasenia)
                    {
                    ErrorMessage = "Las contraseñas no coinciden";
                    await OnGet();
                    return Page();
                    }

                Usuario = new Usuario()
                    {
                    Nombre = Nombre,
                    Apellido = Apellido,
                    TipoDocumento = TipoDocumento,
                    NumeroDocumento = NumeroDocumento,
                    FechaNacimiento = FechaNacimiento,
                    Telefono = Telefono,
                    Email = Email,
                    Contrasenia = Contrasenia,
                    PaisId = PaisId,
                    };

                _context.Usuarios!.Add(Usuario);
                await _context.SaveChangesAsync();

                await OnGet();
                return RedirectToPage("/Login");
                }
            catch (Exception ex)
                {
                Console.WriteLine($"Error: {ex.Message}");
                ErrorMessage = "Error inesperado";
                await OnGet();
                return Page();
                }
            }
        }
    }