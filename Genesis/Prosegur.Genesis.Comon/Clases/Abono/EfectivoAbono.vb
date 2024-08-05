Namespace Clases.Abono

    <Serializable()>
    Public Class EfectivoAbono
        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property EsBillete As Boolean
        Public Property TipoValor As Enumeradores.TipoValor
        Public Property TipoNivelDetalle As Enumeradores.TipoNivelDetalhe
        Public Property Valor As Decimal
        Public Property Peso As Decimal
        Public Property Cantidad As Int64
        Public Property Importe As Double
        Public Property IdentificadorUnidadeMedida As String
        Public Property IdentificadorCalidad As String
        Public Property UsuarioCreacion As String
        Public Property UsuarioModificacion As String
    End Class

End Namespace
