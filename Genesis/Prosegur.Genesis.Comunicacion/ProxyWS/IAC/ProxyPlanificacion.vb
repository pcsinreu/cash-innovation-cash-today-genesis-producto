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
    Public Class ProxyPlanificacion
        Inherits ProxyWS.ServicioBase
        Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.IPlanificacion


        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "IAC/Planificacion.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPlanificacion.Test
            Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Test.Respuesta)

        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPlanificacionProgramacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetPlanificacionProgramacion(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Planificacion.GetPlanificacionProgramacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Planificacion.GetPlanificacionProgramacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPlanificacion.GetPlanificacionProgramacion
            Dim results() As Object = Me.Invoke("GetPlanificacionProgramacion", New Object() {objPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planificacion.GetPlanificacionProgramacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPlanificaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GetPlanificaciones(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Planificacion.GetPlanificaciones.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Planificacion.GetPlanificaciones.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IPlanificacion.GetPlanificaciones
            Dim results() As Object = Me.Invoke("GetPlanificaciones", New Object() {objPeticion})
            Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planificacion.GetPlanificaciones.Respuesta)
        End Function

    End Class

End Namespace