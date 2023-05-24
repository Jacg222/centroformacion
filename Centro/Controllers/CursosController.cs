using Microsoft.AspNetCore.Mvc;
using centro.Datos;
using centro.Models;
using Microsoft.AspNetCore.Authorization;

using centro.Permisos;
using Newtonsoft.Json;

namespace centro.Controllers
{
    [Authorize]
    public class CursosController : Controller
    {
        Cursosdata _cursosdata = new Cursosdata();

        [permisos(Rol.admin)]
        public IActionResult Listar()
        {
            var oLista = _cursosdata.Listar();
            return View(oLista);
        }

        [permisos(Rol.admin)]
        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        [permisos(Rol.admin)]
        public IActionResult Guardar(CursosModel oCurso)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool CursoExistente = _cursosdata.VerificarExistenciaCurso(oCurso.nombre);
            if (CursoExistente)
            {
                ModelState.AddModelError("nombre", "El curso ya existe");
                return View();
            }
            var respuesta = _cursosdata.Guardar(oCurso);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [permisos(Rol.admin)]
        public IActionResult Editar(int id_curso)
        {
            var ocurso = _cursosdata.Obtener(id_curso);
            return View(ocurso);
        }

        [HttpPost]
        [permisos(Rol.admin)]
        public IActionResult Editar(CursosModel oCurso)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // Obtener el alumno existente de la base de datos
            CursosModel cursoExistente = _cursosdata.Obtener(oCurso.id_curso);

            // Validar existencia de curso solo si el campo ha sido modificado y es diferente al valor existente
            if (oCurso.nombre != cursoExistente.nombre && _cursosdata.VerificarExistenciaCurso(oCurso.nombre))
            {
                ModelState.AddModelError("nombre", "El curso ya existe");
                return View();
            }

            var respuesta = _cursosdata.Editar(oCurso);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        [permisos(Rol.admin)]
        public IActionResult Eliminar(int id_curso)
        {
            var ocurso = _cursosdata.Obtener(id_curso);
            return View(ocurso);
        }

        [HttpPost]
        [permisos(Rol.admin)]
        public IActionResult Eliminar(CursosModel ocurso)
        {
            var respuesta = _cursosdata.Eliminar(ocurso.id_curso);
            if (respuesta)
                return RedirectToAction("Listar");
            else
                return View();
        }

        public IActionResult Listacursos()
        {
            var oLista = _cursosdata.Listarcursos();
            return View(oLista);
        }

        private int ObtenerIdAlumnoDeSesion()
        {
            // Verificar si el usuario está autenticado
            if (User.Identity.IsAuthenticated)
            {
                // Obtener la cadena JSON del objeto AlumnosModel de la sesión
                var usuarioJson = HttpContext.Session.GetString("usuario");

                if (!string.IsNullOrEmpty(usuarioJson))
                {
                    // Deserializar la cadena JSON en un objeto AlumnosModel
                    var alumno = JsonConvert.DeserializeObject<AlumnosModel>(usuarioJson);

                    // Obtener el valor de id_alumno del objeto AlumnosModel
                    return alumno.id_alumno;
                }
            }

            // Si no se puede obtener el id_alumno de la sesión, puedes devolver un valor predeterminado o lanzar una excepción según tus requisitos
            // En este ejemplo, se devuelve 0 si no se encuentra el id_alumno en la sesión
            return 0;
        }



        public IActionResult Agregar(int id_curso)
        {
            // Obtener el id_alumno de la sesión actual
            int id_alumno = ObtenerIdAlumnoDeSesion();

            // Verificar si el curso ya está matriculado por el alumno
            bool cursoMatriculado = _cursosdata.VerificarCursoMatriculado(id_alumno, id_curso);

            if (cursoMatriculado)
            {
                TempData["Mensaje"] = "El curso ya está matriculado.";
                return RedirectToAction("Listacursos");
            }

            // Agregar la matrícula
            bool matriculaAgregada = _cursosdata.AgregarMatricula(id_alumno, id_curso);

            if (matriculaAgregada)
            {
                TempData["Mensaje"] = "Matrícula agregada exitosamente.";
            }
            else
            {
                TempData["Mensaje"] = "No se pudo agregar la matrícula.";
            }

            return RedirectToAction("Listacursos");
        }
    }
}
