using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class Cita
    {
        public int id { get; set; }
        public string fecha { get; set; }
        public int duracion_minutos { get; set; }
        public string tipo_atencion{get;set;}
        public string estado { get; set; }
        public int calificacion { get; set; }
        public string profesional { get; set; }
        public string afiliado { get; set; }
        public string pago { get; set; }
        public int valor { get; set; }

    }
}