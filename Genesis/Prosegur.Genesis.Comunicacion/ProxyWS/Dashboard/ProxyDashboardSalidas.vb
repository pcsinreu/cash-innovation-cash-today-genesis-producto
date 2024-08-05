Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.Conteo
Imports Prosegur.Global
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.ContractoServicio

Namespace ProxyWS.Dashboard

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="DashboardSalidas", [Namespace]:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas")> _
    Partial Public Class ProxyDashboardSalidas
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        Implements ContractoServicio.Interfaces.Dashboard.IDashboardSalidas

        Public Sub New(ByVal urlServicio As String)
            MyBase.New()
            Me.Url = urlServicio & "DashboardSalidas.asmx"
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas/RetornaCantidadBilletesContadosPorSector", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadBilletesContadosPorSector(Peticion As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesContadosPorSector.Peticion) As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesContadosPorSector.Respuesta Implements Interfaces.Dashboard.IDashboardSalidas.RetornaCantidadBilletesContadosPorSector
            Dim results() As Object = Me.Invoke("RetornaCantidadBilletesContadosPorSector", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesContadosPorSector.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas/RetornaCantidadBilletesDelDia", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadBilletesDelDia(Peticion As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesUltimaHora.Peticion) As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesUltimaHora.Respuesta Implements Interfaces.Dashboard.IDashboardSalidas.RetornaCantidadBilletesUltimaHora
            Dim results() As Object = Me.Invoke("RetornaCantidadBilletesDelDia", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesUltimaHora.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas/RetornaCantidadContadoPorDenominacion", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadContadoPorDenominacion(Peticion As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadContadoPorDenominacion.Peticion) As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadContadoPorDenominacion.Respuesta Implements Interfaces.Dashboard.IDashboardSalidas.RetornaCantidadContadoPorDenominacion
            Dim results() As Object = Me.Invoke("RetornaCantidadContadoPorDenominacion", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadContadoPorDenominacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas/RetornaCantidadRemesasPorSector", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSalidas", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadRemesasPorSector(Peticion As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadRemesasPorSector.Peticion) As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadRemesasPorSector.Respuesta Implements Interfaces.Dashboard.IDashboardSalidas.RetornaCantidadRemesasPorSector
            Dim results() As Object = Me.Invoke("RetornaCantidadRemesasPorSector", New Object() {Peticion})
            Return CType(results(0), ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadRemesasPorSector.Respuesta)
        End Function

    End Class

End Namespace