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
Public Class ProxyCliente
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ICliente

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Cliente.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetClienteByCodigoAjeno", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetClienteByCodigoAjeno(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Cliente.GetClienteByCodigoAjeno.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Cliente.GetClienteByCodigoAjeno.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICliente.GetClienteByCodigoAjeno
        Dim results() As Object = Me.Invoke("GetClienteByCodigoAjeno", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente.GetClienteByCodigoAjeno.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetClientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetClientes(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Cliente.GetClientes.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Cliente.GetClientes.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICliente.GetClientes
        Dim results() As Object = Me.Invoke("GetClientes", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente.GetClientes.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetClientesDetalle", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetClientesDetalle(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Cliente.GetClientesDetalle.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Cliente.GetClientesDetalle.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICliente.GetClientesDetalle
        Dim results() As Object = Me.Invoke("GetClientesDetalle", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente.GetClientesDetalle.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetClientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetClientes(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Cliente.SetClientes.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Cliente.SetClientes.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICliente.SetClientes
        Dim results() As Object = Me.Invoke("SetClientes", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente.SetClientes.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICliente.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContractoServicio.Test.Respuesta)
    End Function
End Class
