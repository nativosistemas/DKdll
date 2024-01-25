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
        public static List<cDllPedido> ObtenerPedidosEntreFechas(ObtenerPedidosEntreFechasRequest pValue)
        {
            return cLlamadasDLL.ObtenerPedidosEntreFechas(pValue.pDesde, pValue.pHasta, pValue.pLoginWeb);
        }
        public static void ModificarPasswordWEB(string pIdentificadorCliente, string pPassActual, string pPassNueva)
        {
            cLlamadasDLL.ModificarPasswordWEB(pIdentificadorCliente, pPassActual, pPassNueva);
        }
        public static cFactura ObtenerFactura(string pNumeroFactura, string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerFactura(pNumeroFactura, pLoginWeb);
        }
        public static cNotaDeCredito ObtenerNotaDeCredito(string pNumeroNotaDeCredito, string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerNotaDeCredito(pNumeroNotaDeCredito, pLoginWeb);
        }
        public static cNotaDeDebito ObtenerNotaDeDebito(string pNumeroNotaDeDebito, string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerNotaDeDebito(pNumeroNotaDeDebito, pLoginWeb);
        }
        public static cResumen ObtenerResumenCerrado(string pNumeroResumen, string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerResumenCerrado(pNumeroResumen, pLoginWeb);
        }
        public static cObraSocialCliente ObtenerObraSocialCliente(string pNumeroObraSocialCliente, string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerObraSocialCliente(pNumeroObraSocialCliente, pLoginWeb);
        }
        public static cRecibo ObtenerRecibo(string pNumeroDoc, string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerRecibo(pNumeroDoc, pLoginWeb);
        }
        public static void ImprimirComprobante(string pTipoComprobante, string pNroComprobante)
        {
            cLlamadasDLL.ImprimirComprobante(pTipoComprobante, pNroComprobante);
        }
        public static cDllSaldosComposicion ObtenerSaldosPresentacionParaComposicion(string pLoginWeb, DateTime pFecha)
        {
            return cLlamadasDLL.ObtenerSaldosPresentacionParaComposicion(pLoginWeb, pFecha);
        }
        public static List<cDllChequeRecibido> ObtenerChequesEnCartera(string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerChequesEnCartera(pLoginWeb);
        }
        public static List<cCtaCteMovimiento> ObtenerMovimientosDeCuentaCorriente(bool pIncluyeCancelados, DateTime pDesde, DateTime pHasta, string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerMovimientosDeCuentaCorriente(pIncluyeCancelados, pDesde, pHasta, pLoginWeb);
        }
        public static cDllRespuestaResumenAbierto ObtenerResumenAbierto(string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerResumenAbierto(pLoginWeb);
        }
        public static List<cFichaCtaCte> ObtenerMovimientosDeFichaCtaCte(string pLoginWeb, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            return cLlamadasDLL.ObtenerMovimientosDeFichaCtaCte(pLoginWeb, pFechaDesde, pFechaHasta);
        }
        public static decimal? ObtenerCreditoDisponibleSemanal(string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerCreditoDisponibleSemanal(pLoginWeb);
        }
        public static decimal? ObtenerCreditoDisponibleTotal(string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerCreditoDisponibleTotal(pLoginWeb);
        }
        public static List<string> ObtenerTiposDeComprobantesAMostrar(string pIdentificadorCliente)
        {
            return cLlamadasDLL.ObtenerTiposDeComprobantesAMostrar(pIdentificadorCliente);
        }
        public static List<cPlan> ObtenerPlanesDeObrasSociales()
        {
            return cLlamadasDLL.ObtenerPlanesDeObrasSociales();
        }
        public static List<cCbteParaImprimir> ObtenerComprobantesAImprimirEnBaseAResumen(string pNumeroResumen)
        {
            return cLlamadasDLL.ObtenerComprobantesAImprimirEnBaseAResumen(pNumeroResumen);
        }
        public static List<cResumen> ObtenerUltimos10ResumenesDePuntoDeVenta(string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerUltimos10ResumenesDePuntoDeVenta(pLoginWeb);
        }
        public static List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes(string NombrePlan, string LoginWeb, int Anio, int Mes)
        {
            return cLlamadasDLL.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes(NombrePlan, LoginWeb, Anio, Mes);
        }
        public static List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena(string NombrePlan, string LoginWeb, int Anio, int Mes, int Quincena)
        {
            return cLlamadasDLL.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena(NombrePlan, LoginWeb, Anio, Mes, Quincena);
        }
        public static List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana(string NombrePlan, string LoginWeb, int Anio, int Semana)
        {
            return cLlamadasDLL.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana(NombrePlan, LoginWeb, Anio, Semana);
        }
        public static List<cConsObraSocial> ObtenerComprobantesObrasSocialesDePuntoDeVentaEntreFechas(string pLoginWeb, string pPlan, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            return cLlamadasDLL.ObtenerComprobantesObrasSocialesDePuntoDeVentaEntreFechas(pLoginWeb, pPlan, pFechaDesde, pFechaHasta);
        }
        public static List<cComprobantesDiscriminadosDePuntoDeVenta> ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(string pIdentificadorCliente, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            return cLlamadasDLL.ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(pIdentificadorCliente, pFechaDesde, pFechaHasta);
        }
        public static double ObtenerSaldoFinalADiciembrePorCliente(string LoginWeb)
        {
            return cLlamadasDLL.ObtenerSaldoFinalADiciembrePorCliente(LoginWeb);
        }
        public static List<cVencimientoResumen> ObtenerVencimientosResumenPorFecha(string NumeroComprobante, DateTime FechaVencimiento)
        {
            return cLlamadasDLL.ObtenerVencimientosResumenPorFecha(NumeroComprobante, FechaVencimiento);
        }
        public static List<cDevolucionItemPrecarga_java> ObtenerReclamosFacturadoNoEnviadoPorCliente(string LoginWeb)
        {
            return cLlamadasDLL.ObtenerReclamosFacturadoNoEnviadoPorCliente(LoginWeb);
        }
        public static long ObtenerCantidadSolicitadaDevolucionPorProductoFacturaYCliente(string NombreProducto, string NumeroFactura, string LoginWeb)
        {
            return cLlamadasDLL.ObtenerCantidadSolicitadaDevolucionPorProductoFacturaYCliente(NombreProducto, NumeroFactura, LoginWeb);
        }
        public static bool EsFacturaConDevolucionesEnProceso(string NumeroFactura, string LoginWeb)
        {
            return cLlamadasDLL.EsFacturaConDevolucionesEnProceso(NumeroFactura, LoginWeb);
        }
        public static List<cFactura> ObtenerFacturasPorUltimosNumeros(string NumeroFactura, string LoginWeb)
        {
            return cLlamadasDLL.ObtenerFacturasPorUltimosNumeros(NumeroFactura, LoginWeb);
        }
        public static string AgregarSolicitudDevolucionCliente(List<cDevolucionItemPrecarga_java> colSDC, string LoginWeb)
        {
            return cLlamadasDLL.AgregarSolicitudDevolucionCliente(colSDC, LoginWeb);
        }
        public static string AgregarReclamoFacturadoNoEnviado(List<cDevolucionItemPrecarga_java> colSDC, string LoginWeb)
        {
            return cLlamadasDLL.AgregarReclamoFacturadoNoEnviado(colSDC, LoginWeb);
        }
        public static List<cLote> ObtenerNumerosLoteDeProductoDeFacturaProveedorLogLotesConCadena(string NombreProducto, string CadenaBusqueda, string LoginWeb)
        {
            return cLlamadasDLL.ObtenerNumerosLoteDeProductoDeFacturaProveedorLogLotesConCadena(NombreProducto, CadenaBusqueda, LoginWeb);
        }
        public static List<cDevolucionItemPrecarga_java> ObtenerReclamosFacturadoNoEnviadoPorClientePorNumero(string NumeroReclamo, string LoginWeb)
        {
            return cLlamadasDLL.ObtenerReclamosFacturadoNoEnviadoPorClientePorNumero(NumeroReclamo, LoginWeb);
        }
        public static List<cDevolucionItemPrecarga_java> ObtenerSolicitudesDevolucionClientePorNumero(string NumeroSolicitud, string LoginWeb)
        {
            return cLlamadasDLL.ObtenerSolicitudesDevolucionClientePorNumero(NumeroSolicitud, LoginWeb);
        }
        public static List<cDevolucionItemPrecarga_java> ObtenerSolicitudesDevolucionCliente(string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerSolicitudesDevolucionCliente(pLoginWeb);
        }
        public static List<cPedidoItem> ObtenerItemsDePedidoPorNumeroDeFactura(string pNumeroFactura, string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerItemsDePedidoPorNumeroDeFactura(pNumeroFactura, pLoginWeb);
        }
        public static List<cComprobanteDiscriminado> ObtenerComprobantesEntreFechas(string pTipoComprobante, DateTime pDesde, DateTime pHasta, string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerComprobantesEntreFechas(pTipoComprobante, pDesde, pHasta, pLoginWeb);
        }
    }
}