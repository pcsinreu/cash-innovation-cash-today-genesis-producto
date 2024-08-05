Imports Prosegur.Genesis.ContractoServicio.RecepcionyEnvio.ProgramacionServicioService
Imports System.Configuration

Namespace ProxyWS.Sol

    <System.Web.Services.WebServiceBindingAttribute(Name:="DocumentoPortBinding", [Namespace]:="com.prosegur.sol.base.service"), _
     System.Xml.Serialization.XmlIncludeAttribute(GetType(basePeticion))> _
    Partial Public Class ProxyProgramacionServicioService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol

        Private buscarProgramacionServiciosOperationCompleted As System.Threading.SendOrPostCallback

        Public Sub New()
            MyBase.New()
            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("WS_TIMEOUT")) AndAlso IsNumeric(ConfigurationManager.AppSettings("WS_TIMEOUT")) Then
                MyBase.Timeout = Integer.Parse(ConfigurationManager.AppSettings("WS_TIMEOUT")) * 1000
            End If
        End Sub

        Public Event buscarProgramacionServiciosCompleted As buscarProgramacionServiciosCompletedEventHandler

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace:="http://service.base.sol.prosegur.com/", ResponseNamespace:="http://service.base.sol.prosegur.com/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function buscarProgramacionServicios(<System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> arg0 As peticionBuscarProgramacionServicios) As <System.Xml.Serialization.XmlElementAttribute("return", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> RespuestaBuscarProgramacionServicios
            Dim results() As Object = Me.Invoke("buscarProgramacionServicios", New Object() {arg0})
            Return CType(results(0), RespuestaBuscarProgramacionServicios)
        End Function

        Public Function BeginbuscarProgramacionServicios(arg0 As peticionBuscarProgramacionServicios, callback As System.AsyncCallback, asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("buscarProgramacionServicios", New Object() {arg0}, callback, asyncState)
        End Function

        Public Function EndbuscarProgramacionServicios(asyncResult As System.IAsyncResult) As RespuestaBuscarProgramacionServicios
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0), RespuestaBuscarProgramacionServicios)
        End Function

        Public Overloads Sub buscarProgramacionServiciosAsync(arg0 As peticionBuscarProgramacionServicios)
            Me.buscarProgramacionServiciosAsync(arg0, Nothing)
        End Sub

        Public Overloads Sub buscarProgramacionServiciosAsync(arg0 As peticionBuscarProgramacionServicios, userState As Object)
            If (Me.buscarProgramacionServiciosOperationCompleted Is Nothing) Then
                Me.buscarProgramacionServiciosOperationCompleted = AddressOf Me.OnbuscarProgramacionServiciosOperationCompleted
            End If
            Me.InvokeAsync("buscarProgramacionServicios", New Object() {arg0}, Me.buscarProgramacionServiciosOperationCompleted, userState)
        End Sub

        Private Sub OnbuscarProgramacionServiciosOperationCompleted(arg As Object)
            If (Not (Me.buscarProgramacionServiciosCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg, System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent buscarProgramacionServiciosCompleted(Me, New buscarProgramacionServiciosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub

        Public Delegate Sub buscarProgramacionServiciosCompletedEventHandler(sender As Object, e As buscarProgramacionServiciosCompletedEventArgs)

        Public Class buscarProgramacionServiciosCompletedEventArgs
            Inherits System.ComponentModel.AsyncCompletedEventArgs

            Private results() As Object

            Friend Sub New(results() As Object, exception As System.Exception, cancelled As Boolean, userState As Object)
                MyBase.New(exception, cancelled, userState)
                Me.results = results
            End Sub

            Public ReadOnly Property Result() As RespuestaBuscarProgramacionServicios
                Get
                    Me.RaiseExceptionIfNecessary()
                    Return CType(Me.results(0), RespuestaBuscarProgramacionServicios)
                End Get
            End Property

        End Class

    End Class

End Namespace