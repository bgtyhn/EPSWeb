using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class Curso
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string sitio { get; set; }
        public int maximo_personas { get; set; }
        public string profesional { get; set; }
        public string inscrito { get; set; }
        public string lleno { get; set; }
    }
}