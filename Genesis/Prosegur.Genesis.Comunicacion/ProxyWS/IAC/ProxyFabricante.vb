Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Configuration

Namespace ProxyWS.IAC

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.Diagnostics.DebuggerStepThroughAttribute(), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")> _
    Public Class ProxyFabricante
        Inherits ProxyWS.ServicioBase
        Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IFabricante


        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "IAC/Fabricante.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IFabricante.Test
            Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Test.Respuesta)

        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetFabricante", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetFabricante(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Fabricante.GetFabricante.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Fabricante.GetFabricante.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IFabricante.GetFabricante
            Dim results() As Object = Me.Invoke("GetFabricante", New Object() {objPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Fabricante.GetFabricante.Respuesta)
        End Function

    End Class

End Namespace