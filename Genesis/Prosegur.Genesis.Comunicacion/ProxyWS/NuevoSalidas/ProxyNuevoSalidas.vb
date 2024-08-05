
Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Microsoft.Web.Services3.Security.Tokens
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas

Namespace ProxyWS.NuevoSalidas

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="NuevoSalidasSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.NuevoSalidas")> _
    Public Class ProxyNuevoSalidas
        Inherits ProxyWS.ServicioBase
        Implements INuevoSalidas

        '''<remarks/>
        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "NuevoSalidas/Servicio.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements INuevoSalidas.Test
            Dim results() As Object = Me.Invoke("Test", New Object())
            Return CType(results(0), ContractoServicio.Test.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ObtenerSituacionesRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerSituacionesRemesa(Peticion As Remesa.ObtenerSituacionRemesas.Peticion) As Remesa.ObtenerSituacionRemesas.Respuesta Implements INuevoSalidas.ObtenerSituacionRemesas
            Dim results() As Object = Me.Invoke("ObtenerSituacionesRemesa", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Remesa.ObtenerSituacionRemesas.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/RecuperarDatosGeneralesRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDatosGeneralesRemesa(Peticion As Remesa.RecuperarDatosGeneralesRemesa.Peticion) As Remesa.RecuperarDatosGeneralesRemesa.RespuestaCompresion Implements INuevoSalidas.RecuperarDatosGeneralesRemesa
            Dim results() As Object = Me.Invoke("RecuperarDatosGeneralesRemesa", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Remesa.RecuperarDatosGeneralesRemesa.RespuestaCompresion))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ObtenerPuestosPorDelegacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Function ObtenerPuestosPorDelegacion(Peticion As ContractoServicio.Contractos.NuevoSalidas.Puesto.ObtenerPuestosPorDelegacion.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Puesto.ObtenerPuestosPorDelegacion.RespuestaCompresion Implements INuevoSalidas.ObtenerPuestosPorDelegacion
            Dim results() As Object = Me.Invoke("ObtenerPuestosPorDelegacion", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Puesto.ObtenerPuestosPorDelegacion.RespuestaCompresion))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ObtenerTiposBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Function ObtenerTiposBulto(Peticion As ContractoServicio.NuevoSalidas.TipoBulto.ObtenerTiposBulto.Peticion) As ContractoServicio.NuevoSalidas.TipoBulto.ObtenerTiposBulto.Respuesta Implements INuevoSalidas.ObtenerTiposBulto
            Dim results() As Object = Me.Invoke("ObtenerTiposBulto", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.NuevoSalidas.TipoBulto.ObtenerTiposBulto.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ObtenerTiposMercancia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerTiposMercancia() As ContractoServicio.Contractos.NuevoSalidas.TiposMercancia.ObtenerTiposMercancia.Respuesta Implements INuevoSalidas.ObtenerTiposMercancia
            Dim results() As Object = Me.Invoke("ObtenerTiposMercancia", New Object(-1) {})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.TiposMercancia.ObtenerTiposMercancia.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/RecuperarIDsBultosPorCodigosPrecintos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarIDsBultosPorCodigosPrecintos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.RecuperarIDsBultosPorCodigosPrecintos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.RecuperarIDsBultosPorCodigosPrecintos.Respuesta Implements INuevoSalidas.RecuperarIDsBultosPorCodigosPrecintos
            Dim results() As Object = Me.Invoke("RecuperarIDsBultosPorCodigosPrecintos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.RecuperarIDsBultosPorCodigosPrecintos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/GuardarPrecintos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarPrecintos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.GuardarPrecintos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.GuardarPrecintos.Respuesta Implements INuevoSalidas.GuardarPrecintos
            Dim results() As Object = Me.Invoke("GuardarPrecintos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.GuardarPrecintos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ValidarServicios", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ValidarServicios(Peticion As ContractoServicio.Contractos.NuevoSalidas.Servicio.ValidarServicios.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Servicio.ValidarServicios.Respuesta Implements INuevoSalidas.ValidarServicios
            Dim results() As Object = Me.Invoke("ValidarServicios", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Servicio.ValidarServicios.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/AsignarServicioPuesto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AsignarServicioPuesto(Peticion As ContractoServicio.Contractos.NuevoSalidas.Servicio.AsignarServicioPuesto.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Servicio.AsignarServicioPuesto.Respuesta Implements INuevoSalidas.AsignarServicioPuesto
            Dim results() As Object = Me.Invoke("AsignarServicioPuesto", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Servicio.AsignarServicioPuesto.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/CerrarRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarRemesa(Peticion As Remesa.CerrarRemesa.Peticion) As Remesa.CerrarRemesa.Respuesta Implements INuevoSalidas.CerrarRemesa
            Dim results() As Object = Me.Invoke("CerrarRemesa", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Remesa.CerrarRemesa.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/CerrarBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarBulto(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.CerrarBulto.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.CerrarBulto.Respuesta Implements INuevoSalidas.CerrarBulto
            Dim results() As Object = Me.Invoke("CerrarBulto", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.CerrarBulto.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ActualizarRemesaBultos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActualizarRemesaBultos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizarRemesaBultos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizarRemesaBultos.Respuesta Implements INuevoSalidas.ActualizarRemesaBultos
            Dim results() As Object = Me.Invoke("ActualizarRemesaBultos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizarRemesaBultos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/DividirEnBultos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function DividirEnBultos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.DividirEnBultos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.DividirEnBultos.Respuesta Implements INuevoSalidas.DividirEnBultos
            Dim results() As Object = Me.Invoke("DividirEnBultos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.DividirEnBultos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/IniciarRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function IniciarRemesa(Peticion As Remesa.IniciarRemesa.Peticion) As Remesa.IniciarRemesa.Respuesta Implements INuevoSalidas.IniciarRemesa
            Dim results() As Object = Me.Invoke("IniciarRemesa", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Remesa.IniciarRemesa.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/VolverRemesaEstadoAsignado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VolverRemesaEstadoAsignado(Peticion As Remesa.VolverRemesaEstadoAsignado.Peticion) As Remesa.VolverRemesaEstadoAsignado.Respuesta Implements INuevoSalidas.VolverRemesaEstadoAsignado
            Dim results() As Object = Me.Invoke("VolverRemesaEstadoAsignado", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Remesa.VolverRemesaEstadoAsignado.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/IniciarBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function IniciarBulto(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.IniciarBulto.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.IniciarBulto.Respuesta Implements INuevoSalidas.IniciarBulto
            Dim results() As Object = Me.Invoke("IniciarBulto", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.IniciarBulto.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/VolverBultoEstadoAsignado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VolverBultoEstadoAsignado(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.VolverBultoEstadoAsignado.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.VolverBultoEstadoAsignado.Respuesta Implements INuevoSalidas.VolverBultoEstadoAsignado
            Dim results() As Object = Me.Invoke("VolverBultoEstadoAsignado", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.VolverBultoEstadoAsignado.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/VerificarPrecintoDuplicado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VerificarPrecintoDuplicado(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.PrecintoDuplicado.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.PrecintoDuplicado.Respuesta Implements INuevoSalidas.VerificarPrecintoDuplicado
            Dim results() As Object = Me.Invoke("VerificarPrecintoDuplicado", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.PrecintoDuplicado.Respuesta))
        End Function
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/VerificarCodigoCajetinDuplicado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VerificarCodigoCajetinDuplicado(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.CodigoCajetinDuplicado.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.CodigoCajetinDuplicado.Respuesta Implements INuevoSalidas.VerificarCodigoCajetinDuplicado
            Dim results() As Object = Me.Invoke("VerificarCodigoCajetinDuplicado", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.CodigoCajetinDuplicado.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/CalcularTipoMercancia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CalcularTipoMercancia(Peticion As ContractoServicio.Contractos.NuevoSalidas.TiposMercancia.CalcularTipoMercancia.Peticion) As ContractoServicio.Contractos.NuevoSalidas.TiposMercancia.CalcularTipoMercancia.Respuesta Implements INuevoSalidas.CalcularTipoMercancia
            Dim results() As Object = Me.Invoke("CalcularTipoMercancia", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.TiposMercancia.CalcularTipoMercancia.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/GenerarNuevoCodigoReciboRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GenerarNuevoCodigoReciboRemesa(objPeticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.GenerarNuevoCodigoReciboRemesa.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.GenerarNuevoCodigoReciboRemesa.Respuesta Implements INuevoSalidas.GenerarNuevoCodigoReciboRemesa
            Dim results() As Object = Me.Invoke("GenerarNuevoCodigoReciboRemesa", New Object() {objPeticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.GenerarNuevoCodigoReciboRemesa.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/EsUltimoBultoObjetoProcesado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EsUltimoBultoObjetoProcesado(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.EsUltimoBultoObjetoProcesado.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.EsUltimoBultoObjetoProcesado.Respuesta Implements INuevoSalidas.EsUltimoBultoObjetoProcesado
            Dim results() As Object = Me.Invoke("EsUltimoBultoObjetoProcesado", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.EsUltimoBultoObjetoProcesado.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/BloquearRemesasBultos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BloquearRemesasBultos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesasBultos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesasBultos.Respuesta Implements INuevoSalidas.BloquearRemesasBultos
            Dim results() As Object = Me.Invoke("BloquearRemesasBultos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesasBultos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/DesBloquearRemesasBultos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function DesBloquearRemesasBultos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.DesBloquearRemesasBultos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.DesBloquearRemesasBultos.Respuesta Implements INuevoSalidas.DesBloquearRemesasBultos
            Dim results() As Object = Me.Invoke("DesBloquearRemesasBultos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.DesBloquearRemesasBultos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/SolicitarFondosSaldos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SolicitarFondosSaldos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Puesto.SolicitarFondosSaldos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Puesto.SolicitarFondosSaldos.Respuesta Implements INuevoSalidas.SolicitarFondosSaldos
            Dim results() As Object = Me.Invoke("SolicitarFondosSaldos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Puesto.SolicitarFondosSaldos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ObtenerNecesidadFondoPuesto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerNecesidadFondoPuesto(Peticion As ContractoServicio.Contractos.NuevoSalidas.Puesto.NecesidadFondoPuesto.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Puesto.NecesidadFondoPuesto.Respuesta Implements INuevoSalidas.ObtenerNecesidadFondoPuesto
            Dim results() As Object = Me.Invoke("ObtenerNecesidadFondoPuesto", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Puesto.NecesidadFondoPuesto.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/EnviarFondosSaldos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EnviarFondosSaldos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Puesto.EnviarFondosSaldos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Puesto.EnviarFondosSaldos.Respuesta Implements INuevoSalidas.EnviarFondosSaldos
            Dim results() As Object = Me.Invoke("EnviarFondosSaldos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Puesto.EnviarFondosSaldos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/CerrarPreparacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarPreparacion(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.CerrarPreparacion.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.CerrarPreparacion.Respuesta Implements INuevoSalidas.CerrarPreparacion
            Dim results() As Object = Me.Invoke("CerrarPreparacion", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.CerrarPreparacion.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/CerrarHabilitacionATM", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarHabilitacionATM(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.CerrarHabilitacionATM.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.CerrarHabilitacionATM.Respuesta Implements INuevoSalidas.CerrarHabilitacionATM
            Dim results() As Object = Me.Invoke("CerrarHabilitacionATM", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.CerrarHabilitacionATM.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/CuadrarBultos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CuadrarBultos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.CuadrarBultos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.CuadrarBultos.Respuesta Implements INuevoSalidas.CuadrarBultos
            Dim results() As Object = Me.Invoke("CuadrarBultos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.CuadrarBultos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/RecuperarBultosRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarBultosRemesa(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.RecuperarBultosRemesa.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.RecuperarBultosRemesa.Respuesta Implements INuevoSalidas.RecuperarBultosRemesa
            Dim results() As Object = Me.Invoke("RecuperarBultosRemesa", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.RecuperarBultosRemesa.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/RetornarBultosNoArqueados", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornarBultosNoArqueados(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.RecuperarBultosNoArqueados.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.RecuperarBultosNoArqueados.Respuesta Implements INuevoSalidas.RetornarBultosNoArqueados
            Dim results() As Object = Me.Invoke("RetornarBultosNoArqueados", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.RecuperarBultosNoArqueados.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/InsertarBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function InsertarBulto(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.InsertarBulto.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.InsertarBulto.Respuesta Implements INuevoSalidas.InsertarBulto
            Dim results() As Object = Me.Invoke("InsertarBulto", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.InsertarBulto.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/BorrarBulto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BorrarBulto(Peticion As ContractoServicio.Contractos.NuevoSalidas.Bulto.BorrarBulto.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Bulto.BorrarBulto.Respuesta Implements INuevoSalidas.BorrarBulto
            Dim results() As Object = Me.Invoke("BorrarBulto", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Bulto.BorrarBulto.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/DividirServicios", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Sub DividirServicios(Peticion As Remesa.DividirServicios.Peticion) Implements INuevoSalidas.DividirServicios
            Dim results() As Object = Me.Invoke("DividirServicios", New Object() {Peticion})
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/RecuperarDatosRemesasPadreYHija", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarDatosRemesasPadreYHija(Peticion As Remesa.RecuperarDatosRemesasPadreYHija.Peticion) As Remesa.RecuperarDatosRemesasPadreYHija.Respuesta Implements INuevoSalidas.RecuperarDatosRemesasPadreYHija
            Dim results() As Object = Me.Invoke("RecuperarDatosRemesasPadreYHija", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Remesa.RecuperarDatosRemesasPadreYHija.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ActualizarRemesasBultosFusion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActualizarRemesasBultosFusion(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizarRemesasBultosFusion.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizarRemesasBultosFusion.Respuesta Implements INuevoSalidas.ActualizarRemesasBultosFusion
            Dim results() As Object = Me.Invoke("ActualizarRemesasBultosFusion", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizarRemesasBultosFusion.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/CerrarRemesaAdministracion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarRemesaAdministracion(Peticion As Remesa.CerrarRemesaAdministracion.Peticion) As Remesa.CerrarRemesaAdministracion.Respuesta Implements INuevoSalidas.CerrarRemesaAdministracion
            Dim results() As Object = Me.Invoke("CerrarRemesaAdministracion", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Remesa.CerrarRemesaAdministracion.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/CerrarBultoAdministracion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CerrarBultoAdministracion(Peticion As ContractoServicio.NuevoSalidas.Bulto.CerrarBultoAdministracion.Peticion) As ContractoServicio.NuevoSalidas.Bulto.CerrarBultoAdministracion.Respuesta Implements INuevoSalidas.CerrarBultoAdministracion
            Dim results() As Object = Me.Invoke("CerrarBultoAdministracion", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Bulto.CerrarBultoAdministracion.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/VolverRemesasBultosEstadoAsignado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VolverRemesasBultosEstadoAsignado(Peticion As Remesa.VolverRemesasBultosEstadoAsignado.Peticion) As Remesa.VolverRemesasBultosEstadoAsignado.Respuesta Implements INuevoSalidas.VolverRemesasBultosEstadoAsignado
            Dim results() As Object = Me.Invoke("VolverRemesasBultosEstadoAsignado", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Remesa.VolverRemesasBultosEstadoAsignado.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/BloquearRemesas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BloquearRemesas(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesas.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesas.Respuesta Implements INuevoSalidas.BloquearRemesas
            Dim results() As Object = Me.Invoke("BloquearRemesas", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.BloquearRemesas.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/AnularRemesas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AnularRemesas(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.AnularRemesas.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.AnularRemesas.Respuesta Implements INuevoSalidas.AnularRemesas
            Dim results() As Object = Me.Invoke("AnularRemesas", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.AnularRemesas.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ActualizarRemesasSalidasSaldos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActualizarRemesasSalidasSaldos(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizarRemesasSalidasSaldos.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizarRemesasSalidasSaldos.Respuesta Implements INuevoSalidas.ActualizarRemesasSalidasSaldos
            Dim results() As Object = Me.Invoke("ActualizarRemesasSalidasSaldos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizarRemesasSalidasSaldos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ValidarRemesasAnuladasSOL", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ValidarRemesasAnuladasSOL(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.ValidarRemesasAnuladasSOL.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.ValidarRemesasAnuladasSOL.Respuesta Implements INuevoSalidas.ValidarRemesasAnuladasSOL
            Dim results() As Object = Me.Invoke("ValidarRemesasAnuladasSOL", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.ValidarRemesasAnuladasSOL.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ActualizarCodigoBolsa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActualizarCodigoBolsa(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarCodigoBolsa.Peticion) As Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarCodigoBolsa.Respuesta Implements INuevoSalidas.ActualizarCodigoBolsa
            Dim results() As Object = Me.Invoke("ActualizarCodigoBolsa", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarCodigoBolsa.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ActualizarPrecintosSalidasSaldos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActualizarPrecintosSalidasSaldos(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarPrecintosSalidasSaldos.Peticion) As Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarPrecintosSalidasSaldos.Respuesta Implements INuevoSalidas.ActualizarPrecintosSalidasSaldos
            Dim results() As Object = Me.Invoke("ActualizarPrecintosSalidasSaldos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Prosegur.Genesis.ContractoServicio.Contractos.NuevoSalidas.Bulto.ActualizarPrecintosSalidasSaldos.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/GrabarReciboTransporteManual", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarReciboTransporteManual(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.GrabarReciboTransporteManual.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.GrabarReciboTransporteManual.Respuesta Implements INuevoSalidas.GrabarReciboTransporteManual
            Dim results() As Object = Me.Invoke("GrabarReciboTransporteManual", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.GrabarReciboTransporteManual.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ValidarReciboTransporteManual", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ValidarReciboTransporteManual(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.ValidarReciboTransporteManual.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.ValidarReciboTransporteManual.Respuesta Implements INuevoSalidas.ValidarReciboTransporteManual
            Dim results() As Object = Me.Invoke("ValidarReciboTransporteManual", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.ValidarReciboTransporteManual.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/RecuperarTerminosIACRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarTerminosIACRemesa(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.RecuperarTerminosIACRemesa.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.RecuperarTerminosIACRemesa.Respuesta Implements INuevoSalidas.RecuperarTerminosIACRemesa
            Dim results() As Object = Me.Invoke("RecuperarTerminosIACRemesa", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.RecuperarTerminosIACRemesa.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/RecuperarRemesasPorOT", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarRemesasPorOT(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.RecuperarRemesasPorOT.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.RecuperarRemesasPorOT.Respuesta Implements INuevoSalidas.RecuperarRemesasPorOT
            Dim results() As Object = Me.Invoke("RecuperarRemesasPorOT", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.RecuperarRemesasPorOT.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/ActualizacionEstadoPreparacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActualizacionEstadoPreparacion(urlSol As String, peticionSOL As ContractoServicio.NuevoSalidas.ActualizarEstadoPreparacionRemesa.peticionActualizarEstadoPreparacionRemesa) As ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizacionEstadoPreparacion.Respuesta Implements INuevoSalidas.ActualizacionEstadoPreparacion
            Dim results() As Object = Me.Invoke("ActualizacionEstadoPreparacion", New Object() {urlSol, peticionSOL})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.ActualizacionEstadoPreparacion.Respuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidas/EnviarRemesaSOL", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidas/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EnviarRemesaSOL(Peticion As ContractoServicio.Contractos.NuevoSalidas.Remesa.EnviarRemesaSOL.Peticion) As ContractoServicio.Contractos.NuevoSalidas.Remesa.EnviarRemesaSOL.Respuesta Implements INuevoSalidas.EnviarRemesaSOL
            Dim results() As Object = Me.Invoke("EnviarRemesaSOL", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.NuevoSalidas.Remesa.EnviarRemesaSOL.Respuesta))
        End Function
    End Class

End Namespace