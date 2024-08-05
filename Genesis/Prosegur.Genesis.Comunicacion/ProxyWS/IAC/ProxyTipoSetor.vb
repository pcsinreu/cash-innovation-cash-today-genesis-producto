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
Public Class ProxyTipoSetor
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ITipoSetor

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/TipoSetor.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetCaractNoPertenecTipoSector", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetCaractNoPertenecTipoSector(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ITipoSetor.GetCaractNoPertenecTipoSector
        Dim results() As Object = Me.Invoke("GetCaractNoPertenecTipoSector", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetTiposSectores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetTiposSectores(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.TipoSetor.GetTiposSectores.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ITipoSetor.GetTiposSectores
        Dim results() As Object = Me.Invoke("GetTiposSectores", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetTiposSectores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetTiposSectores(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.TipoSetor.SetTiposSectores.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.TipoSetor.SetTiposSectores.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ITipoSetor.SetTiposSectores
        Dim results() As Object = Me.Invoke("SetTiposSectores", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoSetor.SetTiposSectores.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ITipoSetor.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)
    End Function
End Class
