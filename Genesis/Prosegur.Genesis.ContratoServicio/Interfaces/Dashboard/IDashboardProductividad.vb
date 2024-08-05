Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace Interfaces.Dashboard
    Public Interface IDashboardProductividad

        Function RetornaValoresProcesados(Peticion As Contractos.Dashboard.Productividad.RetornaValoresProcesados.Peticion) As Contractos.Dashboard.Productividad.RetornaValoresProcesados.Respuesta
        Function RetornaValoresProcesadosPorHora(Peticion As Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Peticion) As Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Respuesta

    End Interface

End Namespace