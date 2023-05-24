using centro.Datos;
using centro.Models;
using centro.Permisos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace centro.Controllers
{
    [permisos(Rol.admin)]
    [Authorize]
    public class AlumnosController : Controller
    {
        Alumnosdata _alumnosdata = new Alumnosdata();
       
        public IActionResult Listar()
        {
            var oLista = _alumnosdata.Listar();
            return View(oLista);
        }
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(AlumnosModel oAlumno)
        {
             if (!ModelState.IsValid)
            {
                return View();
            }
            // Verificar si el usuario ya existe en la base de datos
            bool usuarioExistente = _alumnosdata.VerificarExistenciaUsuario(oAlumno.usuario);
            bool correoExistente = _alumnosdata.verificarExistenciaCorreo(oAlumno.correo);

            if (usuarioExistente)
            {
                ModelState.AddModelError("usuario", "El usuario ya existe");
                return View();
            }
            if (correoExistente)
            {
                ModelState.AddModelError("correo", "El correo ya existe");
                return View();
            }
            oAlumno.contraseña = GetSha256(oAlumno.contraseña);
            var respuesta = _alumnosdata.Guardar(oAlumno);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Editar(int id_alumno)
        {
            var oAlumno = _alumnosdata.Obtener(id_alumno);
            return View(oAlumno);
        }

        [HttpPost]
        public IActionResult Editar(AlumnosModel oAlumno)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Obtener el alumno existente de la base de datos
            AlumnosModel alumnoExistente = _alumnosdata.Obtener(oAlumno.id_alumno);

            // Validar existencia de usuario solo si el campo ha sido modificado y es diferente al valor existente
            if (oAlumno.usuario != alumnoExistente.usuario && _alumnosdata.VerificarExistenciaUsuario(oAlumno.usuario))
            {
                ModelState.AddModelError("usuario", "El usuario ya existe");
                return View();
            }

            // Validar existencia de correo solo si el campo ha sido modificado y es diferente al valor existente
            if (oAlumno.correo != alumnoExistente.correo && _alumnosdata.verificarExistenciaCorreo(oAlumno.correo))
            {
                ModelState.AddModelError("correo", "El correo ya existe");
                return View();
            }

            var respuesta = _alumnosdata.Editar(oAlumno);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }



        public IActionResult Eliminar(int id_alumno)
        {
            var oAlumno = _alumnosdata.Obtener(id_alumno);
            return View(oAlumno);
        }
        [HttpPost]
        public IActionResult Eliminar(AlumnosModel oAlumno)
        {
            var respuesta = _alumnosdata.Eliminar(oAlumno.id_alumno);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        private string GetSha256(string str)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();

                foreach (byte hashByte in hashBytes)
                {
                    builder.Append(hashByte.ToString("x2"));
                }

                return builder.ToString();
            }
        }

    }
}
