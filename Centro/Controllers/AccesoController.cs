using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;

using centro.Models;
using centro.Logica;
using centro.Datos;
using System.Security.Cryptography;
using System.Text;

namespace centro.Controllers
{
    public class AccesoController : Controller
    {
        Accesodata _alumnosdata = new Accesodata();
        Alumnosdata _alumnosdatas = new Alumnosdata();
        private dynamic mensaje;

        public IActionResult Index()
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string usuario, string contraseña)
        {
            string contraseñaEncriptada = GetSha256(contraseña);

            AlumnosModel objeto = new Lo_usuario().EncontrarUsuario(usuario, contraseñaEncriptada);
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                // Redirigir al índice con el mensaje de contraseña incorrecta
                return RedirectToAction("Index", new { Mensaje = "Contraseña incorrecta" });

            }
            if (objeto == null)
                  {
                return RedirectToAction("Index", new { Mensaje = "Contraseña incorrecta" });

            }
            if (objeto != null)
            {
                string json = JsonConvert.SerializeObject(objeto);
                HttpContext.Session.SetString("usuario", json);

                var claims = new[]
                {
                        new Claim(ClaimTypes.Name, objeto.nombre ?? string.Empty),
                        new Claim("id_alumno", objeto.id_alumno.ToString() ?? string.Empty)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (objeto.id_rol == Rol.admin) // Aquí se compara el valor del enumerador
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (objeto.id_rol == Rol.alumno)
                {
                    return RedirectToAction("Listacursos", "Cursos");
                }
            }

            return RedirectToAction("Index", new { Mensaje = "Contraseña incorrecta" });

        }
        public IActionResult Guardar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Guardar(AccesoModel oAlumno)
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

            // Encriptar la contraseña antes de guardarla
            oAlumno.contraseña = GetSha256(oAlumno.contraseña);

            var respuesta = _alumnosdata.Guardar(oAlumno);

            if (respuesta)
                return RedirectToAction("Index");
            else
                return View();
        }

        [HttpGet]
        public ActionResult startRecovery()
        {
            var model = new AccesoModel(); // Crear una instancia del modelo AccesoModel
            return View(model);
        }





        public ActionResult Recovery()
        {
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
