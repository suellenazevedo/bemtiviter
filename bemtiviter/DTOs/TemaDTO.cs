using bemtiviter.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bemtiviter.DTOs
{
    public class TemaDTO
    {
        [Column(TypeName = "varchar")]
        [StringLength(257)]
        [MinLength(3, ErrorMessage = "O tema tem que ter no mínimo 3 caracteres.")]
        public string Descricao { get; set; } = string.Empty;
    }
}
