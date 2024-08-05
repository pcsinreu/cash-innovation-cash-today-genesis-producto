Namespace Clases.Abono

    <Serializable()>
    Public Class MedioPagoAbono
        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property TipoMedioPago As Enumeradores.TipoMedioPago
        Public Property DescripcionTipoMedioPago As String
        Public Property TipoValor As Enumeradores.TipoValor
        Public Property Importe As Double
        Public Property Cantidad As Int64
        Public Property IdentificadorUnidadeMedida As String
        Public Property TipoNivelDetalle As Enumeradores.TipoNivelDetalhe
        Public Property UsuarioCreacion As String
        Public Property UsuarioModificacion As String
    End Class

End Namespace
