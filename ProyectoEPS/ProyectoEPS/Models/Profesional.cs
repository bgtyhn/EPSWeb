using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class Profesional
    {
        public string id { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string cedula { get; set; }
        public string correo { get; set; }
        public string area { get; set; }
    }
}