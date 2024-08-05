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
Public Class ProxyProceso
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IProceso


    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Proceso.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetProceso", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetProceso(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Proceso.GetProceso.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Proceso.GetProceso.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IProceso.GetProceso
        Dim results() As Object = Me.Invoke("GetProceso", New Object() {peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.Proceso.GetProceso.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetProcesoDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetProcesoDetail(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Proceso.GetProcesoDetail.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Proceso.GetProcesoDetail.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IProceso.GetProcesoDetail
        Dim results() As Object = Me.Invoke("GetProcesoDetail", New Object() {peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.Proceso.GetProcesoDetail.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetProceso", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetProceso(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Proceso.SetProceso.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Proceso.SetProceso.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IProceso.SetProceso
        Dim results() As Object = Me.Invoke("SetProceso", New Object() {peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.Proceso.SetProceso.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IProceso.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta)
    End Function
End Class
