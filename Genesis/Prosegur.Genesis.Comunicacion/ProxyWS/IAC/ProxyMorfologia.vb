﻿Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports ContratoIac = Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports System.Configuration

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")> _
Public Class ProxyMorfologia
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IMorfologia

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Morfologia.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetMorfologia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetMorfologia(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Morfologia.GetMorfologia.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Morfologia.GetMorfologia.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMorfologia.GetMorfologia
        Dim results() As Object = Me.Invoke("GetMorfologia", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.Morfologia.GetMorfologia.Respuesta)
    End Function


    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetMorfologia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetMorfologia(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Morfologia.SetMorfologia.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Morfologia.SetMorfologia.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMorfologia.SetMorfologia
        Dim results() As Object = Me.Invoke("SetMorfologia", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.Morfologia.SetMorfologia.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarMorfologia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarMorfologia(Peticion As [Global].GesEfectivo.IAC.ContractoServicio.Morfologia.VerificarMorfologia.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Morfologia.VerificarMorfologia.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMorfologia.VerificarMorfologia
        Dim results() As Object = Me.Invoke("VerificarMorfologia", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.ContractoServicio.Morfologia.VerificarMorfologia.Respuesta)
    End Function


    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IMorfologia.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function

   
End Class
