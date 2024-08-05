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
Public Class ProxyMantenimientoTira
    Inherits ServicoBase
    Implements IMantenimientoTira


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
        Me.Url = ConfigurationManager.AppSettings("UrlServicio") & "ATM/MantenimientoTira.asmx"
        Me.useDefaultCredentialsSetExplicitly = True
    End Sub

#End Region

#Region "[MÉTODOS]"

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As Test.Respuesta Implements IMantenimientoTira.Test
        Dim results() As Object = Me.Invoke("Test", New Object() {-1})
        Return CType(results(0), Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/VerificarHayConteoTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarHayConteoTira(ByVal Peticion As MantenimientoTira.VerificarHayConteoTira.Peticion) As MantenimientoTira.VerificarHayConteoTira.Respuesta Implements IMantenimientoTira.VerificarHayConteoTira

        Dim results() As Object = Me.Invoke("VerificarHayConteoTira", New Object() {Peticion})
        Return CType(results(0), MantenimientoTira.VerificarHayConteoTira.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/BusquedaTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function BusquedaTira(ByVal Peticion As MantenimientoTira.BusquedaTira.Peticion) As MantenimientoTira.BusquedaTira.Respuesta Implements IMantenimientoTira.BusquedaTira

        Dim results() As Object = Me.Invoke("BusquedaTira", New Object() {Peticion})
        Return CType(results(0), MantenimientoTira.BusquedaTira.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
    System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/EliminarTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function EliminarTira(ByVal Peticion As MantenimientoTira.EliminarTira.Peticion) As MantenimientoTira.EliminarTira.Respuesta Implements IMantenimientoTira.EliminarTira

        Dim results() As Object = Me.Invoke("EliminarTira", New Object() {Peticion})
        Return CType(results(0), MantenimientoTira.EliminarTira.Respuesta)

    End Function

#End Region

End Class
