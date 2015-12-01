using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using ProyectoEPS.Models;

namespace ProyectoEPS.Controllers
{
    public class MessageController : ApiController
    {
        [HttpGet]
        public Message messageGetTest([FromBody] Message message)
        {
            return message;
        }

        [HttpPost]
        public MensajeRespuesta messagePostController(Message message)
        {
            //CRUDEjemplo ce = new CRUDEjemplo();
            CRUDUsuario cu = new CRUDUsuario();
            MensajeRespuesta mensaje;
            try {
                MethodInfo method = cu.GetType().GetMethod("inicioSesion");
                object result = method.Invoke(cu, message.parametrosMetodo());
                //System.Diagnostics.Debug.WriteLine(message.valores["parametro1"]);
                System.Diagnostics.Debug.WriteLine("muestra un mensaje");
                //return new CRUDEjemplo().datos();
                //List<Employee> el = result as List<Employee>;
                //int resultado = result as int;
                System.Diagnostics.Debug.WriteLine(result.ToString());
                //MensajeRespuesta mensaje = new MensajeRespuesta { exito = 1 , datos = el.ToArray()};
                mensaje = new MensajeRespuesta { exito = 1, datos = result };
                //mensaje.datos = result as List<object>;
                //System.Diagnostics.Debug.WriteLine("Tamaño de la lista: " + mensaje.datos.Length);
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
