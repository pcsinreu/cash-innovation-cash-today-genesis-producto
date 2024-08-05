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
''' proxy para acesso ao WS FormatoTira
''' </summary>
''' <remarks></remarks>
''' <history>
''' [kirkpatrick.santos]  20/01/2011  criado
''' </history>
<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="ATMSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.ATM")> _
Public Class ProxyRegistrarTira
    Inherits ServicoBase
    Implements IRegistrarTira


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
        Me.Url = ConfigurationManager.AppSettings("UrlServicio") & "ATM/RegistrarTira.asmx"
        Me.useDefaultCredentialsSetExplicitly = True
    End Sub

#End Region

#Region "[MÉTODOS]"

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/SetRegistrarTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetRegistrarTira(ByVal Peticion As RegistrarTira.SetRegistrarTira.Peticion) As RegistrarTira.SetRegistrarTira.Respuesta Implements IRegistrarTira.SetRegistrarTira

        Dim results() As Object = Me.Invoke("SetRegistrarTira", New Object() {Peticion})
        Return CType(results(0), RegistrarTira.SetRegistrarTira.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/GetValoresUltimaTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetValoresUltimaTira(ByVal Peticion As RegistrarTira.GetValoresUltimaTira.Peticion) As RegistrarTira.GetValoresUltimaTira.Respuesta Implements IRegistrarTira.GetValoresUltimaTira
        Dim results() As Object = Me.Invoke("GetValoresUltimaTira", New Object() {Peticion})
        Return CType(results(0), RegistrarTira.GetValoresUltimaTira.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/RecuperarTiraDeCarga", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarTiraDeCarga(ByVal Peticion As RegistrarTira.RecuperarTiraDeCarga.Peticion) As RegistrarTira.RecuperarTiraDeCarga.Respuesta Implements IRegistrarTira.RecuperarTiraDeCarga
        Dim results() As Object = Me.Invoke("RecuperarTiraDeCarga", New Object() {Peticion})
        Return CType(results(0), RegistrarTira.RecuperarTiraDeCarga.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As Test.Respuesta Implements IRegistrarTira.Test
        Dim results() As Object = Me.Invoke("Test", New Object() {-1})
        Return CType(results(0), Test.Respuesta)
    End Function

#End Region
   
End Class
