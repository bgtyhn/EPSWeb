using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class Evento
    {

        public int id { get; set; }
        public string fecha { get; set; }
        public int curso { get; set; }
        public int duracion_minutos { get; set; }
    }
}