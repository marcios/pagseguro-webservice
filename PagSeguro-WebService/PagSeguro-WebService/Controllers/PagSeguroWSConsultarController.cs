using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PagSeguro_WebService.Controllers
{
    public class PagSeguroWSConsultarController : ApiController
    {
        // GET api/<controller>/5
        public string Get(int referencia)
        {
            PagSeguro pag = new PagSeguro();
            return pag.ConsultarTransacao(referencia);
        }
    }
}