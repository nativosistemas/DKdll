using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using DKbase.dll;

namespace DKdll.codigo
{
    public class Autenticacion : SoapHeader
    {
        private string sUserPass;
        private string sUserName;

        /// <summary> 
        /// Lee o escribe la clave del usuario 
        /// </summary> 
        public string UsuarioClave
        {
            get
            {
                return sUserPass;
            }
            set
            {
                sUserPass = value;
            }

        }
        /// <summary> 
        /// Lee o escribe el nombre del usuario 
        /// </summary> 
        public string UsuarioNombre
        {
            get
            {
                return sUserName;
            }
            set
            {
                sUserName = value;
            }
        }
    }
    public static class Serializador
    {
        public static string SerializarToXml(object obj)
        {
            try
            {
                StringWriter strWriter = new StringWriter();
                XmlSerializer serializer = new XmlSerializer(obj.GetType());

                serializer.Serialize(strWriter, obj);
                string resultXml = strWriter.ToString();
                strWriter.Close();

                return resultXml;
            }
            catch
            {
                return string.Empty;
            }
        }
        //Deserializar un XML a un objeto T 
        public static T DeserializarToXml<T>(string xmlSerializado)
        {
            try
            {
                XmlSerializer xmlSerz = new XmlSerializer(typeof(T));

                using (StringReader strReader = new StringReader(xmlSerializado))
                {
                    object obj = xmlSerz.Deserialize(strReader);
                    return (T)obj;
                }
            }
            catch { return default(T); }
        }
        /// <summary> 
        /// Método extensor para serializar JSON cualquier objeto 
        /// </summary> 
        public static string SerializarAJson(this object objeto)
        {
            string jsonResultado = string.Empty;
            try
            {
                DataContractJsonSerializer jsonSerializar = new DataContractJsonSerializer(objeto.GetType());
                MemoryStream ms = new MemoryStream();
                jsonSerializar.WriteObject(ms, objeto);
                jsonResultado = Encoding.UTF8.GetString(ms.ToArray());
            }
            catch { throw; }
            return jsonResultado;
        }
        public static T DeserializarJson<T>(this string jsonSerializado)
        {
            try
            {
                T obj = Activator.CreateInstance<T>();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonSerializado));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
                ms.Close();
                ms.Dispose();
                return obj;
            }
            catch { return default(T); }
        }

    }
    public class classTiempo
    {
        Stopwatch stopWatch = new Stopwatch();
        string nombre = string.Empty;
        DateTime FechaActual;
        bool isGrabar = false;
        public classTiempo(string NombreFuncion)
        {
            if (isGrabar)
            {
                nombre = NombreFuncion;
                FechaActual = DateTime.Now;
                stopWatch.Start();
            }
        }
        public void Parar()
        {
            if (isGrabar)
            {
                try
                {
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    string path = @"c:/" + @"LogTiempoWebService" + @"/";
                    if (Directory.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                    }
                    string nombreArchivo = "tiempo.txt";
                    string FechaToString = FechaActual.Year.ToString("0000") + "_" + FechaActual.Month.ToString("00") + "_" + FechaActual.Day.ToString("00") + "_h_" + FechaActual.Hour.ToString("00") + "_" + FechaActual.Minute.ToString("00") + "_" + FechaActual.Second.ToString("00") + "_" + FechaActual.Millisecond.ToString("000");
                    string FilePath = path + nombreArchivo;
                    StreamWriter sw = null;
                    if (!File.Exists(FilePath))
                    {
                        sw = File.CreateText(FilePath);
                    }
                    else
                    {
                        sw = File.AppendText(FilePath);
                    }
                    sw.WriteLine("Fecha: " + FechaToString + " // Nombre: " + nombre + " // tiempo: " + ts.ToString());
                    sw.Close();
                }
                catch { }
            }
        }
    }
    public static class dllFuncionesGenerales
    {
        public static dllEstadosPedido ToConvert(dkInterfaceWeb.EstadosPedido pEstado)
        {
            switch (pEstado)
            {
                case dkInterfaceWeb.EstadosPedido.Anulado:
                    return dllEstadosPedido.Anulado;
                //break;
                case dkInterfaceWeb.EstadosPedido.EnPreparacion:
                    return dllEstadosPedido.EnPreparacion;
                //break;
                case dkInterfaceWeb.EstadosPedido.EnSucursal:
                    return dllEstadosPedido.EnSucursal;
                //break;
                case dkInterfaceWeb.EstadosPedido.Enviado:
                    return dllEstadosPedido.Enviado;
                //break;
                case dkInterfaceWeb.EstadosPedido.PendienteDeFacturar:
                    return dllEstadosPedido.PendienteDeFacturar;
                //break;
                case dkInterfaceWeb.EstadosPedido.Detenido:
                    return dllEstadosPedido.Detenido;
                default:
                    return dllEstadosPedido.Anulado;
                    //    break;
            }
        }
        public static string ToConvertToString(dkInterfaceWeb.EstadosPedido pEstado)
        {
            switch (pEstado)
            {
                case dkInterfaceWeb.EstadosPedido.Anulado:
                    return "Anulado";
                //break;
                case dkInterfaceWeb.EstadosPedido.EnPreparacion:
                    return "EnPreparacion";
                //break;
                case dkInterfaceWeb.EstadosPedido.EnSucursal:
                    return "EnSucursal";
                //break;
                case dkInterfaceWeb.EstadosPedido.Enviado:
                    return "Enviado";
                //break;
                case dkInterfaceWeb.EstadosPedido.PendienteDeFacturar:
                    return "PendienteDeFacturar";
                //break;
                case dkInterfaceWeb.EstadosPedido.Detenido:
                    return "Detenido";
                default:
                    return "";
                    //    break;
            }
        }
        public static cDllPedidoItem ToConvert(dkInterfaceWeb.PedidoItem pPedidoItem)
        {
            cDllPedidoItem obj = new cDllPedidoItem();
            obj.Cantidad = pPedidoItem.Cantidad;
            obj.Caracteristica = pPedidoItem.Caracteristica != null ? pPedidoItem.Caracteristica.ToString() : string.Empty;
            obj.Faltas = pPedidoItem.Faltas;
            obj.FechaIngreso = pPedidoItem.FechaIngreso.ToString() != string.Empty ? (DateTime?)(pPedidoItem.FechaIngreso) : null;
            obj.NombreObjetoComercial = pPedidoItem.NombreObjetoComercial;
            return obj;
        }
        public static cDllPedido ToConvert(dkInterfaceWeb.Pedido pPedidos)
        {
            if (pPedidos != null)
            {
                cDllPedido resultado = new cDllPedido();
                resultado.Cancelado = pPedidos.Cancelado;
                resultado.CantidadRenglones = pPedidos.CantidadRenglones;
                resultado.CantidadUnidad = pPedidos.CantidadUnidades;
                resultado.Error = pPedidos.Error;
                resultado.Estado = dllFuncionesGenerales.ToConvert(pPedidos.Estado);
                resultado.EstadoToString = dllFuncionesGenerales.ToConvertToString(pPedidos.Estado);
                resultado.Items = new List<cDllPedidoItem>();
                for (int i = 1; i <= pPedidos.Count(); i++)
                {
                    resultado.Items.Add(dllFuncionesGenerales.ToConvert(pPedidos.get_Item(i)));
                }
                //resultado.FechaIngreso = pPedidos.FechaIngreso.ToString() != string.Empty ? (DateTime?)(pPedidos.FechaIngreso) : null;
                DateTime dateValue;
                resultado.FechaIngreso = DateTime.TryParse(pPedidos.FechaIngreso.ToString(), out dateValue) ? (DateTime)pPedidos.FechaIngreso : (DateTime?)null;
                resultado.FechaIngresoToString = resultado.FechaIngreso != null ? ((DateTime)resultado.FechaIngreso).ToShortDateString() : string.Empty;
                resultado.FechaIngresoHoraToString = resultado.FechaIngreso != null ? ((DateTime)resultado.FechaIngreso).ToShortTimeString() : string.Empty;

                List<cDllPedidoItem> listaPedidoItem = new List<cDllPedidoItem>();
                if (pPedidos.ItemsConProblemasDeCredito != null)
                {
                    foreach (dkInterfaceWeb.PedidoItem itemPedidoItem in pPedidos.ItemsConProblemasDeCredito)
                    {
                        listaPedidoItem.Add(dllFuncionesGenerales.ToConvert(itemPedidoItem));
                    }
                }
                resultado.ItemsConProblemasDeCreditos = listaPedidoItem;
                resultado.Login = pPedidos.Login;
                resultado.MensajeEnFactura = pPedidos.MensajeEnFactura != null ? pPedidos.MensajeEnFactura.ToString() : string.Empty;
                resultado.MensajeEnRemito = pPedidos.MensajeEnRemito != null ? pPedidos.MensajeEnRemito.ToString() : string.Empty;
                resultado.MontoTotal = pPedidos.MontoTotal;
                resultado.NumeroFactura = pPedidos.NumeroFactura != null ? pPedidos.NumeroFactura.ToString() : string.Empty;
                resultado.NumeroRemito = pPedidos.NumeroRemito != null ? pPedidos.NumeroRemito.ToString() : string.Empty;
                resultado.DetalleSucursal = pPedidos.DetalleSucursal != null ? pPedidos.DetalleSucursal.ToString() : string.Empty;
                return resultado;
            }
            else { return null; }
        }
        public static cDllPedidoTransfer ToConvertTransfer(dkInterfaceWeb.Pedido pPedidos)
        {
            cDllPedidoTransfer resultado = new cDllPedidoTransfer();
            resultado.Cancelado = pPedidos.Cancelado;
            resultado.CantidadRenglones = pPedidos.CantidadRenglones;
            resultado.CantidadUnidad = pPedidos.CantidadUnidades;
            resultado.Error = pPedidos.Error;
            resultado.Estado = dllFuncionesGenerales.ToConvert(pPedidos.Estado);
            resultado.EstadoToString = dllFuncionesGenerales.ToConvertToString(pPedidos.Estado);
            resultado.Items = new List<cDllPedidoItem>();
            for (int i = 1; i <= pPedidos.Count(); i++)
            {
                resultado.Items.Add(dllFuncionesGenerales.ToConvert(pPedidos.get_Item(i)));
            }
            //resultado.FechaIngreso = pPedidos.FechaIngreso.ToString() != string.Empty ? (DateTime?)(pPedidos.FechaIngreso) : null;
            DateTime dateValue;
            resultado.FechaIngreso = DateTime.TryParse(pPedidos.FechaIngreso.ToString(), out dateValue) ? (DateTime)pPedidos.FechaIngreso : (DateTime?)null;
            resultado.FechaIngresoToString = resultado.FechaIngreso != null ? ((DateTime)resultado.FechaIngreso).ToShortDateString() : string.Empty;
            resultado.FechaIngresoHoraToString = resultado.FechaIngreso != null ? ((DateTime)resultado.FechaIngreso).ToShortTimeString() : string.Empty;

            List<cDllPedidoItem> listaPedidoItem = new List<cDllPedidoItem>();
            if (pPedidos.ItemsConProblemasDeCredito != null)
            {
                foreach (dkInterfaceWeb.PedidoItem itemPedidoItem in pPedidos.ItemsConProblemasDeCredito)
                {
                    listaPedidoItem.Add(dllFuncionesGenerales.ToConvert(itemPedidoItem));
                }
            }
            resultado.ItemsConProblemasDeCreditos = listaPedidoItem;
            resultado.Login = pPedidos.Login;
            resultado.MensajeEnFactura = pPedidos.MensajeEnFactura != null ? pPedidos.MensajeEnFactura.ToString() : string.Empty;
            resultado.MensajeEnRemito = pPedidos.MensajeEnRemito != null ? pPedidos.MensajeEnRemito.ToString() : string.Empty;
            resultado.MontoTotal = pPedidos.MontoTotal;
            resultado.NumeroFactura = pPedidos.NumeroFactura != null ? pPedidos.NumeroFactura.ToString() : string.Empty;
            resultado.NumeroRemito = pPedidos.NumeroRemito != null ? pPedidos.NumeroRemito.ToString() : string.Empty;
            return resultado;
        }
        public static dllTipoComprobante ToConvert(dkInterfaceWeb.TiposComprobante pEstado)
        {
            switch (pEstado)
            {
                case dkInterfaceWeb.TiposComprobante.CIE:
                    return dllTipoComprobante.CIE;
                case dkInterfaceWeb.TiposComprobante.FAC:
                    return dllTipoComprobante.FAC;
                case dkInterfaceWeb.TiposComprobante.NCI:
                    return dllTipoComprobante.NCI;
                case dkInterfaceWeb.TiposComprobante.NCR:
                    return dllTipoComprobante.NCR;
                case dkInterfaceWeb.TiposComprobante.NDE:
                    return dllTipoComprobante.NDE;
                case dkInterfaceWeb.TiposComprobante.NDI:
                    return dllTipoComprobante.NDI;
                case dkInterfaceWeb.TiposComprobante.REC:
                    return dllTipoComprobante.REC;
                case dkInterfaceWeb.TiposComprobante.RES:
                    return dllTipoComprobante.RES;
                case dkInterfaceWeb.TiposComprobante.OSC:
                    return dllTipoComprobante.OSC;
                default:
                    return dllTipoComprobante.NN;
                    //    break;
            }
        }

        //public static string ToConvertToString(string pValue)
        //{
        //    switch (Convert.ToInt32(pValue))
        //    {
        //        case 0:
        //            return "FAC";
        //        case 1:
        //            return "REC";
        //        case 2:
        //            return "NDE";
        //        case 3:
        //            return "NCR";
        //        case 4:
        //            return "NDI";
        //        case 5:
        //            return "NCI";
        //        case 9:
        //            return "RES";
        //        case 10:
        //            return "CIE";
        //        case 13:
        //            return "OSC";
        //        default:
        //            return "";
        //        //FAC = 0,
        //        //REC = 1,
        //        //NDE = 2,
        //        //NCR = 3,
        //        //NDI = 4,
        //        //NCI = 5,
        //        //RES = 9,
        //        //CIE = 10,
        //        //OSC = 13,
        //    }
        //}
        public static string ToConvertToString(dkInterfaceWeb.TiposComprobante pEstado)
        {
            switch (pEstado)
            {
                case dkInterfaceWeb.TiposComprobante.CIE:
                    return "CIE";
                case dkInterfaceWeb.TiposComprobante.FAC:
                    return "FAC";
                case dkInterfaceWeb.TiposComprobante.NCI:
                    return "NCI";
                case dkInterfaceWeb.TiposComprobante.NCR:
                    return "NCR";
                case dkInterfaceWeb.TiposComprobante.NDE:
                    return "NDE";
                case dkInterfaceWeb.TiposComprobante.NDI:
                    return "NDI";
                case dkInterfaceWeb.TiposComprobante.REC:
                    return "REC";
                case dkInterfaceWeb.TiposComprobante.RES:
                    return "RES";
                case dkInterfaceWeb.TiposComprobante.OSC:
                    return "OSC";
                default:
                    return "";

            }
        }

        public static cResumen ToConvert(dkInterfaceWeb.Resumen pResumen)
        {
            DateTime dateValue;
            cResumen obj = new cResumen();
            obj.Numero = pResumen.Numero;
            obj.NumeroSemana = pResumen.NumeroSemana != null ? pResumen.NumeroSemana.ToString() : string.Empty;
            obj.PeriodoDesde = DateTime.TryParse(pResumen.PeriodoDesde.ToString(), out dateValue) ? (DateTime)pResumen.PeriodoDesde : (DateTime?)null;
            obj.PeriodoDesdeToString = obj.PeriodoDesde != null ? ((DateTime)obj.PeriodoDesde).ToShortDateString() : string.Empty;
            obj.PeriodoHasta = DateTime.TryParse(pResumen.PeriodoHasta.ToString(), out dateValue) ? (DateTime)pResumen.PeriodoHasta : (DateTime?)null;
            obj.PeriodoHastaToString = obj.PeriodoHasta != null ? ((DateTime)obj.PeriodoHasta).ToShortDateString() : string.Empty;
            obj.TotalResumen = pResumen.TotalResumen;
            return obj;
        }
        public static cResumenDetalle ToConvert(dkInterfaceWeb.ResumenItem pResumenDetalle)
        {
            cResumenDetalle obj = new cResumenDetalle();
            obj.Descripcion = pResumenDetalle.Descripcion != null ? pResumenDetalle.Descripcion.ToString() : string.Empty;
            obj.Dia = pResumenDetalle.Dia != null ? pResumenDetalle.Dia.ToString() : string.Empty;
            obj.Importe = pResumenDetalle.Importe != null ? pResumenDetalle.Importe.ToString() : string.Empty;
            obj.NumeroHoja = pResumenDetalle.NumeroHoja != null ? pResumenDetalle.NumeroHoja.ToString() : string.Empty;
            obj.NumeroItem = pResumenDetalle.NumeroItem;
            obj.NumeroResumen = pResumenDetalle.NumeroResumen != null ? pResumenDetalle.NumeroResumen.ToString() : string.Empty;
            obj.TipoComprobante = (pResumenDetalle.TipoComprobante != null && !string.IsNullOrEmpty(pResumenDetalle.TipoComprobante.ToString()))
                ?
                Enum.Parse(typeof(dkInterfaceWeb.TiposComprobante), pResumenDetalle.TipoComprobante.ToString()).ToString() :
                string.Empty;
            return obj;
        }

        public static cVencimientoResumen ToConvert(dkInterfaceWeb.VtoResumenPorFecha pVto)
        {
            DateTime dateValue;
            cVencimientoResumen obj = new cVencimientoResumen();
            obj.Tipo = pVto.Tipo;
            obj.NumeroComprobante = pVto.NumeroComprobante;
            obj.Fecha = DateTime.TryParse(pVto.Fecha.ToString(), out dateValue) ? (DateTime)pVto.Fecha : (DateTime?)null;
            obj.FechaToString = obj.Fecha != null ? ((DateTime)obj.Fecha).ToShortDateString() : "";
            obj.FechaVencimiento = DateTime.TryParse(pVto.FechaVencimiento.ToString(), out dateValue) ? (DateTime)pVto.FechaVencimiento : (DateTime?)null;
            obj.FechaVencimientoToString = obj.FechaVencimiento != null ? ((DateTime)obj.FechaVencimiento).ToShortDateString() : "";
            obj.Importe = pVto.Importe;
            return obj;
        }

        public static cNotaDeDebito ConvertToNotaDeDebito(dkInterfaceWeb.NotaDeDebito pObjNotaDeDebito)
        {
            cNotaDeDebito resultado = null;
            if (pObjNotaDeDebito != null)
            {
                resultado = new cNotaDeDebito();
                resultado.CantidadHojas = pObjNotaDeDebito.CantidadHojas;
                resultado.Destinatario = pObjNotaDeDebito.Destinatario;
                DateTime dateValue;
                resultado.Fecha = DateTime.TryParse(pObjNotaDeDebito.Fecha.ToString(), out dateValue) ? (DateTime)pObjNotaDeDebito.Fecha : (DateTime?)null;
                resultado.FechaToString = resultado.Fecha != null ? ((DateTime)resultado.Fecha).ToShortDateString() : string.Empty;
                try
                {
                    if (pObjNotaDeDebito.Numero[0].ToString() != "B")
                    {
                        resultado.MontoExento = pObjNotaDeDebito.MontoExento;
                        resultado.MontoGravado = pObjNotaDeDebito.MontoGravado;
                        resultado.MontoIvaInscripto = pObjNotaDeDebito.MontoIvaInscripto;
                        resultado.MontoIvaNoInscripto = pObjNotaDeDebito.MontoIvaNoInscripto;
                    }
                }
                catch { }
                resultado.MontoPercepcionDGR = pObjNotaDeDebito.MontoPercepcionDGR;
                resultado.MontoTotal = pObjNotaDeDebito.MontoTotal;
                resultado.Motivo = pObjNotaDeDebito.Motivo;
                resultado.Numero = pObjNotaDeDebito.Numero;
            }
            return resultado;
        }
        public static cRecibo ConvertToRecibo(dkInterfaceWeb.Recibo pObj)
        {
            cRecibo resultado = null;
            if (pObj != null)
            {
                resultado = new cRecibo();
                resultado.Numero = pObj.Numero;
                DateTime dateValue;
                resultado.Fecha = DateTime.TryParse(pObj.Fecha.ToString(), out dateValue) ? (DateTime)pObj.Fecha : (DateTime?)null;
                resultado.FechaToString = resultado.Fecha != null ? ((DateTime)resultado.Fecha).ToShortDateString() : string.Empty;
                resultado.FechaAnulacion = pObj.FechaAnulacion == null ? "" : pObj.FechaAnulacion.ToString();
                resultado.Destinatario = pObj.Destinatario;
                resultado.DireccionDestinatario = pObj.DireccionDestinatario;
                resultado.LocalidadDestinatario = pObj.LocalidadDestinatario;
                resultado.CondicionIVADestinatarioToString = ToConvertToString(pObj.CondicionIVADestinatario);
                resultado.CuitDestinatario = pObj.CuitDestinatario;
                resultado.NumeroCliente = pObj.NumeroCliente;
                resultado.NumeroCuentaCorriente = pObj.NumeroCuentaCorriente;
                resultado.TipoEnvioToString = ToConvertToString(pObj.TipoEnvio);
                resultado.CodigoReparto = pObj.CodigoReparto == null ? "" : pObj.CodigoReparto.ToString();
                resultado.MontoTotal = pObj.MontoTotal;
                resultado.MontoTOTALenLetras = pObj.MontoTOTALenLetras;
                resultado.CantidadHojas = pObj.CantidadHojas;
                resultado.MontoEnDolares = pObj.MontoEnDolares == null ? "" : pObj.MontoEnDolares.ToString();
                resultado.ComprobantePAMI = pObj.ComprobantePAMI;
                // dynamic
                //_objDetalleFactura.Importe = objListaDetalle[i].Importe == null ? "" : objListaDetalle[i].Importe.ToString();
            }
            return resultado;
        }
        public static cNotaDeCredito ConvertToNotaDeCredito(dkInterfaceWeb.NotaDeCredito pObjNotaDeCredito)
        {
            cNotaDeCredito resultado = null;
            if (pObjNotaDeCredito != null)
            {
                resultado = new cNotaDeCredito();
                resultado.CantidadHojas = pObjNotaDeCredito.CantidadHojas;
                resultado.Destinatario = pObjNotaDeCredito.Destinatario;
                DateTime dateValue;
                resultado.Fecha = DateTime.TryParse(pObjNotaDeCredito.Fecha.ToString(), out dateValue) ? (DateTime)pObjNotaDeCredito.Fecha : (DateTime?)null;
                resultado.FechaToString = resultado.Fecha != null ? ((DateTime)resultado.Fecha).ToShortDateString() : string.Empty;
                try
                {
                    if (pObjNotaDeCredito.Numero[0].ToString() != "B")
                    {
                        resultado.MontoExento = pObjNotaDeCredito.MontoExento;
                        resultado.MontoGravado = pObjNotaDeCredito.MontoGravado;
                        resultado.MontoIvaInscripto = pObjNotaDeCredito.MontoIvaInscripto;
                        resultado.MontoIvaNoInscripto = pObjNotaDeCredito.MontoIvaNoInscripto;
                    }
                }
                catch { }
                resultado.MontoPercepcionDGR = pObjNotaDeCredito.MontoPercepcionDGR;
                resultado.MontoTotal = pObjNotaDeCredito.MontoTotal;
                resultado.Motivo = pObjNotaDeCredito.Motivo;
                resultado.Numero = pObjNotaDeCredito.Numero;
                resultado.TotalUnidades = pObjNotaDeCredito.TotalUnidades;
            }
            return resultado;
        }
        public static cFactura ConvertToFactura(dkInterfaceWeb.Factura pObjFactura)
        {
            cFactura resultado = null;
            if (pObjFactura != null)
            {
                resultado = new cFactura();
                resultado.CantidadHojas = pObjFactura.CantidadHojas;
                resultado.CantidadRenglones = pObjFactura.CantidadRenglones;
                resultado.CodigoFormaDePago = pObjFactura.CodigoFormaDePago;
                resultado.DescuentoEspecial = pObjFactura.DescuentoEspecial;
                resultado.DescuentoNetos = pObjFactura.DescuentoNetos;
                resultado.DescuentoPerfumeria = pObjFactura.DescuentoPerfumeria;
                resultado.DescuentoWeb = pObjFactura.DescuentoWeb;
                resultado.Destinatario = pObjFactura.Destinatario;
                resultado.Fecha = pObjFactura.Fecha;
                DateTime dateValue;
                resultado.Fecha = DateTime.TryParse(pObjFactura.Fecha.ToString(), out dateValue) ? (DateTime)pObjFactura.Fecha : (DateTime?)null;
                resultado.FechaToString = resultado.Fecha != null ? ((DateTime)resultado.Fecha).ToShortDateString() : string.Empty;
                try
                {
                    if (pObjFactura.Numero[0].ToString() != "B")
                    {
                        resultado.MontoExento = pObjFactura.MontoExento;
                        resultado.MontoGravado = pObjFactura.MontoGravado;
                        resultado.MontoIvaInscripto = pObjFactura.MontoIvaInscripto;
                        resultado.MontoIvaNoInscripto = pObjFactura.MontoIvaNoInscripto;
                    }
                }
                catch (Exception ex_interno)
                {
                    DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex_interno, DateTime.Now);
                }
                resultado.MontoPercepcionDGR = pObjFactura.MontoPercepcionDGR;
                resultado.MontoTotal = pObjFactura.MontoTotal;
                resultado.Numero = pObjFactura.Numero;
                resultado.NumeroCuentaCorriente = pObjFactura.NumeroCuentaCorriente;
                resultado.NumeroRemito = pObjFactura.NumeroRemito;
                resultado.TotalUnidades = pObjFactura.TotalUnidades;
                resultado.MontoPercepcionMunicipal = pObjFactura.MontoPercepcionMunicipal;
                try
                {
                    dkInterfaceWeb.ServiciosWEB objServWebFacturaTrazable = new dkInterfaceWeb.ServiciosWEB();
                    resultado.FacturaTrazable = objServWebFacturaTrazable.FacturaTrazable(pObjFactura.Numero);
                }
                catch (Exception ex)
                {
                    DKbase.generales.Log.LogError(MethodBase.GetCurrentMethod(), ex, DateTime.Now);
                }
            }
            return resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pEstado"></param>
        /// <returns></returns>
        public static dllEstadoCheque ToConvert(dkInterfaceWeb.EstadoCheque pEstado)
        {
            switch (pEstado)
            {
                case dkInterfaceWeb.EstadoCheque.Aceptado:
                    return dllEstadoCheque.Aceptado;
                case dkInterfaceWeb.EstadoCheque.Cambiado:
                    return dllEstadoCheque.Cambiado;
                case dkInterfaceWeb.EstadoCheque.Depositado:
                    return dllEstadoCheque.Depositado;
                case dkInterfaceWeb.EstadoCheque.EnCartera:
                    return dllEstadoCheque.EnCartera;
                case dkInterfaceWeb.EstadoCheque.Rechazado:
                    return dllEstadoCheque.Rechazado;
                case dkInterfaceWeb.EstadoCheque.Retirado:
                    return dllEstadoCheque.Retirado;
                default:
                    return dllEstadoCheque.Aceptado;
            }
        }
        public static string ToConvertToString(dkInterfaceWeb.EstadoCheque pEstado)
        {
            switch (pEstado)
            {
                case dkInterfaceWeb.EstadoCheque.Aceptado:
                    return "Aceptado";
                //break;
                case dkInterfaceWeb.EstadoCheque.Cambiado:
                    return "Cambiado";
                //break;
                case dkInterfaceWeb.EstadoCheque.Depositado:
                    return "Depositado";
                //break;
                case dkInterfaceWeb.EstadoCheque.EnCartera:
                    return "EnCartera";
                //break;
                case dkInterfaceWeb.EstadoCheque.Rechazado:
                    return "Rechazado";
                //break;
                case dkInterfaceWeb.EstadoCheque.Retirado:
                    return "Retirado";
                default:
                    return "";
                    //    break;
            }
        }
        public static string ToConvertToString(dkInterfaceWeb.CondicionIVAWeb pEstado)
        {
            switch (pEstado)
            {
                case dkInterfaceWeb.CondicionIVAWeb.ConsumidorFinal:
                    return "ConsumidorFinal";
                //break;
                case dkInterfaceWeb.CondicionIVAWeb.Monotributo:
                    return "Monotributo";
                //break;
                case dkInterfaceWeb.CondicionIVAWeb.NoResponsable:
                    return "NoResponsable";
                //break;
                case dkInterfaceWeb.CondicionIVAWeb.ResponsableExento:
                    return "ResponsableExento";
                //break;
                case dkInterfaceWeb.CondicionIVAWeb.ResponsableInscripto:
                    return "ResponsableInscripto";
                //break;
                case dkInterfaceWeb.CondicionIVAWeb.ResponsableNoInscripto:
                    return "ResponsableNoInscripto";
                default:
                    return "";
                    //    break;
            }
        }
        public static string ToConvertToString(dkObjetos.TipoDeEnvio pEstado)
        {
            switch (pEstado)
            {
                case dkObjetos.TipoDeEnvio.Cadetería:
                    return "Cadetería";
                //break;
                case dkObjetos.TipoDeEnvio.Encomienda:
                    return "Encomienda";
                //break;
                case dkObjetos.TipoDeEnvio.Mostrador:
                    return "Mostrador";
                //break;
                case dkObjetos.TipoDeEnvio.Reparto:
                    return "Reparto";
                default:
                    return "";
                    //    break;
            }
        }
        public static cDllChequeRecibido ConvertToChequeRecibido(dkInterfaceWeb.ChequeRecibido pChequeRecibido)
        {
            cDllChequeRecibido resultado = null;
            if (pChequeRecibido != null)
            {
                resultado = new cDllChequeRecibido();
                DateTime dateValue;
                resultado.Banco = pChequeRecibido.Banco;
                resultado.Estado = ToConvert(pChequeRecibido.Estado);
                resultado.EstadoToString = ToConvertToString(pChequeRecibido.Estado);
                resultado.Fecha = pChequeRecibido.Fecha != null ? pChequeRecibido.Fecha.ToString() : string.Empty;
                resultado.FechaVencimiento = DateTime.TryParse(pChequeRecibido.FechaVencimiento.ToString(), out dateValue) ? (DateTime)pChequeRecibido.FechaVencimiento : (DateTime?)null;
                resultado.FechaVencimientoToString = resultado.FechaVencimiento != null ? ((DateTime)resultado.FechaVencimiento).ToShortDateString() : string.Empty;
                resultado.Importe = pChequeRecibido.Importe;
                resultado.Numero = pChequeRecibido.Numero != null ? pChequeRecibido.Numero.ToString() : string.Empty;
            }
            return resultado;
        }
        public static cPlan ToConvert(dkInterfaceWeb.Plan pPlanItem)
        {
            cPlan obj = null;
            if (pPlanItem != null)
            {
                obj = new cPlan();
                obj.Nombre = pPlanItem.Nombre;
                obj.PideSemana = pPlanItem.PideSemana;
            }
            return obj;
        }
        public static cPlanillaObSoc ToConvert(dkInterfaceWeb.PlanillaObSoc pPlanillaObSoc)
        {
            cPlanillaObSoc obj = null;
            if (pPlanillaObSoc != null)
            {
                obj = new cPlanillaObSoc();
                obj.Anio = pPlanillaObSoc.Anio == null ? string.Empty : Convert.ToString(pPlanillaObSoc.Anio);
                obj.Fecha = pPlanillaObSoc.Fecha;
                obj.FechaToString = pPlanillaObSoc.Fecha != null ? ((DateTime)pPlanillaObSoc.Fecha).ToShortDateString() : string.Empty;
                obj.Importe = pPlanillaObSoc.Importe;
                obj.Mes = pPlanillaObSoc.Mes == null ? string.Empty : Convert.ToString(pPlanillaObSoc.Mes);
                obj.Quincena = pPlanillaObSoc.Quincena == null ? string.Empty : Convert.ToString(pPlanillaObSoc.Quincena);
                obj.Semana = pPlanillaObSoc.Semana == null ? string.Empty : Convert.ToString(pPlanillaObSoc.Semana);
            }
            return obj;
        }
        public static cObraSocialCliente ToConvert(dkInterfaceWeb.ObraSocialCliente pObraSocialCliente)
        {
            cObraSocialCliente obj = null;
            if (pObraSocialCliente != null)
            {
                obj = new cObraSocialCliente();
                obj.CantidadHojas = pObraSocialCliente.CantidadHojas == null ? string.Empty : Convert.ToString(pObraSocialCliente.CantidadHojas);
                obj.Destinatario = pObraSocialCliente.Destinatario == null ? string.Empty : Convert.ToString(pObraSocialCliente.Destinatario);
                obj.Fecha = pObraSocialCliente.Fecha;
                obj.FechaToString = pObraSocialCliente.Fecha != null ? ((DateTime)pObraSocialCliente.Fecha).ToShortDateString() : string.Empty;
                obj.MontoTotal = pObraSocialCliente.MontoTotal;
                obj.NombrePlan = pObraSocialCliente.NombrePlan == null ? string.Empty : Convert.ToString(pObraSocialCliente.NombrePlan);
                obj.NumeroPlanilla = pObraSocialCliente.NumeroPlanilla;
            }
            return obj;
        }

        public static cObraSocialClienteItem ToConvert(dkInterfaceWeb.ObraSocialClienteItem pObraSocialClienteItem)
        {
            cObraSocialClienteItem obj = null;
            if (pObraSocialClienteItem != null)
            {
                obj = new cObraSocialClienteItem();
                obj.Descripcion = pObraSocialClienteItem.Descripcion == null ? string.Empty : Convert.ToString(pObraSocialClienteItem.Descripcion);
                obj.Importe = pObraSocialClienteItem.Importe == null ? string.Empty : Convert.ToString(pObraSocialClienteItem.Importe);
                obj.NumeroHoja = pObraSocialClienteItem.NumeroHoja == null ? string.Empty : Convert.ToString(pObraSocialClienteItem.NumeroHoja);
                obj.NumeroItem = pObraSocialClienteItem.NumeroItem;
                obj.NumeroObraSocialCliente = pObraSocialClienteItem.NumeroObraSocialCliente == null ? string.Empty : Convert.ToString(pObraSocialClienteItem.NumeroObraSocialCliente);
            }
            return obj;
        }
        public static cCbteParaImprimir ToConvert(dkInterfaceWeb.CbteParaImprimir pValor)
        {
            cCbteParaImprimir obj = null;
            if (pValor != null)
            {
                obj = new cCbteParaImprimir();
                obj.FechaComprobante = pValor.FechaComprobante;
                obj.FechaComprobanteToString = pValor.FechaComprobante != null ? ((DateTime)pValor.FechaComprobante).ToShortDateString() : string.Empty;
                obj.NumeroComprobante = pValor.NumeroComprobante == null ? string.Empty : Convert.ToString(pValor.NumeroComprobante);
                obj.TipoComprobante = pValor.TipoComprobante == null ? string.Empty : Convert.ToString(pValor.TipoComprobante);
            }
            return obj;
        }
        public static cConsObraSocial ToConvert(dkInterfaceWeb.ConsObraSocial pValor)
        {
            cConsObraSocial obj = null;
            if (pValor != null)
            {
                obj = new cConsObraSocial();
                obj.FechaComprobante = pValor.FechaComprobante;
                obj.FechaComprobanteToString = pValor.FechaComprobante != null ? ((DateTime)pValor.FechaComprobante).ToShortDateString() : string.Empty;
                obj.Detalle = pValor.Detalle == null ? string.Empty : Convert.ToString(pValor.Detalle);
                obj.TipoComprobante = pValor.TipoComprobante == null ? string.Empty : Convert.ToString(pValor.TipoComprobante);
                obj.NumeroComprobante = pValor.NumeroComprobante == null ? string.Empty : Convert.ToString(pValor.NumeroComprobante);
                obj.Importe = pValor.Importe;//== null ? string.Empty : Convert.ToString(pValor.Importe);
            }
            return obj;
        }
        public static cLote ConvertToLote(dkInterfaceWeb.Lote pObjLote)
        {
            cLote resultado = null;
            if (pObjLote != null)
            {
                resultado = new cLote();
                resultado.ID = pObjLote.ID;
                resultado.NombreProducto = pObjLote.NombreProducto;
                resultado.NumeroLote = pObjLote.NumeroLote;
                resultado.FechaVencimiento = pObjLote.FechaVencimiento;
                DateTime dateValue;
                resultado.FechaVencimiento = DateTime.TryParse(pObjLote.FechaVencimiento.ToString(), out dateValue) ? (DateTime)pObjLote.FechaVencimiento : (DateTime?)null;
                resultado.FechaVencimientoToString = resultado.FechaVencimiento != null ? ((DateTime)resultado.FechaVencimiento).ToShortDateString() : string.Empty;
            }
            return resultado;
        }
        public static cDevolucionItemPrecarga ConvertToItemSolicitudDevCliente(dkInterfaceWeb.SolicitudDevCliente pObjSDC)
        {
            cDevolucionItemPrecarga resultado = null;
            if (pObjSDC != null)
            {
                resultado = new cDevolucionItemPrecarga();
                resultado.dev_numeroitem = pObjSDC.NumeroItem;
                resultado.dev_numerocliente = pObjSDC.NumeroCliente;
                resultado.dev_numerofactura = pObjSDC.NumeroFactura;
                resultado.dev_numerosolicituddevolucion = pObjSDC.NumeroSolicitud;
                resultado.dev_nombreproductodevolucion = pObjSDC.NombreProductoDevolucion;
                resultado.dev_fecha = pObjSDC.Fecha;
                DateTime dateValue;
                resultado.dev_fecha = DateTime.TryParse(pObjSDC.Fecha.ToString(), out dateValue) ? (DateTime)pObjSDC.Fecha : DateTime.Now;
                resultado.dev_fechaToString = resultado.dev_fecha != null ? ((DateTime)resultado.dev_fecha).ToShortDateString() : string.Empty;
                switch (pObjSDC.Motivo)
                {
                    case dkInterfaceWeb.MotivoDevolucion.BienFacturadoMalEnviado:
                        resultado.dev_motivo = dllMotivoDevolucion.BienFacturadoMalEnviado;
                        break;
                    case dkInterfaceWeb.MotivoDevolucion.ProductoMalEstado:
                        resultado.dev_motivo = dllMotivoDevolucion.ProductoMalEstado;
                        break;
                    case dkInterfaceWeb.MotivoDevolucion.FacturadoNoPedido:
                        resultado.dev_motivo = dllMotivoDevolucion.FacturadoNoPedido;
                        break;
                    case dkInterfaceWeb.MotivoDevolucion.ProductoDeMasSinSerFacturado:
                        resultado.dev_motivo = dllMotivoDevolucion.ProductoDeMasSinSerFacturado;
                        break;
                    case dkInterfaceWeb.MotivoDevolucion.VencimientoCorto:
                        resultado.dev_motivo = dllMotivoDevolucion.VencimientoCorto;
                        break;
                    case dkInterfaceWeb.MotivoDevolucion.ProductoFallaFabricante:
                        resultado.dev_motivo = dllMotivoDevolucion.ProductoFallaFabricante;
                        break;
                    case dkInterfaceWeb.MotivoDevolucion.Vencido:
                        resultado.dev_motivo = dllMotivoDevolucion.Vencido;
                        break;
                    case dkInterfaceWeb.MotivoDevolucion.PedidoPorError:
                        resultado.dev_motivo = dllMotivoDevolucion.PedidoPorError;
                        break;
                }
                resultado.dev_numeroitemfactura = pObjSDC.NumeroItemFactura;
                resultado.dev_nombreproductofactura = pObjSDC.NombreProductoFactura;
                resultado.dev_cantidad = pObjSDC.Cantidad;
                resultado.dev_numerolote = pObjSDC.NumeroLote;
                resultado.dev_fechavencimientolote = pObjSDC.FechaVencimiento;
                resultado.dev_fechavencimientolote = DateTime.TryParse(pObjSDC.FechaVencimiento.ToString(), out dateValue) ? (DateTime)pObjSDC.FechaVencimiento : DateTime.Now;
                resultado.dev_fechavencimientoloteToString = resultado.dev_fechavencimientolote != null ? ((DateTime)resultado.dev_fechavencimientolote).ToShortDateString() : string.Empty;
                resultado.dev_estado = pObjSDC.Estado;
                resultado.dev_mensaje = pObjSDC.MensajeRechazo;
                resultado.dev_cantidadrecibida = pObjSDC.CantidadRecibida;
                resultado.dev_cantidadrechazada = pObjSDC.CantidadRechazada;
                resultado.dev_idsucursal = pObjSDC.IDSucursal;
                resultado.dev_numerosolicitudNC = pObjSDC.NumeroSolicitudNC;
            }
            return resultado;
        }
        public static dkInterfaceWeb.SolicitudDevCliente ConvertFromItemSolicitudDevCliente(cDevolucionItemPrecarga pObjSDC)
        {
            dkInterfaceWeb.SolicitudDevCliente resultado = null;
            if (pObjSDC != null)
            {
                resultado = new dkInterfaceWeb.SolicitudDevCliente();
                resultado.NumeroItem = pObjSDC.dev_numeroitem;
                resultado.NumeroCliente = pObjSDC.dev_numerocliente;
                resultado.NumeroFactura = pObjSDC.dev_numerofactura;
                resultado.NumeroSolicitud = pObjSDC.dev_numerosolicituddevolucion;
                resultado.NombreProductoDevolucion = pObjSDC.dev_nombreproductodevolucion;
                resultado.Fecha = pObjSDC.dev_fecha;
                DateTime dateValue;
                resultado.Fecha = DateTime.TryParse(pObjSDC.dev_fecha.ToString(), out dateValue) ? (DateTime)pObjSDC.dev_fecha : DateTime.Now;

                switch (pObjSDC.dev_motivo)
                {
                    case dllMotivoDevolucion.BienFacturadoMalEnviado:
                        resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.BienFacturadoMalEnviado;
                        break;
                    case dllMotivoDevolucion.ProductoMalEstado:
                        resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.ProductoMalEstado;
                        break;
                    case dllMotivoDevolucion.FacturadoNoPedido:
                        resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.FacturadoNoPedido;
                        break;
                    case dllMotivoDevolucion.ProductoDeMasSinSerFacturado:
                        resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.ProductoDeMasSinSerFacturado;
                        break;
                    case dllMotivoDevolucion.VencimientoCorto:
                        resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.VencimientoCorto;
                        break;
                    case dllMotivoDevolucion.ProductoFallaFabricante:
                        resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.ProductoFallaFabricante;
                        break;
                    case dllMotivoDevolucion.Vencido:
                        resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.Vencido;
                        break;
                    case dllMotivoDevolucion.PedidoPorError:
                        resultado.Motivo = dkInterfaceWeb.MotivoDevolucion.PedidoPorError;
                        break;
                }
                resultado.NumeroItemFactura = pObjSDC.dev_numeroitemfactura;
                resultado.NombreProductoFactura = pObjSDC.dev_nombreproductofactura;
                resultado.Cantidad = pObjSDC.dev_cantidad;
                resultado.NumeroLote = pObjSDC.dev_numerolote;
                resultado.FechaVencimiento = pObjSDC.dev_fechavencimientolote;
                resultado.FechaVencimiento = Convert.ToDateTime(pObjSDC.dev_fechavencimientoloteToString);
                //resultado.FechaVencimiento = DateTime.TryParse(pObjSDC.dev_fechavencimientolote.ToString(), out dateValue) ? (DateTime)pObjSDC.dev_fechavencimientolote : DateTime.Now;
                resultado.Estado = pObjSDC.dev_estado;
                resultado.MensajeRechazo = pObjSDC.dev_mensaje;
                resultado.CantidadRecibida = pObjSDC.dev_cantidadrecibida;
                resultado.CantidadRechazada = pObjSDC.dev_cantidadrechazada;
                resultado.IDSucursal = pObjSDC.dev_idsucursal;
                resultado.NumeroSolicitudNC = pObjSDC.dev_numerosolicitudNC;
            }
            return resultado;
        }
        public static cVacuna ToConvert(dkInterfaceWeb.Vacuna pValue)
        {
            cVacuna result = null;
            if (pValue != null)
            {
                result = new cVacuna();
                result.ID = pValue.ID;
                result.Login = pValue.Login != null ? pValue.Login.ToString() : string.Empty;
                result.NombreProducto = pValue.NombreProducto != null ? pValue.NombreProducto.ToString() : string.Empty;
                result.UnidadesVendidas = pValue.UnidadesVendidas;
            }
            return result;
        }
        public static cReservaVacuna ToConvert(dkInterfaceWeb.ReservaVacuna pValue)
        {
            cReservaVacuna result = null;
            if (pValue != null)
            {
                result = new cReservaVacuna();
                result.ID = pValue.ID;
                result.TomaWeb = pValue.TomaWeb;
                result.NombreProducto = pValue.NombreProducto != null ? pValue.NombreProducto.ToString() : string.Empty;
                result.UnidadesVendidas = pValue.UnidadesVendidas;
                result.DescripcionPack = pValue.DescripcionPack != null ? pValue.DescripcionPack.ToString() : string.Empty;
                result.FechaAlta = pValue.FechaAlta;
                result.FechaAltaToString = pValue.FechaAlta != null ? pValue.FechaAlta.ToString() : string.Empty;
                //public string Estado { get; set; }
                switch (pValue.Estado)
                {
                    case dkInterfaceWeb.EstadoVacunaWeb.ComprobanteEnEspera:
                        result.Estado = "ComprobanteEnEspera";
                        break;
                    case dkInterfaceWeb.EstadoVacunaWeb.ComprobanteRecibido:
                        result.Estado = "ComprobanteRecibido";
                        break;
                    case dkInterfaceWeb.EstadoVacunaWeb.VacunaAnulado:
                        result.Estado = "VacunaAnulado";
                        break;
                    case dkInterfaceWeb.EstadoVacunaWeb.VacunaFacturado:
                        result.Estado = "VacunaFacturado";
                        break;
                    default:
                        result.Estado = pValue.Estado.ToString();
                        break;
                }
            }
            return result;
        }
        public static dkInterfaceWeb.Vacuna ToConvert(cVacuna pValue)
        {
            dkInterfaceWeb.Vacuna result = null;
            try
            {
                if (pValue != null)
                {
                    result = new dkInterfaceWeb.Vacuna();
                    result.ID = pValue.ID;
                    result.Login = pValue.Login;
                    result.NombreProducto = pValue.NombreProducto;
                    result.UnidadesVendidas = pValue.UnidadesVendidas;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}