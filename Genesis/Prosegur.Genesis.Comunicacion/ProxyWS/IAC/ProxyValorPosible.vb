Option Strict Off
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
Public Class ProxyValorPosible
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IValorPosible

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/ValorPosible.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetValoresPosibles", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetValoresPosibles(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.ValorPosible.GetValoresPosibles.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IValorPosible.GetValoresPosibles
        Dim results() As Object = Me.Invoke("GetValoresPosibles", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetValoresPosibles", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetValoresPosibles(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.ValorPosible.SetValoresPosibles.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.ValorPosible.SetValoresPosibles.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IValorPosible.SetValoresPosibles
        Dim results() As Object = Me.Invoke("SetValoresPosibles", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ValorPosible.SetValoresPosibles.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetCodigosAjenos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IValorPosible.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function
End Class
