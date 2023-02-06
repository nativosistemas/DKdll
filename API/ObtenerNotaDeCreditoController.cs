using DKbase.Models;
using DKdll.codigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DKdll.API
{
    public class ObtenerNotaDeCreditoController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] DocumentoRequest parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(cLlamadasHttp.ObtenerNotaDeCredito(parameter.documentoID, parameter.loginWeb));
        }
    }
}
