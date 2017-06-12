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

        [HttpGet]
        [Authorize]
        public ActionResult Publicar()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Publicar(Publicacion p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Usuario u1 = db.Usuario.First(u => u.usuario1 == User.Identity.Name);
                    p.idUsuario = u1.idUsuario;

                    db.Publicacion.InsertOnSubmit(p);
                    db.SubmitChanges();

                    TempData["publicacionesPublicacionCorrecta"] = true;
                }
                catch (Exception)
                {
                    TempData["publicacionesPublicacionCorrecta"] = false;
                }
            }

            return View(p);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ModificarPublicacion(int? idPublicacion)
        {
            if (idPublicacion != null)
            {
                try
                {
                    Publicacion p = db.Publicacion.First(p1 => p1.idPublicacion == idPublicacion);
                    Usuario u1 = db.Usuario.First(u => u.usuario1 == User.Identity.Name);

                    if (p.idUsuario == u1.idUsuario)
                        return View(p);
                }
                catch (Exception)
                {
                    
                }
            }

            return RedirectToAction("Publicaciones", "Cuenta");
        }

        [HttpPost]
        [Authorize]
        public ActionResult ModificarPublicacion(Publicacion p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Publicacion p1 = db.Publicacion.First(p2 => p2.idPublicacion == p.idPublicacion);

                    p1.cantidad = p.cantidad;
                    p1.descripcion = p.descripcion;
                    p1.direccionImagen = p.direccionImagen;
                    p1.precio = p.precio;
                    p1.titulo = p.titulo;

                    db.SubmitChanges();

                    TempData["publicacionesPublicacionActualizada"] = true;
                }
                catch (Exception)
                {
                    TempData["publicacionesPublicacionActualizada"] = false;
                    return View(p);
                }

                return RedirectToAction("Publicaciones", "Cuenta");
            }
            else
            {
                return View(p);
            }
        }

        public ActionResult Eliminar(int? idPublicacion)
        {
            if (idPublicacion != null)
            {
                try
                {
                    Usuario u = db.Usuario.First(u1 => u1.usuario1 == User.Identity.Name);
                    Publicacion p = db.Publicacion.First(p1 => p1.idPublicacion == idPublicacion);

                    if (p.idUsuario == u.idUsuario)
                    {
                        db.Publicacion.DeleteOnSubmit(p);
                        db.SubmitChanges();

                        TempData["publicacionesPublicacionEliminada"] = true;
                    }
                }
                catch (Exception)
                {
                    TempData["publicacionesPublicacionEliminada"] = false;
                }
            }

            return RedirectToAction("Publicaciones", "Cuenta");
        }

        [HttpGet]
        public ActionResult Buscar(string q)
        {
            if (q == null || q.Trim() == "")
            {
                return Redirect(Request.UrlReferrer.ToString());
            }

            List<Publicacion> publicaciones = new List<Publicacion>();

            try
            {
                publicaciones = db.Publicacion.Where(p => p.titulo.Contains(q)).ToList();
            }
            catch (Exception)
            {
                
            }

            TempData["criterioDeBusqueda"] = q;

            return View(publicaciones);
        }
    }
}