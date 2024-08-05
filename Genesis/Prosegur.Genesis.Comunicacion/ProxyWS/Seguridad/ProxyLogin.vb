Option Strict Off
Option Explicit On

Imports Prosegur.Genesis.ContractoServicio
Imports System.Configuration


Namespace ProxyWS

    ''' <summary>
    ''' Proxy para Efetuar Login
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  23/05/2012  criado
    ''' </history>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="GenesisSoap", [Namespace]:="http://Prosegur.Genesis.Servicio")> _
    Public Class ProxyLogin
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        Implements ILogin


#Region "[VARIÁVEIS]"

        Private sesionInfoValueField As New ContractoServicio.ServicioBase.SesionInfo()
        Private useDefaultCredentialsSetExplicitly As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property SesionInfoValue() As ContractoServicio.ServicioBase.SesionInfo
            Get
                Return Me.sesionInfoValueField
            End Get
            Set(value As ContractoServicio.ServicioBase.SesionInfo)
                Me.sesionInfoValueField = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTOR]"

        Public Sub New()
            MyBase.New()

            If Not String.IsNullOrEmpty(ConfigurationManager.AppSettings("WS_TIMEOUT")) AndAlso IsNumeric(ConfigurationManager.AppSettings("WS_TIMEOUT")) Then
                MyBase.Timeout = Integer.Parse(ConfigurationManager.AppSettings("WS_TIMEOUT")) * 1000
            End If

            Me.useDefaultCredentialsSetExplicitly = True
        End Sub

#End Region

#Region "[MÉTODOS]"

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/EjecutarLogin", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EjecutarLogin(Peticion As Login.EjecutarLogin.Peticion) As Login.EjecutarLogin.Respuesta Implements ILogin.EjecutarLogin
            Dim results() As Object = Me.Invoke("EjecutarLogin", New Object() {Peticion})
            Return CType(results(0), Login.EjecutarLogin.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerAplicacionVersion", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerAplicacionVersion(Peticion As Login.ObtenerAplicacionVersion.Peticion) As Login.ObtenerAplicacionVersion.Respuesta Implements ILogin.ObtenerAplicacionVersion
            Dim results() As Object = Me.Invoke("ObtenerAplicacionVersion", New Object() {Peticion})
            Return CType(results(0), Login.ObtenerAplicacionVersion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerVersiones", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerVersiones(Peticion As Login.ObtenerVersiones.Peticion) As Login.ObtenerVersiones.Respuesta Implements ILogin.ObtenerVersiones
            Dim results() As Object = Me.Invoke("ObtenerVersiones", New Object() {Peticion})
            Return CType(results(0), Login.ObtenerVersiones.Respuesta)
        End Function


        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"),
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerDelegaciones", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ObtenerDelegaciones(Peticion As ContractoServicio.Login.ObtenerDelegaciones.Peticion) As ContractoServicio.Login.ObtenerDelegaciones.Respuesta Implements ContractoServicio.ILogin.ObtenerDelegaciones
            Dim results() As Object = Me.Invoke("ObtenerDelegaciones", New Object() {Peticion})
            Return CType(results(0), Login.ObtenerDelegaciones.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"),
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerPaises", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ObtenerPaises() As ContractoServicio.Login.ObtenerPaises.Respuesta Implements ContractoServicio.ILogin.ObtenerPaises
            Dim results() As Object = Me.Invoke("ObtenerPaises", (New Object() {-1}))
            Return CType(results(0), Login.ObtenerPaises.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"),
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerInformacionLogin", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function ObtenerInformacionLogin(Peticion As Login.ObtenerInformacionLogin.Peticion) As Login.EjecutarLogin.Respuesta Implements ILogin.ObtenerInformacionLogin
            Dim results() As Object = Me.Invoke("ObtenerInformacionLogin", (New Object() {Peticion}))
            Return CType(results(0), Login.EjecutarLogin.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/CrearTokenAcceso", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CrearTokenAcceso(Peticion As ContractoServicio.Login.CrearTokenAcceso.Peticion) As ContractoServicio.Login.CrearTokenAcceso.Respuesta Implements ContractoServicio.ILogin.CrearTokenAcceso
            Dim results() As Object = Me.Invoke("CrearTokenAcceso", New Object() {Peticion})
            Return CType(results(0), Login.CrearTokenAcceso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ConsumirTokenAcceso", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ConsumirTokenAcceso(Peticion As ContractoServicio.Login.ConsumirTokenAcceso.Peticion) As ContractoServicio.Login.ConsumirTokenAcceso.Respuesta Implements ContractoServicio.ILogin.ConsumirTokenAcceso
            Dim results() As Object = Me.Invoke("ConsumirTokenAcceso", New Object() {Peticion})
            Return CType(results(0), Login.ConsumirTokenAcceso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/GetDelegacionesUsuario", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetDelegacionesUsuario(Peticion As ContractoServicio.Login.GetDelegacionesUsuario.Peticion) As ContractoServicio.Login.GetDelegacionesUsuario.Respuesta Implements ContractoServicio.ILogin.GetDelegacionesUsuario
            Dim results() As Object = Me.Invoke("GetDelegacionesUsuario", New Object() {Peticion})
            Return CType(results(0), Login.GetDelegacionesUsuario.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/Test", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ILogin.Test
            Dim results() As Object = Me.Invoke("Test", New Object() {-1})
            Return CType(results(0), ContractoServicio.Test.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/EjecutarLoginAplicacion", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EjecutarLoginAplicacion(Peticion As ContractoServicio.Login.EjecutarLoginAplicacion.Peticion) _
        As ContractoServicio.Login.EjecutarLoginAplicacion.Respuesta Implements ContractoServicio.ILogin.EjecutarLoginAplicacion
            Dim results() As Object = Me.Invoke("EjecutarLoginAplicacion", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.Login.EjecutarLoginAplicacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/AutenticarUsuarioAplicacion", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AutenticarUsuarioAplicacion(Peticion As Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionPeticion) As Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta Implements ILogin.AutenticarUsuarioAplicacion
            Dim results() As Object = Me.Invoke("AutenticarUsuarioAplicacion", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
        System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerPermisosUsuario", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerPermisosUsuario(Peticion As Login.ObtenerPermisosUsuario.Peticion) As Login.ObtenerPermisosUsuario.Respuesta Implements ILogin.ObtenerPermisosUsuario
            Dim results() As Object = Me.Invoke("ObtenerPermisosUsuario", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.Login.ObtenerPermisosUsuario.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
      System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ValidarPermisosUsuario", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ValidarPermisosUsuario(Peticion As Login.ValidarPermisosUsuario.Peticion) As Login.ValidarPermisosUsuario.Respuesta Implements ILogin.ValidarPermisosUsuario
            Dim results() As Object = Me.Invoke("ValidarPermisosUsuario", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.Login.ValidarPermisosUsuario.Respuesta)
        End Function


#End Region

    End Class

End Namespace