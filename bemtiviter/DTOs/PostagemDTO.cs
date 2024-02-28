using bemtiviter.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bemtiviter.DTOs
{
    public class PostagemDTO
    {
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

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do tema não pode ser negativo ou zero.")]
        public int TemaId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do usuario não pode ser negativo ou zero.")]
        public int UsuarioId{ get; set; }
    }
}
