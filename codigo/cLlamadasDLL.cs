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

        public static cDllPedido TomarPedidoTelefonista(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto, string pLoginTelefonista)
        {
            cDllPedido ResultadoFinal = null;
            classTiempo tiempo = new classTiempo("TomarPedidoTelefonista");
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
                Resultado = objServWeb.TomarPedidoTelefonista(pIdCarrito, pedido, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL", pLoginTelefonista);
                if (Resultado != null)
                {
                    ResultadoFinal = dllFuncionesGenerales.ToConvert(Resultado);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pLoginTelefonista);
                cDllPedido resultado_Exception = new cDllPedido();
                resultado_Exception.web_Error = ex.Message;
                return resultado_Exception;
            }
            finally
            {
                tiempo.Parar();
            }
            return ResultadoFinal;
        }
        public static List<cDllPedidoTransfer> TomarPedidoDeTransfersTelefonista(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto, string pLoginTelefonista)
        {
            List<cDllPedidoTransfer> lista = null;

            classTiempo tiempo = new classTiempo("TomarPedidoDeTransfersTelefonista");
            try
            {
                lista = new List<cDllPedidoTransfer>();
                dkInterfaceWeb.PedidoCOL Resultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.PedidoTransfer pedidoTransfer = new dkInterfaceWeb.PedidoTransfer();
                foreach (cDllProductosAndCantidad item in pListaProducto)
                {
                    pedidoTransfer.Add(item.codProductoNombre, item.cantidad, item.IdTransfer);
                }
                pedidoTransfer.Login = pLoginCliente;
                pedidoTransfer.MensajeEnFactura = pMensajeEnFactura;
                pedidoTransfer.MensajeEnRemito = pMensajeEnRemito;

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
                }
                Resultado = objServWeb.TomarPedidoDeTransfersTelefonista(pIdCarrito, pedidoTransfer, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL", pLoginTelefonista);
                if (Resultado != null)
                {
                    for (int i = 1; i <= Resultado.Count(); i++)
                    {
                        lista.Add(dllFuncionesGenerales.ToConvertTransfer(Resultado[i]));
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pLoginTelefonista);
                if (lista == null)
                {
                    lista = new List<cDllPedidoTransfer>();
                }
                cDllPedidoTransfer resultado_Exception = new cDllPedidoTransfer();
                resultado_Exception.web_Error = ex.Message;
                lista.Add(resultado_Exception);
                return lista;
            }
            finally
            {
                tiempo.Parar();
            }

            return lista;
        }
        public static Decimal ObtenerCreditoDisponible(string pLoginWeb)
        {
            decimal result = 0;
            classTiempo tiempo = new classTiempo("ObtenerCreditoDisponible");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                result = objServWeb.ObtenerCreditoDisponible(pLoginWeb);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return 0;
            }
            finally
            {
                tiempo.Parar();
            }
            return result;
        }
    }
}