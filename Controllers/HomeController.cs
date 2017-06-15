using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    public class HomeController : Controller
    {
        private static ULSAVentasDataContext db
        {
            get
            {
                return ContextoEstatico.db;
            }
        }

        public ActionResult Index()
        {
            Random r = new Random();
            List<Publicacion> listaPublicaciones = new List<Publicacion>();

            try
            {
                if (db.Publicacion.Count() > 0)
                {
                    int maximo = db.Publicacion.Max(p => p.idPublicacion) + 1;

                    while (listaPublicaciones.Count < 3)
                    {
                        try
                        {
                            int id1 = r.Next(1, maximo);

                            Publicacion p1 = db.Publicacion.First(p => p.idPublicacion == id1);

                            listaPublicaciones.Add(p1);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return View(listaPublicaciones);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}