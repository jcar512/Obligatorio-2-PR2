using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obligatorio2.Models
    {
    public class Hotel
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelId { get; set; }

        [Required]
        [MaxLength(250)]
        public string? NombreHotel { get; set; }

        [Required]
        [MaxLength(250)]
        public string? Ciudad { get; set; }

        [Required]
        [MaxLength(250)]
        public string? Direccion { get; set; }

        [Required]
        [Range(0, 5)]
        public int Categoria { get; set; }

        [Required]
        [ForeignKey("PaisId")]
        public int PaisId { get; set; }

        public Pais? Pais { get; set; }

        public List<Servicio>? Servicios { get; set; }

        [Range(0, 20)]
        public List<Habitacion>? Habitaciones { get; set; }
        }
    }