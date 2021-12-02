using DKbase.dll;
using DKbase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DKdll.codigo
{
    public class cLlamadasDLL
    {

        public static cDllPedido TomarPedidoConIdCarrito(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
        {
            cDllPedido ResultadoFinal = null;
            classTiempo tiempo = new classTiempo("TomarPedidoConIdCarrito");
            try
            {
                dkInterfaceWeb.Pedido Resultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.Pedido pedido = new dkInterfaceWeb.Pedido();

                // llenar datos pedidos
                pedido.Login = pLoginCliente;
                pedido.MensajeEnFactura = pMensajeEnFactura;
                pedido.MensajeEnRemito = pMensajeEnRemito;

                // Cargar productos al carrito
                foreach (cDllProductosAndCantidad item in pListaProducto)
                {
                    pedido.Add(item.codProductoNombre, item.cantidad, item.isOferta ? "$" : " ");
                }
                dkInterfaceWeb.TipoEnvio tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                switch (pTipoEnvio)
                {
                    case "E":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Encomienda;
                        break;
                    case "R":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Reparto;
                        break;
                    case "C":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Cadeteria;
                        break;
                    case "M":
                        tipoEnvio = dkInterfaceWeb.TipoEnvio.Mostrador;
                        break;
                        //default:
                        //    break;
                }

                Resultado = objServWeb.TomarPedidoConIdCarrito(pIdCarrito, pedido, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL", pIsUrgente);
                if (Resultado != null)
                {
                    ResultadoFinal = dllFuncionesGenerales.ToConvert(Resultado);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pIsUrgente);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
            return ResultadoFinal;
        }
    }
}