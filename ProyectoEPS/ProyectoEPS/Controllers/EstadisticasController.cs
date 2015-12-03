using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoEPS.Models;
using System.Reflection;

namespace ProyectoEPS.Controllers
{
    public class EstadisticasController : ApiController
    {
        [HttpPost]
        public MensajeRespuesta accionEstadisticas(Message mensajeSolicitud)
        {
            CRUDEstadistica ce = new CRUDEstadistica();
            MensajeRespuesta mensaje;
            try
            {
                //MethodInfo method = cu.GetType().GetMethod("inicioSesion");
                System.Diagnostics.Debug.WriteLine("acción pedida: " + mensajeSolicitud.accion);
                MethodInfo metodo = ce.GetType().GetMethod(mensajeSolicitud.accion);
                object result = metodo.Invoke(ce, mensajeSolicitud.parametrosMetodo());
                System.Diagnostics.Debug.WriteLine("muestra un mensaje");
                System.Diagnostics.Debug.WriteLine(result.ToString());
                //MensajeRespuesta mensaje = new MensajeRespuesta { exito = 1 , datos = el.ToArray()};
                mensaje = new MensajeRespuesta { exito = 1, datos = result, mensajeExito = "Operación exitosa" };
                return mensaje;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                mensaje = new MensajeRespuesta { exito = 0, mensajeError = "Error al obtener los datos", datos = e.Message };
                return mensaje;
            }
        }
    }
}
