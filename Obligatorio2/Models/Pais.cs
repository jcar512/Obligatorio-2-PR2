using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obligatorio2.Models
    {
    [Index(nameof(NombrePais), IsUnique = true)]
    public class Pais
        {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaisId { get; set; }

        [Required]
        [MaxLength(250)]
        public string? NombrePais { get; set; }

        public List<Usuario>? Usuarios { get; set; }

        public List<Hotel>? Hoteles { get; set; }
        }
    }