using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Obligatorio2.Data;
using Obligatorio2.Models;
using System.Security.Claims;

namespace Obligatorio2.Pages
    {
    [Authorize]
    public class ReservasModel : PageModel
        {
        private readonly ApplicationDbContext _context;

        public ReservasModel(ApplicationDbContext context)
            {
            _context = context;
            }

        public IEnumerable<Reserva> Reservas { get; set; } = Enumerable.Empty<Reserva>();

        public IEnumerable<Pago> Pagos { get; set; } = Enumerable.Empty<Pago>();

        public async Task OnGet()
            {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userId = Int32.Parse(userIdClaim!);

            Reservas = await _context.Reservas!
                .Where(r => r.UsuarioId == userId)
                .ToListAsync();

            var reservaIds = Reservas.Select(r => r.ReservaId).ToList();

            Pagos = await _context.Pagos!
                .Where(p => reservaIds.Contains(p.ReservaId))
                .ToListAsync();
            }

        public async Task<IActionResult> OnPostDelete(int reservaId)
            {
            var reserva = await _context.Reservas!.FindAsync(reservaId);

            if (reserva == null)
                {
                return NotFound();
                }

            _context.Reservas!.Remove(reserva!);
            await _context.SaveChangesAsync();

            await OnGet();
            return Page();
            }
        }
    }