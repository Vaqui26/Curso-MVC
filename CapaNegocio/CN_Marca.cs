using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Marca
    {
        private CD_Marca objCapaDatos = new CD_Marca();

        public List<Marca> Listar()
        {
            return objCapaDatos.listar();
        }
        public int Registrar(Marca marca, out string Mensaje)

        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || string.IsNullOrWhiteSpace(marca.Descripcion))
            {
                Mensaje = "La descripcion de la marca no puede ser vacia!";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Registrar(marca, out Mensaje);
            }
            else
            {
                return 0;
            }
        }
        public bool Editar(Marca marca, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || string.IsNullOrWhiteSpace(marca.Descripcion))
            {
                Mensaje = "La descripcion de la marca no puede ser vacia!";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Editar(marca, out Mensaje);
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
