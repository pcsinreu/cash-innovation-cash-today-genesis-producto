Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace Interfaces.Dashboard
    Public Interface IDashboardSalidas
#Region "Dashboard Salidas"

        Function RetornaCantidadRemesasPorSector(Peticion As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadRemesasPorSector.Peticion) As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadRemesasPorSector.Respuesta
        Function RetornaCantidadBilletesContadosPorSector(Peticion As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesContadosPorSector.Peticion) As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesContadosPorSector.Respuesta
        Function RetornaCantidadContadoPorDenominacion(Peticion As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadContadoPorDenominacion.Peticion) As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadContadoPorDenominacion.Respuesta
        Function RetornaCantidadBilletesUltimaHora(Peticion As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesUltimaHora.Peticion) As ContractoServicio.NuevoSalidas.Remesa.RetornaCantidadBilletesUltimaHora.Respuesta
#End Region
    End Interface

End Namespace