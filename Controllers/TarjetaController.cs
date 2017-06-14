using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    [Authorize]
    public class TarjetaController : Controller
    {
        private ULSAVentasDataContext db
        {
            get
            {
                return ContextoEstatico.db;
            }
        }

        // Tarjetas
        // Y sus acciones de agregar, modificar y eliminar
        public ActionResult Index()
        {
            Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

            return PartialView(db.TarjetaDeCredito.Where(t => t.idPersona == usuario.Persona.idPersona).ToList());
        }

        [HttpGet]
        public ActionResult AgregarTarjeta()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AgregarTarjeta(TarjetaDeCredito t)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

                    t.idPersona = usuario.Persona.idPersona;

                    db.TarjetaDeCredito.InsertOnSubmit(t);

                    db.SubmitChanges();
                    TempData["tarjetaTarjetaAgregada"] = true;
                }
                catch (Exception)
                {
                    TempData["tarjetaTarjetaAgregada"] = false;
                }

                return PartialView();
            }

            return PartialView(t);
        }

        [HttpGet]
        public ActionResult ModificarTarjeta(int? idTarjeta = null)
        {
            if (idTarjeta != null)
            {
                TarjetaDeCredito t = db.TarjetaDeCredito.First(t1 => t1.idTarjeta == idTarjeta);
                return PartialView(t);
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult ModificarTarjeta(TarjetaDeCredito t)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TarjetaDeCredito t1 = db.TarjetaDeCredito.First(t2 => t2.idTarjeta == t.idTarjeta);

                    t1.numeroDeTarjeta = t.numeroDeTarjeta;

                    db.SubmitChanges();
                    TempData["tarjetaTarjetaModificada"] = true;
                }
                catch (Exception)
                {
                    TempData["tarjetaTarjetaModificada"] = false;
                }

                return PartialView();
            }

            return PartialView(t);
        }

        [HttpGet]
        public ActionResult EliminarTarjeta(int idTarjeta)
        {
            try
            {
                TarjetaDeCredito t = db.TarjetaDeCredito.First(t1 => t1.idTarjeta == idTarjeta);
                db.TarjetaDeCredito.DeleteOnSubmit(t);

                db.SubmitChanges();
                TempData["tarjetaTarjetaEliminada"] = true;
            }
            catch (Exception)
            {
                TempData["tarjetaTarjetaEliminada"] = false;
            }

            return PartialView();
        }
    }
}