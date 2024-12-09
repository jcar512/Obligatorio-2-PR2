using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obligatorio2.Models
    {
    [Index(nameof(Email), IsUnique = true)]
    public class Usuario
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Nombre { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Apellido { get; set; }

        [Required]
        public string? TipoDocumento { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9]{6,9}$",
            ErrorMessage = "El formato del CI/pasaporte es inválido.")]
        public string? NumeroDocumento { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}$",
            ErrorMessage = "Debe ingresar un número de teléfono válido.")]
        public string? Telefono { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Contrasenia { get; set; }

        [Required]
        [ForeignKey("PaisId")]
        public int PaisId { get; set; }

        public Pais? Pais { get; set; }

        public List<Reserva>? Reservas { get; set; }
        }
    }