using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obligatorio2.Models
    {
    public class Pago
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PagoId { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime FechaPago { get; set; }

        [Required]
        public double MontoAbonado { get; set; }

        [Required]
        public string? MetodoPago { get; set; }

        [ForeignKey("ReservaId")]
        public int ReservaId { get; set; }

        public Reserva? Reserva { get; set; }
        }
    }