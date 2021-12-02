using DKbase.Models;
using DKdll.codigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace DKdll.API
{
    [Route("api/[controller]")]
    public class TomarPedidoConIdCarritoIntranetController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody] TomarPedidoConIdCarritoRequest parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           return Ok(cLlamadasHttp.TomarPedidoConIdCarrito(parameter));
        }
    }
}
