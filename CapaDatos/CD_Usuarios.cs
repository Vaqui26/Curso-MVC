using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        private List<Usuario> listasUsuarios;
        public List<Usuario> listar()
        {
            listasUsuarios = new List<Usuario>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    string query = "Select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo From USUARIO";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listasUsuarios.Add(
                                new Usuario()
                                {
                                    IdUsuario = int.Parse(dr["IdUsuario"].ToString()),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Clave = dr["Clave"].ToString(),
                                    Reestablecer = bool.Parse(dr["Reestablecer"].ToString()),
                                    Activo = bool.Parse(dr["Activo"].ToString())
                                }
                            );
                        }
                    }
                }
                return listasUsuarios;
            }
            catch (Exception)
            {
                return listasUsuarios;
            }
        }
        public int Registrar(Usuario usuario, out string Mensaje)
        {

            int idAutogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("Nombres", usuario.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("Clave", usuario.Clave);
                    cmd.Parameters.AddWithValue("Activo", usuario.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    idAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                Mensaje = ex.Message;
            }
            return idAutogenerado;

        }
        public bool Editar(Usuario usuario, out string Mensaje)
        {

            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombres", usuario.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", usuario.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("Activo", usuario.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            return resultado;

        }
        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("Delete top(1) from USUARIO where IdUsuario = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
