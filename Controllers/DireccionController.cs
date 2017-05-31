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
        private static ULSAVentasDataContext _db = new ULSAVentasDataContext();
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
        public ActionResult AgregarDireccionPost(Direccion d)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

                d.idPersona = usuario.Persona.idPersona;

                db.Direccion.InsertOnSubmit(d);
                db.SubmitChanges();

                ViewBag.cuentaDireccionAgregada = true;

                return PartialView();
            }

            return PartialView("AgregarDireccion");
        }

        [HttpGet]
        public ActionResult ModificarDireccion()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ModificarDireccionPost(Direccion d)
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult EliminarDireccion(int idDireccion)
        {
            return PartialView();
        }
    }
}