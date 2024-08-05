Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro


<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")> _
Public Class ProxyIacIntegracion
    Inherits ProxyWS.ServicioBase
    Implements Integracion.ContractoServicio.IIntegracion


    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Integracion.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 10000
        End If

    End Sub

    Public Sub New(urlServicioVersao As String)
        MyBase.New()

        Me.Url = urlServicioVersao & "IAC/Integracion.asmx"

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 10000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetCliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetCliente(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.SetCliente.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.SetCliente.Respuesta Implements [Global].GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.SetCliente
        Dim results() As Object = Me.Invoke("SetCliente", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.SetCliente.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetMediosPagoIntegracion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetMediosPagoIntegracion(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetMediosPago.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetMediosPago.Respuesta Implements [Global].GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetMediosPago
        Dim results() As Object = Me.Invoke("GetMediosPagoIntegracion", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetMediosPago.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetIacIntegracion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetIacIntegracion(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetIac.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetIac.Respuesta Implements [Global].GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetIac
        Dim results() As Object = Me.Invoke("GetIacIntegracion", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetIac.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetProceso", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetProceso(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProceso.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProceso.Respuesta Implements [Global].GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetProceso
        Dim results() As Object = Me.Invoke("GetProceso", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProceso.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetProcesoCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetProcesoCP(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProceso.Peticion) As Integracion.ContractoServicio.GetProceso.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetProcesoCP
        Dim results() As Object = Me.Invoke("GetProcesoCP", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProceso.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetProcesos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetProcesos(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProcesos.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProcesos.Respuesta Implements [Global].GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetProcesos
        Dim results() As Object = Me.Invoke("GetProcesos", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProcesos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As Integracion.ContractoServicio.Test.Respuesta Implements Integracion.ContractoServicio.IIntegracion.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetMorfologiaDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetMorfologiaDetail(Peticion As Integracion.ContractoServicio.GetMorfologiaDetail.Peticion) As Integracion.ContractoServicio.GetMorfologiaDetail.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetMorfologiaDetail
        Dim results() As Object = Me.Invoke("GetMorfologiaDetail", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetMorfologiaDetail.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetProcesosPorDelegacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetProcesosPorDelegacion(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Respuesta Implements [Global].GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetProcesosPorDelegacion
        Dim results() As Object = Me.Invoke("GetProcesosPorDelegacion", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetATMByRegistrarTira", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetATMByRegistrarTira(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetATMByRegistrarTira.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetATMByRegistrarTira.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetATMByRegistrarTira
        Dim results() As Object = Me.Invoke("GetATMByRegistrarTira", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetATMByRegistrarTira.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetATM", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetATM(Peticion As Integracion.ContractoServicio.GetATM.Peticion) As Integracion.ContractoServicio.GetATM.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetATM
        Dim results() As Object = Me.Invoke("GetATM", New Object() {Peticion})
        Return CType(results(0), Integracion.ContractoServicio.GetATM.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/ImportarParametros", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ImportarParametros(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.ImportarParametros.Peticion) As Integracion.ContractoServicio.ImportarParametros.Respuesta Implements Integracion.ContractoServicio.IIntegracion.ImportarParametros
        Dim results() As Object = Me.Invoke("ImportarParametros", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.ImportarParametros.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/RecuperarParametros", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarParametros(Peticion As Integracion.ContractoServicio.RecuperarParametros.Peticion) As Integracion.ContractoServicio.RecuperarParametros.Respuesta Implements Integracion.ContractoServicio.IIntegracion.RecuperarParametros
        Dim results() As Object = Me.Invoke("RecuperarParametros", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.RecuperarParametros.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetParametrosDelegacionPais", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetParametrosDelegacionPais(Peticion As Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion) As Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetParametrosDelegacionPais
        Dim results() As Object = Me.Invoke("GetParametrosDelegacionPais", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/RecuperaValoresPosiblesPorNivel", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperaValoresPosiblesPorNivel(Peticion As Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.Peticion) As Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.Respuesta Implements Integracion.ContractoServicio.IIntegracion.RecuperaValoresPosiblesPorNivel
        Dim results() As Object = Me.Invoke("RecuperaValoresPosiblesPorNivel", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getConfiguracionCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getConfiguracionCP(Peticion As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Peticion) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Respuesta Implements Integracion.ContractoServicio.IIntegracion.getConfiguracionCP
        Dim results() As Object = Me.Invoke("getConfiguracionCP", New Object() {Peticion})
        Return CType(results(0), Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguracion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getConfiguracionesCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getConfiguracionesCP(Peticion As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Peticion) As Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Respuesta Implements Integracion.ContractoServicio.IIntegracion.getConfiguracionesCP
        Dim results() As Object = Me.Invoke("getConfiguracionesCP", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.CargaPreviaEletronica.GetConfiguraciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetConfiguracionesReportesDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetConfiguracionesReportesDetail(Peticion As Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Peticion) As Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetConfiguracionesReportesDetail
        Dim results() As Object = Me.Invoke("GetConfiguracionesReportesDetail", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetConfiguracionReporte", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetConfiguracionReporte(Peticion As Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Peticion) As Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Respuesta Implements Integracion.ContractoServicio.IIntegracion.SetConfiguracionReporte
        Dim results() As Object = Me.Invoke("SetConfiguracionReporte", New Object() {Peticion})
        Return CType(results(0), Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetConfiguracionesReportes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetConfiguracionesReportes(Peticion As Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Respuesta Implements [Global].GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetConfiguracionesReportes
        Dim results() As Object = Me.Invoke("GetConfiguracionesReportes", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetATMsSimplificado", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetATMsSimplificado(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificado.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificado.Respuesta Implements [Global].GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetATMsSimplificado
        Dim results() As Object = Me.Invoke("GetATMsSimplificado", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificado.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetATMsSimplificadoV2", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetATMsSimplificadoV2(Peticion As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificadoV2.Peticion) As [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificadoV2.Respuesta Implements [Global].GesEfectivo.IAC.Integracion.ContractoServicio.IIntegracion.GetATMsSimplificadoV2
        Dim results() As Object = Me.Invoke("GetATMsSimplificadoV2", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetATMsSimplificadoV2.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetPuestos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetPuestos(Peticion As Integracion.ContractoServicio.GetPuestos.Peticion) As Integracion.ContractoServicio.GetPuestos.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetPuestos
        Dim results() As Object = Me.Invoke("GetPuestos", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetPuestos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetValores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetValores(Peticion As Integracion.ContractoServicio.TiposYValores.GetValores.Peticion) As Integracion.ContractoServicio.TiposYValores.GetValores.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetValores
        Dim results() As Object = Me.Invoke("GetValores", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.TiposYValores.GetValores.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetValor", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetValor(Peticion As Integracion.ContractoServicio.TiposYValores.SetValor.Peticion) As Integracion.ContractoServicio.TiposYValores.SetValor.Respuesta Implements Integracion.ContractoServicio.IIntegracion.SetValor
        Dim results() As Object = Me.Invoke("SetValor", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.TiposYValores.SetValor.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/SetModulo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetModulo(Peticion As Integracion.ContractoServicio.Modulo.SetModulo.Peticion) As Integracion.ContractoServicio.Modulo.SetModulo.Respuesta Implements Integracion.ContractoServicio.IIntegracion.SetModulo
        Dim results() As Object = Me.Invoke("SetModulo", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.SetModulo.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetModulo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetModulo(Peticion As Integracion.ContractoServicio.Modulo.GetModulo.Peticion) As Integracion.ContractoServicio.Modulo.GetModulo.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetModulo
        Dim results() As Object = Me.Invoke("GetModulo", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.GetModulo.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetModuloCliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetModuloCliente(Peticion As Integracion.ContractoServicio.Modulo.GetModuloCliente.Peticion) As Integracion.ContractoServicio.Modulo.GetModuloCliente.Respuesta Implements Integracion.ContractoServicio.IIntegracion.GetModuloCliente
        Dim results() As Object = Me.Invoke("GetModuloCliente", New Object() {Peticion})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.GetModuloCliente.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/obtenerParametros", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function obtenerParametros(peticion As obtenerParametros.Peticion) As obtenerParametros.Respuesta Implements Integracion.ContractoServicio.IIntegracion.obtenerParametros
        Dim results() As Object = Me.Invoke("obtenerParametros", New Object() {peticion})
        Return CType(results(0), obtenerParametros.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/RecuperarModulos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarModulos() As Integracion.ContractoServicio.Modulo.RecuperarModulos.Respuesta Implements Integracion.ContractoServicio.IIntegracion.RecuperarModulos
        Dim results() As Object = Me.Invoke("RecuperarModulos", New Object(-1) {})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.Modulo.RecuperarModulos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/RecuperarTodasDivisasYDenominaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarTodasDivisasYDenominaciones() As Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.Respuesta Implements Integracion.ContractoServicio.IIntegracion.RecuperarTodasDivisasYDenominaciones
        Dim results() As Object = Me.Invoke("RecuperarTodasDivisasYDenominaciones", New Object(-1) {})
        Return CType(results(0), [Global].GesEfectivo.IAC.Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.Respuesta)
    End Function
End Class
