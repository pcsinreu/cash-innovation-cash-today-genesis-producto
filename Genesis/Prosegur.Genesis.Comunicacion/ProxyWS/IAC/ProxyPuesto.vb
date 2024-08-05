Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Prosegur.Global.GesEfectivo.IAC
Imports System.Configuration
Imports System.Net
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio

Namespace ProxyWS.IAC
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")> _
    Public Class ProxyPuesto
        Inherits ProxyWS.ServicioBase
        Implements IPuesto

        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "IAC/Puesto.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPuestos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetPuestos(Peticion As Puesto.GetPuestos.Peticion) As Puesto.GetPuestos.Respuesta Implements IPuesto.GetPuestos
            Dim results() As Object = Me.Invoke("GetPuestos", New Object() {Peticion})
            Return CType(results(0), Puesto.GetPuestos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPuestoDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetPuestoDetail(Peticion As Puesto.GetPuestoDetail.Peticion) As Puesto.GetPuestoDetail.Respuesta Implements IPuesto.GetPuestoDetail
            Dim results() As Object = Me.Invoke("GetPuestoDetail", New Object() {Peticion})
            Return CType(results(0), Puesto.GetPuestoDetail.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetPuesto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SetPuesto(peticion As Puesto.SetPuesto.Peticion) As Puesto.SetPuesto.Respuesta Implements IPuesto.SetPuesto
            Dim results() As Object = Me.Invoke("SetPuesto", New Object() {peticion})
            Return CType(results(0), Puesto.SetPuesto.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As Test.Respuesta Implements IPuesto.Test
            Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
            Return CType(results(0), Test.Respuesta)
        End Function

    End Class
End Namespace