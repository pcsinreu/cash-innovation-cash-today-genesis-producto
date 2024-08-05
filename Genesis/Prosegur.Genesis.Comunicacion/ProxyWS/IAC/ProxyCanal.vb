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
Public Class ProxyCanal
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ICanal
    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Canal.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getCanales", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getCanales(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Canal.GetCanales.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Canal.GetCanales.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICanal.getCanales
        Dim results() As Object = Me.Invoke("getCanales", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal.GetCanales.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getSubCanalesByCanal", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getSubCanalesByCanal(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICanal.getSubCanalesByCanal
        Dim results() As Object = Me.Invoke("getSubCanalesByCanal", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetSubCanalesByCertificado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetSubCanalesByCertificado(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Canal.GetSubCanalesByCertificado.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Canal.GetSubCanalesByCertificado.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICanal.GetSubCanalesByCertificado
        Dim results() As Object = Me.Invoke("GetSubCanalesByCertificado", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal.GetSubCanalesByCertificado.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/setCanales", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function setCanales(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Canal.SetCanal.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Canal.SetCanal.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICanal.setCanales
        Dim results() As Object = Me.Invoke("setCanales", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal.SetCanal.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICanal.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoCanal", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoCanal(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Canal.VerificarCodigoCanal.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Canal.VerificarCodigoCanal.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICanal.VerificarCodigoCanal
        Dim results() As Object = Me.Invoke("VerificarCodigoCanal", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal.VerificarCodigoCanal.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoSubCanal", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoSubCanal(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Canal.VerificarCodigoSubCanal.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Canal.VerificarCodigoSubCanal.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICanal.VerificarCodigoSubCanal
        Dim results() As Object = Me.Invoke("VerificarCodigoSubCanal", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal.VerificarCodigoSubCanal.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionCanal", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarDescripcionCanal(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Canal.VerificarDescripcionCanal.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Canal.VerificarDescripcionCanal.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICanal.VerificarDescripcionCanal
        Dim results() As Object = Me.Invoke("VerificarDescripcionCanal", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal.VerificarDescripcionCanal.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionSubCanal", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarDescripcionSubCanal(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Canal.VerificarDescripcionSubCanal.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Canal.VerificarDescripcionSubCanal.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICanal.VerificarDescripcionSubCanal
        Dim results() As Object = Me.Invoke("VerificarDescripcionSubCanal", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal.VerificarDescripcionSubCanal.Respuesta)
    End Function
End Class
