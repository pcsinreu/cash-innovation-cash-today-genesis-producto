Imports Prosegur.Genesis.ContractoServicio.RecepcionyEnvio.OtService
Imports System.Configuration

Namespace ProxyWS.Sol

    <System.Web.Services.WebServiceBindingAttribute(Name:="OtPortBinding", [Namespace]:="com.prosegur.sol.base.service"), _
     System.Xml.Serialization.XmlIncludeAttribute(GetType(basePeticion))> _
    Partial Public Class ProxyOtService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol

        Private buscarOT2OperationCompleted As System.Threading.SendOrPostCallback

        Public Sub New()
            MyBase.New()
            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("WS_TIMEOUT")) AndAlso IsNumeric(ConfigurationManager.AppSettings("WS_TIMEOUT")) Then
                MyBase.Timeout = Integer.Parse(ConfigurationManager.AppSettings("WS_TIMEOUT")) * 1000
            End If
        End Sub

        Public Event buscarOT2Completed As buscarOT2CompletedEventHandler

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace:="http://service.base.sol.prosegur.com/", ResponseNamespace:="http://service.base.sol.prosegur.com/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function buscarOT2(<System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> arg0 As peticionBuscarOT) As <System.Xml.Serialization.XmlElementAttribute("return", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> respuestaBuscarOT
            Dim results() As Object = Me.Invoke("buscarOT2", New Object() {arg0})
            Return CType(results(0), respuestaBuscarOT)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://com.prosegur.sol.base.service/ValidaRemessaEnviada", RequestNamespace:="http://com.prosegur.sol.base.service/", ResponseNamespace:="http://com.prosegur.sol.base.service/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ValidaRemessaEnviada(arg0 As peticionValidaRemesa) As peticionValidaRemesa
            Dim results() As Object = Me.Invoke("ValidaRemessaEnviada", New Object() {arg0})
            Return CType(results(0), peticionValidaRemesa)
        End Function

        Public Overloads Sub buscarOT2Async(arg0 As peticionBuscarOT)
            Me.buscarOT2Async(arg0, Nothing)
        End Sub

        Public Overloads Sub buscarOT2Async(arg0 As peticionBuscarOT, userState As Object)
            If (Me.buscarOT2OperationCompleted Is Nothing) Then
                Me.buscarOT2OperationCompleted = AddressOf Me.OnbuscarOT2OperationCompleted
            End If
            Me.InvokeAsync("buscarOT2", New Object() {arg0}, Me.buscarOT2OperationCompleted, userState)
        End Sub

        Private Sub OnbuscarOT2OperationCompleted(arg As Object)
            If (Not (Me.buscarOT2CompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent buscarOT2Completed(Me, New buscarOT2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        Public Delegate Sub buscarOT2CompletedEventHandler(sender As Object, e As buscarOT2CompletedEventArgs)

        Public Class buscarOT2CompletedEventArgs
            Inherits System.ComponentModel.AsyncCompletedEventArgs

            Private results() As Object

            Friend Sub New(results() As Object, exception As System.Exception, cancelled As Boolean, userState As Object)
                MyBase.New(exception, cancelled, userState)
                Me.results = results
            End Sub

            Public ReadOnly Property Result() As respuestaBuscarOT
                Get
                    Me.RaiseExceptionIfNecessary()
                    Return CType(Me.results(0), respuestaBuscarOT)
                End Get
            End Property

        End Class

    End Class

End Namespace