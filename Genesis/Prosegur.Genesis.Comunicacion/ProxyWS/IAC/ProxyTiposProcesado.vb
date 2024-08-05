Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports ContratoIac = Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports System.Configuration

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")> _
Public Class ProxyTiposProcesado
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ITipoProcesado


    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/TiposProcesado.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub


    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetTiposProcesado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetTiposProcesado(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ITipoProcesado.GetTiposProcesado
        Dim results() As Object = Me.Invoke("GetTiposProcesado", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetTiposProcesado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetTiposProcesado(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.SetTiposProcesado.Repuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ITipoProcesado.SetTiposProcesado
        Dim results() As Object = Me.Invoke("SetTiposProcesado", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.SetTiposProcesado.Repuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoTipoProcesado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoTipoProcesado(peticion As [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ITipoProcesado.VerificarCodigoTipoProcesado
        Dim results() As Object = Me.Invoke("VerificarCodigoTipoProcesado", New Object() {peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionTipoProcesado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarDescripcionTipoProcesado(peticion As [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ITipoProcesado.VerificarDescripcionTipoProcesado
        Dim results() As Object = Me.Invoke("VerificarDescripcionTipoProcesado", New Object() {peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ITipoProcesado.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function

End Class
