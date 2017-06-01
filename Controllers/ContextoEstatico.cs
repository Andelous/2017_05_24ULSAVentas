﻿using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2017_05_24ULSAVentas.Controllers
{
    public static class ContextoEstatico
    {
        private static ULSAVentasDataContext _db = new ULSAVentasDataContext();
        public static ULSAVentasDataContext db
        {
            get
            {
                return _db;
            }
        }
    }

    public static class MetodosExtension
    {
        public static string ToString(this Direccion d)
        {
            return d.calle + " " + d.numeroExterior + ", " + d.colonia + ", " + d.estado;
        }
    }
}