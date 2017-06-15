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
                u = db.Usuario.First();
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
                u = db.Usuario.First();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

            return PartialView(u);
        }
    }
}