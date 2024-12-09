using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obligatorio2.Models
    {
    public class Reserva
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservaId { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime FechaFin { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime FechaReserva { get; set; }

        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        [ForeignKey("HabitacionId")]
        public int HabitacionId { get; set; }

        public Habitacion? Habitacion { get; set; }

        public Pago? Pago { get; set; }
        }
    }