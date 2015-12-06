using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class Sancion
    {
        public int id { get; set; }
        public string afiliado { get; set; }
        public string fecha { get; set; }
        public int monto { get; set; }
        public string estado { get; set; }
        public string descripcion { get; set; }

    }
}