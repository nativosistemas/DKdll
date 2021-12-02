using DKbase.dll;
using DKbase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DKdll.codigo
{
    public class cLlamadasHttp
    {
        public static cDllPedido TomarPedidoConIdCarrito(TomarPedidoConIdCarritoRequest pValue)
        {
            return cLlamadasDLL.TomarPedidoConIdCarrito(pValue.pIdCarrito, pValue.pLoginCliente, pValue.pIdSucursal, pValue.pMensajeEnFactura, pValue.pMensajeEnRemito, pValue.pTipoEnvio, pValue.pListaProducto, pValue.pIsUrgente);
        }
    }
}