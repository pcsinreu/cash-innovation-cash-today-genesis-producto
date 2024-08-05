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

Namespace LoginGlobal

    '
    'This source code was auto-generated by wsdl, Version=2.0.50727.42.
    '

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="SeguridadSoap", [Namespace]:="http://Prosegur.Global.Seguridad")> _
    Partial Public Class Seguridad
        Inherits Microsoft.Web.Services3.WebServicesClientProtocol

        Private LoginOperationCompleted As System.Threading.SendOrPostCallback

        Private ObtenerUsuariosADOperationCompleted As System.Threading.SendOrPostCallback

        '''<remarks/>
        Public Sub New()

            MyBase.New()

            Me.Url = Parametros.Configuracion.UrlLoginGlobal

            Dim token As New UsernameToken(Parametros.Configuracion.UsuarioWSLogin, Parametros.Configuracion.PasswordWSLogin, PasswordOption.SendPlainText)
            Me.SetClientCredential(token)
            Me.SetPolicy("ClientPolicy")

        End Sub

        '''<remarks/>
        Public Event LoginCompleted As LoginCompletedEventHandler

        '''<remarks/>
        Public Event ObtenerUsuariosADCompleted As ObtenerUsuariosADCompletedEventHandler

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/LoginDelegacion", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LoginDelegacion(Peticion As LoginDelegacion.Peticion) As LoginDelegacion.Respuesta
            Dim results() As Object = Me.Invoke("LoginDelegacion", New Object() {Peticion})
            Return CType(results(0), LoginDelegacion.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/LoginAplicacionVersion", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LoginAplicacionVersion(Peticion As LoginAplicacionVersion.Peticion) As LoginAplicacionVersion.Respuesta
            Dim results() As Object = Me.Invoke("LoginAplicacionVersion", New Object() {Peticion})
            Return CType(results(0), LoginAplicacionVersion.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerDelegaciones", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Function ObtenerDelegaciones(Peticion As ObtenerDelegaciones.Peticion) As ObtenerDelegaciones.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerDelegaciones", New Object() {Peticion})
            Return CType(results(0), ObtenerDelegaciones.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/CrearTokenAcceso", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Function CrearTokenAcceso(Peticion As CrearTokenAcceso.Peticion) As CrearTokenAcceso.Respuesta
            Dim results() As Object = Me.Invoke("CrearTokenAcceso", New Object() {Peticion})
            Return CType(results(0), CrearTokenAcceso.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerTokenAcceso", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Function ObtenerTokenAcceso(Peticion As ObtenerTokenAcceso.Peticion) As ObtenerTokenAcceso.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerTokenAcceso", New Object() {Peticion})
            Return CType(results(0), ObtenerTokenAcceso.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/BorrarTokenAcceso", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Function BorrarTokenAcceso(Peticion As BorrarTokenAcceso.Peticion) As BorrarTokenAcceso.Respuesta
            Dim results() As Object = Me.Invoke("BorrarTokenAcceso", New Object() {Peticion})
            Return CType(results(0), BorrarTokenAcceso.Respuesta)
        End Function

        '''<remarks/>
        Public Function BeginLogin(Peticion As LoginAplicacionVersion.Peticion, callback As System.AsyncCallback, asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("LoginAplicacionVersion", New Object() {Peticion}, callback, asyncState)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/Login", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Login(Peticion As Login.Peticion) As Login.Respuesta
            Dim results() As Object = Me.Invoke("Login", New Object() {Peticion})
            Return CType(results(0), Login.Respuesta)
        End Function

        '''<remarks/>
        Public Function EndLogin(asyncResult As System.IAsyncResult) As LoginAplicacionVersion.Respuesta
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0), LoginAplicacionVersion.Respuesta)
        End Function

        '''<remarks/>
        Public Overloads Sub LoginAsync(Peticion As LoginAplicacionVersion.Peticion)
            Me.LoginAsync(Peticion, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub LoginAsync(Peticion As LoginAplicacionVersion.Peticion, userState As Object)
            If (Me.LoginOperationCompleted Is Nothing) Then
                Me.LoginOperationCompleted = AddressOf Me.OnLoginOperationCompleted
            End If
            Me.InvokeAsync("LoginAplicacionVersion", New Object() {Peticion}, Me.LoginOperationCompleted, userState)
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/CambiarContrasena", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CambiarContrasena(Peticion As CambiarContrasena.Peticion) As CambiarContrasena.Respuesta
            Dim results() As Object = Me.Invoke("CambiarContrasena", New Object() {Peticion})
            Return CType(results(0), CambiarContrasena.Respuesta)
        End Function

        Private Sub OnLoginOperationCompleted(arg As Object)
            If (Not (Me.LoginCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent LoginCompleted(Me, New LoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerUsuariosAD", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerUsuariosAD(Peticion As ObtenerUsuariosAD.Peticion) As ObtenerUsuariosAD.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerUsuariosAD", New Object() {Peticion})
            Return CType(results(0), ObtenerUsuariosAD.Respuesta)
        End Function

        '''<remarks/>
        Public Function BeginObtenerUsuariosAD(Peticion As ObtenerUsuariosAD.Peticion, callback As System.AsyncCallback, asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("ObtenerUsuariosAD", New Object() {Peticion}, callback, asyncState)
        End Function

        '''<remarks/>
        Public Function EndObtenerUsuariosAD(asyncResult As System.IAsyncResult) As ObtenerUsuariosAD.Respuesta
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0), ObtenerUsuariosAD.Respuesta)
        End Function

        '''<remarks/>
        Public Overloads Sub ObtenerUsuariosADAsync(Peticion As ObtenerUsuariosAD.Peticion)
            Me.ObtenerUsuariosADAsync(Peticion, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub ObtenerUsuariosADAsync(Peticion As ObtenerUsuariosAD.Peticion, userState As Object)
            If (Me.ObtenerUsuariosADOperationCompleted Is Nothing) Then
                Me.ObtenerUsuariosADOperationCompleted = AddressOf Me.OnObtenerUsuariosADOperationCompleted
            End If
            Me.InvokeAsync("ObtenerUsuariosAD", New Object() {Peticion}, Me.ObtenerUsuariosADOperationCompleted, userState)
        End Sub

        Private Sub OnObtenerUsuariosADOperationCompleted(arg As Object)
            If (Not (Me.ObtenerUsuariosADCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObtenerUsuariosADCompleted(Me, New ObtenerUsuariosADCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        Public Shadows Sub CancelAsync(userState As Object)
            MyBase.CancelAsync(userState)
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerAplicacionVersion", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerAplicacionVersion(Peticion As ObtenerAplicacionVersion.Peticion) As ObtenerAplicacionVersion.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerAplicacionVersion", New Object() {Peticion})
            Return CType(results(0), ObtenerAplicacionVersion.Respuesta)
        End Function


        '''<remarks/>
        Public Function BeginObtenerAplicacionVersion(Peticion As ObtenerAplicacionVersion.Peticion, callback As System.AsyncCallback, asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("ObtenerAplicacionVersion", New Object() {Peticion}, callback, asyncState)
        End Function

        '''<remarks/>
        Public Function EndObtenerAplicacionVersion(asyncResult As System.IAsyncResult) As ObtenerAplicacionVersion.Respuesta
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0), ObtenerAplicacionVersion.Respuesta)
        End Function



        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerVersiones", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerVersiones(Peticion As ObtenerVersiones.Peticion) As ObtenerVersiones.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerVersiones", New Object() {Peticion})
            Return CType(results(0), ObtenerVersiones.Respuesta)
        End Function


        '''<remarks/>
        Public Function BeginObtenerVersiones(Peticion As ObtenerVersiones.Peticion, callback As System.AsyncCallback, asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("ObtenerVersiones", New Object() {Peticion}, callback, asyncState)
        End Function

        '''<remarks/>
        Public Function EndObtenerVersiones(asyncResult As System.IAsyncResult) As ObtenerVersiones.Respuesta
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0), ObtenerVersiones.Respuesta)
        End Function


        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerPermisosUsuario", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerPermisosUsuario(Peticion As ObtenerPermisosUsuario.Peticion) As ObtenerPermisosUsuario.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerPermisosUsuario", New Object() {Peticion})
            Return CType(results(0), ObtenerPermisosUsuario.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ObtenerDelegacionesDelUsuario", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerDelegacionesDelUsuario(Peticion As ObtenerDelegacionesDelUsuario.Peticion) As ObtenerDelegacionesDelUsuario.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerDelegacionesDelUsuario", New Object() {Peticion})
            Return CType(results(0), ObtenerDelegacionesDelUsuario.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/ValidarPermisosUsuario", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ValidarPermisosUsuario(Peticion As ValidarPermisosUsuario.Peticion) As ValidarPermisosUsuario.Respuesta
            Dim results() As Object = Me.Invoke("ValidarPermisosUsuario", New Object() {Peticion})
            Return CType(results(0), ValidarPermisosUsuario.Respuesta)
        End Function

    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")> _
    Public Delegate Sub LoginCompletedEventHandler(sender As Object, e As LoginCompletedEventArgs)

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code")> _
    Partial Public Class LoginCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(results() As Object, exception As System.Exception, cancelled As Boolean, userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As LoginAplicacionVersion.Respuesta
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), LoginAplicacionVersion.Respuesta)
            End Get
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")> _
    Public Delegate Sub ObtenerUsuariosADCompletedEventHandler(sender As Object, e As ObtenerUsuariosADCompletedEventArgs)

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code")> _
    Partial Public Class ObtenerUsuariosADCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(results() As Object, exception As System.Exception, cancelled As Boolean, userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As ObtenerUsuariosAD.Respuesta
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), ObtenerUsuariosAD.Respuesta)
            End Get
        End Property
    End Class
End Namespace