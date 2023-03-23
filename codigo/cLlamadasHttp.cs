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
            return cLlamadasDLL.ObtenerMovimientosDeFichaCtaCte( pLoginWeb,  pFechaDesde,  pFechaHasta);
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
            return cLlamadasDLL.ObtenerComprobantesAImprimirEnBaseAResumen( pNumeroResumen);
        }
        public static List<cResumen> ObtenerUltimos10ResumenesDePuntoDeVenta(string pLoginWeb)
        {
            return cLlamadasDLL.ObtenerUltimos10ResumenesDePuntoDeVenta(pLoginWeb);
        }
        public static List<cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes(string NombrePlan, string LoginWeb, int Anio, int Mes)
        {
            return cLlamadasDLL.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes( NombrePlan,  LoginWeb,  Anio,  Mes);
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
            return cLlamadasDLL.ObtenerComprobantesObrasSocialesDePuntoDeVentaEntreFechas(pLoginWeb,  pPlan,  pFechaDesde,  pFechaHasta);
        }
        public static List<cComprobantesDiscriminadosDePuntoDeVenta> ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(string pIdentificadorCliente, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            return cLlamadasDLL.ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas( pIdentificadorCliente,  pFechaDesde,  pFechaHasta);
        }
        public static double ObtenerSaldoFinalADiciembrePorCliente(string LoginWeb)
        {
            return cLlamadasDLL.ObtenerSaldoFinalADiciembrePorCliente(LoginWeb);
        }
        public static List<cVencimientoResumen> ObtenerVencimientosResumenPorFecha(string NumeroComprobante, DateTime FechaVencimiento)
        {
            return cLlamadasDLL.ObtenerVencimientosResumenPorFecha( NumeroComprobante, FechaVencimiento);
        }
    }
}