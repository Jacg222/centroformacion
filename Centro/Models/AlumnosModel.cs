using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace centro.Models
{
    public class AlumnosModel
    {
        [Required(ErrorMessage = "El campo id_alumno es obligatorio")]
        public int id_alumno { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [RegularExpression(@"^[\p{L}\s,.'\p{M}]+$", ErrorMessage = "El campo nombre solo permite letras.")]
        [StringLength(50, ErrorMessage = "maximo 50 caracteres para el nombre")]

        public string? nombre { get; set; }

        [Required(ErrorMessage = "El campo correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        [Remote("VerificarExistenciaCorreo", "Acceso", ErrorMessage = "Este correo electrónico ya está registrado")]
        [StringLength(50, ErrorMessage = "maximo 50 caracteres para el correo")]
        public string? correo { get; set; }

        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        [RegularExpression(@"^[\p{L}\s,.'\p{M}]+$", ErrorMessage = "El campo nombre solo permite letras.")]
        [Remote("VerificarExistenciaUsuario", "Acceso", ErrorMessage = "Este usuario ya está registrado")]
        [StringLength(20, ErrorMessage = "maximo 50 caracteres para el usuario")]
        public string? usuario { get; set; }
        [StringLength(20, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres.")]
        public string? contraseña { get; set; }

        public string? tipo { get; set; }

        public Rol id_rol { get; set; }
    }
}
