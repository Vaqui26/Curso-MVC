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
               
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Creación de Cuenta";
                string mensajeCorreo = "<h3>Su cuenta fue creada correctamente</h3><br><p>Su contraseña para acceder es: !clave!</p>";
                mensajeCorreo = mensajeCorreo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(usuario.Correo, asunto, mensajeCorreo);

                if (respuesta)
                {
                    usuario.Clave = CN_Recursos.ConvertidorSha256(clave);
                    return objCapaDatos.Registrar(usuario, out Mensaje);

                }
                else
                {
                    Mensaje = "No se puede enviar el correo!";
                    return 0;
                }

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
