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
Public Class ProxyAsociarFormatoTira
    Inherits ServicoBase
    Implements IAsociarFormatoTira

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
        Me.Url = ConfigurationManager.AppSettings("UrlServicio") & "ATM/AsociarFormatoTira.asmx"
        Me.useDefaultCredentialsSetExplicitly = True
    End Sub

#End Region

#Region "[MÉTODOS]"

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/GrabarAsociacionTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GrabarAsociacionTira(ByVal Peticion As AsociarFormatoTira.GrabarAsociacionTira.Peticion) As AsociarFormatoTira.GrabarAsociacionTira.Respuesta Implements IAsociarFormatoTira.GrabarAsociacionTira

        Dim results() As Object = Me.Invoke("GrabarAsociacionTira", New Object() {Peticion})
        Return CType(results(0), AsociarFormatoTira.GrabarAsociacionTira.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
     System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/ObtenerFormatoTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerFormatoTira() As AsociarFormatoTira.ObtenerFormatoTira.Respuesta Implements IAsociarFormatoTira.ObtenerFormatoTira

        Dim results() As Object = Me.Invoke("ObtenerFormatoTira", New Object(-1) {})
        Return CType(results(0), AsociarFormatoTira.ObtenerFormatoTira.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
     System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/ObtenerModeloCajero", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerModeloCajero() As AsociarFormatoTira.ObtenerModeloCajero.Respuesta Implements IAsociarFormatoTira.ObtenerModeloCajero

        Dim results() As Object = Me.Invoke("ObtenerModeloCajero", New Object(-1) {})
        Return CType(results(0), AsociarFormatoTira.ObtenerModeloCajero.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
     System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/ObtenerRed", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerRed() As AsociarFormatoTira.ObtenerRed.Respuesta Implements IAsociarFormatoTira.ObtenerRed

        Dim results() As Object = Me.Invoke("ObtenerRed", New Object(-1) {})
        Return CType(results(0), AsociarFormatoTira.ObtenerRed.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
     System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/ObtenerRed", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As Test.Respuesta Implements IAsociarFormatoTira.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), Test.Respuesta)
    End Function

#End Region

End Class
