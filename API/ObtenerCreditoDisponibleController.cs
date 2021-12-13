using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DKbase.Models;
using DKdll.codigo;

namespace DKdll.API
{
    [Route("api/[controller]")]
    public class ObtenerCreditoDisponibleController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string loginWeb)
        {
            return Ok(cLlamadasHttp.ObtenerCreditoDisponible(loginWeb));
        }
    }
}
