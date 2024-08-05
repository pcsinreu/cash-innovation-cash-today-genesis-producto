Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace Interfaces.Dashboard
    Public Interface IDashboardConteo
#Region "Dashboard Conteo"
        Function RetornaCantidadRemesasPorSector(Peticion As Contractos.Conteo.Remesa.RetornaCantidadRemesasPorSector.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadRemesasPorSector.Respuesta
        Function RetornaCantidadBilletesContadosPorSector(Peticion As Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector.Respuesta
        Function RetornaCantidadContadoPorDenominacion(Peticion As Contractos.Conteo.Remesa.RetornaCantidadContadoPorDenominacion.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadContadoPorDenominacion.Respuesta
        Function RetornaCantidadBilletesUltimaHora(Peticion As Contractos.Conteo.Remesa.RetornaCantidadBilletesUltimaHora.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadBilletesUltimaHora.Respuesta
        Function RetornaCantidadBilletesContadosPorSector8Horas(Peticion As Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector8Horas.Peticion) As Contractos.Conteo.Remesa.RetornaCantidadBilletesContadosPorSector8Horas.Respuesta
        Function RetornaSomaValoresProcesadosCliente(Peticion As Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Peticion) As Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Respuesta
#End Region
    End Interface

End Namespace