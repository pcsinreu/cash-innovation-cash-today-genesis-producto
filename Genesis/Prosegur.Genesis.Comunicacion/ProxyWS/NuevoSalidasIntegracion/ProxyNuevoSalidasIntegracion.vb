
Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Microsoft.Web.Services3.Security.Tokens
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas

Namespace ProxyWS.NuevoSalidasIntegracion

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="NuevoSalidasIntegracionSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.NuevoSalidasIntegracion")> _
    Public Class ProxyNuevoSalidasIntegracion
        Inherits ProxyWS.ServicioBase
        Implements INuevoSalidasIntegracion

        '''<remarks/>
        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "NuevoSalidasIntegracion/Integracion.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidasIntegracion/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidasIntegracion/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidasIntegracion/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements INuevoSalidasIntegracion.Test
            Dim results() As Object = Me.Invoke("Test", New Object())
            Return CType(results(0), ContractoServicio.Test.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.NuevoSalidasIntegracion/RecuperarRemesasPorIdentificadorCodigoExternos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidasIntegracion/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidasIntegracion/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarRemesasPorIdentificadorCodigoExternos(Peticion As ContractoServicio.Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Peticion) As ContractoServicio.Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Respuesta Implements INuevoSalidasIntegracion.RecuperarRemesasPorIdentificadorCodigoExternos
            Dim results() As Object = Me.Invoke("RecuperarRemesasPorIdentificadorCodigoExternos", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), ContractoServicio.Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Respuesta))
        End Function

    End Class

End Namespace