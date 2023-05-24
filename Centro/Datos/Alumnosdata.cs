using centro.Models;
using System.Data.SqlClient;
using System.Data;

namespace centro.Datos
{
    public class Alumnosdata
    {
        public List<AlumnosModel> Listar()
        {
            var oLista = new List<AlumnosModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("listar_alumnos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new AlumnosModel()
                        {
                            id_alumno = Convert.ToInt32(dr["id_alumno"]),
                            nombre = dr["nombre"].ToString(),
                            correo = dr["correo"].ToString(),
                            usuario = dr["usuario"].ToString(),
                            tipo = dr["tipo"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }


        public AlumnosModel Obtener(int id_alumno)
        {
            var oAlumno = new AlumnosModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("obtener_alumnos", conexion);
                cmd.Parameters.AddWithValue("id_alumno", id_alumno);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oAlumno.id_alumno = Convert.ToInt32(dr["id_alumno"]);
                        oAlumno.nombre = dr["nombre"].ToString();
                        oAlumno.correo = dr["correo"].ToString();
                        oAlumno.usuario = dr["usuario"].ToString();
                    }
                }
            }
            return oAlumno;
        }


        public Boolean Guardar(AlumnosModel oAlumno)
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


        public Boolean Editar(AlumnosModel oAlumno)
        {
            Boolean rpta;

            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("editar_alumnos", conexion);
                    cmd.Parameters.AddWithValue("id_alumno", oAlumno.id_alumno);
                    cmd.Parameters.AddWithValue("nombre", oAlumno.nombre);
                    cmd.Parameters.AddWithValue("correo", oAlumno.correo);
                    cmd.Parameters.AddWithValue("usuario", oAlumno.usuario);
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


        public Boolean Eliminar(int id_alumno)
        {
            Boolean rpta;

            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("eliminar_alumnos", conexion);
                    cmd.Parameters.AddWithValue("id_alumno", id_alumno);
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

        public Boolean AgregarToken(string correo, string token)
        {
            Boolean rpta;

            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("agregar_token", conexion);
                    cmd.Parameters.AddWithValue("correo", correo);
                    cmd.Parameters.AddWithValue("token", token);
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

    }
}
