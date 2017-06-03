using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    public class PublicacionesController : Controller
    {
        private static ULSAVentasDataContext _db = ContextoEstatico.db;
        private static ULSAVentasDataContext db
        {
            get
            {
                return _db;
            }
        }

        // GET: Publicaciones
        public ActionResult Index()
        {
            Random r = new Random();
            List<Publicacion> listaPublicaciones = new List<Publicacion>();

            if (db.Publicacion.Count() > 0)
            {
                int id1 = r.Next(0, db.Publicacion.Count());
                int id2 = r.Next(0, db.Publicacion.Count());
                int id3 = r.Next(0, db.Publicacion.Count());

                listaPublicaciones.Add(db.Publicacion.First());
            }

            return View();
        }
    }
}