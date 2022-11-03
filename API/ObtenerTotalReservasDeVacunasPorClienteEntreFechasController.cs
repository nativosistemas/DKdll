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
    [Route("api/[controller]")]
    public class ObtenerTotalReservasDeVacunasPorClienteEntreFechasController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create([FromBody] VacunasRequest parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(cLlamadasDLL.ObtenerTotalReservasDeVacunasPorClienteEntreFechas(parameter.pDesde, parameter.pHasta, parameter.pLoginWEB));
        }
    }
}