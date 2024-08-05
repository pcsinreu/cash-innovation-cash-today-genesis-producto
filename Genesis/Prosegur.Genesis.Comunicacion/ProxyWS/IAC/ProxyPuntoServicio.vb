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
Public Class ProxyPuntoServicio
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IPuntoServicio

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/PuntoServicio.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPuntoServicio", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetPuntoServicio(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPuntoServicio.GetPuntoServicio
        Dim results() As Object = Me.Invoke("GetPuntoServicio", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPuntoServicioByCodigoAjeno", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetPuntoServicioByCodigoAjeno(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPuntoServicio.GetPuntoServicioByCodigoAjeno
        Dim results() As Object = Me.Invoke("GetPuntoServicioByCodigoAjeno", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPuntoServicioDetalle", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetPuntoServicioDetalle(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPuntoServicio.GetPuntoServicioDetalle
        Dim results() As Object = Me.Invoke("GetPuntoServicioDetalle", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetPuntoServicio", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetPuntoServicio(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPuntoServicio.SetPuntoServicio
        Dim results() As Object = Me.Invoke("SetPuntoServicio", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPuntoServicio.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function
End Class
