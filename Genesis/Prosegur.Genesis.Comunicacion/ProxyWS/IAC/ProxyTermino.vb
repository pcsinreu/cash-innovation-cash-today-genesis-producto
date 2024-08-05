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
Public Class ProxyTermino
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ITermino

    Public Sub New()
        MyBase.New()
        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Termino.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

   
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getTerminos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getTerminos(Peticion As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoIac.Peticion) As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoIac.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ITermino.getTerminos
        Dim results() As Object = Me.Invoke("getTerminos", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoIac.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getTerminoDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getTerminoDetail(Peticion As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.Peticion) As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ITermino.getTerminoDetail
        Dim results() As Object = Me.Invoke("getTerminoDetail", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.GetTerminoDetailIac.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/setTermino", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function setTermino(Peticion As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.SetTerminoIac.Peticion) As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.SetTerminoIac.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ITermino.setTermino
        Dim results() As Object = Me.Invoke("setTermino", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.SetTerminoIac.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoTermino", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoTermino(Peticion As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.VerificarCodigoTermino.Peticion) As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.VerificarCodigoTermino.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ITermino.VerificarCodigoTermino
        Dim results() As Object = Me.Invoke("VerificarCodigoTermino", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.VerificarCodigoTermino.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionTermino", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarDescripcionTermino(Peticion As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.VerificarDescripcionTermino.Peticion) As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.VerificarDescripcionTermino.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ITermino.VerificarDescripcionTermino
        Dim results() As Object = Me.Invoke("VerificarDescripcionTermino", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TerminoIac.VerificarDescripcionTermino.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ITermino.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Test.Respuesta)
    End Function

    
End Class
