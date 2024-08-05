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
Public Class ProxyMedioPago
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IMedioPago

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/MedioPago.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetMedioPagoDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetMedioPagoDetail(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMedioPago.GetMedioPagoDetail
        Dim results() As Object = Me.Invoke("GetMedioPagoDetail", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetMedioPagos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetMedioPagos(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.GetMedioPagos.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.GetMedioPagos.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMedioPago.GetMedioPagos
        Dim results() As Object = Me.Invoke("GetMedioPagos", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.MedioPago.GetMedioPagos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetMedioPago", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetMedioPago(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.SetMedioPago.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.SetMedioPago.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMedioPago.SetMedioPago
        Dim results() As Object = Me.Invoke("SetMedioPago", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.MedioPago.SetMedioPago.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMedioPago.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoMedioPago", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoMedioPago(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.VerificarCodigoMedioPago.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.VerificarCodigoMedioPago.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMedioPago.VerificarCodigoMedioPago
        Dim results() As Object = Me.Invoke("VerificarCodigoMedioPago", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.MedioPago.VerificarCodigoMedioPago.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoTerminoMedioPago", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoTerminoMedioPago(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMedioPago.VerificarCodigoTerminoMedioPago
        Dim results() As Object = Me.Invoke("VerificarCodigoTerminoMedioPago", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Respuesta)
    End Function
End Class
