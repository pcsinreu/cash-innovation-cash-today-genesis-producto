Namespace SOL

    <Serializable()>
    Public Class DocumentoItem

        Public Property tipoMercanciaCodigo() As String

        Public Property divisaCodigo() As String

        Public Property cantidad() As Integer

        Public Property valor() As Decimal

        Public Property codTipoEmbalaje() As String

        Public Property desgloses() As List(Of Desglose)

        Public Property bulto() As BultoSOL

        Public Property tipoMercanciaDescripcion() As String

    End Class

End Namespace
