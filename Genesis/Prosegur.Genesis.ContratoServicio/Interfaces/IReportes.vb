Imports Prosegur.Genesis.ContractoServicio

Namespace Interfaces
    Public Interface IReportes
        Function Test() As ContractoServicio.Test.Respuesta

        Function GrabarRecepcionRuta(Peticion As Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaPeticion) As Contractos.Reportes.GrabarRecepcionRuta.GrabarRecepcionRutaRespuesta

        Function GrabarTraspaseResponsabilidad(Peticion As Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadPeticion) As Contractos.Reportes.GrabarTraspaseResponsabilidad.GrabarTraspaseResponsabilidadRespuesta

    End Interface

End Namespace

