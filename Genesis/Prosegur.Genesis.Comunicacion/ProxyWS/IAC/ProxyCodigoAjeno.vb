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
Public Class ProxyCodigoAjeno
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ICodigoAjeno


    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/CodigoAjeno.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetCodigosAjenos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetCodigosAjenos(objPeticion As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion) As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ICodigoAjeno.GetCodigosAjenos
        Dim results() As Object = Me.Invoke("GetCodigosAjenos", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta)
    End Function


    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetCodigosAjenos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetCodigosAjenos(objPeticion As ContratoIac.CodigoAjeno.SetCodigosAjenos.Peticion) As ContratoIac.CodigoAjeno.SetCodigosAjenos.Respuesta Implements ContratoIac.ICodigoAjeno.SetCodigosAjenos
        Dim results() As Object = Me.Invoke("SetCodigosAjenos", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarIdentificadorXCodigoAjeno", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarIdentificadorXCodigoAjeno(objPeticion As ContratoIac.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Peticion) As ContratoIac.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Respuesta Implements ContratoIac.ICodigoAjeno.VerificarIdentificadorXCodigoAjeno
        Dim results() As Object = Me.Invoke("VerificarIdentificadorXCodigoAjeno", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ICodigoAjeno.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetAjenoByClienteSubClientePuntoServicio", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetAjenoByClienteSubClientePuntoServicio(objPeticion As ContratoIac.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.Peticion) As ContratoIac.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.Respuesta Implements ContratoIac.ICodigoAjeno.GetAjenoByClienteSubClientePuntoServicio
        Dim results() As Object = Me.Invoke("GetAjenoByClienteSubClientePuntoServicio", New Object() {objPeticion})
        Return CType(results(0), ContratoIac.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.Respuesta)
    End Function

End Class
