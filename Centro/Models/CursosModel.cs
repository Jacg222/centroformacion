using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace centro.Models
{
    public class CursosModel
    {
        public int id_curso { get; set; }
        [Required(ErrorMessage ="El campo nombre es obligatorio")]
        [Remote("VerificarExistenciaCurso", "Acceso", ErrorMessage = "Este curso ya está registrado")]
        [RegularExpression(@"^[\p{L}\s,.'\p{M}]+$", ErrorMessage = "El campo nombre solo permite letras.")]

        public string? nombre { get; set; }
        [StringLength(100, ErrorMessage = "maximo 100 caracteres para la descripcion")]
        [Required(ErrorMessage = "El campo descripcion es obligatorio")]
        [RegularExpression(@"^[\p{L}\s,.'\p{M}]+$", ErrorMessage = "El campo nombre solo permite letras.")]
        public string? descripcion { get; set; }
        [Required(ErrorMessage = "El campo temario es obligatorio")]
        [StringLength(250, ErrorMessage = "maximo 100 caracteres para el temario")]
        [RegularExpression(@"^[\p{L}\s,.'\p{M}]+$", ErrorMessage = "El campo nombre solo permite letras.")]

        public string? temario { get; set; }
    }
}
