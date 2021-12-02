using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DKdll.API
{
    [Route("api/[controller]")]
    public class HolaController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
           // var obj = DKdll.codigo.cLlamadasDLL.TomarPedidoConIdCarrito(1, "", "", "", "", "", null, true);
            return new string[] { "value1", "value2", "value3", "value4" };
        }
    }
}
