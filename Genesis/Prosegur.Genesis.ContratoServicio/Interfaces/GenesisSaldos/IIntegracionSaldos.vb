Namespace Interfaces
    Public Interface IIntegracionSaldos

        Function Test() As Test.Respuesta
        Function crearDocumentoReenvio(peticion As Contractos.Integracion.crearDocumentoReenvio.Peticion) As Contractos.Integracion.crearDocumentoReenvio.Respuesta

        Function GrabarReciboTransporteManual(peticion As Contractos.Integracion.GrabarReciboTransporteManual.Peticion) As Contractos.Integracion.GrabarReciboTransporteManual.Respuesta

        Function generarCertificado(peticion As Contractos.Integracion.generarCertificado.Peticion) As Contractos.Integracion.generarCertificado.Respuesta

    End Interface

End Namespace


