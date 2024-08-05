Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Configuration

Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio.Ruta
Imports Prosegur.Genesis.ContractoServicio.Documento
Imports Prosegur.Genesis.ContractoServicio.Interfaces

Namespace ProxyWS.RecepcionyEnvio

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="ComonSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/")> _
    Public Class ProxyRecepcionyEnvio
        Inherits ProxyWS.ServicioBase
        Implements IRecepcionyEnvio

        Public Sub New()
            MyBase.New()
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/AlocarDesalocarDocumento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AlocarDesalocarDocumento(peticion As AlocarDesalocarDocumento.Peticion) As AlocarDesalocarDocumento.Respuesta Implements IRecepcionyEnvio.AlocarDesalocarDocumento
            Dim results() As Object = Me.Invoke("AlocarDesalocarDocumento", New Object() {peticion})
            Return CType(results(0), AlocarDesalocarDocumento.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/AlocarPedidosExternos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AlocarPedidosExternos(peticion As AlocarPedidosExternos.Peticion) As AlocarPedidosExternos.Respuesta Implements IRecepcionyEnvio.AlocarPedidosExternos
            Dim results() As Object = Me.Invoke("AlocarPedidosExternos", New Object() {peticion})
            Return CType(results(0), AlocarPedidosExternos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/GrabarDocumentosSalidas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarDocumentosSalidas(peticion As GrabarDocumentosSalidas.Peticion) As GrabarDocumentosSalidas.Respuesta Implements IRecepcionyEnvio.GrabarDocumentosSalidas
            Dim results() As Object = Me.Invoke("GrabarDocumentosSalidas", New Object() {peticion})
            Return CType(results(0), GrabarDocumentosSalidas.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/GrabarDocumentosSalidasConIntegracionSol", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarDocumentosSalidasConIntegracionSol(peticion As GrabarDocumentosSalidasConIntegracionSol.Peticion) As GrabarDocumentosSalidasConIntegracionSol.Respuesta Implements IRecepcionyEnvio.GrabarDocumentosSalidasConIntegracionSol
            Dim results() As Object = Me.Invoke("GrabarDocumentosSalidasConIntegracionSol", New Object() {peticion})
            Return CType(results(0), GrabarDocumentosSalidasConIntegracionSol.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/GrabarIngresoDocumentosSaldosySol", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarIngresoDocumentosSaldosySol(peticion As GrabarIngresoDocumentosSaldosySol.Peticion) As GrabarIngresoDocumentosSaldosySol.Respuesta Implements IRecepcionyEnvio.GrabarIngresoDocumentosSaldosySol
            Dim results() As Object = Me.Invoke("GrabarIngresoDocumentosSaldosySol", New Object() {peticion})
            Return CType(results(0), GrabarIngresoDocumentosSaldosySol.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/GrabaryReenviarGrupoDocumentos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabaryReenviarGrupoDocumentos(peticion As GrabaryReenviarGrupoDocumentos.Peticion) As GrabaryReenviarGrupoDocumentos.Respuesta Implements IRecepcionyEnvio.GrabaryReenviarGrupoDocumentos
            Dim results() As Object = Me.Invoke("GrabaryReenviarGrupoDocumentos", New Object() {peticion})
            Return CType(results(0), GrabaryReenviarGrupoDocumentos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/ObtenerDocumentosNoAlocados", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerDocumentosNoAlocados(peticion As obtenerDocumentosNoAlocados.Peticion) As obtenerDocumentosNoAlocados.Respuesta Implements IRecepcionyEnvio.ObtenerDocumentosNoAlocados
            Dim results() As Object = Me.Invoke("ObtenerDocumentosNoAlocados", New Object() {peticion})
            Return CType(results(0), obtenerDocumentosNoAlocados.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/ObtenerRutas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerRutas(peticion As ObtenerRutas.Peticion) As ObtenerRutas.Respuesta Implements IRecepcionyEnvio.ObtenerRutas
            Dim results() As Object = Me.Invoke("ObtenerRutas", New Object() {peticion})
            Return CType(results(0), ObtenerRutas.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/CambiarEstadoDocumentoContenedor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CambiarEstadoDocumentoContenedor(peticion As CambiarEstadoDocumentoContenedor.Peticion) As CambiarEstadoDocumentoContenedor.Respuesta Implements IRecepcionyEnvio.CambiarEstadoDocumentoContenedor
            Dim results() As Object = Me.Invoke("CambiarEstadoDocumentoContenedor", New Object() {peticion})
            Return CType(results(0), CambiarEstadoDocumentoContenedor.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.RecepcionyEnvio/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements IRecepcionyEnvio.Test
            Dim results() As Object = Me.Invoke("Test", New Object())
            Return CType(results(0), ContractoServicio.Test.Respuesta)
        End Function
    End Class

End Namespace