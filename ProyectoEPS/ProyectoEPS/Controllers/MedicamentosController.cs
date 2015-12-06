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
    public class MedicamentosController : ApiController
    {

        [HttpPost]
        public MensajeRespuesta accionMedicamentos(Message mensajeSolicitud)
        {
            CRUDMedicamentos cm = new CRUDMedicamentos();
            MensajeRespuesta mensaje;
            try
            {
                //MethodInfo method = cu.GetType().GetMethod("inicioSesion");
                System.Diagnostics.Debug.WriteLine("acción pedida: " + mensajeSolicitud.accion);
                MethodInfo metodo = cm.GetType().GetMethod(mensajeSolicitud.accion);
                object result = metodo.Invoke(cm, mensajeSolicitud.parametrosMetodo());
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
