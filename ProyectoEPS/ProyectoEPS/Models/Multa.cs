using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class Multa
    {
        public int id { get; set; }
        public int valor { get; set; }
        public int cita { get; set; }
        public string estado { get; set; }
    }
}