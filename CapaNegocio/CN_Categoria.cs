using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objCapaDatos = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objCapaDatos.listar();
        }
        public int Registrar(Categoria categoria, out string Mensaje)

        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
            {
                Mensaje = "La descripcion de la categoria no puede ser vacia!";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Registrar(categoria, out Mensaje);
            }
            else
            {
                return 0;
            }
        }
        public bool Editar(Categoria categoria, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(categoria.Descripcion) || string.IsNullOrWhiteSpace(categoria.Descripcion))
            {
                Mensaje = "La descripcion de la categoria no puede ser vacia!";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Editar(categoria, out Mensaje);
            }
            else
            {
                return false;
            }
        }
        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDatos.Eliminar(id, out Mensaje);
        }

    }
}
