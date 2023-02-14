using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DKdll.API
{
    public class ObtenerMovimientosDeCuentaCorrienteController : ApiController
    {
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> Create([FromBody] DKbase.Models.DocumentoRequest parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(codigo.cLlamadasHttp.ObtenerMovimientosDeCuentaCorriente(parameter.isIncluyeCancelado,parameter.fechaDesde,parameter.fechaHasta, parameter.loginWeb));
        }
    }
}
