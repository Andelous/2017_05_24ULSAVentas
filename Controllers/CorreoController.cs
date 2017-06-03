using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    public class CorreoController : Controller
    {
        private static ULSAVentasDataContext _db = ContextoEstatico.db;
        private static ULSAVentasDataContext db
        {
            get
            {
                return _db;
            }
        }

        // CORREOS
        // Y sus acciones de agregar, modificar y eliminar
        public ActionResult Index()
        {
            Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

            return PartialView(db.Correo.Where(c => c.idPersona == usuario.Persona.idPersona).ToList());
        }

        [HttpGet]
        public ActionResult AgregarCorreo()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AgregarCorreo(Correo c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

                    c.idPersona = usuario.Persona.idPersona;

                    db.Correo.InsertOnSubmit(c);

                    db.SubmitChanges();
                    TempData["correoCorreoAgregado"] = true;
                }
                catch (Exception)
                {
                    TempData["correoCorreoAgregado"] = false;
                }

                return PartialView();
            }

            return PartialView(c);
        }

        [HttpGet]
        public ActionResult ModificarCorreo(int? idCorreo = null)
        {
            if (idCorreo != null)
            {
                Correo c = db.Correo.First(c1 => c1.idCorreo == idCorreo);
                return PartialView(c);
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult ModificarCorreo(Correo c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Correo c1 = db.Correo.First(c2 => c2.idCorreo == c.idCorreo);

                    c1.direccionDeCorreo = c.direccionDeCorreo;

                    db.SubmitChanges();
                    TempData["correoCorreoModificado"] = true;
                }
                catch (Exception)
                {
                    TempData["correoCorreoModificado"] = false;
                }

                return PartialView();
            }

            return PartialView(c);
        }

        [HttpGet]
        public ActionResult EliminarCorreo(int idCorreo)
        {
            try
            {
                Correo c = db.Correo.First(c1 => c1.idCorreo == idCorreo);
                db.Correo.DeleteOnSubmit(c);

                db.SubmitChanges();
                TempData["correoCorreoEliminado"] = true;
            }
            catch (Exception)
            {
                TempData["correoCorreoEliminado"] = false;
            }

            return PartialView();
        }
    }
}