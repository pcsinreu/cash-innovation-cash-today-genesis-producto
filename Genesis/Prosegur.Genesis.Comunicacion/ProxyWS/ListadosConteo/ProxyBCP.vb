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

Namespace ListadosConteo

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.Diagnostics.DebuggerStepThroughAttribute(), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="ReportesSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.Reportes")> _
    Public Class ProxyBCP
        Inherits ProxyWS.ServicioBase
        Implements IBCP

        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "Reportes/BCP.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/GuardarItemProcesoConteo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarItemProcesoConteo(objPeticion As bcp.GuardarItemProcesoConteo.Peticion) As bcp.GuardarItemProcesoConteo.Respuesta Implements IBCP.GuardarItemProcesoConteo
            Dim results() As Object = Me.Invoke("GuardarItemProcesoConteo", New Object() {objPeticion})
            Return CType(results(0), bcp.GuardarItemProcesoConteo.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/RecuperarPedidosReportadosBCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarPedidosReportadosBCP(objPeticion As bcp.RecuperarPedidosReportadosBCP.Peticion) As bcp.RecuperarPedidosReportadosBCP.Respuesta Implements IBCP.RecuperarPedidosReportadosBCP
            Dim results() As Object = Me.Invoke("RecuperarPedidosReportadosBCP", New Object() {objPeticion})
            Return CType(results(0), bcp.RecuperarPedidosReportadosBCP.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As Test.Respuesta Implements IBCP.Test
            Dim results() As Object = Me.Invoke("Test", New Object() {-1})
            Return CType(results(0), Test.Respuesta)
        End Function

    End Class

End Namespace