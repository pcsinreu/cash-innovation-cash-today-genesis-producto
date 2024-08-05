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
    Public Class ProxyRespaldoCompleto
        Inherits ProxyWS.ServicioBase
        Implements IRespaldoCompleto

        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "Reportes/RespaldoCompleto.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/ListarRespaldoCompleto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ListarRespaldoCompleto(objPeticion As RespaldoCompleto.GetRespaldosCompletos.Peticion) As RespaldoCompleto.GetRespaldosCompletos.Respuesta Implements IRespaldoCompleto.ListarRespaldoCompleto
            Dim results() As Object = Me.Invoke("ListarRespaldoCompleto", New Object() {objPeticion})
            Return CType(results(0), RespaldoCompleto.GetRespaldosCompletos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As Test.Respuesta Implements IRespaldoCompleto.Test
            Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
            Return CType(results(0), Test.Respuesta)
        End Function
    End Class

End Namespace