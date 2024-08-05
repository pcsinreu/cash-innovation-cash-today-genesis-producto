Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.SOL
Imports System.Configuration

Namespace ProxyWS.Sol

    <System.Web.Services.WebServiceBindingAttribute(Name:="DocumentoPortBinding", [Namespace]:="com.prosegur.sol.base.service")> _
    Public Class ProxyModuloService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol

        Public Sub New()
            MyBase.New()
            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("WS_TIMEOUT")) AndAlso IsNumeric(ConfigurationManager.AppSettings("WS_TIMEOUT")) Then
                MyBase.Timeout = Integer.Parse(ConfigurationManager.AppSettings("WS_TIMEOUT")) * 1000
            End If
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace:="http://service.base.sol.prosegur.com/", ResponseNamespace:="http://service.base.sol.prosegur.com/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function grabarModulo(<System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> arg0 As moduloService.peticionGrabarModulo) As <System.Xml.Serialization.XmlElementAttribute("return", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> respuesta
            Dim results() As Object = Me.Invoke("grabarModulo", New Object() {arg0})
            Return CType(results(0), respuesta)
        End Function

    End Class

End Namespace