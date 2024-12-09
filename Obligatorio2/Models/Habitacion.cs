using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obligatorio2.Models
    {
    public class Habitacion
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HabitacionId { get; set; }

        [Display(Name = "Veces reservada")]
        public int VecesReservada { get; set; } = 0;

        [Required]
        [Display(Name = "Tipo de habitación")]
        public string? Tipo { get; set; }

        [Required]
        [Display(Name = "Capacidad")]
        public int Capacidad { get; set; }

        [Required]
        [Display(Name = "Precio diario")]
        public double PrecioDiario { get; set; }

        [ForeignKey("HotelId")]
        public int HotelId { get; set; }

        public Hotel Hotel { get; set; } = new();

        public List<Reserva> Reservas { get; set; } = new();
        }
    }