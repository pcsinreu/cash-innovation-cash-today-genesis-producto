Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.Conteo
Imports Prosegur.Global
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.ContractoServicio

Namespace ProxyWS.Dashboard

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="Dashboard", [Namespace]:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos")> _
    Partial Public Class ProxyDashboardMAE
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        Implements ContractoServicio.Interfaces.Dashboard.IDashboardMAE

        Public Sub New(ByVal urlServicio As String)
            MyBase.New()
            Me.Url = urlServicio & "DashboardMAE.asmx"
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE/RetornaSaldoTodasMAEPorDelegacion", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaSaldoTodasMAEPorDelegacion(Peticion As Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Peticion) As Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Respuesta Implements Interfaces.Dashboard.IDashboardMAE.RetornaSaldoTodasMAEPorDelegacion
            Dim results() As Object = Me.Invoke("RetornaSaldoTodasMAEPorDelegacion", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE/RetornaCantidadMAESPorClientes", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaCantidadMAESPorClientes(Peticion As Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Peticion) As Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Respuesta Implements Interfaces.Dashboard.IDashboardMAE.RetornaCantidadMAESPorClientes
            Dim results() As Object = Me.Invoke("RetornaCantidadMAESPorClientes", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE/RetornaSaldoMAEPorCliente", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaSaldoMAEPorCliente(Peticion As Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Peticion) As Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Respuesta Implements Interfaces.Dashboard.IDashboardMAE.RetornaSaldoMAEPorCliente
            Dim results() As Object = Me.Invoke("RetornaSaldoMAEPorCliente", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE/RetornaSaldoClienteMAEDetallado", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardMAE", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaSaldoClienteMAEDetallado(Peticion As Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.Peticion) As Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.Respuesta Implements Interfaces.Dashboard.IDashboardMAE.RetornaSaldoClienteMAEDetallado
            Dim results() As Object = Me.Invoke("RetornaSaldoClienteMAEDetallado", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.Respuesta)
        End Function

    End Class

End Namespace