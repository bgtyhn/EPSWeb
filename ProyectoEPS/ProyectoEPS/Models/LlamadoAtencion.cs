using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoEPS.Models
{
    public class LlamadoAtencion
    {
        public int id { get; set;  }
        public string afiliado { get; set; }
        public string fecha { get; set; }
        public string descripcion { get; set; }
        
    }
}