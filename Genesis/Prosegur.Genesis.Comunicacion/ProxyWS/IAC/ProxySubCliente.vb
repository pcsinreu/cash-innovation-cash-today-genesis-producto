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
Public Class ProxySubCliente
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ISubCliente

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/SubCliente.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetSubclienteByCodigoAjeno", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetSubclienteByCodigoAjeno(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISubCliente.GetSubclienteByCodigoAjeno
        Dim results() As Object = Me.Invoke("GetSubclienteByCodigoAjeno", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetSubClientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetSubClientes(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.SubCliente.SetSubClientes.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.SubCliente.SetSubClientes.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISubCliente.SetSubClientes
        Dim results() As Object = Me.Invoke("SetSubClientes", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.SetSubClientes.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetSubClientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetSubClientes(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientes.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISubCliente.GetSubClientes
        Dim results() As Object = Me.Invoke("GetSubClientes", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientes.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetSubClientesDetalle", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetSubClientesDetalle(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISubCliente.GetSubClientesDetalle
        Dim results() As Object = Me.Invoke("GetSubClientesDetalle", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Respuesta)
    End Function


    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISubCliente.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), Prosegur.Genesis.ContractoServicio.Test.Respuesta)
    End Function
End Class
