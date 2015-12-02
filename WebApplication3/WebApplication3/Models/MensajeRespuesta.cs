using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class MensajeRespuesta
    {
        public int exito { get; set; }
        public String mensajeExito { get; set; }
        public string mensajeError { get; set; }
        public object datos { get; set; }
    }
}