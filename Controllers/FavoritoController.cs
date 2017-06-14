using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    [Authorize]
    public class FavoritoController : Controller
    {
        private static ULSAVentasDataContext db
        {
            get
            {
                return ContextoEstatico.db;
            }
        }

        // GET: Favorito
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarFavorito(int idPublicacion)
        {
            Publicacion p = null;
            Usuario u = null;
            Favorito f = null;

            try
            {
                u = db.Usuario.First(u1 => u1.usuario1 == User.Identity.Name);
                p = db.Publicacion.First(p1 => p1.idPublicacion == idPublicacion);

                try
                {
                    f = db.Favorito.First(f1 => f1.idPublicacion == p.idPublicacion && f1.idUsuario == u.idUsuario);
                }
                catch (Exception)
                {
                    
                }

                if (f == null)
                {
                    f = new Favorito();

                    f.idPublicacion = p.idPublicacion;
                    f.idUsuario = u.idUsuario;

                    db.Favorito.InsertOnSubmit(f);
                    db.SubmitChanges();

                    TempData["favoritoFavoritoAgregado"] = true;
                }
            }
            catch (Exception)
            {
                TempData["favoritoFavoritoAgregado"] = false;
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Eliminar(int idPublicacion)
        {
            Publicacion p = null;
            Usuario u = null;
            Favorito f = null;

            try
            {
                u = db.Usuario.First(u1 => u1.usuario1 == User.Identity.Name);
                p = db.Publicacion.First(p1 => p1.idPublicacion == idPublicacion);

                f = db.Favorito.First(f1 => f1.idPublicacion == p.idPublicacion && f1.idUsuario == u.idUsuario);

                db.Favorito.DeleteOnSubmit(f);
                db.SubmitChanges();

                TempData["favoritoFavoritoEliminado"] = true;
            }
            catch (Exception)
            {
                TempData["favoritoFavoritoEliminado"] = false;
            }

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}