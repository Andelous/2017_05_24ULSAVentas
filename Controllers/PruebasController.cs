using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    public class PruebasController : Controller
    {
        private static ULSAVentasDataContext _db = ContextoEstatico.db;
        private static ULSAVentasDataContext db
        {
            get
            {
                return _db;
            }
        }


        // GET: Pruebas
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Direccion.ToList());
        }

        public ActionResult MapaParcial(string lat, string lng)
        {
            string[] s = { lat, lng };
            return PartialView(s);
        }
    }
}