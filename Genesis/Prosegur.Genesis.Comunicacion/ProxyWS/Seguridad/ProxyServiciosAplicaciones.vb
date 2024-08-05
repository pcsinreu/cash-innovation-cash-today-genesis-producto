Option Strict Off
Option Explicit On
Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Prosegur.Global.GesEfectivo.IAC
Imports System.Configuration
Imports System.Net
Imports Microsoft.Web.Services3.Security.Tokens


Namespace ProxyWS
   
    ''' <summary>
    ''' Proxy para Efetuar Login
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  23/05/2012  criado
    ''' </history>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"), _
      System.ComponentModel.DesignerCategoryAttribute("code"), _
      System.Web.Services.WebServiceBindingAttribute(Name:="ServiciosSoap", [Namespace]:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios")> _
    Public Class ProxyServiciosAplicaciones
        Inherits Microsoft.Web.Services3.WebServicesClientProtocol
        'Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        Implements Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.IServiciosAplicaciones


        Public Sub New()
            MyBase.New()

            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("IAC_UrlServicioAdminPermisos")) Then
                Me.Url = ConfigurationManager.AppSettings("IAC_UrlServicioAdminPermisos") & "ServiciosAplicaciones.asmx"
            End If
            Dim token As New UsernameToken(ConfigurationManager.AppSettings("UsuarioWSLogin"), _
                                           ConfigurationManager.AppSettings("PasswordWSLogin"), PasswordOption.SendPlainText)




            Me.SetClientCredential(token)
            Me.SetPolicy("ClientPolicy")

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios/CrearAplicacionVersion", RequestNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", ResponseNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CrearAplicacionVersion(Peticion As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Aplicacion.CrearAplicacionVersion.Peticion) As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Aplicacion.CrearAplicacionVersion.Respuesta Implements Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.IServiciosAplicaciones.CrearAplicacionVersion
            Dim results() As Object = Me.Invoke("CrearAplicacionVersion", New Object() {Peticion})
            Return CType(results(0), Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Aplicacion.CrearAplicacionVersion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios/GrabarDelegacion", RequestNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", ResponseNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarDelegacion(Peticion As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Delegacion.GrabarDelegacion.Peticion) As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Delegacion.GrabarDelegacion.Respuesta Implements Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.IServiciosAplicaciones.GrabarDelegacion
            Dim results() As Object = Me.Invoke("GrabarDelegacion", New Object() {Peticion})
            Return CType(results(0), Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Delegacion.GrabarDelegacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios/GrabarPlanta", RequestNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", ResponseNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarPlanta(Peticion As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Planta.GrabarPlanta.Peticion) As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Planta.GrabarPlanta.Respuesta Implements Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.IServiciosAplicaciones.GrabarPlanta
            Dim results() As Object = Me.Invoke("GrabarPlanta", New Object() {Peticion})
            Return CType(results(0), Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Planta.GrabarPlanta.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios/GrabarTipoSector", RequestNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", ResponseNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarTipoSector(Peticion As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector.GrabarTipoSector.Peticion) As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector.GrabarTipoSector.Respuesta Implements Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.IServiciosAplicaciones.GrabarTipoSector
            Dim results() As Object = Me.Invoke("GrabarTipoSector", New Object() {Peticion})
            Return CType(results(0), Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector.GrabarTipoSector.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios/GrabarDelegacionTipoSector", RequestNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", ResponseNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarDelegacionTipoSector(Peticion As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector.GrabarDelegacionTipoSector.Peticion) As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector.GrabarDelegacionTipoSector.Respuesta Implements Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.IServiciosAplicaciones.GrabarDelegacionTipoSector
            Dim results() As Object = Me.Invoke("GrabarDelegacionTipoSector", New Object() {Peticion})
            Return CType(results(0), Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector.GrabarDelegacionTipoSector.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios/GrabarPlantaTipoSector", RequestNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", ResponseNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarPlantaTipoSector(Peticion As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector.GrabarPlantaTipoSector.Peticion) As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector.GrabarPlantaTipoSector.Respuesta Implements Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.IServiciosAplicaciones.GrabarPlantaTipoSector
            Dim results() As Object = Me.Invoke("GrabarPlantaTipoSector", New Object() {Peticion})
            Return CType(results(0), Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.TipoSector.GrabarPlantaTipoSector.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios/Test", RequestNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", ResponseNamespace:="http://Prosegur.Global.Seguridad.AdministracionPermisos.Servicio.Servicios", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Test.Respuesta Implements Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.IServiciosAplicaciones.Test
            Dim results() As Object = Me.Invoke("Test", New Object() {-1})
            Return CType(results(0), Prosegur.Global.Seguridad.AdministracionPermisos.ContractoServicio.Test.Respuesta)
        End Function
    End Class

End Namespace