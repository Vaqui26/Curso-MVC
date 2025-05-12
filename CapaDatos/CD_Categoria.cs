using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Categoria
    {
        private List<Categoria> listaCategorias;

        public List<Categoria> listar()
        {
            listaCategorias = new List<Categoria>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    string query = "Select IdCategoria, Descripcion, Activo From CATEGORIA";
                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaCategorias.Add(
                               new Categoria
                               {
                                   IdCategoria = int.Parse(dr["IdCategoria"].ToString()),
                                   Descripcion = dr["Descripcion"].ToString(),
                                   Activo = bool.Parse(dr["Activo"].ToString())
                               }
                            );
                        }
                    }
                }
                return listaCategorias;
            }
            catch (Exception)
            {
                return listaCategorias;
            }
        }
        public int Registrar(Categoria categoria, out string Mensaje)
        {

            int idAutogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", categoria.Activo);
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
        public bool Editar(Categoria categoria, out string Mensaje)
        {

            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("IdCategoria", categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", categoria.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", categoria.Activo);
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("IdCategoria", id);
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
    }
}
