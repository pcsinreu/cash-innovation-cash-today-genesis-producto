Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Prosegur.Global.GesEfectivo.Conteo
Imports System.Configuration

Namespace ProxyIngresoContadoLegado

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="LegadoSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.Conteo.Legado")> _
    Public Class Proxy
        Inherits ProxyWS.ServicioBase
        Private useDefaultCredentialsSetExplicitly As Boolean
        '''<remarks/>
        Public Sub New()
            MyBase.New()

            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("WS_TIMEOUT")) AndAlso IsNumeric(ConfigurationManager.AppSettings("WS_TIMEOUT")) Then
                MyBase.Timeout = Integer.Parse(ConfigurationManager.AppSettings("WS_TIMEOUT")) * 1000
            End If

            Me.useDefaultCredentialsSetExplicitly = True
        End Sub

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo.Legado/TransferirDatosConteo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo.Legado", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo.Legado", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function TransferirDatosConteo(ByVal Peticion As Legado.ContractoServicio.TransferirDatos.Peticion) As Legado.ContractoServicio.TransferirDatos.Respuesta
            Dim results() As Object = Me.Invoke("TransferirDatosConteo", New Object() {Peticion})
            Return CType(results(0), Legado.ContractoServicio.TransferirDatos.Respuesta)
        End Function

    End Class
End Namespace