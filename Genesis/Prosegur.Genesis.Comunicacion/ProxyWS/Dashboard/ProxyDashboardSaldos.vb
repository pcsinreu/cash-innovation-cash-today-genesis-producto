Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.Conteo
Imports Prosegur.Global
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.ContractoServicio

Namespace ProxyWS.Dashboard

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="Dashboard", [Namespace]:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos")> _
    Partial Public Class ProxyDashboardSaldos
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        Implements ContractoServicio.Interfaces.Dashboard.IDashboardSaldos

        Public Sub New(ByVal urlServicio As String)
            MyBase.New()
            Me.Url = urlServicio & "DashboardSaldos.asmx"
        End Sub


        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos/RetornaBilletajexSector", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaBilletajexSector(Peticion As Contractos.GenesisSaldos.Saldo.RetornaBilletajexSector.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaBilletajexSector.Respuesta Implements Interfaces.Dashboard.IDashboardSaldos.RetornaBilletajexSector
            Dim results() As Object = Me.Invoke("RetornaBilletajexSector", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Saldo.RetornaBilletajexSector.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos/RetornaRankingClientes", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaRankingClientes(Peticion As Contractos.GenesisSaldos.Saldo.RetornaRankingClientes.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaRankingClientes.Respuesta Implements Interfaces.Dashboard.IDashboardSaldos.RetornaRankingClientes
            Dim results() As Object = Me.Invoke("RetornaRankingClientes", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Saldo.RetornaRankingClientes.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos/RetornaSaldoDisponivelEfetivo", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaSaldoDisponivelEfetivo(Peticion As Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelEfetivo.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelEfetivo.Respuesta Implements Interfaces.Dashboard.IDashboardSaldos.RetornaSaldoDisponivelEfetivo
            Dim results() As Object = Me.Invoke("RetornaSaldoDisponivelEfetivo", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelEfetivo.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos/RetornaSaldoDisponivelMedioPago", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaSaldoDisponivelMedioPago(Peticion As Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelMedioPago.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelMedioPago.Respuesta Implements Interfaces.Dashboard.IDashboardSaldos.RetornaSaldoDisponivelMedioPago
            Dim results() As Object = Me.Invoke("RetornaSaldoDisponivelMedioPago", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelMedioPago.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos/RetornaSaldosCliente", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardSaldos", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaSaldosCliente(Peticion As Contractos.GenesisSaldos.Saldo.RetornaSaldosCliente.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaSaldosCliente.Respuesta Implements Interfaces.Dashboard.IDashboardSaldos.RetornaSaldosCliente
            Dim results() As Object = Me.Invoke("RetornaSaldosCliente", New Object() {Peticion})
            Return CType(results(0), Contractos.GenesisSaldos.Saldo.RetornaSaldosCliente.Respuesta)
        End Function


    End Class

End Namespace