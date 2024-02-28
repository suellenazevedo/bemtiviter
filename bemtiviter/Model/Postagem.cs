using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bemtiviter.Model
{
    public class Postagem : Auditable
    {
        public Postagem(string titulo, string texto, long temaId, long usuarioId)
        {
            this.Titulo = titulo;
            this.Texto = texto;
            this.TemaId = temaId;
            this.UsuarioId = usuarioId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        [MinLength(5, ErrorMessage = "O titulo da postagem precisa ter pelo menos 5 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        [MinLength(10, ErrorMessage = "A postagem precisa ter no minimo 10 caracteres.")]
        public string Texto { get; set; } = string.Empty;

        [ForeignKey("Tema")]
        public long TemaId { get; set; } 
        public virtual Tema? Tema { get; set; }

        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
