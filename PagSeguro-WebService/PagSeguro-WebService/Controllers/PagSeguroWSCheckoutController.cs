using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PagSeguro_WebService.Controllers
{
    public class PagSeguroWSCheckoutController : ApiController
    {
        
        public string Get(string nomeCliente, string itens, string valorTotal, string quantidade, string telefone, string codigo)
        {
            PagSeguro pag = new PagSeguro();
            return pag.CheckOut(nomeCliente, itens, valorTotal, quantidade, telefone, codigo);          
        }
        
    }
}