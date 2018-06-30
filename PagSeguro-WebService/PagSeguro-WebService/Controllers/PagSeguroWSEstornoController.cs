using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PagSeguro_WebService.Controllers
{
    public class PagSeguroWSEstornoController : ApiController
    {
        // GET api/<controller>/5
        public string Get(string referencia)
        {
            PagSeguro pag = new PagSeguro();
            return pag.Estornar(referencia).ToString();
        }

    }
}