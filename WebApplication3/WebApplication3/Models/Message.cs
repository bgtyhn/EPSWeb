using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Message
    {
        public String accion { get; set; }
        public String mensaje { get; set; }
        public Dictionary<string, string> valores { get; set; }

        public object[] parametrosMetodo()
        {
            return valores.Values.ToArray();
        }

        
    }

    
}