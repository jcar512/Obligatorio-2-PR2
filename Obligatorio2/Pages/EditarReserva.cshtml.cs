using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Obligatorio2.Data;
using Obligatorio2.Models;
using Obligatorio2.Utils;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Obligatorio2.Pages
    {
    public class EditarReservaModel : PageModel
        {
        private readonly ApplicationDbContext _context;

        public EditarReservaModel(ApplicationDbContext context)
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
        public double MontoAbonado { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar un método de pago.")]
        public string? MetodoPago { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar una habitación válida.")]
        [Range(1, 20, ErrorMessage = "Debe seleccionar una habitación válida.")]
        public int HabitacionId { get; set; }

        public IEnumerable<Habitacion> Habitaciones { get; set; } = Enumerable.Empty<Habitacion>();

        [BindProperty]
        public Reserva? Reserva { get; set; }

        public Pago? Pago { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task OnGet(int reservaId)
            {
            Habitaciones = await _context.Habitaciones!
                .ToListAsync();

            Reserva = await _context.Reservas!
                .FindAsync(reservaId);

            Pago = await _context.Pagos!
                .FirstOrDefaultAsync(pago => pago.ReservaId == reservaId);
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

                await OnGet(Reserva!.ReservaId);
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
                    await OnGet(Reserva!.ReservaId);
                    return Page();
                    }
                else if (fechas.Count > 30)
                    {
                    ErrorMessage = "La cantidad de días de estadía no puede exceder los treinta.";
                    await OnGet(Reserva!.ReservaId);
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
                    await OnGet(Reserva!.ReservaId);
                    return Page();
                    }

                var reserva = await _context.Reservas!.FindAsync(Reserva!.ReservaId);

                if (reserva != null)
                    {
                    reserva.FechaInicio = FechaInicio;
                    reserva.FechaFin = FechaFin;
                    reserva.FechaReserva = FechaReserva;
                    reserva.HabitacionId = HabitacionId;
                    await _context.SaveChangesAsync();
                    }

                var habitacion = await _context.Habitaciones!.FindAsync(HabitacionId);

                if (habitacion == null)
                    {
                    ErrorMessage = "Error con la habitación.";
                    await OnGet(Reserva!.ReservaId);
                    return Page();
                    }

                MontoAbonado = (fechas.Count - 1) * habitacion.PrecioDiario;

                var pago = await _context.Pagos!.FirstOrDefaultAsync(pago => pago.ReservaId == Reserva.ReservaId);

                if (pago != null)
                    {
                    pago.FechaPago = FechaReserva;
                    pago.MontoAbonado = MontoAbonado;
                    pago.MetodoPago = MetodoPago;
                    await _context.SaveChangesAsync();
                    }

                habitacion.VecesReservada++;

                await _context.SaveChangesAsync();

                return RedirectToPage("/Reservas");
                }
            catch (Exception ex)
                {
                ErrorMessage = "Error inesperado";
                Console.WriteLine(ex.ToString());
                await OnGet(Reserva!.ReservaId);
                return Page();
                }
            }
        }
    }