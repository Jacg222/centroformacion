using centro.Models;
using System.Data.SqlClient;
using System.Data;

namespace centro.Datos
{
    public class Cursosdata
    {
        public List<CursosModel> Listar()
        {
            var oLista = new List<CursosModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("listar_cursos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr  = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new CursosModel()
                        {
                            id_curso = Convert.ToInt32(dr["id_curso"]),
                            nombre = dr["nombre"].ToString(),
                            descripcion = dr["descripcion"].ToString(),
                            temario = dr["temario"].ToString()
                        });
                    }
                }
            }
        return oLista;
        }


        public CursosModel Obtener(int id_curso)
        {
            var oCurso = new CursosModel();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("obtener_cursos", conexion);
                cmd.Parameters.AddWithValue("id_curso", id_curso);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oCurso.id_curso = Convert.ToInt32(dr["id_curso"]);
                        oCurso.nombre = dr["nombre"].ToString();
                        oCurso.descripcion = dr["descripcion"].ToString();
                        oCurso.temario = dr["temario"].ToString();
                    }
                }
            }
            return oCurso;
        }


        public Boolean Guardar(CursosModel oCurso)
        {
            Boolean rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("guardar_cursos", conexion);
                    cmd.Parameters.AddWithValue("nombre", oCurso.nombre);
                    cmd.Parameters.AddWithValue("descripcion", oCurso.descripcion);
                    cmd.Parameters.AddWithValue("temario", oCurso.temario);
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


        public Boolean Editar(CursosModel oCurso)
        {
            Boolean rpta;

            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("editar_cursos", conexion);
                    cmd.Parameters.AddWithValue("id_curso", oCurso.id_curso);
                    cmd.Parameters.AddWithValue("nombre", oCurso.nombre);
                    cmd.Parameters.AddWithValue("descripcion", oCurso.descripcion);
                    cmd.Parameters.AddWithValue("temario", oCurso.temario);
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


        public Boolean Eliminar(int id_curso)
        {
            Boolean rpta;

            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("eliminar_cursos", conexion);
                    cmd.Parameters.AddWithValue("id_curso", id_curso);
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

        public bool VerificarExistenciaCurso(string curso)
        {
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("verificar_existencia_curso", conexion);
                    cmd.Parameters.AddWithValue("curso", curso);
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

        public List<CursosModel> Listarcursos()
        {
            var oLista = new List<CursosModel>();
            var cn = new Conexion();
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("listar_cursos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new CursosModel()
                        {
                            id_curso = Convert.ToInt32(dr["id_curso"]),
                            nombre = dr["nombre"].ToString(),
                            descripcion = dr["descripcion"].ToString(),
                            temario = dr["temario"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        public bool AgregarMatricula(int id_alumno, int id_curso)
        {
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("agregar_curso", conexion);
                    cmd.Parameters.AddWithValue("id_alumno", id_alumno);
                    cmd.Parameters.AddWithValue("id_curso", id_curso);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return false;
            }
        }

        public bool VerificarCursoMatriculado(int id_alumno, int id_curso)
        {
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("verificar_curso_matriculado", conexion);
                    cmd.Parameters.AddWithValue("id_alumno", id_alumno);
                    cmd.Parameters.AddWithValue("id_curso", id_curso);
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

    }
} 
