using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obligatorio2.Models
    {
    public class Servicio
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServicioId { get; set; }

        [Required]
        [MaxLength(200)]
        public string? NombreServicio { get; set; }

        [Required]
        public bool EsAdicional { get; set; }

        [ForeignKey("HotelId")]
        public int HotelId { get; set; }

        public Hotel? Hotel { get; set; }
        }
    }