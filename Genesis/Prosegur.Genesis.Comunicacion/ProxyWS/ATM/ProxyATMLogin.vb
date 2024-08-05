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


''' <summary>
''' proxy para acesso ao WS AsociarFormatoTira
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa]  14/12/2010  criado
''' </history>
<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="ATMSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.ATM")> _
Public Class ProxyATMLogin
    Inherits ServicoBase
    Implements ILogin

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
        Me.Url = ConfigurationManager.AppSettings("UrlServicio") & "ATM/Login.asmx"
        Me.useDefaultCredentialsSetExplicitly = True
    End Sub

#End Region

#Region "[MÉTODOS]"

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/EjecutarLogin", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function EjecutarLogin(ByVal Peticion As Login.EjecutarLogin.Peticion) As Login.EjecutarLogin.Respuesta Implements ILogin.EjecutarLogin

        Dim results() As Object = Me.Invoke("EjecutarLogin", New Object() {Peticion})
        Return CType(results(0), Login.EjecutarLogin.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
     System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/EjecutarLoginEmulador", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function EjecutarLoginEmulador(ByVal Peticion As Login.EjecutarLoginEmulador.Peticion) As Login.EjecutarLoginEmulador.Respuesta Implements ILogin.EjecutarLoginEmulador

        Dim results() As Object = Me.Invoke("EjecutarLoginEmulador", New Object() {Peticion})
        Return CType(results(0), Login.EjecutarLoginEmulador.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/RecuperaDatosUsuario", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperaDatosUsuario(ByVal Peticion As Login.RecuperaDatosUsuario.Peticion) As Login.RecuperaDatosUsuario.Respuesta Implements ILogin.RecuperaDatosUsuario

        Dim results() As Object = Me.Invoke("RecuperaDatosUsuario", New Object() {Peticion})
        Return CType(results(0), Login.RecuperaDatosUsuario.Respuesta)

    End Function


    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As Test.Respuesta Implements ILogin.Test
        Dim results() As Object = Me.Invoke("Test", New Object() {-1})
        Return CType(results(0), Test.Respuesta)
    End Function

#End Region


End Class


