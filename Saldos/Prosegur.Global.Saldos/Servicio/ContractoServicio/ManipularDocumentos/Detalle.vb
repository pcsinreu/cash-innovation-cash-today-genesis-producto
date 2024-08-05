Namespace ManipularDocumentos

    ''' <summary>
    ''' Detalle
    ''' </summary>
    <Serializable()> _
    Public Class Detalle

#Region "[VARIÁVEIS]"

        Private _IdMoneda As String
        Private _IdEspecie As String
        Private _Cantidad As Integer
        Private _Importe As Decimal

#End Region

#Region "[PROPRIEDADES]"

        Public Property IdMoneda() As String
            Get
                Return _IdMoneda
            End Get
            Set(value As String)
                _IdMoneda = value
            End Set
        End Property

        Public Property IdEspecie() As String
            Get
                Return _IdEspecie
            End Get
            Set(value As String)
                _IdEspecie = value
            End Set
        End Property

        Public Property Cantidad() As Integer
            Get
                Return _Cantidad
            End Get
            Set(value As Integer)
                _Cantidad = value
            End Set
        End Property

        Public Property Importe() As Decimal
            Get
                Return _Importe
            End Get
            Set(value As Decimal)
                _Importe = value
            End Set
        End Property

#End Region

    End Class

End Namespace