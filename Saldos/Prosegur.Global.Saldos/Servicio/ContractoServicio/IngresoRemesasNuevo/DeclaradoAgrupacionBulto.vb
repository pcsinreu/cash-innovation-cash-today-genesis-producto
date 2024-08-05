Namespace IngresoRemesasNuevo

    <Serializable()> _
    Public Class DeclaradoAgrupacionBulto

#Region "[VARIAVEIS]"

        Private _CodigoAgrupacion As String
        Private _NumImporte As Nullable(Of Decimal)
        Private _CodCaracteristicaConteo As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodCaracteristicaConteo() As String
            Get
                Return _CodCaracteristicaConteo
            End Get
            Set(value As String)
                _CodCaracteristicaConteo = value
            End Set
        End Property

        Public Property CodigoAgrupacion() As String
            Get
                Return _CodigoAgrupacion
            End Get
            Set(value As String)
                _CodigoAgrupacion = value
            End Set
        End Property

        Public Property NumImporte() As Nullable(Of Decimal)
            Get
                Return _NumImporte
            End Get
            Set(value As Nullable(Of Decimal))
                _NumImporte = value
            End Set
        End Property

#End Region

    End Class

End Namespace
