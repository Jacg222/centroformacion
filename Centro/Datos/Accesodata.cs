using centro.Models;
using System.Data.SqlClient;
using System.Data;

namespace centro.Datos
{
    public class Accesodata
    {
        public Boolean Guardar(AccesoModel oAlumno)
        {
            Boolean rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("guardar_alumnos", conexion);
                    cmd.Parameters.AddWithValue("nombre", oAlumno.nombre);
                    cmd.Parameters.AddWithValue("correo", oAlumno.correo);
                    cmd.Parameters.AddWithValue("usuario", oAlumno.usuario);
                    cmd.Parameters.AddWithValue("contraseña", oAlumno.contraseña);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }
        public bool VerificarExistenciaUsuario(string usuario)
        {
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("verificar_existencia_usuario", conexion);
                    cmd.Parameters.AddWithValue("usuario", usuario);
                    cmd.CommandType = CommandType.StoredProcedure;

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return false;
            }
        }
        public bool verificarExistenciaCorreo(string correo)
        {
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("verificar_existencia_correo", conexion);
                cmd.Parameters.AddWithValue("correo", correo);
                cmd.CommandType = CommandType.StoredProcedure;

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


    }
}
