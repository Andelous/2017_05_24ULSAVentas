using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    [Authorize]
    public class TelefonoController : Controller
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

            return PartialView(db.Telefono.Where(t => t.idPersona == usuario.Persona.idPersona).ToList());
        }

        [HttpGet]
        public ActionResult AgregarTelefono()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AgregarTelefono(Telefono t)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

                    t.idPersona = usuario.Persona.idPersona;

                    db.Telefono.InsertOnSubmit(t);

                    db.SubmitChanges();
                    TempData["telefonoTelefonoAgregado"] = true;
                }
                catch (Exception)
                {
                    TempData["telefonoTelefonoAgregado"] = false;
                }

                return PartialView();
            }

            return PartialView(t);
        }

        [HttpGet]
        public ActionResult ModificarTelefono(int? idTelefono = null)
        {
            if (idTelefono != null)
            {
                Telefono t = db.Telefono.First(t1 => t1.idTelefono == idTelefono);
                return PartialView(t);
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult ModificarTelefono(Telefono t)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Telefono t1 = db.Telefono.First(t2 => t2.idTelefono == t.idTelefono);

                    t1.numero = t.numero;

                    db.SubmitChanges();
                    TempData["telefonoTelefonoModificado"] = true;
                }
                catch (Exception)
                {
                    TempData["telefonoTelefonoModificado"] = false;
                }

                return PartialView();
            }

            return PartialView(t);
        }

        [HttpGet]
        public ActionResult EliminarTelefono(int idTelefono)
        {
            try
            {
                Telefono t = db.Telefono.First(t1 => t1.idTelefono == idTelefono);
                db.Telefono.DeleteOnSubmit(t);

                db.SubmitChanges();
                TempData["telefonoTelefonoEliminado"] = true;
            }
            catch (Exception)
            {
                TempData["telefonoTelefonoEliminado"] = false;
            }

            return PartialView();
        }
    }
}