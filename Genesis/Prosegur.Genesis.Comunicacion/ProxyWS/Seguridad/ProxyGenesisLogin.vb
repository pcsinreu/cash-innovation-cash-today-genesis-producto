Option Explicit On

Imports Prosegur.Genesis.ContractoServicio
Imports System.Configuration


Namespace ProxyWS

    ''' <summary>
    ''' Proxy para Efetuar Login
    ''' </summary>
    ''' <remarks></remarks>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"),
         System.ComponentModel.DesignerCategoryAttribute("code"),
         System.Web.Services.WebServiceBindingAttribute(Name:="GenesisSoap", [Namespace]:="http://Prosegur.Genesis.Servicio")>
    Public Class ProxyGenesisLogin
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        Implements IGenesisLogin


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

#Region "[MÉTODOS]"
        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ConsumirTokenAcceso2", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ConsumirTokenAcceso2(Peticion As GenesisLogin.ConsumirTokenAcceso.Peticion) As GenesisLogin.ConsumirTokenAcceso.Respuesta Implements IGenesisLogin.ConsumirTokenAcceso2
            Dim results() As Object = Me.Invoke("ConsumirTokenAcceso2", New Object() {Peticion})
            Return CType(results(0), GenesisLogin.ConsumirTokenAcceso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/CrearTokenAcceso2", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function CrearTokenAcceso2(Peticion As GenesisLogin.CrearTokenAcceso.Peticion) As GenesisLogin.CrearTokenAcceso.Respuesta Implements IGenesisLogin.CrearTokenAcceso2
            Dim results() As Object = Me.Invoke("CrearTokenAcceso2", New Object() {Peticion})
            Return CType(results(0), GenesisLogin.CrearTokenAcceso.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/EjecutarLogin2", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function EjecutarLogin2(Peticion As GenesisLogin.EjecutarLogin.Peticion) As GenesisLogin.EjecutarLogin.Respuesta Implements IGenesisLogin.EjecutarLogin2
            Dim results() As Object = Me.Invoke("EjecutarLogin2", New Object() {Peticion})
            Return CType(results(0), GenesisLogin.EjecutarLogin.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerAplicaciones2", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerAplicaciones2(Peticion As GenesisLogin.ObtenerAplicaciones.Peticion) As GenesisLogin.ObtenerAplicaciones.Respuesta Implements IGenesisLogin.ObtenerAplicaciones2
            Dim results() As Object = Me.Invoke("ObtenerAplicaciones2", New Object() {Peticion})
            Return CType(results(0), GenesisLogin.ObtenerAplicaciones.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerDelegaciones2", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerDelegaciones2(Peticion As GenesisLogin.ObtenerDelegaciones.Peticion) As GenesisLogin.ObtenerDelegaciones.Respuesta Implements IGenesisLogin.ObtenerDelegaciones2
            Dim results() As Object = Me.Invoke("ObtenerDelegaciones2", New Object() {Peticion})
            Return CType(results(0), GenesisLogin.ObtenerDelegaciones.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerPermisos2", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerPermisos2(Peticion As GenesisLogin.ObtenerPermisos.Peticion) As GenesisLogin.ObtenerPermisos.Respuesta Implements IGenesisLogin.ObtenerPermisos2
            Dim results() As Object = Me.Invoke("ObtenerPermisos2", New Object() {Peticion})
            Return CType(results(0), GenesisLogin.ObtenerPermisos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/Test", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As Test.Respuesta Implements IGenesisLogin.Test
            Dim results() As Object = Me.Invoke("Test", New Object() {-1})
            Return CType(results(0), ContractoServicio.Test.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
            System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerAplicacionVersion", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerAplicacionVersion(Peticion As Login.ObtenerAplicacionVersion.Peticion) As Login.ObtenerAplicacionVersion.Respuesta Implements IGenesisLogin.ObtenerAplicacionVersion
            Dim results() As Object = Me.Invoke("ObtenerAplicacionVersion", New Object() {Peticion})
            Return CType(results(0), Login.ObtenerAplicacionVersion.Respuesta)
        End Function

#End Region

    End Class

End Namespace