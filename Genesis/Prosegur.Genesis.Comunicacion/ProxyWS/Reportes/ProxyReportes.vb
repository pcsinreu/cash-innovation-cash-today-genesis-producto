Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Configuration
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Interfaces

Namespace ProxyWS.Reportes

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="ComonSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.Reportes/")> _
    Public Class ProxyReportes
        Inherits ProxyWS.ServicioBase
        Implements IReportes

        Private useDefaultCredentialsSetExplicitly As Boolean

        Public Sub New()
            MyBase.New()
            Me.useDefaultCredentialsSetExplicitly = True
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As Test.Respuesta Implements IReportes.Test
            Dim results() As Object = Me.Invoke("Test", New Object())
            Return CType(results(0), Test.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/GrabarRecepcionRuta", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarRecepcionRuta(Peticion As Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaPeticion) As Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaRespuesta Implements IReportes.GrabarRecepcionRuta
            Dim results() As Object = Me.Invoke("GrabarRecepcionRuta", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaRespuesta))
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Reportes/GrabarTraspaseResponsabilidad", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Reportes/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Reportes/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarTraspaseResponsabilidad(Peticion As Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadPeticion) As Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadRespuesta Implements IReportes.GrabarTraspaseResponsabilidad
            Dim results() As Object = Me.Invoke("GrabarTraspaseResponsabilidad", New Object() {Peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadRespuesta))
        End Function
    End Class

End Namespace