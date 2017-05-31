using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    [Authorize]
    public class CuentaController : Controller
    {
        private static ULSAVentasDataContext _db = new ULSAVentasDataContext();
        private static ULSAVentasDataContext db
        {
            get
            {
                return _db;
            }
        }

        // GET: Cuenta
        [HttpGet]
        public ActionResult Index()
        {
            Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

            return View(usuario);
        }

        [HttpGet]
        public ActionResult Informacion()
        {
            Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

            return View(usuario.Persona);
        }

        [HttpPost]
        public ActionResult Informacion(Persona p)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

                usuario.Persona.apellidoMaterno = p.apellidoMaterno;
                usuario.Persona.apellidoPaterno = p.apellidoPaterno;
                usuario.Persona.nombres = p.nombres;

                try
                {
                    db.SubmitChanges();
                    ViewBag.cuentaPersonaModificada = true;
                }
                catch (Exception)
                {
                    ViewBag.cuentaPersonaModificada = false;
                }
            }

            return View(p);
        }

        public ActionResult Publicaciones()
        {
            return View();
        }

        public ActionResult Favoritos()
        {
            return View();
        }

        public ActionResult Compras()
        {
            return View();
        }

        // CORREOS
        // Y sus acciones de agregar, modificar y eliminar
        public ActionResult Correos()
        {
            Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

            return PartialView(usuario.Persona.Correo.ToList());
        }
    }
}