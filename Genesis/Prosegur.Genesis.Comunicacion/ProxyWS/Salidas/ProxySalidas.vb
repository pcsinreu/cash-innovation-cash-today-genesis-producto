
Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports ContractoSalidas = Prosegur.Global.GesEfectivo.Salidas.ContractoServicio
Imports Microsoft.Web.Services3.Security.Tokens

Namespace ProxyWS.Salidas

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="SalidasSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.Salidas")> _
    Public Class ProxySalidas
        Inherits ProxyWS.ServicioBase
        Implements ContractoSalidas.ISalidas

        Private useDefaultCredentialsSetExplicitly As Boolean

        '''<remarks/>
        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "Salidas/Salidas.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

            Me.useDefaultCredentialsSetExplicitly = True
        End Sub

        ''' <summary>
        ''' Loga erro da aplicação
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Salidas/LogarErro",
            RequestNamespace:="http://Prosegur.Global.GesEfectivo.Salidas",
            ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Salidas",
            Use:=System.Web.Services.Description.SoapBindingUse.Literal,
            ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LogarErro(ByVal Peticion As ContractoSalidas.LogarErro.Peticion) As ContractoSalidas.LogarErro.Respuesta Implements ContractoSalidas.ISalidas.LogarErro
            Dim results() As Object = Me.Invoke("LogarErro", New Object() {Peticion})
            Return CType(results(0), ContractoSalidas.LogarErro.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Salidas/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Salidas", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Salidas", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As ContractoSalidas.Test.Respuesta Implements ContractoSalidas.ISalidas.Test
            Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
            Return CType(results(0), ContractoSalidas.Test.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Salidas/ConsultarTiposDeBultos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Salidas", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Salidas", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ConsultarTiposDeBultos(Peticion As [Global].GesEfectivo.Salidas.ContractoServicio.TipoBulto.ConsultarTiposBultos.Peticion) As [Global].GesEfectivo.Salidas.ContractoServicio.TipoBulto.ConsultarTiposBultos.Respuesta Implements [Global].GesEfectivo.Salidas.ContractoServicio.ISalidas.ConsultarTiposDeBultos
            Dim results() As Object = Me.Invoke("ConsultarTiposDeBultos", New Object() {Peticion})
            Return CType(results(0), ContractoSalidas.TipoBulto.ConsultarTiposBultos.Respuesta)
        End Function
    End Class

End Namespace