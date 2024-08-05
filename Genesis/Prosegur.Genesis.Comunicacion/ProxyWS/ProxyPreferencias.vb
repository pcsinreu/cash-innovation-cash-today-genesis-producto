Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.GuardarPreferencias
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.ObtenerPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.BorrarPreferenciasAplicacion
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Global.GesEfectivo.IAC
Imports System.Configuration
Imports Prosegur.Global
Imports Prosegur.Genesis

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
      System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="PreferenciasSoap", [Namespace]:="http://Prosegur.Genesis.Servicio.Preferencias")> _
Public Class ProxyPreferencias
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Genesis.ContractoServicio.IPreferencias

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "Preferencias.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    Public Sub New(urlServicioParametro)
        Me.New()

        If Not String.IsNullOrEmpty(urlServicioParametro) Then
            Me.Url = urlServicioParametro & "Preferencias.asmx"
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio.Preferencias/ObtenerPreferencias", _
        RequestNamespace:="http://Prosegur.Genesis.Servicio.Preferencias", _
        ResponseNamespace:="http://Prosegur.Genesis.Servicio.Preferencias", _
        Use:=System.Web.Services.Description.SoapBindingUse.Literal, _
        ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerPreferencias(peticion As ObtenerPreferenciasPeticion) As ObtenerPreferenciasRespuesta Implements IPreferencias.ObtenerPreferencias
        Dim results() As Object = Me.Invoke("ObtenerPreferencias", New Object() {peticion})
        Return CType(results(0), ObtenerPreferenciasRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio.Preferencias/GuardarPreferencias", _
        RequestNamespace:="http://Prosegur.Genesis.Servicio.Preferencias", _
        ResponseNamespace:="http://Prosegur.Genesis.Servicio.Preferencias", _
        Use:=System.Web.Services.Description.SoapBindingUse.Literal, _
        ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GuardarPreferencias(peticion As GuardarPreferenciasPeticion) As GuardarPreferenciasRespuesta Implements IPreferencias.GuardarPreferencias
        Dim results() As Object = Me.Invoke("GuardarPreferencias", New Object() {peticion})
        Return CType(results(0), GuardarPreferenciasRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio.Preferencias/BorrarPreferenciasAplicacion", _
        RequestNamespace:="http://Prosegur.Genesis.Servicio.Preferencias", _
        ResponseNamespace:="http://Prosegur.Genesis.Servicio.Preferencias", _
        Use:=System.Web.Services.Description.SoapBindingUse.Literal, _
        ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function BorrarPreferenciasAplicacion(peticion As BorrarPreferenciasAplicacionPeticion) As BorrarPreferenciasAplicacionRespuesta Implements IPreferencias.BorrarPreferenciasAplicacion
        Dim results() As Object = Me.Invoke("BorrarPreferenciasAplicacion", New Object() {peticion})
        Return CType(results(0), BorrarPreferenciasAplicacionRespuesta)
    End Function
End Class
