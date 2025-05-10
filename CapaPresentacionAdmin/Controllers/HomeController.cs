﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Usuarios()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            lista = new CN_Usuarios().Listar();

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarUsuario(Usuario user)
        {
            object resultado;
            string mensaje = string.Empty;  
            if(user.IdUsuario == 0)
            {
                resultado = new CN_Usuarios().Registrar(user,out mensaje);
            }
            else
            {
                resultado = new CN_Usuarios().Editar(user, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}