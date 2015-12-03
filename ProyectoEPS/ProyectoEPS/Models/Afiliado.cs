using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class Afiliado
    {
        public string id { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int edad { get; set; }
        public string cedula { get; set; }
        public string fecha_afiliacion { get; set; }
        public string estado { get; set; }
        public int categoria { get; set; }
    }
}