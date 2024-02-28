using bemtiviter.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bemtiviter.DTOs
{
    public class UsuarioDTO
    {
        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        [MinLength(3, ErrorMessage = "O nome do usuario deve ter ao menos 3 caracteres!")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(5000)]
        public string? Foto { get; set; } = string.Empty;
    }
}
