Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Microsoft.Web.Services3.Security.Tokens
Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.ATM.ContractoServicio

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
System.Diagnostics.DebuggerStepThroughAttribute(), _
System.ComponentModel.DesignerCategoryAttribute("code"), _
System.Web.Services.WebServiceBindingAttribute(Name:="ATMSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.ATM")> _
Public Class ProxyAtmIntegracion
    Inherits ProxyWS.ServicioBase
    Implements IIntegracion

#Region "[VARIÁVEIS]"

    Private sesionInfoValueField As New ServicoBase.SesionInfo()
    Private useDefaultCredentialsSetExplicitly As Boolean

#End Region

#Region "[PROPRIEDADES]"

    Public Property SesionInfoValue() As ServicoBase.SesionInfo
        Get
            Return Me.sesionInfoValueField
        End Get
        Set(ByVal value As ServicoBase.SesionInfo)
            Me.sesionInfoValueField = value
        End Set
    End Property

#End Region

#Region "[CONSTRUTOR]"

    Public Sub New()
        MyBase.New()
        Me.Url = UrlServicio & "ATM/Integracion.asmx"
        Me.useDefaultCredentialsSetExplicitly = True
    End Sub

    Public Sub New(urlBase As String)
        MyBase.New()

        If String.IsNullOrEmpty(urlBase) Then
            Me.Url = UrlServicio() & "ATM/Integracion.asmx"
        Else
            Me.Url = urlBase & "ATM/Integracion.asmx"
        End If
    End Sub

#End Region

#Region "[MÉTODOS]"

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/ActualizarEstadoHabilitacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ActualizarEstadoHabilitacion(Peticion As Integracion.ActualizarEstadoHabilitacion.Peticion) As Integracion.ActualizarEstadoHabilitacion.Respuesta Implements IIntegracion.ActualizarEstadoHabilitacion
        Dim results() As Object = Me.Invoke("ActualizarEstadoHabilitacion", New Object() {Peticion})
        Return CType(results(0), Integracion.ActualizarEstadoHabilitacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/RecibirSolicitudCarga", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecibirSolicitudCarga(Peticion As Integracion.RecibirSolicitudCarga.Peticion) As Integracion.RecibirSolicitudCarga.Respuesta Implements IIntegracion.RecibirSolicitudCarga
        Dim results() As Object = Me.Invoke("RecibirSolicitudCarga", New Object() {Peticion})
        Return CType(results(0), Integracion.RecibirSolicitudCarga.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/RecuperarDatosTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarDatosTira(Peticion As Integracion.RecuperarDatosTira.Peticion) As Integracion.RecuperarDatosTira.Respuesta Implements IIntegracion.RecuperarDatosTira
        Dim results() As Object = Me.Invoke("RecuperarDatosTira", New Object() {Peticion})
        Return CType(results(0), Integracion.RecuperarDatosTira.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/BalancearTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function BalancearTira(Peticion As Integracion.BalancearTira.Peticion) As Integracion.BalancearTira.Respuesta Implements IIntegracion.BalancearTira
        Dim results() As Object = Me.Invoke("BalancearTira", New Object() {Peticion})
        Return CType(results(0), Integracion.BalancearTira.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/GetTiras", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetTiras(Peticion As Integracion.GetTiras.Peticion) As Integracion.GetTiras.Respuesta Implements IIntegracion.GetTiras
        Dim results() As Object = Me.Invoke("GetTiras", New Object() {Peticion})
        Return CType(results(0), Integracion.GetTiras.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/GetTirasDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetTirasDetail(Peticion As Integracion.GetTirasDetail.Peticion) As Integracion.GetTirasDetail.Respuesta Implements IIntegracion.GetTirasDetail
        Dim results() As Object = Me.Invoke("GetTirasDetail", New Object() {Peticion})
        Return CType(results(0), Integracion.GetTirasDetail.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/SincronizarDivisasATM", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SincronizarDivisasATM(Peticion As Integracion.SincronizarDivisasATM.Peticion) As Integracion.SincronizarDivisasATM.Respuesta Implements IIntegracion.SincronizarDivisasATM
        Dim results() As Object = Me.Invoke("SincronizarDivisasATM", New Object() {Peticion})
        Return CType(results(0), Integracion.SincronizarDivisasATM.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
   System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As Integracion.Test.Respuesta Implements IIntegracion.Test
        Dim results() As Object = Me.Invoke("Test", New Object() {-1})
        Return CType(results(0), Integracion.Test.Respuesta)
    End Function

#End Region


End Class
