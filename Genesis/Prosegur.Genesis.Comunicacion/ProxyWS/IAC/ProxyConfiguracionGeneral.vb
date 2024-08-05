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
Public Class ProxyConfiguracionGeneral
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IConfiguracionGeneral

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/ConfiguracionGeneral.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/AtualizarConfiguracionGeneralReporte", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function AtualizarConfiguracionGeneralReporte(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Configuracion.General.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IConfiguracionGeneral.AtualizarConfiguracionGeneralReporte
        Dim results() As Object = Me.Invoke("AtualizarConfiguracionGeneralReporte", New Object() {peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/ExcluirConfiguracionGeneralReporte", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ExcluirConfiguracionGeneralReporte(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Configuracion.General.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IConfiguracionGeneral.ExcluirConfiguracionGeneralReporte
        Dim results() As Object = Me.Invoke("ExcluirConfiguracionGeneralReporte", New Object() {peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetConfiguracionGeneralReporte", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetConfiguracionGeneralReporte(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Configuracion.General.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IConfiguracionGeneral.GetConfiguracionGeneralReporte
        Dim results() As Object = Me.Invoke("GetConfiguracionGeneralReporte", New Object() {peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetConfiguracionGeneralReportes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetConfiguracionGeneralReportes() As [Global].GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IConfiguracionGeneral.GetConfiguracionGeneralReportes
        Dim results() As Object = Me.Invoke("GetConfiguracionGeneralReportes", New Object(-1) {})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/InserirConfiguracionGeneralReporte", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function InserirConfiguracionGeneralReporte(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Configuracion.General.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IConfiguracionGeneral.InserirConfiguracionGeneralReporte
        Dim results() As Object = Me.Invoke("InserirConfiguracionGeneralReporte", New Object() {peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Configuracion.General.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IConfiguracionGeneral.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function
End Class
