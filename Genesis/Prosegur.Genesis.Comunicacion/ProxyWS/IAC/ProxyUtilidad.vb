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
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboTiposCuenta

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"),
     System.Diagnostics.DebuggerStepThroughAttribute(),
     System.ComponentModel.DesignerCategoryAttribute("code"),
     System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")>
Public Class ProxyUtilidad
    Inherits ProxyWS.ServicioBase
    Implements IAC.ContractoServicio.IUtilidad

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Utilidad.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboMaquinas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboMaquinas() As IAC.ContractoServicio.Utilidad.GetComboMaquinas.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboMaquinas
        Dim results() As Object = Me.Invoke("GetComboMaquinas", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboMaquinas.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboAlgoritmos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboAlgoritmos(objPeticion As IAC.ContractoServicio.Utilidad.GetComboAlgoritmos.Peticion) As IAC.ContractoServicio.Utilidad.GetComboAlgoritmos.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboAlgoritmos
        Dim results() As Object = Me.Invoke("GetComboAlgoritmos", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboAlgoritmos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboFormatos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboFormatos() As IAC.ContractoServicio.Utilidad.GetComboFormatos.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboFormatos
        Dim results() As Object = Me.Invoke("GetComboFormatos", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboFormatos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboMascaras", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboMascaras(objPeticion As IAC.ContractoServicio.Utilidad.GetComboMascaras.Peticion) As IAC.ContractoServicio.Utilidad.GetComboMascaras.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboMascaras
        Dim results() As Object = Me.Invoke("GetComboMascaras", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboMascaras.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboTerminosIAC", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboTerminosIAC(objPeticion As IAC.ContractoServicio.Utilidad.GetComboTerminosIAC.Peticion) As IAC.ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboTerminosIAC
        Dim results() As Object = Me.Invoke("GetComboTerminosIAC", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboMediosPagoByTipoAndDivisa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboMediosPagoByTipoAndDivisa(objPeticion As IAC.ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Peticion) As IAC.ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboMediosPagoByTipoAndDivisa
        Dim results() As Object = Me.Invoke("GetComboMediosPagoByTipoAndDivisa", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboTiposMedioPagoByDivisa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboTiposMedioPagoByDivisa(objPeticion As IAC.ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Peticion) As IAC.ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboTiposMedioPagoByDivisa
        Dim results() As Object = Me.Invoke("GetComboTiposMedioPagoByDivisa", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboDivisas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboDivisas() As IAC.ContractoServicio.Utilidad.GetComboDivisas.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboDivisas
        Dim results() As Object = Me.Invoke("GetComboDivisas", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboDivisas.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboPais", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboPais() As IAC.ContractoServicio.Utilidad.GetComboPais.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboPais
        Dim result() As Object = Me.Invoke("GetComboPais", New Object(-1) {})
        Return CType(result(0), IAC.ContractoServicio.Utilidad.GetComboPais.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboDivisasByTipoMedioPago", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboDivisasByTipoMedioPago(objPeticion As IAC.ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Peticion) As IAC.ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboDivisasByTipoMedioPago
        Dim results() As Object = Me.Invoke("GetComboDivisasByTipoMedioPago", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetDivisasMedioPago", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetDivisasMedioPago() As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetDivisasMedioPago
        Dim results() As Object = Me.Invoke("GetDivisasMedioPago", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboClientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboClientes(objPeticion As IAC.ContractoServicio.Utilidad.GetComboClientes.Peticion) As IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboClientes
        Dim results() As Object = Me.Invoke("GetComboClientes", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboClientes.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboTiposMedioPago", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboTiposMedioPago() As IAC.ContractoServicio.Utilidad.GetComboTiposMedioPago.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboTiposMedioPago
        Dim results() As Object = Me.Invoke("GetComboTiposMedioPago", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboTiposMedioPago.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboSubclientesByCliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboSubclientesByCliente(objPeticion As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Peticion) As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboSubclientesByCliente
        Dim results() As Object = Me.Invoke("GetComboSubclientesByCliente", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboPuntosServiciosByClienteSubcliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboPuntosServiciosByClienteSubcliente(objPeticion As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion) As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboPuntosServiciosByClienteSubcliente
        Dim results() As Object = Me.Invoke("GetComboPuntosServiciosByClienteSubcliente", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboPuntosServiciosByClientesSubclientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboPuntosServiciosByClientesSubclientes(objPeticion As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Peticion) As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboPuntosServiciosByClientesSubclientes
        Dim results() As Object = Me.Invoke("GetComboPuntosServiciosByClientesSubclientes", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboCanales", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboCanales() As IAC.ContractoServicio.Utilidad.GetComboCanales.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboCanales
        Dim results() As Object = Me.Invoke("GetComboCanales", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboCanales.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboSubcanalesByCanal", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboSubcanalesByCanal(objPeticion As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion) As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboSubcanalesByCanal
        Dim results() As Object = Me.Invoke("GetComboSubcanalesByCanal", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboProductos", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboProductos() As IAC.ContractoServicio.Utilidad.GetComboProductos.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboProductos
        Dim results() As Object = Me.Invoke("GetComboProductos", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboProductos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboDelegaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboDelegaciones() As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboDelegaciones
        Dim results() As Object = Me.Invoke("GetComboDelegaciones", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboDelegacionesPorPais", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboDelegacionesPorPais(objPeticion As IAC.ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Peticion) As IAC.ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboDelegacionesPorPais
        Dim results() As Object = Me.Invoke("GetComboDelegacionesPorPais", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboModalidadesRecuento", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboModalidadesRecuento() As IAC.ContractoServicio.Utilidad.GetComboModalidadesRecuento.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboModalidadesRecuento
        Dim results() As Object = Me.Invoke("GetComboModalidadesRecuento", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboModalidadesRecuento.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboInformacionAdicional", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboInformacionAdicional() As IAC.ContractoServicio.Utilidad.GetComboInformacionAdicional.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboInformacionAdicional
        Dim results() As Object = Me.Invoke("GetComboInformacionAdicional", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboInformacionAdicional.Respuesta)
    End Function
    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboInformacionAdicionalConFiltros", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboInformacionAdicionalConFiltros(objPeticion As IAC.ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Peticion) As IAC.ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboInformacionAdicionalConFiltros
        Dim results() As Object = Me.Invoke("GetComboInformacionAdicionalConFiltros", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetListaAgrupaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetListaAgrupaciones() As IAC.ContractoServicio.Utilidad.GetListaAgrupaciones.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetListaAgrupaciones
        Dim results() As Object = Me.Invoke("GetListaAgrupaciones", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetListaAgrupaciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoCaracteristica", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarCodigoCaracteristica(objPeticion As IAC.ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Peticion) As IAC.ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarCodigoCaracteristica
        Dim results() As Object = Me.Invoke("VerificarCodigoCaracteristica", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoConteoCaracteristica", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarCodigoConteoCaracteristica(objPeticion As IAC.ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Peticion) As IAC.ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarCodigoConteoCaracteristica
        Dim results() As Object = Me.Invoke("VerificarCodigoConteoCaracteristica", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionCaracteristica", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarDescripcionCaracteristica(objPeticion As IAC.ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Peticion) As IAC.ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarDescripcionCaracteristica
        Dim results() As Object = Me.Invoke("VerificarDescripcionCaracteristica", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboCaracteristicas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboCaracteristicas() As IAC.ContractoServicio.Utilidad.GetComboCaracteristicas.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboCaracteristicas
        Dim results() As Object = Me.Invoke("GetComboCaracteristicas", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboCaracteristicas.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboModelosCajero", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboModelosCajero() As IAC.ContractoServicio.Utilidad.GetComboModelosCajero.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboModelosCajero
        Dim results() As Object = Me.Invoke("GetComboModelosCajero", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboModelosCajero.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboRedes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboRedes() As IAC.ContractoServicio.Utilidad.GetComboRedes.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboRedes
        Dim results() As Object = Me.Invoke("GetComboRedes", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboRedes.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function Test() As IAC.ContractoServicio.Test.Respuesta Implements IAC.ContractoServicio.IUtilidad.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboAplicaciones", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboAplicaciones() As IAC.ContractoServicio.Utilidad.getComboAplicaciones.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboAplicaciones
        Dim results() As Object = Me.Invoke("GetComboAplicaciones", New Object() {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.getComboAplicaciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboNivelesParametros", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboNivelesParametros(objPeticion As IAC.ContractoServicio.Utilidad.GetComboNivelesParametros.Peticion) As IAC.ContractoServicio.Utilidad.GetComboNivelesParametros.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboNivelesParametros
        Dim results() As Object = Me.Invoke("GetComboNivelesParametros", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboNivelesParametros.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboCaractTipoSector", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboCaractTipoSector() As IAC.ContractoServicio.Utilidad.GetComboCaractTipoSector.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboCaractTipoSector
        Dim results() As Object = Me.Invoke("GetComboCaractTipoSector", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboCaractTipoSector.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetConfigNivelSaldo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetConfigNivelSaldo(Peticion As IAC.ContractoServicio.Utilidad.GetConfigNivel.Peticion) As IAC.ContractoServicio.Utilidad.GetConfigNivel.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetConfigNivelSaldo
        Dim results() As Object = Me.Invoke("GetConfigNivelSaldo", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetConfigNivel.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboTiposSubCliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboTiposSubCliente() As IAC.ContractoServicio.Utilidad.GetComboTiposSubCliente.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboTiposSubCliente
        Dim results() As Object = Me.Invoke("GetComboTiposSubCliente", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboTiposSubCliente.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboTiposPuntoServicio", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboTiposPuntoServicio() As IAC.ContractoServicio.Utilidad.GetComboTiposPuntoServicio.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboTiposPuntoServicio
        Dim results() As Object = Me.Invoke("GetComboTiposPuntoServicio", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboTiposPuntoServicio.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboTiposProcedencia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboTiposProcedencia() As IAC.ContractoServicio.Utilidad.GetComboTiposProcedencia.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboTiposProcedencia
        Dim results() As Object = Me.Invoke("GetComboTiposProcedencia", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboTiposProcedencia.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoCliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarCodigoCliente(objPeticion As IAC.ContractoServicio.Utilidad.VerificarCodigoCliente.Peticion) As IAC.ContractoServicio.Utilidad.VerificarCodigoCliente.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarCodigoCliente
        Dim results() As Object = Me.Invoke("VerificarCodigoCliente", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarCodigoCliente.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoPtoServicio", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarCodigoPtoServicio(objPeticion As IAC.ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Peticion) As IAC.ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarCodigoPtoServicio
        Dim results() As Object = Me.Invoke("VerificarCodigoPtoServicio", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoSubCliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarCodigoSubCliente(objPeticion As IAC.ContractoServicio.Utilidad.VerificarCodigoSubCliente.Peticion) As IAC.ContractoServicio.Utilidad.VerificarCodigoSubCliente.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarCodigoSubCliente
        Dim results() As Object = Me.Invoke("VerificarCodigoSubCliente", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarCodigoSubCliente.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionCliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarDescripcionCliente(objPeticion As IAC.ContractoServicio.Utilidad.VerificarDescripcionCliente.Peticion) As IAC.ContractoServicio.Utilidad.VerificarDescripcionCliente.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarDescripcionCliente
        Dim results() As Object = Me.Invoke("VerificarDescripcionCliente", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarDescripcionCliente.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionPtoServicio", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarDescripcionPtoServicio(objPeticion As IAC.ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Peticion) As IAC.ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarDescripcionPtoServicio
        Dim results() As Object = Me.Invoke("VerificarDescripcionPtoServicio", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarDescripcionSubCliente", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarDescripcionSubCliente(objPeticion As IAC.ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Peticion) As IAC.ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarDescripcionSubCliente
        Dim results() As Object = Me.Invoke("VerificarDescripcionSubCliente", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoAccesoDenominacion", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarCodigoAccesoDenominacion(Peticion As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Peticion) As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarCodigoAccesoDenominacion
        Dim results() As Object = Me.Invoke("VerificarCodigoAccesoDenominacion", New Object() {Peticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoAccesoDivisa", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarCodigoAccesoDivisa(Peticion As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Peticion) As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarCodigoAccesoDivisa
        Dim results() As Object = Me.Invoke("VerificarCodigoAccesoDivisa", New Object() {Peticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/VerificarCodigoAccesoMedioPago", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function VerificarCodigoAccesoMedioPago(Peticion As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Peticion) As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Respuesta Implements IAC.ContractoServicio.IUtilidad.VerificarCodigoAccesoMedioPago
        Dim results() As Object = Me.Invoke("VerificarCodigoAccesoMedioPago", New Object() {Peticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboSectores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboSectores(objPeticion As IAC.ContractoServicio.Utilidad.GetComboSectores.Peticion) As IAC.ContractoServicio.Utilidad.GetComboSectores.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboSectores
        Dim results() As Object = Me.Invoke("GetComboSectores", New Object() {objPeticion})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboSectores.Respuesta)
    End Function


    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboTiposCuenta", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboTiposCuenta() As IAC.ContractoServicio.Utilidad.GetComboTiposCuenta.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboTiposCuenta
        Dim results() As Object = Me.Invoke("GetComboTiposCuenta", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboTiposCuenta.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetComboTiposCuenta", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
    Public Function GetComboTiposPeriodo() As IAC.ContractoServicio.Utilidad.GetComboTiposPeriodo.Respuesta Implements IAC.ContractoServicio.IUtilidad.GetComboTiposPeriodo
        Dim results() As Object = Me.Invoke("GetComboTiposPeriodo", New Object(-1) {})
        Return CType(results(0), IAC.ContractoServicio.Utilidad.GetComboTiposPeriodo.Respuesta)
    End Function
End Class