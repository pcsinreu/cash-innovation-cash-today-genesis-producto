Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.Conteo
Imports Prosegur.Global
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.ContractoServicio

Namespace ProxyWS.Dashboard

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="Dashboard", [Namespace]:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo")> _
    Partial Public Class ProxyDashboardConteo
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        Implements ContractoServicio.Interfaces.Dashboard.IDashboardConteo

        Public Sub New(ByVal urlServicio As String)
            MyBase.New()
            Me.Url = urlServicio & "DashboardConteo.asmx"
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo/RetornaCantidadBilletesContadosPorSector", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadBilletesContadosPorSector(Peticion As Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector.Respuesta Implements Interfaces.Dashboard.IDashboardConteo.RetornaCantidadBilletesContadosPorSector
            Dim results() As Object = Me.Invoke("RetornaCantidadBilletesContadosPorSector", New Object() {Peticion})
            Return CType(results(0), Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo/RetornaCantidadBilletesContadosPorSector8Horas", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadBilletesContadosPorSector8Horas(Peticion As Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector8Horas.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector8Horas.Respuesta Implements Interfaces.Dashboard.IDashboardConteo.RetornaCantidadBilletesContadosPorSector8Horas
            Dim results() As Object = Me.Invoke("RetornaCantidadBilletesContadosPorSector8Horas", New Object() {Peticion})
            Return CType(results(0), Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector8Horas.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo/RetornaCantidadBilletesDoDia", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadBilletesDoDia(Peticion As Contractos.Conteo.Remesa.RetornaCantidadBilletesUltimaHora.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadBilletesUltimaHora.Respuesta Implements Interfaces.Dashboard.IDashboardConteo.RetornaCantidadBilletesUltimaHora
            Dim results() As Object = Me.Invoke("RetornaCantidadBilletesDoDia", New Object() {Peticion})
            Return CType(results(0), Contractos.Conteo.Remesa.RetornaCantidadBilletesUltimaHora.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo/RetornaCantidadContadoPorDenominacion", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadContadoPorDenominacion(Peticion As Contractos.Conteo.Remesa.RetornaCantidadContadoPorDenominacion.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadContadoPorDenominacion.Respuesta Implements Interfaces.Dashboard.IDashboardConteo.RetornaCantidadContadoPorDenominacion
            Dim results() As Object = Me.Invoke("RetornaCantidadContadoPorDenominacion", New Object() {Peticion})
            Return CType(results(0), Contractos.Conteo.Remesa.RetornaCantidadContadoPorDenominacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo/RetornaCantidadRemesasPorSector", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadRemesasPorSector(Peticion As Contractos.Conteo.Remesa.RetornaCantidadRemesasPorSector.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadRemesasPorSector.Respuesta Implements Interfaces.Dashboard.IDashboardConteo.RetornaCantidadRemesasPorSector
            Dim results() As Object = Me.Invoke("RetornaCantidadRemesasPorSector", New Object() {Peticion})
            Return CType(results(0), Contractos.Conteo.Remesa.RetornaCantidadRemesasPorSector.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo/RetornaSomaValoresProcesadosCliente", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardConteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaSomaValoresProcesadosCliente(Peticion As Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Peticion) As Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Respuesta Implements Interfaces.Dashboard.IDashboardConteo.RetornaSomaValoresProcesadosCliente
            Dim results() As Object = Me.Invoke("RetornaSomaValoresProcesadosCliente", New Object() {Peticion})
            Return CType(results(0), Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Respuesta)
        End Function

    End Class

End Namespace