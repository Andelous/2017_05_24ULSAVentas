using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    [Authorize]
    public class CompraController : Controller
    {
        private ULSAVentasDataContext db
        {
            get
            {
                return ContextoEstatico.db;
            }
        }

        // GET: Compra
        public ActionResult Index(int idPublicacion)
        {
            Publicacion p = null;
            Usuario u = null;

            try
            {
                u = db.Usuario.First(u1 => u1.usuario1 == User.Identity.Name);
                p = db.Publicacion.First(p1 => p1.idPublicacion == idPublicacion);

                List<Direccion> direcciones = db.Direccion.Where(d => d.idPersona == u.Persona.idPersona).ToList();
                List<TarjetaDeCredito> tarjetas = db.TarjetaDeCredito.Where(t => t.idPersona == u.Persona.idPersona).ToList();

                TempData["compraDireccionesUsuario"] = direcciones;
                TempData["compraTarjetasUsuario"] = tarjetas;

            }
            catch (Exception)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(p);
        }

        [HttpPost]
        public ActionResult Comprar(int idPublicacion, int cantidad, int idTarjeta, int idDireccion)
        {
            return RedirectToAction("Compras", "Cuenta");
        }
    }
}