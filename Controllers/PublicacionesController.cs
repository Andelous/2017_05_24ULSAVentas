using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Linq;

namespace _2017_05_24ULSAVentas.Controllers
{
    [Authorize]
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
        public ActionResult Publicar()
        {
            return View();
        }

        [HttpPost]
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
                    return View(p);
                }

                return RedirectToAction("Publicaciones", "Cuenta");
            }
            else
            {
                return View(p);
            }
        }

        [HttpGet]
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

        [HttpGet]
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
        [AllowAnonymous]
        public ActionResult Buscar(string q)
        {
            if (q == null || q.Trim() == "")
            {
                return Redirect(Request.UrlReferrer.ToString());
            }

            List<Publicacion> publicaciones = new List<Publicacion>();

            try
            {
                publicaciones = db.Publicacion.Where(p => p.titulo.Contains(q.Trim())).ToList();
            }
            catch (Exception)
            {
                
            }

            TempData["criterioDeBusqueda"] = q;

            return View(publicaciones);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult VerPublicacion(int? idPublicacion)
        {
            if (idPublicacion == null)
                return RedirectToAction("Index", "Home");

            Publicacion p = null;
            Direccion d = null;

            try
            {
                p = db.Publicacion.First(p1 => p1.idPublicacion == idPublicacion);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                d = p.Usuario.Persona.Direccion.First(d1 => true);
            }
            catch (Exception)
            {
                
            }

            TempData["publicacionesDireccion"] = d;

            return View(p);
        }
    }
}