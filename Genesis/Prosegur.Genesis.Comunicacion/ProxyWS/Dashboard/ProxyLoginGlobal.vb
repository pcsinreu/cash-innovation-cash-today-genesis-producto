﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.1433
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Prosegur.Global.Seguridad.ContractoServicio
Imports Microsoft.Web.Services3.Security.Tokens
Imports Prosegur.Genesis.ContractoServicio.Interfaces.Dashboard

Namespace ProxyWS.Dashboard

    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="SeguridadSoap", [Namespace]:="http://Prosegur.Genesis.Dashboard.Servicio")> _
    Partial Public Class ProxyLoginGlobal
        Inherits Microsoft.Web.Services3.WebServicesClientProtocol
        Implements ILoginGlobal

        Public Sub New(ByVal urlLoginGlobal As String, ByVal usuarioWSLogin As String, ByVal passwordWSLogin As String)
            MyBase.New()

            Me.Url = urlLoginGlobal

            Dim token As New UsernameToken(usuarioWSLogin, passwordWSLogin, PasswordOption.SendPlainText)
            Me.SetClientCredential(token)
            Me.SetPolicy("ClientPolicy")
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/Login", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Login(ByVal Peticion As Login.Peticion) As Login.Respuesta Implements ILoginGlobal.Login
            Dim results() As Object = Me.Invoke("Login", New Object() {Peticion})
            Return CType(results(0), Login.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerDelegaciones", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Function ObtenerDelegaciones(ByVal Peticion As ObtenerDelegaciones.Peticion) As ObtenerDelegaciones.Respuesta Implements ILoginGlobal.ObtenerDelegaciones
            Dim results() As Object = Me.Invoke("ObtenerDelegaciones", New Object() {Peticion})
            Return CType(results(0), ObtenerDelegaciones.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerPermisosUsuario", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Function ObtenerPermisosUsuario(ByVal Peticion As ObtenerPermisosUsuario.Peticion) As ObtenerPermisosUsuario.Respuesta Implements ILoginGlobal.ObtenerPermisosUsuario
            Dim results() As Object = Me.Invoke("ObtenerPermisosUsuario", New Object() {Peticion})
            Return CType(results(0), ObtenerPermisosUsuario.Respuesta)
        End Function

    End Class
End Namespace