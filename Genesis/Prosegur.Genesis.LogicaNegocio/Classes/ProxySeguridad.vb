Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Microsoft.Web.Services3.Security.Tokens
Imports Prosegur.Global.Seguridad.ContractoServicio
Imports GenesisSeguridad = Prosegur.Global.Seguridad.ContractoServicio.Genesis

Namespace Proxy

    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="SeguridadSoap", [Namespace]:="http://Prosegur.Global.Seguridad")> _
    Partial Public Class Seguridad
        Inherits Microsoft.Web.Services3.WebServicesClientProtocol

        Public Sub New()
            MyBase.New()
            Me.Url = Parametros.Configuracion.UrlLoginGlobal
            Dim token As New UsernameToken(Parametros.Configuracion.UsuarioWSLogin, Parametros.Configuracion.PasswordWSLogin, PasswordOption.SendPlainText)
            Me.SetClientCredential(token)
            Me.SetPolicy("ClientPolicy")
        End Sub


        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/GenesisEjecutarLogin", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GenesisEjecutarLogin(Peticion As GenesisSeguridad.EjecutarLogin.Peticion) As GenesisSeguridad.EjecutarLogin.Respuesta
            Dim results() As Object = Me.Invoke("GenesisEjecutarLogin", New Object() {Peticion})
            Return CType(results(0), GenesisSeguridad.EjecutarLogin.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/GenesisObtenerDelegaciones", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GenesisObtenerDelegaciones(Peticion As GenesisSeguridad.ObtenerDelegaciones.Peticion) As GenesisSeguridad.ObtenerDelegaciones.Respuesta
            Dim results() As Object = Me.Invoke("GenesisObtenerDelegaciones", New Object() {Peticion})
            Return CType(results(0), GenesisSeguridad.ObtenerDelegaciones.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/GenesisObtenerAplicaciones", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GenesisObtenerAplicaciones(Peticion As GenesisSeguridad.ObtenerAplicaciones.Peticion) As GenesisSeguridad.ObtenerAplicaciones.Respuesta
            Dim results() As Object = Me.Invoke("GenesisObtenerAplicaciones", New Object() {Peticion})
            Return CType(results(0), GenesisSeguridad.ObtenerAplicaciones.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/GenesisObtenerPermisos", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GenesisObtenerPermisos(Peticion As GenesisSeguridad.ObtenerPermisos.Peticion) As GenesisSeguridad.ObtenerPermisos.Respuesta
            Dim results() As Object = Me.Invoke("GenesisObtenerPermisos", New Object() {Peticion})
            Return CType(results(0), GenesisSeguridad.ObtenerPermisos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/CrearTokenAcceso", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CrearTokenAcceso(Peticion As CrearTokenAcceso.Peticion) As CrearTokenAcceso.Respuesta
            Dim results() As Object = Me.Invoke("CrearTokenAcceso", New Object() {Peticion})
            Return CType(results(0), CrearTokenAcceso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerTokenAcceso", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerTokenAcceso(Peticion As ObtenerTokenAcceso.Peticion) As ObtenerTokenAcceso.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerTokenAcceso", New Object() {Peticion})
            Return CType(results(0), ObtenerTokenAcceso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/BorrarTokenAcceso", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function BorrarTokenAcceso(Peticion As BorrarTokenAcceso.Peticion) As BorrarTokenAcceso.Respuesta
            Dim results() As Object = Me.Invoke("BorrarTokenAcceso", New Object() {Peticion})
            Return CType(results(0), BorrarTokenAcceso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ValidarPermisosUsuario", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ValidarPermisosUsuario(Peticion As ValidarPermisosUsuario.Peticion) As ValidarPermisosUsuario.Respuesta
            Dim results() As Object = Me.Invoke("ValidarPermisosUsuario", New Object() {Peticion})
            Return CType(results(0), ValidarPermisosUsuario.Respuesta)
        End Function

    End Class

End Namespace
