using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bemtiviter.Model
{
    public class Tema
    {
        public Tema(string descricao)
        {
            Descricao = descricao;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(257)]
        [MinLength(3, ErrorMessage = "O tema tem que ter no mínimo 3 caracteres.")]
        public string Descricao { get; set; } = string.Empty;
    }
}
