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
Public Class ProxyGrupo
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IGrupo

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Grupo.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetATMsbyGrupo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetATMsbyGrupo(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Grupo.GetATMsbyGrupo.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Grupo.GetATMsbyGrupo.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IGrupo.GetATMsbyGrupo
        Dim results() As Object = Me.Invoke("GetATMsbyGrupo", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Grupo.GetATMsbyGrupo.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetGrupos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetGrupos(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Grupo.GetGrupos.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Grupo.GetGrupos.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IGrupo.GetGrupos
        Dim results() As Object = Me.Invoke("GetGrupos", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Grupo.GetGrupos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetGrupo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetGrupo(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Grupo.SetGrupo.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Grupo.SetGrupo.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IGrupo.SetGrupo
        Dim results() As Object = Me.Invoke("SetGrupo", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Grupo.SetGrupo.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IGrupo.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarGrupo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarGrupo(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Grupo.VerificarGrupo.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Grupo.VerificarGrupo.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IGrupo.VerificarGrupo
        Dim results() As Object = Me.Invoke("VerificarGrupo", New Object() {Peticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Grupo.VerificarGrupo.Respuesta)
    End Function
End Class
