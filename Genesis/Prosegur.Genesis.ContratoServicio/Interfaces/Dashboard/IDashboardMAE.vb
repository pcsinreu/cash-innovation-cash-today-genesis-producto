Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace Interfaces.Dashboard
Public Interface IDashboardMAE
#Region "Dashboard MAE"
        Function RetornaSaldoTodasMAEPorDelegacion(Peticion As Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Peticion) As Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Respuesta
        Function RetornaCantidadMAESPorClientes(Peticion As Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Peticion) As Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Respuesta
        Function RetornaSaldoMAEPorCliente(Peticion As Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Peticion) As Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Respuesta
        Function RetornaSaldoClienteMAEDetallado(Peticion As Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.Peticion) As Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.Respuesta
#End Region
    End Interface

End Namespace