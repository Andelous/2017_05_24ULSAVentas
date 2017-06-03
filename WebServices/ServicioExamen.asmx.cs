using _2017_05_24ULSAVentas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace _2017_05_24ULSAVentas.WebServices
{
    /// <summary>
    /// Descripción breve de ServicioExamen
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioExamen : System.Web.Services.WebService
    {
        private static ULSAVentasDataContext _db = ContextoEstatico.db;
        private static ULSAVentasDataContext db
        {
            get
            {
                return _db;
            }
        }

        [WebMethod]
        public string DevolverCadena()
        {
            return "Ejercicio de servicio web de cadena.";
        }

        [WebMethod]
        public double calcularTotal(int idPublicacion, int cantidad)
        {
            Publicacion p = db.Publicacion.First(p1 => p1.idPublicacion == idPublicacion);
            
            return ((double)p.precio) * ((double)cantidad);
        }

        [WebMethod]
        public List<string> listaModelo(string coincidencia)
        {
            List<Direccion> direcciones = db.Direccion.Where(d => d.calle.Contains(coincidencia)).ToList();
            List<string> listaStrings = new List<string>();

            foreach (Direccion d in direcciones)
            {
                listaStrings.Add(d.getString());
            }

            return listaStrings;
        }
    }
}
