using DKbase.Models;
using DKdll.codigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DKdll.API
{
    public class TomarPedidoDeTransfersController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody] TomarPedidoConIdCarritoRequest parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(cLlamadasHttp.TomarPedidoDeTransfers(parameter));
        }
    }
}