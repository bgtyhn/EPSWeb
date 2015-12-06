using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Reflection;
using ProyectoEPS.Models;

namespace ProyectoEPS.Controllers
{
    public class SolicitudesController : ApiController
    {
        [HttpPost]
        public MensajeRespuesta accionSolicitudes(Message mensajeSolicitud)
        {
            CRUDSolicitudesRegistro cs = new CRUDSolicitudesRegistro();
            MensajeRespuesta mensaje;
            try
            {
                //MethodInfo method = cu.GetType().GetMethod("inicioSesion");
                System.Diagnostics.Debug.WriteLine("acción pedida: " + mensajeSolicitud.accion);
                MethodInfo metodo = cs.GetType().GetMethod(mensajeSolicitud.accion);
                object result = metodo.Invoke(cs, mensajeSolicitud.parametrosMetodo());
                if (result != null)
                {
                    System.Diagnostics.Debug.WriteLine("muestra un mensaje");
                    System.Diagnostics.Debug.WriteLine(result.ToString());
                }
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
