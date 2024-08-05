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
Public Class ProxyDivisa
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IDivisa

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Divisa.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getDenominacionesByDivisa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getDenominacionesByDivisa(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.GetDenominacionesByDivisa.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.GetDenominacionesByDivisa.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDivisa.getDenominacionesByDivisa
        Dim results() As Object = Me.Invoke("getDenominacionesByDivisa", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa.GetDenominacionesByDivisa.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getDivisas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getDivisas(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.GetDivisas.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.GetDivisas.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDivisa.getDivisas
        Dim results() As Object = Me.Invoke("getDivisas", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa.GetDivisas.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetDivisasPaginacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetDivisasPaginacion(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.GetDivisasPaginacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.GetDivisasPaginacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDivisa.GetDivisasPaginacion
        Dim results() As Object = Me.Invoke("GetDivisasPaginacion", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa.GetDivisasPaginacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/setDivisaDenominaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function setDivisaDenominaciones(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDivisa.setDivisaDenominaciones
        Dim results() As Object = Me.Invoke("setDivisaDenominaciones", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDivisa.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoDenominacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoDenominacion(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.VerificarCodigoDenominacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.VerificarCodigoDenominacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDivisa.VerificarCodigoDenominacion
        Dim results() As Object = Me.Invoke("VerificarCodigoDenominacion", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa.VerificarCodigoDenominacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoDivisa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoDivisa(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.VerificarCodigoDivisa.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDivisa.VerificarCodigoDivisa
        Dim results() As Object = Me.Invoke("VerificarCodigoDivisa", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionDivisa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarDescripcionDivisa(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.VerificarDescripcionDivisa.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Divisa.VerificarDescripcionDivisa.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDivisa.VerificarDescripcionDivisa
        Dim results() As Object = Me.Invoke("VerificarDescripcionDivisa", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Divisa.VerificarDescripcionDivisa.Respuesta)
    End Function
End Class
