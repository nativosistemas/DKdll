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
                ResultadoFinal = new cDllPedido();
                ResultadoFinal.web_Error = ex.Message;
                ResultadoFinal.web_Error_StackTrace = ex.StackTrace;
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
                    lista = null;
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto);
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
                lista[0].web_Error_StackTrace = ex.StackTrace;
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
                    lista = null;
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto);
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
                lista[0].web_Error_StackTrace = ex.StackTrace;
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
                ResultadoFinal = new cDllPedido();
                ResultadoFinal.web_Error = ex.Message;
                ResultadoFinal.web_Error_StackTrace = ex.StackTrace;
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
        public static List<cVacuna> ObtenerTotalReservasDeVacunasPorClienteEntreFechas(DateTime pDesde, DateTime pHasta, String pLoginWEB)
        {
            List<cVacuna> resultado = null;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.VacunaCOL oResult = objServWeb.ObtenerTotalReservasDeVacunasPorClienteEntreFechas(pDesde, pHasta, pLoginWEB);
                if (oResult != null)
                {
                    resultado = new List<cVacuna>();
                    for (int i = 1; i <= oResult.Count(); i++)
                    {
                        resultado.Add(dllFuncionesGenerales.ToConvert(oResult.Item[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pDesde, pHasta, pLoginWEB);
                return null;
            }
            return resultado;
        }
        public static List<cReservaVacuna> ObtenerReservasDeVacunasPorClienteEntreFechas(DateTime pDesde, DateTime pHasta, String pLoginWEB)
        {
            List<cReservaVacuna> resultado = null;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.ReservaVacunaCOL oResult = objServWeb.ObtenerReservasDeVacunasPorClienteEntreFechas(pDesde, pHasta, pLoginWEB);
                if (oResult != null)
                {
                    resultado = new List<cReservaVacuna>();
                    for (int i = 1; i <= oResult.Count(); i++)
                    {
                        resultado.Add(dllFuncionesGenerales.ToConvert(oResult[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pDesde, pHasta, pLoginWEB);
                return null;
            }
            return resultado;
        }

        public static bool? AgregarVacunas(List<cVacuna> pVacunas, string pLoginTelefonista = null)
        {
            bool resultado = false;
            try
            {
                // new dkInterfaceWeb.SolicitudDevClienteCOL();
                dkInterfaceWeb.VacunaCOL parameter;
                parameter = new dkInterfaceWeb.VacunaCOL();
                foreach (cVacuna item in pVacunas)
                {
                    var o = dllFuncionesGenerales.ToConvert(item);
                    if (o != null)
                    {
                        parameter.Add(o);
                    }
                }
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                if (string.IsNullOrEmpty(pLoginTelefonista))
                {
                    objServWeb.AgregarVacunas(parameter);
                }
                else
                {
                    objServWeb.AgregarVacunasTelefonista(parameter, pLoginTelefonista);
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pVacunas);
                return null;
            }
            return resultado;
        }
        public static void ModificarPasswordWEB(string pIdentificadorCliente, string pPassActual, string pPassNueva)
        {
            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
            try
            {
                objServWeb.ModificarPasswordWEB(pIdentificadorCliente, pPassActual, pPassNueva);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdentificadorCliente, pPassActual, pPassNueva);
            }
        }
        public static List<cFacturaDetalle> ObtenerDetalleFactura(string pNumeroFactura)
        {
            List<cFacturaDetalle> resultado = null;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.FacturaItemCOL objListaDetalle = objServWeb.ObtenerItemsDeFactura(pNumeroFactura);
                if (objListaDetalle != null)
                {
                    resultado = new List<cFacturaDetalle>();
                    for (int i = 1; i <= objListaDetalle.Count(); i++)
                    {
                        cFacturaDetalle _objDetalleFactura = new cFacturaDetalle();
                        _objDetalleFactura.Cantidad = objListaDetalle[i].Cantidad == null ? "" : objListaDetalle[i].Cantidad.ToString();
                        _objDetalleFactura.Caracteristica = objListaDetalle[i].Caracteristica == null ? "" : objListaDetalle[i].Caracteristica.ToString();
                        _objDetalleFactura.Descripcion = objListaDetalle[i].Descripcion;
                        _objDetalleFactura.Importe = objListaDetalle[i].Importe == null ? "" : objListaDetalle[i].Importe.ToString();
                        _objDetalleFactura.NumeroFactura = objListaDetalle[i].NumeroFactura;
                        _objDetalleFactura.NumeroHoja = objListaDetalle[i].NumeroHoja;
                        _objDetalleFactura.NumeroItem = objListaDetalle[i].NumeroItem;
                        _objDetalleFactura.PrecioPublico = objListaDetalle[i].PrecioPublico == null ? "" : objListaDetalle[i].PrecioPublico.ToString();
                        _objDetalleFactura.PrecioUnitario = objListaDetalle[i].PrecioUnitario == null ? "" : objListaDetalle[i].PrecioUnitario.ToString();
                        _objDetalleFactura.Troquel = objListaDetalle[i].Troquel == null ? "" : objListaDetalle[i].Troquel.ToString();
                        resultado.Add(_objDetalleFactura);
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroFactura);
            }
            return resultado;
        }
        public static cFactura ObtenerFactura(string pNumeroFactura, string pLoginWeb)
        {
            cFactura resultado = null;
            classTiempo tiempo = new classTiempo("ObtenerFactura");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.Factura objFactura = objServWeb.ObtenerFactura(pNumeroFactura, pLoginWeb);
                if (objFactura != null)
                {
                    resultado = dllFuncionesGenerales.ConvertToFactura(objFactura);
                    resultado.lista = ObtenerDetalleFactura(pNumeroFactura);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroFactura, pLoginWeb);
                return null;
            }
            finally
            {
                tiempo.Parar();
            }
            return resultado;
        }
    }
}