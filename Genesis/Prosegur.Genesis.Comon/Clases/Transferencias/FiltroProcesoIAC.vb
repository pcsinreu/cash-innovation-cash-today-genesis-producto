Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Documentos.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroProcesoIAC

        Public Property codigoCliente As String
        Public Property codigoSubCliente As String
        Public Property codigoPuntoServicio As String
        Public Property codigoSubCanal As String
        Public Property codigoDelegacion As String
        Public Property codigoDelegacionCentral As String
        Public Property grupoTerminosIAC_Remesa As Clases.GrupoTerminosIAC
        Public Property grupoTerminosIAC_Bulto As Clases.GrupoTerminosIAC
        Public Property grupoTerminosIAC_Parcial As Clases.GrupoTerminosIAC

    End Class

End Namespace

