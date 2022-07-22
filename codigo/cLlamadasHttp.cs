﻿using DKbase.dll;
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
        public static decimal ObtenerCreditoDisponible(string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerCreditoDisponible(pLoginWeb);
        }
        public static cDllPedido TomarPedido(TomarPedidoConIdCarritoRequest pValue)
        {
            return cLlamadasDLL.TomarPedido(pValue.pLoginCliente, pValue.pIdSucursal, pValue.pMensajeEnFactura, pValue.pMensajeEnRemito, pValue.pTipoEnvio, pValue.pListaProducto, pValue.pIsUrgente);
        }
        public static List<cDllPedidoTransfer> TomarPedidoDeTransfers(TomarPedidoConIdCarritoRequest pValue)
        {
            return cLlamadasDLL.TomarPedidoDeTransfers(pValue.pLoginCliente, pValue.pIdSucursal, pValue.pMensajeEnFactura, pValue.pMensajeEnRemito, pValue.pTipoEnvio, pValue.pListaProducto);
        }
        public static List<cDllPedidoTransfer> TomarPedidoDeTransfersConIdCarrito(TomarPedidoConIdCarritoRequest pValue)
        {
            return cLlamadasDLL.TomarPedidoDeTransfersConIdCarrito(pValue.pIdCarrito, pValue.pLoginCliente, pValue.pIdSucursal, pValue.pMensajeEnFactura, pValue.pMensajeEnRemito, pValue.pTipoEnvio, pValue.pListaProducto);
        }
        public static cDllPedido TomarPedidoConIdCarrito(TomarPedidoConIdCarritoRequest pValue)
        {
            return cLlamadasDLL.TomarPedidoConIdCarrito(pValue.pIdCarrito, pValue.pLoginCliente, pValue.pIdSucursal, pValue.pMensajeEnFactura, pValue.pMensajeEnRemito, pValue.pTipoEnvio, pValue.pListaProducto, pValue.pIsUrgente);
        }
        public static bool ValidarExistenciaDeCarritoWebPasado(TomarPedidoConIdCarritoRequest pValue)
        {
            return cLlamadasDLL.ValidarExistenciaDeCarritoWebPasado(pValue.pIdCarrito);
        }
    }
}