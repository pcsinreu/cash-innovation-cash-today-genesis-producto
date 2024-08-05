Imports Prosegur.Genesis.ContractoServicio.RecepcionyEnvio.DocumentoService
Imports System.Configuration

Namespace ProxyWS.Sol

    <System.Web.Services.WebServiceBindingAttribute(Name:="DocumentoPortBinding", [Namespace]:="com.prosegur.sol.base.service"), _
     System.Xml.Serialization.XmlIncludeAttribute(GetType(basePeticion))> _
        Partial Public Class ProxyDocumentoService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol

        Private grabarDocumentoOperationCompleted As System.Threading.SendOrPostCallback


        Public Sub New()
            MyBase.New()
            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("WS_TIMEOUT")) AndAlso IsNumeric(ConfigurationManager.AppSettings("WS_TIMEOUT")) Then
                MyBase.Timeout = Integer.Parse(ConfigurationManager.AppSettings("WS_TIMEOUT")) * 1000
            End If
        End Sub


        Public Event grabarDocumentoCompleted As grabarDocumentoCompletedEventHandler


        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace:="http://service.base.sol.prosegur.com/", ResponseNamespace:="http://service.base.sol.prosegur.com/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function grabarDocumento(<System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> arg0 As peticionDocumento) As <System.Xml.Serialization.XmlElementAttribute("return", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> respuesta
            Dim results() As Object = Me.Invoke("grabarDocumento", New Object() {arg0})
            Return CType(results(0), respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace:="http://service.base.sol.prosegur.com/", ResponseNamespace:="http://service.base.sol.prosegur.com/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function borrarDocumento(<System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> arg0 As peticionBorrarDocumento) As <System.Xml.Serialization.XmlElementAttribute("return", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> respuesta
            Dim results() As Object = Me.Invoke("borrarDocumento", New Object() {arg0})
            Return CType(results(0), respuesta)
        End Function

        Public Function BegingrabarDocumento(arg0 As peticionDocumento, callback As System.AsyncCallback, asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("grabarDocumento", New Object() {arg0}, callback, asyncState)
        End Function


        Public Function EndgrabarDocumento(asyncResult As System.IAsyncResult) As respuesta
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0), respuesta)
        End Function


        Public Overloads Sub grabarDocumentoAsync(arg0 As peticionDocumento)
            Me.grabarDocumentoAsync(arg0, Nothing)
        End Sub


        Public Overloads Sub grabarDocumentoAsync(arg0 As peticionDocumento, userState As Object)
            If (Me.grabarDocumentoOperationCompleted Is Nothing) Then
                Me.grabarDocumentoOperationCompleted = AddressOf Me.OngrabarDocumentoOperationCompleted
            End If
            Me.InvokeAsync("grabarDocumento", New Object() {arg0}, Me.grabarDocumentoOperationCompleted, userState)
        End Sub

        Private Sub OngrabarDocumentoOperationCompleted(arg As Object)
            If (Not (Me.grabarDocumentoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent grabarDocumentoCompleted(Me, New grabarDocumentoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub


        Public Shadows Sub CancelAsync(userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
    End Class


    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")> _
    Public Delegate Sub grabarDocumentoCompletedEventHandler(sender As Object, e As grabarDocumentoCompletedEventArgs)

    Public Class grabarDocumentoCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(results() As Object, exception As System.Exception, cancelled As Boolean, userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub


        Public ReadOnly Property Result() As respuesta
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), respuesta)
            End Get
        End Property

    End Class

End Namespace