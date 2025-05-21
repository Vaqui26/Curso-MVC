using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Producto
    {
        private List<Producto> listaProductos;

        public List<Producto> listar()
        {
            listaProductos = new List<Producto>();

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("Select IdProducto, Nombre, p.Descripcion,");
                    sb.AppendLine("m.IdMarca, m.Descripcion[DesMarca],");
                    sb.AppendLine("c.IdCategoria, c.Descripcion[DesCategoria],");
                    sb.AppendLine("p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo");
                    sb.AppendLine("From PRODUCTO as p");
                    sb.AppendLine("join CATEGORIA as c on p.IdCategoria = c.IdCategoria");
                    sb.AppendLine("join MARCA as m on p.IdMarca = m.IdMarca");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaProductos.Add(
                               new Producto
                               {
                                   IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                   Nombre = dr["Nombre"].ToString(),    
                                   Descripcion = dr["Descripcion"].ToString(),
                                   oMarca = new Marca { IdMarca = Convert.ToInt32(dr["IdMarcar"]), Descripcion = dr["DesMarca"].ToString() },
                                   oCategoria = new Categoria { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion= dr["DesCategoria"].ToString() },
                                   Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-AR")),
                                   Stock = Convert.ToInt32(dr["Stock"]),
                                   RutaImagen = dr["RutaImagen"].ToString(),
                                   NombreImagen = dr["NombreImagen"].ToString(),
                                   Activo = bool.Parse(dr["Activo"].ToString())
                               }
                            );
                        }
                    }
                }
                return listaProductos;
            }
            catch (Exception)
            {
                return listaProductos;
            }
        }

        public int Registrar(Producto Producto, out string Mensaje)
        {

            int idAutogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", Producto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", Producto.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", Producto.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", Producto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", Producto.Precio);
                    cmd.Parameters.AddWithValue("Stock", Producto.Stock);
                    cmd.Parameters.AddWithValue("Activo", Producto.Activo);
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

        public bool Editar(Producto Producto, out string Mensaje)
        {

            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", Producto.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", Producto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", Producto.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", Producto.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", Producto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", Producto.Precio);
                    cmd.Parameters.AddWithValue("Stock", Producto.Stock);
                    cmd.Parameters.AddWithValue("Activo", Producto.Activo);
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

        public bool GuardarDatosImagen(Producto Producto, out string Mensaje)
        {
            bool resultado = false; 
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    string query = "Update PRODUCTO set RutaImagen = @RutaImagen, NombreImagen = @NombreImagen where IdProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@IdProducto", Producto.IdProducto);
                    cmd.Parameters.AddWithValue("@NombreImagen", Producto.NombreImagen);
                    cmd.Parameters.AddWithValue("@RutaImagen", Producto.RutaImagen);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se puede actualizar la imagen";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado=false;    
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oConexion);
                    cmd.Parameters.AddWithValue("IdProducto", id);
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
