Namespace IngresoRemesasNuevo

    <Serializable()> _
    Public Class DeclaradoAgrupacionParcial

#Region "[VARIAVEIS]"

        Private _CodigoAgrupacion As String
        Private _NumImporte As Nullable(Of Decimal)

#End Region

#Region "[PROPRIEDADES]"

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
