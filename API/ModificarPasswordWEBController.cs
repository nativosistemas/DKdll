using DKdll.codigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DKdll.API
{
    [Route("api/[controller]")]
    public class ModificarPasswordWEBController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string pIdentificadorCliente, string pPassActual, string pPassNueva)
        {
            cLlamadasHttp.ModificarPasswordWEB(pIdentificadorCliente, pPassActual, pPassNueva);
            return Ok();
        }
    }
}
