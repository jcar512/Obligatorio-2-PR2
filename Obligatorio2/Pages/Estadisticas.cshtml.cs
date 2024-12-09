using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Obligatorio2.Data;
using Obligatorio2.Models;

namespace Obligatorio2.Pages
    {
    [Authorize]
    public class EstadisticasModel : PageModel
        {
        private readonly ApplicationDbContext _context;

        public EstadisticasModel(ApplicationDbContext context)
            {
            _context = context;
            }

        [BindProperty]
        public int CantidadDeReservasMasAlta { get; set; }

        [BindProperty]
        public int IdPaisConMasUsuarios { get; set; }

        [BindProperty]
        public double GananciasTotales { get; set; }

        [BindProperty]
        public int PromedioDeEdadUsuarios { get; set; }

        [BindProperty]
        public int PromedioDeEstadiaUsuarios { get; set; }

        [BindProperty]
        public int NumerohabitacionMasReservada { get; set; }

        [BindProperty]
        public Usuario? Usuario { get; set; }

        [BindProperty]
        public Pais? Pais { get; set; }

        [BindProperty]
        public string? ErrorMessage { get; set; }

        public async Task<int> PaisConMasUsuarios()
            {
            var paisConMasUsuarios = await _context.Usuarios!
                .GroupBy(u => u.PaisId)
                .Select(grupo => new
                    {
                    PaisId = grupo.Key,
                    UsuariosCount = grupo.Count()
                    })
                .OrderByDescending(grupo => grupo.UsuariosCount)
                .FirstOrDefaultAsync();

            return paisConMasUsuarios!.PaisId;
            }

        public async Task<double> GananciasTotalesGeneradas()
            {
            var gananciasTotalesGeneradas = await _context.Pagos!
                .SumAsync(p => p.MontoAbonado);

            return gananciasTotalesGeneradas;
            }

        public async Task<int> PromedioEdadUsuarios()
            {
            var usuarios = await _context.Usuarios!
                .ToListAsync();

            List<int> edades = new List<int>();

            foreach (var usuario in usuarios)
                {
                var edad = DateTime.Now.Year - usuario.FechaNacimiento.Year;
                edades.Add(edad);
                }

            return (int)edades.Average();
            }

        public async Task<int> PromedioEstadiaUsuarios()
            {
            var reservas = await _context.Reservas!
                .ToListAsync();

            List<int> largoDeEstadias = new List<int>();

            foreach (var reserva in reservas)
                {
                int cantidadDias = Utils.Utils.CalcularFechas(reserva.FechaInicio, reserva.FechaFin).Count;
                largoDeEstadias.Add(cantidadDias);
                }

            return (int)largoDeEstadias.Average();
            }

        public async Task<int> HabitacionMasReservada()
            {
            var habitacionMasReservada = await _context.Habitaciones!
                .OrderByDescending(h => h.VecesReservada)
                .FirstOrDefaultAsync();

            return habitacionMasReservada!.HabitacionId;
            }

        public async Task OnGet()
            {
            try
                {
                var usuarioConMasReservas = await _context.Reservas!
               .GroupBy(r => r.UsuarioId)
               .Select(grupo => new
                   {
                   UsuarioId = grupo.Key,
                   ReservasCount = grupo.Count()
                   })
               .OrderByDescending(grupo => grupo.ReservasCount)
               .FirstOrDefaultAsync();

                CantidadDeReservasMasAlta = usuarioConMasReservas!.ReservasCount;

                Usuario = await _context.Usuarios!
                    .FindAsync(usuarioConMasReservas!.UsuarioId);

                IdPaisConMasUsuarios = await PaisConMasUsuarios();

                Pais = await _context.Paises!
                    .FindAsync(IdPaisConMasUsuarios);

                GananciasTotales = await GananciasTotalesGeneradas();

                PromedioDeEdadUsuarios = await PromedioEdadUsuarios();

                PromedioDeEstadiaUsuarios = await PromedioEstadiaUsuarios();

                NumerohabitacionMasReservada = await HabitacionMasReservada();
                }
            catch (Exception ex)
                {
                ErrorMessage = "Error 404";
                Console.WriteLine(ex.Message);
                Page();
                }
            }
        }
    }