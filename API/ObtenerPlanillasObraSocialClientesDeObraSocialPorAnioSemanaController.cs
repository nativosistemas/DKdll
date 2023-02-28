using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DKdll.API
{
    public class ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemanaController : ApiController
    {
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> Create([FromBody] DKbase.Models.ObraSocialRequest parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(codigo.cLlamadasHttp.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana(parameter.nombrePlan, parameter.loginWeb, parameter.anio, parameter.semana));
        }
    }
}
