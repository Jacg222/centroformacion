using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace centro.Models
    {
        public class AccesoModel
        {
            public int id_alumno { get; set; }

            [Required(ErrorMessage = "El campo nombre es obligatorio.")]
            [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "El campo nombre solo permite letras y espacios.")]
            public string nombre { get; set; }

            [Required(ErrorMessage = "El campo correo es obligatorio.")]
            [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
            [Remote("VerificarExistenciaCorreo", "Acceso", ErrorMessage = "Este correo electrónico ya está registrado.")]
            public string correo { get; set; }

            [Required(ErrorMessage = "El campo usuario es obligatorio.")]
            [Remote("VerificarExistenciaUsuario", "Acceso", ErrorMessage = "Este usuario ya está registrado.")]
            public string usuario { get; set; }

            [Required(ErrorMessage = "El campo contraseña es obligatorio.")]
            [StringLength(20, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 20 caracteres.")]
            public string contraseña { get; set; }

            public string tipo { get; set; }

            public Rol id_rol { get; set; }
        }
    }

