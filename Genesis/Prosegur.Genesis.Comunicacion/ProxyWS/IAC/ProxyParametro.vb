Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Configuration
Imports Prosegur.Global.GesEfectivo

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")> _
Public Class ProxyParametro
    Inherits ProxyWS.ServicioBase
    Implements IAC.ContractoServicio.IParametro

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Parametro.asmx"
        End If        

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/BajarAgrupacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function BajarAgrupacion(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.BajarAgrupacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.BajarAgrupacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.BajarAgrupacion
        Dim results() As Object = Me.Invoke("BajarAgrupacion", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.BajarAgrupacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetAgrupacionDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetAgrupacionDetail(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.GetAgrupacionDetail
        Dim results() As Object = Me.Invoke("GetAgrupacionDetail", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetAgrupaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetAgrupaciones(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetAgrupaciones.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.GetAgrupaciones
        Dim results() As Object = Me.Invoke("GetAgrupaciones", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetParametroDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetParametroDetail(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetParametroDetail.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetParametroDetail.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.GetParametroDetail
        Dim results() As Object = Me.Invoke("GetParametroDetail", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.GetParametroDetail.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetParametroOpciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetParametroOpciones(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetParametroOpciones.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetParametroOpciones.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.GetParametroOpciones
        Dim results() As Object = Me.Invoke("GetParametroOpciones", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.GetParametroOpciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetParametros", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetParametros(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetParametros.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetParametros.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.GetParametros
        Dim results() As Object = Me.Invoke("GetParametros", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.GetParametros.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetParametrosValues", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetParametrosValues(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetParametrosValues.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.GetParametrosValues.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.GetParametrosValues
        Dim results() As Object = Me.Invoke("GetParametrosValues", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.GetParametrosValues.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetAgrupacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetAgrupacion(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.SetAgrupacion.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.SetAgrupacion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.SetAgrupacion
        Dim results() As Object = Me.Invoke("SetAgrupacion", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.SetAgrupacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetParametro", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetParametro(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.SetParametro.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.SetParametro.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.SetParametro
        Dim results() As Object = Me.Invoke("SetParametro", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.SetParametro.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetParametrosValues", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetParametrosValues(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.SetParametrosValues.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.SetParametrosValues.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.SetParametrosValues
        Dim results() As Object = Me.Invoke("SetParametrosValues", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.SetParametrosValues.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoParametroOpcion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarCodigoParametroOpcion(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Peticion, codParametro As String, codAplicacion As String) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.VerificarCodigoParametroOpcion
        Dim results() As Object = Me.Invoke("VerificarCodigoParametroOpcion", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescricaoParametroOpcion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarDescricaoParametroOpcion(peticion As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Peticion, codParametro As String, codAplicacion As String) As [Global].GesEfectivo.IAC.ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.VerificarDescricaoParametroOpcion
        Dim results() As Object = Me.Invoke("VerificarDescricaoParametroOpcion", New Object() {peticion})
        Return CType(results(0), IAC.ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.IParametro.Test
        Dim results() As Object = Me.Invoke("Test", New Object() {})
        Return CType(results(0), IAC.ContractoServicio.Test.Respuesta)
    End Function

End Class