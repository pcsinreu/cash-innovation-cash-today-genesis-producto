Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.Reportes.ContractoServ
Imports System.Collections.Specialized
Imports System.Web

Namespace ListadosConteo

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="ReportesSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.Reportes")> _
    Public Class ProxyInformeResultadoContaje
        Inherits ProxyWS.ServicioBase
        Implements IInformeResutadoContaje

        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "Reportes/InformeResultadoContaje.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/ListarResultadoContaje", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ListarResultadoContaje(Peticion As InformeResultadoContaje.ListarResultadoContaje.Peticion) As InformeResultadoContaje.ListarResultadoContaje.Respuesta Implements IInformeResutadoContaje.ListarResultadoContaje
            Dim results() As Object = Me.Invoke("ListarResultadoContaje", New Object() {Peticion})
            Return CType(results(0), InformeResultadoContaje.ListarResultadoContaje.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/BuscarResultadoContaje", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BuscarResultadoContaje(Peticion As InformeResultadoContaje.BuscarResultadoContaje.Peticion) As InformeResultadoContaje.BuscarResultadoContaje.Respuesta Implements IInformeResutadoContaje.BuscarResultadoContaje
            Dim results() As Object = Me.Invoke("BuscarResultadoContaje", New Object() {Peticion})
            Return CType(results(0), InformeResultadoContaje.BuscarResultadoContaje.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As Test.Respuesta Implements IInformeResutadoContaje.Test
            Dim results() As Object = Me.Invoke("Test", New Object() {-1})
            Return CType(results(0), Test.Respuesta)
        End Function

    End Class

End Namespace