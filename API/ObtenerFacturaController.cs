using DKdll.codigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DKdll.API
{
    public class ObtenerFacturaController : ApiController
    {
        // GET: ObtenerFactura
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost]
        public IHttpActionResult Create(string pNumeroFactura, string pLoginWeb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(cLlamadasHttp.ObtenerFactura(pNumeroFactura,pLoginWeb));
        }
    }
}