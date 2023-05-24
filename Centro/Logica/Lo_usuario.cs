using centro.Models;
using centro.Datos;
using System.Data.SqlClient;
using System.Data;

namespace centro.Logica
{
    public class Lo_usuario
    {
        public AlumnosModel EncontrarUsuario(string usuario, string contraseña)
        {
            AlumnosModel objeto = new AlumnosModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("obtener_alumno_por_credenciales", conexion);
                cmd.Parameters.AddWithValue("usuario", usuario);
                cmd.Parameters.AddWithValue("contraseña", contraseña);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        objeto = new AlumnosModel()
                        {
                            id_alumno = (int)dr["id_alumno"],
                            nombre = dr["nombre"].ToString(),
                            correo = dr["correo"].ToString(),
                            usuario = dr["usuario"].ToString(),
                            contraseña = dr["contraseña"].ToString(),
                            id_rol = (Rol)dr["id_rol"]
                        };
                    }
                }
            }
            return objeto;
        }
    }
}
