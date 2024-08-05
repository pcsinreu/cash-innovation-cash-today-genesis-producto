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
Public Class ProxyDireccion
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IDireccion

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Direccion.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetDirecciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetDirecciones(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Direccion.GetDirecciones.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Direccion.GetDirecciones.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDireccion.GetDirecciones
        Dim results() As Object = Me.Invoke("GetDirecciones", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Direccion.GetDirecciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetDirecciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetDirecciones(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Direccion.SetDirecciones.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Direccion.SetDirecciones.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDireccion.SetDirecciones
        Dim results() As Object = Me.Invoke("SetDirecciones", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Direccion.SetDirecciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDireccion.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function
End Class
