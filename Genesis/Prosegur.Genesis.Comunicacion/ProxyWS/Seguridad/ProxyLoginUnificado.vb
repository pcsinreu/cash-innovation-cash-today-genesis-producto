Option Strict Off
Option Explicit On

Imports Prosegur.Genesis.ContractoServicio
Imports System.Configuration
Imports Prosegur.Genesis.ContractoServicio.Interfaces


Namespace ProxyWS

    ''' <summary>
    ''' Proxy para Efetuar Login
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  23/05/2012  criado
    ''' </history>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="GenesisSoap", [Namespace]:="http://Prosegur.Genesis.Web")> _
    Public Class ProxyLoginUnificado
        Inherits ServicioBase
        Implements IIntegracionLoginUnificado

        Private useDefaultCredentialsSetExplicitly As Boolean

        Public Sub New()
            MyBase.New()

            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("WS_TIMEOUT")) AndAlso IsNumeric(ConfigurationManager.AppSettings("WS_TIMEOUT")) Then
                MyBase.Timeout = Integer.Parse(ConfigurationManager.AppSettings("WS_TIMEOUT")) * 1000
            End If

            Me.useDefaultCredentialsSetExplicitly = True
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Web/AutenticarUsuarioAplicacionLoginUnificado", RequestNamespace:="http://Prosegur.Genesis.Web", ResponseNamespace:="http://Prosegur.Genesis.Web", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AutenticarUsuarioAplicacionLoginUnificado(Peticion As ContractoServicio.Login.EjecutarLogin.Peticion) As ContractoServicio.Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta Implements ContractoServicio.Interfaces.IIntegracionLoginUnificado.AutenticarUsuarioAplicacionLoginUnificado
            Dim results() As Object = Me.Invoke("AutenticarUsuarioAplicacionLoginUnificado", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Web/CrearTokenAcceso", RequestNamespace:="http://Prosegur.Genesis.Web", ResponseNamespace:="http://Prosegur.Genesis.Web", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CrearTokenAcceso(Peticion As Login.CrearTokenAcceso.Peticion) As Login.CrearTokenAcceso.Respuesta Implements IIntegracionLoginUnificado.CrearTokenAcceso
            Dim results() As Object = Me.Invoke("CrearTokenAcceso", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.Login.CrearTokenAcceso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Web/EjecutarLogin", RequestNamespace:="http://Prosegur.Genesis.Web", ResponseNamespace:="http://Prosegur.Genesis.Web", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EjecutarLogin(Peticion As Login.EjecutarLogin.Peticion) As Login.EjecutarLogin.Respuesta Implements IIntegracionLoginUnificado.EjecutarLogin
            Dim results() As Object = Me.Invoke("EjecutarLogin", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.Login.EjecutarLogin.Respuesta)
        End Function
    End Class

End Namespace