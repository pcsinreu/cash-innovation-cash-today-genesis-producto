Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace Interfaces.Dashboard
    Public Interface IDashboardSaldos
#Region "Dashboard Saldos"
        Function RetornaSaldoDisponivelEfetivo(Peticion As Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelEfetivo.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelEfetivo.Respuesta
        Function RetornaSaldoDisponivelMedioPago(Peticion As Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelMedioPago.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaSaldoDisponivelMedioPago.Respuesta
        Function RetornaSaldosCliente(Peticion As Contractos.GenesisSaldos.Saldo.RetornaSaldosCliente.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaSaldosCliente.Respuesta
        Function RetornaBilletajexSector(Peticion As Contractos.GenesisSaldos.Saldo.RetornaBilletajexSector.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaBilletajexSector.Respuesta
        Function RetornaRankingClientes(Peticion As Contractos.GenesisSaldos.Saldo.RetornaRankingClientes.Peticion) As Contractos.GenesisSaldos.Saldo.RetornaRankingClientes.Respuesta
#End Region
    End Interface

End Namespace