using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoEPS.Models;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net.Http.Headers;
using System.Drawing;

namespace ProyectoEPS.Controllers
{
    public class ProfesionalController : ApiController
    {
        [HttpPost]
        public MensajeRespuesta accionProfesionales(Message mensajeSolicitud)
        {
            CRUDProfesionales cp = new CRUDProfesionales();
            MensajeRespuesta mensaje;
            try
            {
                //MethodInfo method = cu.GetType().GetMethod("inicioSesion");
                System.Diagnostics.Debug.WriteLine("acción pedida: " + mensajeSolicitud.accion);
                MethodInfo metodo = cp.GetType().GetMethod(mensajeSolicitud.accion);
                object result = metodo.Invoke(cp, mensajeSolicitud.parametrosMetodo());
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

        [HttpPost]
        public MensajeRespuesta  Upload()
        {
            try {
                var fileName = HttpContext.Current.Request.Form["idUsuario"];

                HttpPostedFile filePosted = HttpContext.Current.Request.Files["file"];

                string fileNameApplication = System.IO.Path.GetFileName(filePosted.FileName);
                string fileExtensionApplication = System.IO.Path.GetExtension(fileNameApplication);
                if (filePosted != null)
                {
                    System.Diagnostics.Debug.WriteLine("No es nulo");
                    System.Diagnostics.Debug.WriteLine("extension: " + fileExtensionApplication); 
                    filePosted.SaveAs("C:\\imagenes\\foto\\" + fileName +  fileExtensionApplication);
                    new CRUDProfesionales().crear("/foto/" + fileName + fileExtensionApplication);
                }

                //var provider = new MultipartFormDataStreamProvider("C:\\imagenes\\foto");
                //await Request.Content.ReadAsMultipartAsync(provider);

                //var myParameter = provider.FormData.GetValues("myParameter").FirstOrDefault();
                //System.Diagnostics.Debug.WriteLine(fileName);
                //var count = provider.FileData.Count;

                return new MensajeRespuesta() { exito = 1, datos = "cosa subida", mensajeExito = "buena" };
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return new MensajeRespuesta() { exito = 0, datos = "paila", mensajeExito = "buena" };
            }
        }

        [HttpPost]
        public Image imagenProfesional()
        {
            System.Diagnostics.Debug.WriteLine("pide imagen");
            byte[] datos = new CRUDProfesionales().imagen("awe");
            System.Diagnostics.Debug.WriteLine(datos.Length);
            MemoryStream ms = new MemoryStream(datos);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(ms.ToArray());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            System.Diagnostics.Debug.WriteLine("pide");
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
