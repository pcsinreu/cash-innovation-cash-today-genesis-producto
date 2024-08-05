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
Public Class ProxyPlanta
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IPlanta


    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Planta.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPlantas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetPlantas(objPeticion As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planta.GetPlanta.Peticion) As Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planta.GetPlanta.Respuesta Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IPlanta.GetPlanta
        Dim results() As Object = Me.Invoke("GetPlantas", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planta.GetPlanta.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPlantaDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetPlantaDetail(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Planta.GetPlantaDetail.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Planta.GetPlantaDetail.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPlanta.GetPlantaDetail
        Dim results() As Object = Me.Invoke("GetPlantaDetail", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planta.GetPlantaDetail.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetPlantas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetPlantas(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Planta.SetPlanta.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Planta.SetPlanta.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPlanta.SetPlanta
        Dim results() As Object = Me.Invoke("SetPlantas", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planta.SetPlanta.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPlanta.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), ContratoIac.Test.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificaCodigoPlanta", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificaCodigoPlanta(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Planta.VerificaCodigoPlanta.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Planta.VerificaCodigoPlanta.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPlanta.VerificaCodigoPlanta
        Dim results() As Object = Me.Invoke("VerificaCodigoPlanta", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planta.VerificaCodigoPlanta.Respuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificaExistencia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificaExistencia(ObjPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Planta.VerificaExistencia.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Planta.VerificaExistencia.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPlanta.VerificaExistencia
        Dim results() As Object = Me.Invoke("VerificaExistencia", New Object() {ObjPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planta.VerificaExistencia.Respuesta)
    End Function
End Class
