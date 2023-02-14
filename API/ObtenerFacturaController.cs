using DKbase.Models;
using DKdll.codigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DKdll.API
{
    public class ObtenerFacturaController : ApiController
    {
        // GET: ObtenerFactura
        public async Task<IHttpActionResult> Index(string pNumeroFactura, string pLoginWeb)
        {
            return Ok(cLlamadasHttp.ObtenerFactura(pNumeroFactura, pLoginWeb));
        }
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] DocumentoRequest parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(cLlamadasHttp.ObtenerFactura(parameter.documentoID, parameter.loginWeb));
        }
    }
}