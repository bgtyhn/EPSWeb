using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class Noticia
    {
        public int id { get; set; }
        public string fecha_publicacion{get;set;}
        public string fecha_expiracion { get; set; }
        public string contenido { get; set; }
        public string encabezado { get; set; }
        public string titulo { get; set; }
    }
}