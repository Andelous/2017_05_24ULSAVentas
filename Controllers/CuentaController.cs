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
        public ActionResult Index()
        {
            AspNetUsers usuarioAsp = db.AspNetUsers.First(ua => ua.UserName == User.Identity.Name);
            Usuario usuario = db.Usuario.First(u => u.Id == usuarioAsp.Id);

            return View(usuario);
        }
    }
}