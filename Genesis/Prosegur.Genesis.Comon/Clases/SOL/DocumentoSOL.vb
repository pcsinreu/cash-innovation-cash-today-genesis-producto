Namespace SOL

    <Serializable()>
    Public Class DocumentoSOL

        Public Property documentoID() As String
        Public Property fechaEntradaDocumento() As DateTime
        Public Property localizacionCodigo() As Integer
        Public Property numeroSerie() As String
        Public Property numero() As String
        Public Property documentoClienteCodigo() As String
        Public Property observacion() As String
        Public Property documentoImporte() As List(Of DocumentoImporte)
        Public Property bultos() As List(Of BultoSOL)
        Public Property documentoItens() As List(Of DocumentoItem)

    End Class

End Namespace