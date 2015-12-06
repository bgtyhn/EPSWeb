using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class Diagnostico
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public int cita { get; set; }
    }
}