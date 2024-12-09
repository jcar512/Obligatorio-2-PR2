using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Obligatorio2.Data;
using Obligatorio2.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Obligatorio2.Pages
    {
    [Authorize]
    public class NuevaReservaModel : PageModel
        {
        private readonly ApplicationDbContext _context;

        public NuevaReservaModel(ApplicationDbContext context)
            {
            _context = context;
            }

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar una fecha.")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar una fecha.")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        public DateTime FechaReserva { get; set; } = DateTime.Now.Date;

        public DateTime FechaReservaMaxima { get; set; } = DateTime.Now.AddYears(1).Date;

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar una habitación válida.")]
        [Range(1, 20, ErrorMessage = "Debe seleccionar una habitación válida.")]
        public int HabitacionId { get; set; }

        [BindProperty]
        public Reserva? Reserva { get; set; }

        [BindProperty]
        public Pago? Pago { get; set; }

        public double MontoAbonado { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar un método de pago.")]
        public string? MetodoPago { get; set; }

        public IEnumerable<Habitacion> Habitaciones { get; set; } = Enumerable.Empty<Habitacion>();

        public string? ErrorMessage { get; set; }

        public async Task OnGet()
            {
            Habitaciones = await _context.Habitaciones!.ToListAsync();
            }

        public async Task<IActionResult> OnPost()
            {
            if (!ModelState.IsValid)
                {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                    Console.WriteLine(error.ErrorMessage);
                    }
                ErrorMessage = "Debe seleccionar una habitación.";
                await OnGet();
                return Page();
                }

            try
                {
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var userId = Int32.Parse(userIdClaim!);

                var reservas = await _context.Reservas!
                    .ToListAsync();

                List<DateTime>? fechas = FechaInicio < FechaFin
                    ? Utils.Utils.CalcularFechas(FechaInicio, FechaFin)
                    : null;

                if (fechas == null)
                    {
                    ErrorMessage = "La fecha de finalización debe ser mayor a la de inicio.";
                    return Page();
                    }
                else if (fechas.Count > 30)
                    {
                    ErrorMessage = "La cantidad de días de estadía no puede exceder los treinta.";
                    await OnGet();
                    return Page();
                    }

                var fechasNoDisponibles = await _context.Reservas!
                .Where(r => r.HabitacionId == HabitacionId &&
                ((FechaInicio >= r.FechaInicio && FechaInicio <= r.FechaFin) ||
                (FechaFin >= r.FechaInicio && FechaFin <= r.FechaFin)))
                .ToListAsync();

                if (fechasNoDisponibles.Any())
                    {
                    ErrorMessage = "Las fechas seleccionadas no están disponibles.";
                    await OnGet();
                    return Page();
                    }

                Reserva = new Reserva()
                    {
                    FechaInicio = FechaInicio,
                    FechaFin = FechaFin,
                    FechaReserva = FechaReserva,
                    UsuarioId = userId,
                    HabitacionId = HabitacionId,
                    };

                _context.Reservas!.Add(Reserva);
                await _context.SaveChangesAsync();

                var habitacion = await _context.Habitaciones!.FindAsync(HabitacionId);

                if (habitacion == null)
                    {
                    ErrorMessage = "Error con la habitación.";
                    await OnGet();
                    return Page();
                    }

                MontoAbonado = (fechas.Count - 1) * habitacion.PrecioDiario;

                Pago = new Pago()
                    {
                    FechaPago = FechaReserva,
                    MontoAbonado = MontoAbonado,
                    MetodoPago = MetodoPago,
                    ReservaId = Reserva.ReservaId,
                    };

                _context.Pagos!.Add(Pago);

                habitacion.VecesReservada++;
                await _context.SaveChangesAsync();

                return RedirectToPage("/Reservas");
                }
            catch (Exception ex)
                {
                ErrorMessage = "Error inesperado";
                Console.WriteLine(ex.ToString());
                await OnGet();
                return Page();
                }
            }
        }
    }