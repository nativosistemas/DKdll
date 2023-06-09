using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DKdll.API
{
    public class AgregarSolicitudDevolucionClienteController : ApiController
    {
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> Create([FromBody] DKbase.Models.DocumentoRequest parameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (parameter.itemDevolucionPrecarga != null)
            {
                foreach (Dkbase.dll.cDevolucionItemPrecarga item in parameter.itemDevolucionPrecarga)
                {
                    item.dev_motivo = (DKbase.dll.dllMotivoDevolucion)item.dev_motivo_int;
                }
            }
            return Ok(codigo.cLlamadasHttp.AgregarSolicitudDevolucionCliente(parameter.itemDevolucionPrecarga, parameter.loginWeb));
        }
    }
}
