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
     System.Diagnostics.DebuggerStepThroughAttribute(), _
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
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/Login", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Login(Peticion As Login.Peticion) As Login.Respuesta
            Dim results() As Object = Me.Invoke("Login", New Object() {Peticion})
            Return CType(results(0), Login.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/LoginDelegacion", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LoginDelegacion(Peticion As LoginDelegacion.Peticion) As LoginDelegacion.Respuesta
            Dim results() As Object = Me.Invoke("LoginDelegacion", New Object() {Peticion})
            Return CType(results(0), LoginDelegacion.Respuesta)
        End Function

        '''<remarks/>
        Public Function BeginLogin(Peticion As Login.Peticion, callback As System.AsyncCallback, asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Login", New Object() {Peticion}, callback, asyncState)
        End Function

        '''<remarks/>
        Public Function EndLogin(asyncResult As System.IAsyncResult) As Login.Respuesta
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0), Login.Respuesta)
        End Function

        '''<remarks/>
        Public Overloads Sub LoginAsync(Peticion As Login.Peticion)
            Me.LoginAsync(Peticion, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub LoginAsync(Peticion As Login.Peticion, userState As Object)
            If (Me.LoginOperationCompleted Is Nothing) Then
                Me.LoginOperationCompleted = AddressOf Me.OnLoginOperationCompleted
            End If
            Me.InvokeAsync("Login", New Object() {Peticion}, Me.LoginOperationCompleted, userState)
        End Sub

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

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad/GetUsuariosDetail", RequestNamespace:="http://Prosegur.Global.Seguridad", ResponseNamespace:="http://Prosegur.Global.Seguridad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetUsuariosDetail(Peticion As GetUsuariosDetail.Peticion) As GetUsuariosDetail.Respuesta
            Dim results() As Object = Me.Invoke("GetUsuariosDetail", New Object() {Peticion})
            Return CType(results(0), GetUsuariosDetail.Respuesta)
        End Function

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
        Public ReadOnly Property Result() As Login.Respuesta
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), Login.Respuesta)
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