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
            //classTiempo tiempo = new classTiempo("TomarPedidoTelefonista");
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
            //finally
            //{
            //    tiempo.Parar();
            //}
            return ResultadoFinal;
        }
        public static List<cDllPedidoTransfer> TomarPedidoDeTransfersTelefonista(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto, string pLoginTelefonista)
        {
            List<cDllPedidoTransfer> lista = null;

            //classTiempo tiempo = new classTiempo("TomarPedidoDeTransfersTelefonista");
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
            //finally
            //{
            //    tiempo.Parar();
            //}

            return lista;
        }
        public static Decimal ObtenerCreditoDisponible(string pLoginWeb)
        {
            decimal result = 0;
            //classTiempo tiempo = new classTiempo("ObtenerCreditoDisponible");
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
            //finally
            //{
            //    tiempo.Parar();
            //}
            return result;
        }
        public static cDllPedido TomarPedido(string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
        {
            cDllPedido ResultadoFinal = null;
            //classTiempo tiempo = new classTiempo("TomarPedido");

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
            //finally
            //{
            //    tiempo.Parar();
            //}
            return ResultadoFinal;
        }
        public static List<cDllPedidoTransfer> TomarPedidoDeTransfers(string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto)
        {
            List<cDllPedidoTransfer> lista = null;
            //classTiempo tiempo = new classTiempo("TomarPedidoDeTransfers");
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
            //finally
            //{
            //    tiempo.Parar();
            //}
            return lista;
        }
        public static List<cDllPedidoTransfer> TomarPedidoDeTransfersConIdCarrito(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto)
        {
            List<cDllPedidoTransfer> lista = null;
            //classTiempo tiempo = new classTiempo("TomarPedidoDeTransfersConIdCarrito");
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
            //finally
            //{
            //    tiempo.Parar();
            //}
            return lista;
        }
        public static cDllPedido TomarPedidoConIdCarrito(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
        {
            cDllPedido ResultadoFinal = null;
            //classTiempo tiempo = new classTiempo("TomarPedidoConIdCarrito");
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
            //finally
            //{
            //    tiempo.Parar();
            //}
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
            //classTiempo tiempo = new classTiempo("ObtenerFactura");
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
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static cNotaDeCredito ObtenerNotaDeCredito(string pNumeroNotaDeCredito, string pLoginWeb)
        {
            cNotaDeCredito resultado = null;
            //classTiempo tiempo = new classTiempo("ObtenerNotaDeCredito");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.NotaDeCredito objNotaDeCredito = objServWeb.ObtenerNotaDeCredito(pNumeroNotaDeCredito, pLoginWeb);
                if (objNotaDeCredito != null)
                {
                    resultado = dllFuncionesGenerales.ConvertToNotaDeCredito(objNotaDeCredito);
                    resultado.lista = ObtenerDetalleNotaDeCredito(pNumeroNotaDeCredito);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroNotaDeCredito, pLoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static List<cNotaDeCreditoDetalle> ObtenerDetalleNotaDeCredito(string pNumeroNotaDeCredito)
        {
            try
            {
                List<cNotaDeCreditoDetalle> resultado = null;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.NotaDeCreditoItemCOL objListaDetalle = objServWeb.ObtenerItemsDeNotaDeCredito(pNumeroNotaDeCredito);
                if (objListaDetalle != null)
                {
                    resultado = new List<cNotaDeCreditoDetalle>();
                    for (int i = 1; i <= objListaDetalle.Count(); i++)
                    {
                        cNotaDeCreditoDetalle _objDetalleNotaDeCredito = new cNotaDeCreditoDetalle();
                        _objDetalleNotaDeCredito.Cantidad = objListaDetalle.get_Item(i).Cantidad == null ? "" : objListaDetalle.get_Item(i).Cantidad.ToString();
                        _objDetalleNotaDeCredito.Descripcion = objListaDetalle.get_Item(i).Descripcion;
                        _objDetalleNotaDeCredito.Importe = objListaDetalle.get_Item(i).Importe == null ? "" : objListaDetalle.get_Item(i).Importe.ToString();
                        _objDetalleNotaDeCredito.NumeroHoja = objListaDetalle.get_Item(i).NumeroHoja;
                        _objDetalleNotaDeCredito.NumeroItem = objListaDetalle.get_Item(i).NumeroItem;
                        _objDetalleNotaDeCredito.NumeroNotaDeCredito = objListaDetalle.get_Item(i).NumeroNotaDeCredito;
                        _objDetalleNotaDeCredito.PrecioPublico = objListaDetalle.get_Item(i).PrecioPublico == null ? "" : objListaDetalle.get_Item(i).PrecioPublico.ToString();
                        _objDetalleNotaDeCredito.PrecioUnitario = objListaDetalle.get_Item(i).PrecioUnitario == null ? "" : objListaDetalle.get_Item(i).PrecioUnitario.ToString();
                        _objDetalleNotaDeCredito.Troquel = objListaDetalle.get_Item(i).Troquel == null ? "" : objListaDetalle.get_Item(i).Troquel.ToString();
                        resultado.Add(_objDetalleNotaDeCredito);
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroNotaDeCredito);
                return null;
            }
        }
        public static cNotaDeDebito ObtenerNotaDeDebito(string pNumeroNotaDeDebito, string pLoginWeb)
        {
            cNotaDeDebito resultado = null;

            //classTiempo tiempo = new classTiempo("ObtenerNotaDeDebito");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.NotaDeDebito objNotaDeDebito = objServWeb.ObtenerNotaDeDebito(pNumeroNotaDeDebito, pLoginWeb);
                if (objNotaDeDebito != null)
                {
                    resultado = dllFuncionesGenerales.ConvertToNotaDeDebito(objNotaDeDebito);
                    resultado.lista = ObtenerDetalleNotaDeDebito(pNumeroNotaDeDebito);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroNotaDeDebito, pLoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static List<cNotaDeDebitoDetalle> ObtenerDetalleNotaDeDebito(string pNumeroNotaDeDebito)
        {
            List<cNotaDeDebitoDetalle> resultado = null;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.NotaDeDebitoItemCOL objListaDetalle = objServWeb.ObtenerItemsDeNotaDeDebito(pNumeroNotaDeDebito);
                if (objListaDetalle != null)
                {
                    resultado = new List<cNotaDeDebitoDetalle>();
                    for (int i = 1; i <= objListaDetalle.Count(); i++)
                    {
                        cNotaDeDebitoDetalle _objDetalleNotaDeDebito = new cNotaDeDebitoDetalle();
                        _objDetalleNotaDeDebito.Descripcion = objListaDetalle.get_Item(i).Descripcion;
                        _objDetalleNotaDeDebito.Importe = objListaDetalle.get_Item(i).Importe == null ? "" : objListaDetalle.get_Item(i).Importe.ToString();
                        _objDetalleNotaDeDebito.NumeroHoja = objListaDetalle.get_Item(i).NumeroHoja;
                        _objDetalleNotaDeDebito.NumeroItem = objListaDetalle.get_Item(i).NumeroItem;
                        _objDetalleNotaDeDebito.NumeroNotaDeDebito = objListaDetalle.get_Item(i).NumeroNotaDeDebito;
                        resultado.Add(_objDetalleNotaDeDebito);
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroNotaDeDebito);
            }
            return resultado;
        }
        public static cResumen ObtenerResumenCerrado(string pNumeroResumen, string pLoginWeb)
        {
            cResumen resultado = null;
            //classTiempo tiempo = new classTiempo("ObtenerResumenCerrado");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.Resumen objResumen = objServWeb.ObtenerResumenCerrado(pNumeroResumen);
                if (objResumen != null)
                {
                    resultado = dllFuncionesGenerales.ToConvert(objResumen);
                    dkInterfaceWeb.ResumenItemCOL objItemsResumen = objServWeb.ObtenerItemsDeResumenCerrado(pNumeroResumen);
                    if (objItemsResumen != null)
                    {
                        resultado.lista = new List<cResumenDetalle>();
                        for (int i = 1; i <= objItemsResumen.Count(); i++)
                        {
                            cResumenDetalle objResumenDetalle = dllFuncionesGenerales.ToConvert(objItemsResumen[i]);
                            resultado.lista.Add(objResumenDetalle);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroResumen, pLoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static cObraSocialCliente ObtenerObraSocialCliente(string pNumeroObraSocialCliente, string pLoginWeb)
        {
            cObraSocialCliente resultado = null;
            //classTiempo tiempo = new classTiempo("ObtenerObraSocialCliente");
            try
            {
                dkInterfaceWeb.ObraSocialCliente objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerObraSocialCliente(pNumeroObraSocialCliente, pLoginWeb);
                resultado = dllFuncionesGenerales.ToConvert(objResultado);
                resultado.lista = ObtenerItemsDeObraSocialCliente(pNumeroObraSocialCliente);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroObraSocialCliente, pLoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static List<cObraSocialClienteItem> ObtenerItemsDeObraSocialCliente(string pNumeroObraSocialCliente)
        {
            List<cObraSocialClienteItem> lista = null;
            lista = new List<cObraSocialClienteItem>();
            try
            {
                dkInterfaceWeb.ObraSocialClienteItemCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerItemsDeObraSocialCliente(pNumeroObraSocialCliente);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado.get_Item(i))));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroObraSocialCliente);
                return null;
            }
            finally
            {
            }
            return lista;
        }
        public static cRecibo ObtenerRecibo(string pNumeroDoc, string pLoginWeb)
        {
            cRecibo resultado = null;
            //classTiempo tiempo = new classTiempo("ObtenerRecibo");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.Recibo obj = objServWeb.ObtenerReciboPorNumero(pNumeroDoc, pLoginWeb);
                if (obj != null)
                {
                    resultado = dllFuncionesGenerales.ConvertToRecibo(obj);
                    resultado.lista = ObtenerDetalleRecibo(pNumeroDoc, pLoginWeb);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroDoc, pLoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}            
            return resultado;
        }
        public static List<cReciboDetalle> ObtenerDetalleRecibo(string pNumeroDoc, string pLoginWeb)
        {
            List<cReciboDetalle> resultado = null;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.ReciboItemCOLCOL objListaDetalle = objServWeb.ObtenerReciboItemsPorNumeroNuevo(pNumeroDoc, pLoginWeb);
                if (objListaDetalle != null)
                {
                    resultado = new List<cReciboDetalle>();
                    for (int i = 1; i <= objListaDetalle.Count(); i++)
                    {
                        cReciboDetalle _obj = new cReciboDetalle();
                        dkInterfaceWeb.ReciboItem objItem = objListaDetalle.Item[i];
                        _obj.NumeroRecibo = objItem.NumeroRecibo;
                        _obj.NumeroHoja = objItem.NumeroHoja;
                        _obj.NumeroItem = objItem.NumeroItem;
                        _obj.Descripcion = objItem.Descripcion;
                        _obj.Importe = objItem.Importe == null ? "" : objItem.Importe.ToString();
                        _obj.ID = objItem.ID == null ? "" : objItem.ID.ToString();
                        resultado.Add(_obj);
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroDoc, pLoginWeb);
            }
            return resultado;
        }
        public static void ImprimirComprobante(string pTipoComprobante, string pNroComprobante)
        {
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objServWeb.ImprimirComprobante(pTipoComprobante, pNroComprobante);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pTipoComprobante, pNroComprobante);
            }
        }
        public static cDllSaldosComposicion ObtenerSaldosPresentacionParaComposicion(string pLoginWeb, DateTime pFecha)
        {
            cDllSaldosComposicion resultado = null;
            //   classTiempo tiempo = new classTiempo("ObtenerSaldosPresentacionParaComposicion");
            try
            {
                resultado = new cDllSaldosComposicion();
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                try
                {
                    resultado.SaldoResumenAbierto = objServWeb.ObtenerSaldoResumenAbierto(pLoginWeb);
                }
                catch (Exception ex1)
                {
                    DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex1, DateTime.Now, pLoginWeb, pFecha);
                }

                try
                {
                    resultado.SaldoChequeCartera = objServWeb.ObtenerSaldoChequesEnCartera(pLoginWeb);
                }
                catch (Exception ex2) { DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex2, DateTime.Now, pLoginWeb, pFecha); }
                try
                {
                    resultado.SaldoCtaCte = objServWeb.ObtenerSaldoCtaCteAFecha(pLoginWeb, pFecha);
                }
                catch (Exception ex3) { DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex3, DateTime.Now, pLoginWeb, pFecha); }
                try
                {
                    resultado.SaldoTotal = objServWeb.ObtenerSaldoTotal(pLoginWeb);
                }
                catch (Exception ex4) { DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex4, DateTime.Now, pLoginWeb, pFecha); }
                resultado.isPoseeCuentaResumen = objServWeb.PoseeCuentaResumen(pLoginWeb);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pFecha);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static List<cDllChequeRecibido> ObtenerChequesEnCartera(string pLoginWeb)
        {
            List<cDllChequeRecibido> resultado = null;
            //classTiempo tiempo = new classTiempo("ObtenerChequesEnCartera");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.ChequeRecibidoCOL objChequesRecibidos = objServWeb.ObtenerChequesEnCartera(pLoginWeb);
                if (objChequesRecibidos != null)
                {
                    resultado = new List<cDllChequeRecibido>();
                    for (int i = 1; i <= objChequesRecibidos.Count(); i++)
                    {
                        cDllChequeRecibido objCheque = dllFuncionesGenerales.ConvertToChequeRecibido(objChequesRecibidos[i]);
                        if (objCheque != null)
                        {
                            resultado.Add(objCheque);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static List<cCtaCteMovimiento> ObtenerMovimientosDeCuentaCorriente(bool pIncluyeCancelados, DateTime pDesde, DateTime pHasta, string pLoginWeb)
        {
            List<cCtaCteMovimiento> resultado = null;
            //classTiempo tiempo = new classTiempo("ObtenerMovimientosDeCuentaCorriente");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.CtaCteMovimientoCOL objCtaCteMovimiento = objServWeb.ObtenerMovimientosDeCuentaCorriente(pLoginWeb, pDesde, pHasta, pIncluyeCancelados);
                if (objCtaCteMovimiento != null)
                {
                    resultado = new List<cCtaCteMovimiento>();
                    if (objCtaCteMovimiento.Count() > 0)
                    {
                        DateTime dateValue;
                        for (int i = 1; i <= objCtaCteMovimiento.Count(); i++)
                        {
                            cCtaCteMovimiento obj = new cCtaCteMovimiento();
                            dkInterfaceWeb.CtaCteMovimiento objItem = objCtaCteMovimiento[i];
                            obj.Atraso = objItem.Atraso != null ? objItem.Atraso.ToString() : "";
                            obj.Fecha = DateTime.TryParse(objItem.Fecha.ToString(), out dateValue) ? (DateTime)objItem.Fecha : (DateTime?)null;
                            obj.FechaToString = obj.Fecha != null ? ((DateTime)obj.Fecha).ToShortDateString() : "";
                            obj.FechaPago = DateTime.TryParse(objItem.FechaPago.ToString(), out dateValue) ? (DateTime)objItem.FechaPago : (DateTime?)null;
                            obj.FechaPagoToString = obj.FechaPago != null ? ((DateTime)obj.FechaPago).ToShortDateString() : "";
                            obj.FechaVencimiento = DateTime.TryParse(objItem.FechaVencimiento.ToString(), out dateValue) ? (DateTime)objItem.FechaVencimiento : (DateTime?)null;
                            obj.FechaVencimientoToString = obj.FechaVencimiento != null ? ((DateTime)obj.FechaVencimiento).ToShortDateString() : "";
                            obj.Importe = objItem.Importe;
                            obj.MedioPago = objItem.MedioPago != null ? objItem.MedioPago.ToString() : "";
                            obj.NumeroComprobante = objItem.NumeroComprobante != null ? objItem.NumeroComprobante.ToString() : "";
                            obj.NumeroRecibo = objItem.NumeroRecibo != null ? objItem.NumeroRecibo.ToString() : "";
                            obj.Pago = objItem.Pago != null ? objItem.Pago.ToString() : ""; ;
                            obj.Saldo = objItem.Saldo;
                            obj.Semana = objItem.Semana != null ? objItem.Semana.ToString() : "";
                            obj.TipoComprobante = dllFuncionesGenerales.ToConvert(objItem.TipoComprobante);
                            obj.TipoComprobanteToString = dllFuncionesGenerales.ToConvertToString(objItem.TipoComprobante);
                            resultado.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIncluyeCancelados, pDesde, pHasta, pLoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static cDllRespuestaResumenAbierto ObtenerResumenAbierto(string pLoginWeb)
        {
            cDllRespuestaResumenAbierto resultado = null;
            //classTiempo tiempo = new classTiempo("ObtenerResumenAbierto");
            try
            {
                resultado = new cDllRespuestaResumenAbierto();
                resultado.lista = new List<cDllCtaResumenMovimiento>();
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado.isPoseeCuenta = objServWeb.PoseeCuentaResumen(pLoginWeb);
                resultado.ImporteTotal = objServWeb.ObtenerSaldoResumenAbierto(pLoginWeb);
                dkInterfaceWeb.CtaResumenMovimientoCOL movCuentaResumen = objServWeb.ObtenerMovimientosDeCuentaResumen(pLoginWeb);
                for (int i = 1; i <= movCuentaResumen.Count(); i++)
                {
                    cDllCtaResumenMovimiento objDllCtaResumenMovimiento = new cDllCtaResumenMovimiento();
                    objDllCtaResumenMovimiento.Fecha = movCuentaResumen[i].Fecha;
                    objDllCtaResumenMovimiento.FechaToString = objDllCtaResumenMovimiento.Fecha.ToShortDateString();
                    objDllCtaResumenMovimiento.Importe = movCuentaResumen[i].Importe;
                    objDllCtaResumenMovimiento.NumeroComprobante = movCuentaResumen[i].NumeroComprobante;
                    objDllCtaResumenMovimiento.TipoComprobante = dllFuncionesGenerales.ToConvert(movCuentaResumen[i].Comprobante);
                    objDllCtaResumenMovimiento.TipoComprobanteToString = dllFuncionesGenerales.ToConvertToString(movCuentaResumen[i].Comprobante);
                    resultado.lista.Add(objDllCtaResumenMovimiento);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static List<cFichaCtaCte> ObtenerMovimientosDeFichaCtaCte(string pLoginWeb, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            List<cFichaCtaCte> resultado = new List<cFichaCtaCte>();
            // classTiempo tiempo = new classTiempo("ObtenerMovimientosDeFichaCtaCte");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.MovimientoFichaCtaCteCOL movFichas = objServWeb.ObtenerMovimientosDeFichaCtaCte(pLoginWeb, pFechaDesde, pFechaHasta);
                for (int i = 1; i <= movFichas.Count(); i++)
                {
                    cFichaCtaCte obj = new cFichaCtaCte();
                    if (i == 1)
                    {
                        obj.Saldo = movFichas[i].Monto;
                    }
                    else
                    {
                        if (movFichas[i].Monto <= 0)
                        {
                            obj.Haber = movFichas[i].Monto;
                            obj.Saldo = obj.Haber + resultado[i - 2].Saldo;
                        }
                        else
                        {
                            obj.Debe = movFichas[i].Monto;
                            obj.Saldo = obj.Debe + resultado[i - 2].Saldo;
                        }
                    }
                    obj.Fecha = movFichas[i].FechaMovimiento;
                    obj.FechaToString = movFichas[i].FechaMovimiento.ToShortDateString();
                    obj.FechaVencimiento = movFichas[i].FechaVencimiento == null ? (DateTime?)null : (DateTime)movFichas[i].FechaVencimiento;
                    obj.FechaVencimientoToString = movFichas[i].FechaVencimiento == null ? string.Empty : ((DateTime)movFichas[i].FechaVencimiento).ToShortDateString();
                    obj.Comprobante = movFichas[i].NumeroComprobante;
                    obj.TipoComprobante = dllFuncionesGenerales.ToConvert(movFichas[i].TipoComprobante);
                    obj.TipoComprobanteToString = dllFuncionesGenerales.ToConvertToString(movFichas[i].TipoComprobante);
                    obj.Motivo = movFichas[i].Motivo == null ? string.Empty : movFichas[i].Motivo.ToString();
                    resultado.Add(obj);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pFechaDesde, pFechaHasta);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}          
            return resultado;
        }
        public static decimal? ObtenerCreditoDisponibleSemanal(string pLoginWeb)
        {
            decimal? result = null;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                var objResultado = objServWeb.ObtenerCreditoDisponibleSemanal(pLoginWeb);
                if (objResultado != null)
                {
                    result = Convert.ToDecimal(objResultado);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }

            return result;
        }
        public static decimal? ObtenerCreditoDisponibleTotal(string pLoginWeb)
        {
            decimal? result = null;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                result = objServWeb.ObtenerCreditoDisponibleTotal(pLoginWeb);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            return result;
        }
        public static List<string> ObtenerTiposDeComprobantesAMostrar(string pIdentificadorCliente)
        {
            List<string> resultado = null;
            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
            resultado = new List<string>();
            try
            {
                VBA._Collection lista = objServWeb.ObtenerTiposDeComprobantesAMostrar(pIdentificadorCliente);
                foreach (var item in lista)
                {
                    resultado.Add((string)item);
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdentificadorCliente);
                resultado = null;
            }
            return resultado;
        }
        public static List<cPlan> ObtenerPlanesDeObrasSociales()
        {
            List<cPlan> lista = null;
            //  classTiempo tiempo = new classTiempo("ObtenerPlanesDeObrasSociales");
            try
            {
                lista = new List<cPlan>();
                dkInterfaceWeb.PlanCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerPlanesDeObrasSociales();
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado[i])));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return lista;
        }
        public static List<cCbteParaImprimir> ObtenerComprobantesAImprimirEnBaseAResumen(string pNumeroResumen)
        {
            List<cCbteParaImprimir> lista = null;

            lista = new List<cCbteParaImprimir>();
            try
            {
                dkInterfaceWeb.CbteParaImprimirCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerComprobantesAImprimirEnBaseAResumen(pNumeroResumen);
                if (objResultado != null)
                {
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado[i])));
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroResumen);
                return null;
            }

            return lista;
        }
        public static List<cResumen> ObtenerUltimos10ResumenesDePuntoDeVenta(string pLoginWeb)
        {
            List<cResumen> lista = null;
            lista = new List<cResumen>();
            try
            {
                dkInterfaceWeb.ResumenCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerUltimos10ResumenesDePuntoDeVenta(pLoginWeb);
                if (objResultado != null)
                {
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado[i])));
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
            return lista;
        }
        public static List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes(string NombrePlan, string LoginWeb, int Anio, int Mes)
        {
            List<cPlanillaObSoc> lista = null;
            //classTiempo tiempo = new classTiempo("ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes");
            try
            {
                lista = new List<cPlanillaObSoc>();
                dkInterfaceWeb.PlanillaObSocCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes(NombrePlan, LoginWeb, Anio, Mes);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado.get_Item(i))));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NombrePlan, LoginWeb, Anio, Mes);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}            
            return lista;
        }
        public static List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena(string NombrePlan, string LoginWeb, int Anio, int Mes, int Quincena)
        {

            List<cPlanillaObSoc> lista = null;

            //classTiempo tiempo = new classTiempo("ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena");
            try
            {
                lista = new List<cPlanillaObSoc>();
                dkInterfaceWeb.PlanillaObSocCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena(NombrePlan, LoginWeb, Anio, Mes, Quincena);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado.get_Item(i))));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NombrePlan, LoginWeb, Anio, Mes, Quincena);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}

            return lista;
        }
        public static List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana(string NombrePlan, string LoginWeb, int Anio, int Semana)
        {

            List<cPlanillaObSoc> lista = null;
            //classTiempo tiempo = new classTiempo("ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana");
            try
            {
                lista = new List<cPlanillaObSoc>();
                dkInterfaceWeb.PlanillaObSocCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana(NombrePlan, LoginWeb, Anio, Semana);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado.get_Item(i))));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NombrePlan, LoginWeb, Anio, Semana);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return lista;
        }
        public static List<cConsObraSocial> ObtenerComprobantesObrasSocialesDePuntoDeVentaEntreFechas(string pLoginWeb, string pPlan, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            List<cConsObraSocial> lista = null;
            lista = new List<cConsObraSocial>();
            try
            {
                dkInterfaceWeb.ConsObraSocialCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerComprobantesObrasSocialesDePuntoDeVentaEntreFechas(pLoginWeb, pPlan, pFechaDesde, pFechaHasta);

                if (objResultado != null)
                {
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvert((objResultado[i])));
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pPlan, pFechaDesde, pFechaHasta);
                return null;
            }
            return lista;
        }
        public static List<cComprobantesDiscriminadosDePuntoDeVenta> ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(string pIdentificadorCliente, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            List<cComprobantesDiscriminadosDePuntoDeVenta> resultado = null;
            dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
            try
            {
                resultado = new List<cComprobantesDiscriminadosDePuntoDeVenta>();
                dkInterfaceWeb._ComprobDiscriminadoCOL objLista = objServWeb.ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(pIdentificadorCliente, pFechaDesde, pFechaHasta);
                if (objLista != null)
                {
                    for (int i = 1; i <= objLista.Count(); i++)
                    { //objLista.get_Item(i).
                        cComprobantesDiscriminadosDePuntoDeVenta obj = new cComprobantesDiscriminadosDePuntoDeVenta();
                        obj.Comprobante = objLista.get_Item(i).Comprobante;
                        obj.Destinatario = objLista.get_Item(i).Destinatario;//string
                                                                             //obj.Fecha = objLista.get_Item(i).Fecha == null ? (DateTime?)null : (DateTime)objLista.get_Item(i).Fecha;
                                                                             //obj.FechaToString = objLista.get_Item(i).Fecha == null ? string.Empty : ((DateTime)objLista.get_Item(i).Fecha).ToShortDateString(); //DateTime
                        obj.Fecha = (DateTime)objLista.get_Item(i).Fecha;//DateTime
                        obj.FechaToString = ((DateTime)objLista.get_Item(i).Fecha).ToString(); //string
                        obj.MontoExento = objLista.get_Item(i).MontoExento;//decimal
                        obj.MontoGravado = objLista.get_Item(i).MontoGravado;//decimal
                        obj.MontoIvaInscripto = objLista.get_Item(i).MontoIvaInscripto;//decimal
                        obj.MontoIvaNoInscripto = objLista.get_Item(i).MontoIvaNoInscripto;//decimal
                        obj.MontoPercepcionesDGR = objLista.get_Item(i).MontoPercepcionesDGR;//decimal
                        obj.MontoTotal = objLista.get_Item(i).MontoTotal;//decimal
                        obj.NumeroComprobante = objLista.get_Item(i).NumeroComprobante;//string
                        obj.MontoPercepcionesMunicipal = objLista.get_Item(i).MontoPercepcionesMunicipal;//decimal
                        resultado.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdentificadorCliente, pFechaDesde, pFechaHasta);
                resultado = null;
            }
            return resultado;
        }
        public static double ObtenerSaldoFinalADiciembrePorCliente(string LoginWeb)
        {
            double resultado = 0;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.ObtenerSaldoFinalADiciembrePorCliente(LoginWeb);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, LoginWeb);
            }
            return resultado;
        }
        public static List<cVencimientoResumen> ObtenerVencimientosResumenPorFecha(string NumeroComprobante, DateTime FechaVencimiento)
        {
            List<cVencimientoResumen> resultado = new List<cVencimientoResumen>();
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.VtoResumenPorFechaCOL cVtos = objServWeb.ObtenerVencimientosResumenPorFecha(NumeroComprobante, FechaVencimiento);
                if (cVtos != null)
                {
                    for (int i = 1; i <= cVtos.Count(); i++)
                    {
                        cVencimientoResumen obj = dllFuncionesGenerales.ToConvert(cVtos[i]);
                        resultado.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NumeroComprobante, FechaVencimiento);
            }
            return resultado;
        }
        public static List<cDevolucionItemPrecarga_java> ObtenerReclamosFacturadoNoEnviadoPorCliente(string LoginWeb)
        {
            List<cDevolucionItemPrecarga_java> lista = null;
            // classTiempo tiempo = new classTiempo("ObtenerReclamosFacturadoNoEnviadoPorCliente");
            try
            {
                lista = new List<cDevolucionItemPrecarga_java>();
                dkInterfaceWeb.SolicitudDevClienteCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerReclamosFacturadoNoEnviadoPorCliente(LoginWeb);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ConvertToItemSolicitudDevCliente_java(objResultado[i]));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, LoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return lista;
        }
        public static long ObtenerCantidadSolicitadaDevolucionPorProductoFacturaYCliente(string NombreProducto, string NumeroFactura, string LoginWeb)
        {
            long resultado = 0;
            // classTiempo tiempo = new classTiempo("ObtenerCantidadSolicitadaDevolucionPorProductoFacturaYCliente");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.ObtenerCantidadSolicitadaDevolucionPorProductoFacturaYCliente(NombreProducto, NumeroFactura, LoginWeb);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NombreProducto, NumeroFactura, LoginWeb);
                return 0;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return resultado;
        }
        public static bool EsFacturaConDevolucionesEnProceso(string NumeroFactura, string LoginWeb)
        {
            // classTiempo tiempo = new classTiempo("EsFacturaConDevolucionesEnProceso");
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                return objServWeb.EsFacturaConDevolucionesEnProceso(NumeroFactura, LoginWeb);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NumeroFactura, LoginWeb);
                return false;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
        }
        public static List<cFactura> ObtenerFacturasPorUltimosNumeros(string NumeroFactura, string LoginWeb)
        {
            List<cFactura> lista = null;
            // classTiempo tiempo = new classTiempo("ObtenerFacturasPorUltimosNumeros");
            try
            {
                lista = new List<cFactura>();
                dkInterfaceWeb.FacturaCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerFacturasPorUltimosNumeros(NumeroFactura, LoginWeb);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ConvertToFactura(objResultado[i]));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NumeroFactura, LoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return lista;
        }
        public static string AgregarSolicitudDevolucionCliente(List<cDevolucionItemPrecarga_java> colSDC, string LoginWeb)
        {
            string resultado = null;
            //classTiempo tiempo = new classTiempo("AgregarSolicitudDevolucionCliente");
            try
            {
                dkInterfaceWeb.SolicitudDevClienteCOL colInput;
                colInput = new dkInterfaceWeb.SolicitudDevClienteCOL();
                if (colSDC != null)
                    for (int i = 0; i < colSDC.Count(); i++)
                        colInput.Add(dllFuncionesGenerales.ConvertFromItemSolicitudDevCliente_java(colSDC[i]));
                else
                    return null;

                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.AgregarSolicitudDevolucionCliente(colInput, LoginWeb);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, colSDC, LoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}            
            return resultado;
        }
        public static string AgregarReclamoFacturadoNoEnviado(List<cDevolucionItemPrecarga_java> colSDC, string LoginWeb)
        {
            string resultado = null;
            //classTiempo tiempo = new classTiempo("AgregarReclamoFacturadoNoEnviado");
            try
            {
                dkInterfaceWeb.SolicitudDevClienteCOL colInput;
                colInput = new dkInterfaceWeb.SolicitudDevClienteCOL();
                if (colSDC != null)
                    for (int i = 0; i < colSDC.Count(); i++)
                        colInput.Add(dllFuncionesGenerales.ConvertFromItemSolicitudDevCliente_java(colSDC[i]));
                else
                    return null;

                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                resultado = objServWeb.AgregarReclamoFacturadoNoEnviadoDesdeLaWeb(colInput, LoginWeb);
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, colSDC, LoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}            
            return resultado;
        }
        public static List<cLote> ObtenerNumerosLoteDeProductoDeFacturaProveedorLogLotesConCadena(string NombreProducto, string CadenaBusqueda, string LoginWeb)
        {

            List<cLote> lista = null;
            // classTiempo tiempo = new classTiempo("ObtenerNumerosLoteDeProductoDeFacturaProveedorLogLotesConCadena");
            try
            {
                lista = new List<cLote>();
                dkInterfaceWeb.LoteCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerNumerosLoteDeProductoDeFacturaProveedorLogLotesConCadena(NombreProducto, CadenaBusqueda, LoginWeb);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ConvertToLote(objResultado[i]));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NombreProducto, CadenaBusqueda, LoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}            
            return lista;
        }
        public static List<cDevolucionItemPrecarga_java> ObtenerReclamosFacturadoNoEnviadoPorClientePorNumero(string NumeroReclamo, string LoginWeb)
        {
            List<cDevolucionItemPrecarga_java> lista = null;
            // classTiempo tiempo = new classTiempo("ObtenerReclamosFacturadoNoEnviadoPorClientePorNumero");
            try
            {
                lista = new List<cDevolucionItemPrecarga_java>();
                dkInterfaceWeb.SolicitudDevClienteCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerReclamosFacturadoNoEnviadoPorClientePorNumero(NumeroReclamo, LoginWeb);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ConvertToItemSolicitudDevCliente_java(objResultado[i]));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NumeroReclamo, LoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}            
            return lista;
        }
        public static List<cDevolucionItemPrecarga_java> ObtenerSolicitudesDevolucionClientePorNumero(string NumeroSolicitud, string LoginWeb)
        {
            List<cDevolucionItemPrecarga_java> lista = null;
            //classTiempo tiempo = new classTiempo("ObtenerSolicitudesDevolucionClientePorNumero");
            try
            {
                lista = new List<cDevolucionItemPrecarga_java>();
                dkInterfaceWeb.SolicitudDevClienteCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerSolicitudesDevolucionClientePorNumero(NumeroSolicitud, LoginWeb);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ConvertToItemSolicitudDevCliente_java(objResultado[i]));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, NumeroSolicitud, LoginWeb);
                return null;
            }
            //finally
            //{
            //    tiempo.Parar();
            //}
            return lista;
        }
        public static List<cDevolucionItemPrecarga_java> ObtenerSolicitudesDevolucionCliente(string pLoginWeb)
        {
            List<cDevolucionItemPrecarga_java> lista = null;
            try
            {

                lista = new List<cDevolucionItemPrecarga_java>();
                dkInterfaceWeb.SolicitudDevClienteCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerSolicitudesDevolucionCliente(pLoginWeb);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ConvertToItemSolicitudDevCliente_java(objResultado[i]));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }


            return lista;
        }
        public static List<cPedidoItem> ObtenerItemsDePedidoPorNumeroDeFactura(string pNumeroFactura, string pLoginWeb)
        {
            List<cPedidoItem> lista = null;
            try
            {
                lista = new List<cPedidoItem>();
                dkInterfaceWeb.PedidoItemCOL objResultado;
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                objResultado = objServWeb.ObtenerItemsDePedidoPorNumeroDeFactura(pNumeroFactura, pLoginWeb);
                if (objResultado != null)
                    for (int i = 1; i <= objResultado.Count(); i++)
                        lista.Add(dllFuncionesGenerales.ToConvertPedidoItem(objResultado[i]));
                else
                    return null;
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroFactura, pLoginWeb);
                return null;
            }


            return lista;
        }
        public static List<cComprobanteDiscriminado> ObtenerComprobantesEntreFechas(string pTipoComprobante, DateTime pDesde, DateTime pHasta, string pLoginWeb)
        {
            List<cComprobanteDiscriminado> resultado = null;
            try
            {
                dkInterfaceWeb.ServiciosWEB objServWeb = new dkInterfaceWeb.ServiciosWEB();
                dkInterfaceWeb.ComprobDiscriminadoCOL objComprobantes = objServWeb.ObtenerComprobantesDePuntoDeVentaEntreFechas(pLoginWeb, pDesde, pHasta, pTipoComprobante);
                if (objComprobantes != null)
                {
                    resultado = new List<cComprobanteDiscriminado>();
                    if (objComprobantes.Count() > 0)
                    {

                        for (int i = 1; i <= objComprobantes.Count(); i++)
                        {
                            cComprobanteDiscriminado obj = new cComprobanteDiscriminado();
                            dkInterfaceWeb.ComprobanteDiscriminado objItem = objComprobantes.get_Item(i);
                            dkInterfaceWeb.ComprobanteDiscriminado r = new dkInterfaceWeb.ComprobanteDiscriminado();
                            obj.Comprobante = objItem.Comprobante;
                            obj.Destinatario = objItem.Destinatario;
                            obj.DetallePercepciones = objItem.DetallePercepciones != null ? objItem.DetallePercepciones.ToString() : "";
                            obj.Fecha = objItem.Fecha;
                            obj.FechaToString = objItem.Fecha != null ? objItem.Fecha.ToShortDateString() : "";
                            obj.MontoExento = objItem.MontoExento;
                            obj.MontoGravado = objItem.MontoGravado;
                            obj.MontoIvaInscripto = objItem.MontoIvaInscripto;
                            obj.MontoIvaNoInscripto = objItem.MontoIvaNoInscripto;
                            obj.MontoPercepcionDGR = objItem.MontoPercepcionesDGR;
                            obj.MontoTotal = objItem.MontoTotal;
                            obj.NumeroComprobante = objItem.NumeroComprobante;
                            resultado.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pTipoComprobante, pDesde, pHasta, pLoginWeb);
                return null;
            }
            return resultado;
        }
    }
}