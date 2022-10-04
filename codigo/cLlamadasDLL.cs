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
                ResultadoFinal = new cDllPedido();
                ResultadoFinal.web_Error = ex.Message;
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
                    lista.Add(new cDllPedidoTransfer());
                }
                else if (lista.Count == 0)
                {
                    lista.Add(new cDllPedidoTransfer());
                }
                lista[0].web_Error = ex.Message;
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
        public static cDllPedido TomarPedido(string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
        {
            cDllPedido ResultadoFinal = null;
            classTiempo tiempo = new classTiempo("TomarPedido");

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
                }

                Resultado = objServWeb.TomarPedido(pedido, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL", pIsUrgente);
                if (Resultado != null)
                {
                    ResultadoFinal = dllFuncionesGenerales.ToConvert(Resultado);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pIsUrgente);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
            return ResultadoFinal;
        }
        public static List<cDllPedidoTransfer> TomarPedidoDeTransfers(string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto)
        {
            List<cDllPedidoTransfer> lista = null;
            classTiempo tiempo = new classTiempo("TomarPedidoDeTransfers");
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
                Resultado = objServWeb.TomarPedidoDeTransfers(pedidoTransfer, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL");
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
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
            return lista;
        }
        public static List<cDllPedidoTransfer> TomarPedidoDeTransfersConIdCarrito(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto)
        {
            List<cDllPedidoTransfer> lista = null;
            classTiempo tiempo = new classTiempo("TomarPedidoDeTransfersConIdCarrito");
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
                Resultado = objServWeb.TomarPedidoDeTransfersConIdCarrito(pIdCarrito, pedidoTransfer, tipoEnvio, pIdSucursal, @"C:\RutaArchivoDLL");
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
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
            return lista;
        }
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
        public static bool ValidarExistenciaDeCarritoWebPasado(int pIdCarrito)
        {
            bool result = false;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                result = objServWeb.ValidarExistenciaDeCarritoWebPasado(pIdCarrito);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito);
            }
            return result;
        }
        public static List<cDllPedido> ObtenerPedidosEntreFechas(DateTime pDesde, DateTime pHasta, string pLoginWeb)
        {
            List<cDllPedido> resultado = null;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.PedidoCOL objPedidos = objServWeb.ObtenerPedidosDePuntoDeVentaEntreFechas(pLoginWeb, pDesde, pHasta);
                if (objPedidos != null)
                {
                    resultado = new List<cDllPedido>();
                    if (objPedidos.Count() > 0)
                    {
                        for (int i = 1; i <= objPedidos.Count(); i++)
                        {
                            cDllPedido obj = dllFuncionesGenerales.ToConvert(objPedidos[i]);
                            resultado.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pDesde, pHasta, pLoginWeb);
                return null;
            }
            return resultado;
        }
    }
}