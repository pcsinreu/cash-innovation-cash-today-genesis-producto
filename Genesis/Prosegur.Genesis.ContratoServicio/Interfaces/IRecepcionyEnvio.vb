Imports Prosegur.Genesis.ContractoServicio.Documento
Imports Prosegur.Genesis.ContractoServicio.Ruta

Namespace Interfaces
    Public Interface IRecepcionyEnvio

        Function AlocarDesalocarDocumento(peticion As AlocarDesalocarDocumento.Peticion) As AlocarDesalocarDocumento.Respuesta
        Function AlocarPedidosExternos(peticion As AlocarPedidosExternos.Peticion) As AlocarPedidosExternos.Respuesta
        Function GrabarDocumentosSalidas(peticion As GrabarDocumentosSalidas.Peticion) As GrabarDocumentosSalidas.Respuesta
        Function GrabarDocumentosSalidasConIntegracionSol(peticion As GrabarDocumentosSalidasConIntegracionSol.Peticion) As GrabarDocumentosSalidasConIntegracionSol.Respuesta
        Function GrabarIngresoDocumentosSaldosySol(peticion As GrabarIngresoDocumentosSaldosySol.Peticion) As GrabarIngresoDocumentosSaldosySol.Respuesta
        Function GrabaryReenviarGrupoDocumentos(peticion As GrabaryReenviarGrupoDocumentos.Peticion) As GrabaryReenviarGrupoDocumentos.Respuesta
        Function ObtenerDocumentosNoAlocados(peticion As ObtenerDocumentosNoAlocados.Peticion) As ObtenerDocumentosNoAlocados.Respuesta
        Function ObtenerRutas(peticion As ObtenerRutas.Peticion) As ObtenerRutas.Respuesta
        Function CambiarEstadoDocumentoContenedor(peticion As CambiarEstadoDocumentoContenedor.Peticion) As CambiarEstadoDocumentoContenedor.Respuesta
        Function Test() As ContractoServicio.Test.Respuesta

    End Interface

End Namespace
