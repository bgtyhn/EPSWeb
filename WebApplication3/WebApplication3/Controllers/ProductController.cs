using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication3.Controllers
{
    public class ProductController : ApiController
    {
        [HttpPost]
        public string TestMethod([FromBody]string value)
        {

            Console.WriteLine("entra al metodo");
            return "Hello from http post web api controller: " + value;
        }

        [HttpGet]
        public string testGet()
        {
            return "holi";
        }
    }
}
