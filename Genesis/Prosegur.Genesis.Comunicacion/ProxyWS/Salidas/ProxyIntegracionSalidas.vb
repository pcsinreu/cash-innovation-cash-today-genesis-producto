﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3053
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Prosegur.Global.GesEfectivo.Salidas.ContractoServicio
Imports ContractoSalidas = Prosegur.Global.GesEfectivo.Salidas.ContractoServicio
Imports System.Configuration.ConfigurationManager

Namespace ProxyWS.Salidas

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"), _
    System.ComponentModel.DesignerCategoryAttribute("code"), _
    System.Web.Services.WebServiceBindingAttribute(Name:="IntegracionSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.Integracion")> _
    Public Class ProxyIntegracionSalidas

        Inherits ProxyWS.ServicioBase

        Private EnviarMIFOperationCompleted As System.Threading.SendOrPostCallback
        Private useDefaultCredentialsSetExplicitly As Boolean

        '''<remarks/>
        Public Sub New(urlVersao As String)

            MyBase.New()

            If String.IsNullOrEmpty(urlVersao) Then
                Me.Url = UrlServicio & "Salidas/Integracion.asmx"
            Else
                Me.Url = urlVersao & "/Salidas/Integracion.asmx"
            End If
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/RecibirMifIntersector", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecibirMifIntersector(ByVal Peticion As MovimentacionFondo.RecibirMifIntersector.Peticion) As MovimentacionFondo.RecibirMifIntersector.Respuesta
            Dim results() As Object = Me.Invoke("RecibirMifIntersector", New Object() {Peticion})
            Return CType(results(0), MovimentacionFondo.RecibirMifIntersector.Respuesta)
        End Function


        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/RecibirRemesasPendientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecibirRemesasPendientes(ByVal Peticion As ContractoSalidas.Remesa.RecibirRemesasPendientes.Peticion) As ContractoSalidas.Remesa.RecibirRemesasPendientes.Respuesta
            Dim results() As Object = Me.Invoke("RecibirRemesasPendientes", New Object() {Peticion})
            Return CType(results(0), ContractoSalidas.Remesa.RecibirRemesasPendientes.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/RecibirRemesasPendientesConTerminos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecibirRemesasPendientesConTerminos(ByVal Peticion As ContractoSalidas.Remesa.RecibirRemesasPendientesConTerminos.Peticion) As ContractoSalidas.Remesa.RecibirRemesasPendientesConTerminos.Respuesta
            Dim results() As Object = Me.Invoke("RecibirRemesasPendientesConTerminos", New Object() {Peticion})
            Return CType(results(0), ContractoSalidas.Remesa.RecibirRemesasPendientesConTerminos.Respuesta)
        End Function

        ''' <summary>
        ''' Recupera as remesas no salidas
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/RecuperarRemesas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarRemesas(ByVal Peticion As ContractoSalidas.Remesa.Recuperar.Peticion) As ContractoSalidas.Remesa.Recuperar.Respuesta
            Dim results() As Object = Me.Invoke("RecuperarRemesas", New Object() {Peticion})
            Return CType(results(0), ContractoSalidas.Remesa.Recuperar.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/RecuperarRemesasConTerminos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarRemesasConTerminos(ByVal Peticion As ContractoSalidas.Remesa.RecuperarRemesasConTerminos.Peticion) As ContractoSalidas.Remesa.RecuperarRemesasConTerminos.Respuesta
            Dim results() As Object = Me.Invoke("RecuperarRemesasConTerminos", New Object() {Peticion})
            Return CType(results(0), ContractoSalidas.Remesa.RecuperarRemesasConTerminos.Respuesta)
        End Function

        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/AnularRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function AnularRemesa(ByVal Peticion As Remesa.AnularRemesa.Peticion) As Remesa.AnularRemesa.Respuesta
            Dim results() As Object = Me.Invoke("AnularRemesa", New Object() {Peticion})
            Return CType(results(0), Remesa.AnularRemesa.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/SincronizarDivisasSalidas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SincronizarDivisasSalidas(Peticion As Divisa.SincronizarDivisasSalidas.Peticion) As Divisa.SincronizarDivisasSalidas.Respuesta
            Dim results() As Object = Me.Invoke("SincronizarDivisasSalidas", New Object() {Peticion})
            Return CType(results(0), Divisa.SincronizarDivisasSalidas.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/SetPuesto", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SetPuesto(Peticion As Puesto.SetPuesto.Peticion) As Puesto.SetPuesto.Respuesta
            Dim results() As Object = Me.Invoke("SetPuesto", New Object() {Peticion})
            Return CType(results(0), Puesto.SetPuesto.Respuesta)
        End Function


        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/DividirEnBultos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function DividirEnBultos(ByVal Peticion As Bulto.DividirEnBultos.Peticion) As Bulto.DividirEnBultos.Respuesta
            Dim results() As Object = Me.Invoke("DividirEnBultos", New Object() {Peticion})
            Return CType(results(0), Bulto.DividirEnBultos.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/ObtenerCodigoRemesa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerCodigoRemesa() As Remesa.ObtenerCodigoRemesa.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerCodigoRemesa", New Object(-1) {})
            Return CType(results(0), Remesa.ObtenerCodigoRemesa.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Integracion/ObtenerRemesaSiguiente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerRemesaSiguiente() As Remesa.ObtenerRemesaSiguiente.Respuesta
            Dim results() As Object = Me.Invoke("ObtenerRemesaSiguiente", New Object(-1) {})
            Return CType(results(0), Remesa.ObtenerRemesaSiguiente.Respuesta)
        End Function

    End Class


End Namespace