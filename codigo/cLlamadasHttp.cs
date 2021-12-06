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
        public static cDllPedido TomarPedidoTelefonista(TomarPedidoConIdCarritoRequest pValue)
        {
            return cLlamadasDLL.TomarPedidoTelefonista(pValue.pIdCarrito, pValue.pLoginCliente, pValue.pIdSucursal, pValue.pMensajeEnFactura, pValue.pMensajeEnRemito, pValue.pTipoEnvio, pValue.pListaProducto, pValue.pLoginTelefonista);
        }
        public static List<cDllPedidoTransfer> TomarPedidoDeTransfersTelefonista(TomarPedidoConIdCarritoRequest pValue)
        {
            return cLlamadasDLL.TomarPedidoDeTransfersTelefonista(pValue.pIdCarrito, pValue.pLoginCliente, pValue.pIdSucursal, pValue.pMensajeEnFactura, pValue.pMensajeEnRemito, pValue.pTipoEnvio, pValue.pListaProducto, pValue.pLoginTelefonista);
        }
    }
}