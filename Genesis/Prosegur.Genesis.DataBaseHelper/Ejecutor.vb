Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon

Public Class Ejecutor
    Public Shared Sub GuardarGrupoDocumentosNuevo(GrupoDocumentos As GrupoDocumentos, usuario As String, Optional CodAplicacion As String = Constantes.CODIGO_APLICACION_GENESIS_SALDOS)
        Dim spw As SPWrapper = Colector.ColectarGrupoDocumentosNuevo(GrupoDocumentos, usuario, CodAplicacion)

        AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, True)

    End Sub

End Class
