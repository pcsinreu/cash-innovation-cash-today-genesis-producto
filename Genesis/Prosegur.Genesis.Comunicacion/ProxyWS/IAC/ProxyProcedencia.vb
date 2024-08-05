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
Public Class ProxyProcedencia
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IProcedencia

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Procedencia.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/ActualizaProcedencia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ActualizaProcedencia(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Procedencia.SetProcedencia.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Procedencia.SetProcedencia.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IProcedencia.ActualizaProcedencia
        Dim results() As Object = Me.Invoke("ActualizaProcedencia", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Procedencia.SetProcedencia.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/AltaProcedencia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function AltaProcedencia(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Procedencia.SetProcedencia.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Procedencia.SetProcedencia.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IProcedencia.AltaProcedencia
        Dim results() As Object = Me.Invoke("AltaProcedencia", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Procedencia.SetProcedencia.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetProcedencias", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetProcedencias(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Procedencia.GetProcedencias.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Procedencia.GetProcedencias.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IProcedencia.GetProcedencias
        Dim results() As Object = Me.Invoke("GetProcedencias", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Procedencia.GetProcedencias.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IProcedencia.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificaExisteProcedencia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificaExisteProcedencia(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Procedencia.VerificarExisteProcedencia.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Procedencia.VerificarExisteProcedencia.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IProcedencia.VerificaExisteProcedencia
        Dim results() As Object = Me.Invoke("VerificaExisteProcedencia", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Procedencia.VerificarExisteProcedencia.Respuesta)
    End Function
End Class
