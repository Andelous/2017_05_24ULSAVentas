﻿using _2017_05_24ULSAVentas.Models;
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
        private static ULSAVentasDataContext _db = ContextoEstatico.db;
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
                    TempData["cuentaPersonaModificada"] = true;
                }
                catch (Exception)
                {
                    TempData["cuentaPersonaModificada"] = false;
                }

                return RedirectToAction("Informacion", "Cuenta");
            }

            return View(p);
        }

        public ActionResult Publicaciones()
        {
            Usuario usuario = db.Usuario.First(u => u.usuario1 == User.Identity.Name);
            List<Publicacion> publicaciones = db.Publicacion.Where(p => p.idUsuario == usuario.idUsuario).ToList();

            return View(publicaciones);
        }

        public ActionResult Favoritos()
        {
            List<Publicacion> listaFavoritos = new List<Publicacion>();

            try
            {
                Usuario u = db.Usuario.First(u1 => u1.usuario1 == User.Identity.Name);
                List<Favorito> comodin = db.Favorito.Where(f => f.idUsuario == u.idUsuario).ToList();

                foreach (Favorito f in comodin)
                {
                    listaFavoritos.Add(db.Publicacion.First(p => p.idPublicacion == f.idPublicacion));
                }
            }
            catch (Exception)
            {
                
            }

            return View(listaFavoritos);
        }

        public ActionResult Compras()
        {
            List<Compra> compras = new List<Compra>();

            try
            {
                Usuario u = db.Usuario.First(u1 => u1.usuario1 == User.Identity.Name);
                compras = db.Compra.Where(c => c.idUsuario == u.idUsuario).ToList();
            }
            catch (Exception)
            {
                
            }

            return View(compras);
        }

        public ActionResult Ventas()
        {
            List<Compra> ventas = new List<Compra>();

            try
            {
                Usuario u = db.Usuario.First(u1 => u1.usuario1 == User.Identity.Name);
                ventas = db.Compra.Where(c => c.Publicacion.idUsuario == u.idUsuario).ToList();
            }
            catch (Exception)
            {

            }

            return View(ventas);
        }
    }
}