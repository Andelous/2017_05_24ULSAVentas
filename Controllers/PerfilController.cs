using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    public class PerfilController : Controller
    {
        private static ULSAVentasDataContext db
        {
            get
            {
                return ContextoEstatico.db;
            }
        }

        // GET: Perfil
        public ActionResult Index(int idUsuario)
        {
            Usuario u = null;

            try
            {
                u = db.Usuario.First(u1 => u1.idUsuario == idUsuario);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(u);
        }

        public ActionResult RenderizarPerfil(int idUsuario)
        {
            Usuario u = null;

            try
            {
                u = db.Usuario.First(u1 => u1.idUsuario == idUsuario);

                List<Compra> compras = db.Compra.Where(c => c.idUsuario == u.idUsuario).ToList();
                List<Compra> ventas = db.Compra.Where(c => c.Publicacion.idUsuario == u.idUsuario).ToList();

                TempData["compras"] = compras.Count;
                TempData["ventas"] = ventas.Count;

                TempData["calificacionesCompradorPositivas"] = compras.Count(c => c.calificacionDeVenta == true);
                TempData["calificacionesCompradorNegativas"] = compras.Count(c => c.calificacionDeVenta == false);
                TempData["calificacionesCompradorNeutrales"] = compras.Count(c => c.calificacionDeVenta == null);

                TempData["calificacionesVendedorPositivas"] = ventas.Count(c => c.calificacionDeCompra == true);
                TempData["calificacionesVendedorNegativas"] = ventas.Count(c => c.calificacionDeCompra == false);
                TempData["calificacionesVendedorNeutrales"] = ventas.Count(c => c.calificacionDeCompra == null);

                TempData["publicacionesPerfil"] = db.Publicacion.Where(p => p.idUsuario == u.idUsuario).ToList();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

            return PartialView(u);
        }
    }
}