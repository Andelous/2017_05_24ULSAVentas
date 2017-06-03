using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    [Authorize]
    public class DireccionController : Controller
    {
        private static ULSAVentasDataContext _db = ContextoEstatico.db;
        private static ULSAVentasDataContext db
        {
            get
            {
                return _db;
            }
        }

        // GET: Direccion
        public ActionResult Index()
        {
            Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

            return PartialView(db.Direccion.Where(d => d.idPersona == usuario.Persona.idPersona).ToList());
        }

        [HttpGet]
        public ActionResult AgregarDireccion()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AgregarDireccion(Direccion d)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

                    d.idPersona = usuario.Persona.idPersona;

                    db.Direccion.InsertOnSubmit(d);

                    db.SubmitChanges();
                    TempData["direccionDireccionAgregada"] = true;
                }
                catch (Exception)
                {
                    TempData["direccionDireccionAgregada"] = false;
                }

                return PartialView();
            }

            return PartialView(d);
        }

        [HttpGet]
        public ActionResult ModificarDireccion(int? idDireccion = null)
        {
            if (idDireccion != null)
            {
                Direccion d = db.Direccion.First(d1 => d1.idDireccion == idDireccion);
                return PartialView(d);
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult ModificarDireccion(Direccion d)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Direccion d1 = db.Direccion.First(d2 => d2.idDireccion == d.idDireccion);

                    d1.calle = d.calle;
                    d1.codigoPostal = d.codigoPostal;
                    d1.colonia = d.colonia;
                    d1.estado = d.estado;
                    d1.latitud = d.latitud;
                    d1.longitud = d.longitud;
                    d1.numeroExterior = d.numeroExterior;
                    d1.numeroInterior = d.numeroInterior;
                    
                    db.SubmitChanges();
                    TempData["direccionDireccionModificada"] = true;
                }
                catch (Exception)
                {
                    TempData["direccionDireccionModificada"] = false;
                }

                return PartialView();
            }

            return PartialView(d);
        }

        [HttpGet]
        public ActionResult EliminarDireccion(int idDireccion)
        {
            try
            {
                Direccion d = db.Direccion.First(d1 => d1.idDireccion == idDireccion);
                db.Direccion.DeleteOnSubmit(d);

                db.SubmitChanges();
                TempData["direccionDireccionEliminada"] = true;
            }
            catch (Exception)
            {
                TempData["direccionDireccionEliminada"] = false;
            }

            return PartialView();
        }
    }
}