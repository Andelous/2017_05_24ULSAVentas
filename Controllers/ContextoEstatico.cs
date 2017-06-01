using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2017_05_24ULSAVentas.Controllers
{
    public class ContextoEstatico
    {

    }

    public static class MetodosExtension
    {
        public static string ToString(this Direccion d)
        {
            return d.calle + " " + d.numeroExterior + ", " + d.colonia + ", " + d.estado;
        }
    }
}