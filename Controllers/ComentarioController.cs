using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2017_05_24ULSAVentas.Controllers
{
    [Authorize]
    public class ComentarioController : Controller
    {
        private static ULSAVentasDataContext _db = ContextoEstatico.db;
        private static ULSAVentasDataContext db
        {
            get
            {
                return _db;
            }
        }

        // GET: Comentario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult VerComentarios(int idPublicacion)
        {
            Publicacion p = null;
            List<Comentario> comentarios = new List<Comentario>();

            try
            {
                p = db.Publicacion.First(p1 => p1.idPublicacion == idPublicacion);
                comentarios = db.Comentario.Where(c => c.idPublicacion == p.idPublicacion).OrderByDescending(c => c.idComentario).ToList();
            }
            catch (Exception)
            {
                
            }

            return PartialView(comentarios);
        }

        [HttpPost]
        public ActionResult VerComentarios(string comentario, int idPublicacion)
        {
            try
            {
                Usuario u = db.Usuario.First(u1 => u1.usuario1 == User.Identity.Name);

                Comentario c = new Comentario();

                c.comentario1 = comentario;
                c.fecha = DateTime.Now;
                c.idPublicacion = idPublicacion;
                c.idUsuario = u.idUsuario;

                db.Comentario.InsertOnSubmit(c);
                db.SubmitChanges();

                TempData["comentarioComentarioAgregado"] = true;
            }
            catch (Exception)
            {
                TempData["comentarioComentarioAgregado"] = false;
                throw;
            }

            return RedirectToAction("VerPublicacion", "Publicaciones", new { idPublicacion = idPublicacion});
        }
    }
}