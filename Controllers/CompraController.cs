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
            Publicacion p = null;
            TarjetaDeCredito t = null;
            Direccion d = null;
            Usuario u = null;

            Compra c = null;

            try
            {
                u = db.Usuario.First(u1 => u1.usuario1 == User.Identity.Name);
                p = db.Publicacion.First(p1 => p1.idPublicacion == idPublicacion);
                t = db.TarjetaDeCredito.First(t1 => t1.idTarjeta == idTarjeta);
                d = db.Direccion.First(d1 => d1.idDireccion == idDireccion);

                c = new Compra();

                c.cantidad = cantidad;
                c.fecha = DateTime.Now;
                c.idDireccion = d.idDireccion;
                c.idPublicacion = p.idPublicacion;
                c.idTarjeta = t.idTarjeta;
                c.idUsuario = u.idUsuario;

                p.cantidad -= cantidad;

                db.Compra.InsertOnSubmit(c);
                db.SubmitChanges();

                TempData["compraCompraRealizada"] = true;
            }
            catch (Exception)
            {
                TempData["compraCompraRealizada"] = false;
                return Redirect(Request.UrlReferrer.ToString());
            }

            return RedirectToAction("Compras", "Cuenta");
        }

        [HttpGet]
        public ActionResult VerDetalle(int idCompra)
        {
            Compra c = null;

            try
            {
                c = db.Compra.First(c1 => c1.idCompra == idCompra);
            }
            catch (Exception)
            {
                return RedirectToAction("Compras", "Cuenta");
            }

            return View(c);
        }

        [HttpPost]
        public ActionResult VerDetalle(int idCompra, bool? calificacionDeCompra, string comentarioDeCompra, bool? calificacionDeVenta, string comentarioDeVenta)
        {
            Compra c = null;

            try
            {
                c = db.Compra.First(c1 => c1.idCompra == idCompra);

                c.calificacionDeCompra = calificacionDeCompra;
                c.calificacionDeVenta = calificacionDeVenta;
                c.comentarioDeCompra = comentarioDeCompra;
                c.comentarioDeVenta = comentarioDeVenta;

                db.SubmitChanges();

                TempData["compraCompraActualizada"] = true;
            }
            catch (Exception)
            {
                TempData["compraCompraActualizada"] = false;
                return RedirectToAction("Compras", "Cuenta");
            }

            return View(c);
        }
    }
}