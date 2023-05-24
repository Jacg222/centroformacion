using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using centro.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace centro.Permisos
{
    public class permisosAttribute : ActionFilterAttribute
    {
        private readonly Rol _id_rol;

        public permisosAttribute(Rol id_rol)
        {
            _id_rol = id_rol;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("usuario") != null)
            {
                string usuarioJson = context.HttpContext.Session.GetString("usuario");
                AlumnosModel usuario = JsonConvert.DeserializeObject<AlumnosModel>(usuarioJson);
                // Realiza las acciones que necesites con el objeto usuario y el id_rol
                if (usuario.id_rol != _id_rol)
                {
                    context.Result = new RedirectResult("~/Home/sinpermiso");
                    // Lógica para manejar el caso en que el id_rol no coincida
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
