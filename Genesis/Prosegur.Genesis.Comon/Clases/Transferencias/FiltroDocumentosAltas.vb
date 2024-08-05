Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Documentos.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroDocumentosAltas

        Public Property remesas As List(Of FiltroRemesaAltas)
        Public Property CodigoUsuario() As String

    End Class

    <Serializable()>
    Public Class FiltroRemesaAltas

        Public Property CodigoDelegacion() As String
        Public Property CodigoPlanta() As String
        Public Property CodigoSector() As String
        Public Property CodigoCliente() As String
        Public Property CodigoSubCliente() As String
        Public Property CodigoPuntoServicio() As String
        Public Property CodigoCanal() As String
        Public Property CodigoSubCanal() As String
        Public Property CodigoExterno() As String

    End Class

End Namespace
