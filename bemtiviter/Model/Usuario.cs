using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bemtiviter.Model
{
    public class Usuario
    {
        public Usuario(string nome, string email, string? foto)
        {
            Nome = nome;
            Email = email;
            Foto = foto;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        [MinLength(3, ErrorMessage = "O nome do usuario deve ter ao menos 3 caracteres!")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(5000)]
        public string? Foto { get; set; } = string.Empty;
    }
}
