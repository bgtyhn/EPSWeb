using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class SolicitudRegistro
    {
        public int numero { get; set; }
        public string id { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int categoria { get; set; }
        public int edad { get; set; }
        public string estado { get; set; }
        public string correo { get; set; }
        public string cedula { get; set; }

    }
}