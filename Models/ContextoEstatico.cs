using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2017_05_24ULSAVentas.Models
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
        public static string getString(this Direccion d)
        {
            return d.calle + " " + d.numeroExterior + ", " + d.colonia + ", " + d.estado;
        }

        public static bool igual(this Publicacion p, object o)
        {
            if (o != null)
            {
                if (o.GetType() == typeof(Publicacion))
                {
                    Publicacion p1 = (Publicacion)o;

                    if (p.idPublicacion == p1.idPublicacion)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}