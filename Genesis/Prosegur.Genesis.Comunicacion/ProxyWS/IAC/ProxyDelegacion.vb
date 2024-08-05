Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Configuration

Namespace ProxyWS.IAC

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.Diagnostics.DebuggerStepThroughAttribute(), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")> _
    Public Class ProxyDelegacion
        Inherits ProxyWS.ServicioBase
        Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IDelegacion


        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "IAC/Delegacion.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub


        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetCodigoDelegacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetCodigoDelegacion(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetCodigoDelegacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetCodigoDelegacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDelegacion.GetCodigoDelegacion
            Dim results() As Object = Me.Invoke("GetCodigoDelegacion", New Object() {objPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion.GetCodigoDelegacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetDelegaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function GetDelegaciones(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDelegacion.GetDelegacion
            Dim results() As Object = Me.Invoke("GetDelegaciones", New Object() {objPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta)

        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetDelegaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function GetDelegacionByCertificado(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacionByCertificado.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacionByCertificado.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDelegacion.GetDelegacionByCertificado
            Dim results As Object = Me.Invoke("GetDelegacionByCertificado", New Object() {objPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacionByCertificado.Respuesta)
        End Function


        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetDelegaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function GetDelegacionByPlanificacion(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDelegacion.GetDelegacionByPlanificacion
            Dim results As Object = Me.Invoke("GetDelegacionByPlanificacion", New Object() {objPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetDelegacioneDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetDelegacioneDetail(ObjPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDelegacion.GetDelegacioneDetail
            Dim results As Object = Me.Invoke("GetDelegacioneDetail", New Object() {ObjPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetDelegaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SetDelegaciones(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.SetDelegacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.SetDelegacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDelegacion.SetDelegacion
            Dim results As Object = Me.Invoke("SetDelegaciones", New Object() {objPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion.SetDelegacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDelegacion.Test
            Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Test.Respuesta)

        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificaCodigoDelegacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function VerificaCodigoDelegacion(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.VerificaCodigoDelegacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Delegacion.VerificaCodigoDelegacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IDelegacion.VerificaCodigoDelegacion
            Dim results As Object = Me.Invoke("VerificaCodigoDelegacion", New Object() {objPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion.VerificaCodigoDelegacion.Respuesta)

        End Function
    End Class

End Namespace