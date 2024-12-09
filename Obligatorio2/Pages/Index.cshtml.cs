using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Obligatorio2.Data;
using Obligatorio2.Models;

namespace Obligatorio2.Pages
    {
    [Authorize]
    public class IndexModel : PageModel
        {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
            {
            _context = context;
            }

        public Hotel? Hotel { get; set; }

        public IEnumerable<Servicio> Servicios { get; set; } = Enumerable.Empty<Servicio>();

        public string? ErrorMessage { get; set; }

        public async Task OnGet()
            {
            Servicios = await _context.Servicios!
                .Where(s => s.Hotel!.HotelId == 1)
                .ToListAsync();

            Hotel = await _context.Hoteles!
                .FindAsync(1);

            if (Hotel == null || Servicios == null)
                {
                ErrorMessage = "Error 404";
                }
            }
        }
    }