using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDatos = new CD_Usuarios();

        public List<Usuario> Listar()
        {
            return objCapaDatos.listar();   
        }
        public int Registrar(Usuario usuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(string.IsNullOrEmpty(usuario.Nombres) || string.IsNullOrWhiteSpace(usuario.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacio!";
            }
            else if (string.IsNullOrEmpty(usuario.Apellidos) || string.IsNullOrWhiteSpace(usuario.Apellidos))
            {
                Mensaje = "El apellido del usuario no puede ser vacio!";
            }
            else if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio!";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                //Creacion del mail, y enviado de clave encriptada.
                string clave = "test123";
                usuario.Clave = CN_Recursos.ConvertidorSha256(clave);  

                return objCapaDatos.Registrar(usuario, out Mensaje);
            }
            return 0;    
        }
        public bool Editar(Usuario usuario, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Nombres) || string.IsNullOrWhiteSpace(usuario.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacio!";
            }
            else if (string.IsNullOrEmpty(usuario.Apellidos) || string.IsNullOrWhiteSpace(usuario.Apellidos))
            {
                Mensaje = "El apellido del usuario no puede ser vacio!";
            }
            else if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio!";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Editar(usuario, out Mensaje);
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
