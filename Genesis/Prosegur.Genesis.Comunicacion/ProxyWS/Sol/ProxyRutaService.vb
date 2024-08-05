Imports Prosegur.Genesis.ContractoServicio.RecepcionyEnvio.RutaService
Imports System.Configuration

Namespace ProxyWS.Sol

    '''<remarks/>
    <System.Web.Services.WebServiceBindingAttribute(Name:="RutaPortBinding", [Namespace]:="com.prosegur.sol.base.service"), _
     System.Xml.Serialization.XmlIncludeAttribute(GetType(basePeticion))> _
    Partial Public Class ProxyRutaService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol

        Private liberarBovedaOperationCompleted As System.Threading.SendOrPostCallback

        '''<remarks/>
        Public Sub New()
            MyBase.New()
            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("WS_TIMEOUT")) AndAlso IsNumeric(ConfigurationManager.AppSettings("WS_TIMEOUT")) Then
                MyBase.Timeout = Integer.Parse(ConfigurationManager.AppSettings("WS_TIMEOUT")) * 1000
            End If
        End Sub

        '''<remarks/>
        Public Event liberarBovedaCompleted As liberarBovedaCompletedEventHandler

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace:="http://service.base.sol.prosegur.com/", ResponseNamespace:="http://service.base.sol.prosegur.com/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function liberarBoveda(<System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> arg0 As peticionLiberarBoveda) As <System.Xml.Serialization.XmlElementAttribute("return", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> respuesta
            Dim results() As Object = Me.Invoke("liberarBoveda", New Object() {arg0})
            Return CType(results(0), respuesta)
        End Function

        '''<remarks/>
        Public Function BeginliberarBoveda(arg0 As peticionLiberarBoveda, callback As System.AsyncCallback, asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("liberarBoveda", New Object() {arg0}, callback, asyncState)
        End Function

        '''<remarks/>
        Public Function EndliberarBoveda(asyncResult As System.IAsyncResult) As respuesta
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0), respuesta)
        End Function

        '''<remarks/>
        Public Overloads Sub liberarBovedaAsync(arg0 As peticionLiberarBoveda)
            Me.liberarBovedaAsync(arg0, Nothing)
        End Sub

        '''<remarks/>
        Public Overloads Sub liberarBovedaAsync(arg0 As peticionLiberarBoveda, userState As Object)
            If (Me.liberarBovedaOperationCompleted Is Nothing) Then
                Me.liberarBovedaOperationCompleted = AddressOf Me.OnliberarBovedaOperationCompleted
            End If
            Me.InvokeAsync("liberarBoveda", New Object() {arg0}, Me.liberarBovedaOperationCompleted, userState)
        End Sub

        Private Sub OnliberarBovedaOperationCompleted(arg As Object)
            If (Not (Me.liberarBovedaCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent liberarBovedaCompleted(Me, New liberarBovedaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        '''<remarks/>
        Public Shadows Sub CancelAsync(userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")> _
    Public Delegate Sub liberarBovedaCompletedEventHandler(sender As Object, e As LiberarBovedaCompletedEventArgs)

    Public Class liberarBovedaCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs

        Private results() As Object

        Friend Sub New(results() As Object, exception As System.Exception, cancelled As Boolean, userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub

        '''<remarks/>
        Public ReadOnly Property Result() As respuesta
            Get
                Me.RaiseExceptionIfNecessary()
                Return CType(Me.results(0), respuesta)
            End Get
        End Property

    End Class

End Namespace