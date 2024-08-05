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
Public Class ProxyAgrupacion
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IAgrupacion

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Agrupacion.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetAgrupaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetAgrupaciones(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.GetAgrupaciones.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.GetAgrupaciones.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IAgrupacion.GetAgrupaciones
        Dim results() As Object = Me.Invoke("GetAgrupaciones", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Agrupacion.GetAgrupaciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetAgrupacionesDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetAgrupacionesDetail(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IAgrupacion.GetAgrupacionesDetail
        Dim results() As Object = Me.Invoke("GetAgrupacionesDetail", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetAgrupaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetAgrupaciones(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IAgrupacion.SetAgrupaciones
        Dim results() As Object = Me.Invoke("SetAgrupaciones", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IAgrupacion.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoAgrupacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoAgrupacion(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IAgrupacion.VerificarCodigoAgrupacion
        Dim results() As Object = Me.Invoke("VerificarCodigoAgrupacion", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionAgrupacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarDescripcionAgrupacion(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IAgrupacion.VerificarDescripcionAgrupacion
        Dim results() As Object = Me.Invoke("VerificarDescripcionAgrupacion", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Respuesta)
    End Function
End Class
