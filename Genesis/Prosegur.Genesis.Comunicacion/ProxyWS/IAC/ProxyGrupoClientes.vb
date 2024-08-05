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

''' <summary>
''' Classe de Proxy para Grupo Cliente
''' </summary>
''' <remarks></remarks>
''' <history>
''' [matheus.araujo] 24/10/2012 - Criado
''' </history>
<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")> _
Public Class ProxyGrupoClientes
    Inherits ProxyWS.ServicioBase
    Implements IGrupoCliente

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/GrupoCliente.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetGruposClientesDetalle", _
        RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", _
        ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", _
        Use:=System.Web.Services.Description.SoapBindingUse.Literal, _
        ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetGruposClientesDetalle(Peticion As GrupoCliente.GetGruposClientesDetalle.Peticion) _
        As GrupoCliente.GetGruposClientesDetalle.Respuesta _
        Implements IGrupoCliente.GetGruposClientesDetalle

        Dim results() As Object = Me.Invoke("GetGruposClientesDetalle", New Object() {Peticion})
        Return CType(results(0), GrupoCliente.GetGruposClientesDetalle.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetGruposCliente", _
        RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", _
        ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", _
        Use:=System.Web.Services.Description.SoapBindingUse.Literal, _
        ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetGruposCliente(Peticion As GrupoCliente.GetGruposCliente.Peticion) _
        As GrupoCliente.GetGruposCliente.Respuesta _
        Implements IGrupoCliente.GetGruposCliente

        Dim results() As Object = Me.Invoke("GetGruposCliente", New Object() {Peticion})
        Return CType(results(0), GrupoCliente.GetGruposCliente.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetGrupoCliente", _
        RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", _
        ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", _
        Use:=System.Web.Services.Description.SoapBindingUse.Literal, _
        ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetGrupoCliente(Peticion As GrupoCliente.SetGruposCliente.Peticion) _
        As GrupoCliente.SetGruposCliente.Respuesta _
        Implements IGrupoCliente.SetGrupoCliente

        Dim results() As Object = Me.Invoke("SetGrupoCliente", New Object() {Peticion})
        Return CType(results(0), GrupoCliente.SetGruposCliente.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As Test.Respuesta Implements IGrupoCliente.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), Test.Respuesta)
    End Function

End Class
