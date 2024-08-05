Namespace DetalleParciales.GetDetalleParciales

    Public Class Declarado

#Region " Variáveis "

        Private _TipoDeclarado As String = String.Empty
        Private _Divisa As String = String.Empty
        Private _ImporteTotal As Decimal = 0

#End Region

#Region " Propriedades "

        Public Property TipoDeclarado() As String
            Get
                Return _TipoDeclarado
            End Get
            Set(value As String)
                _TipoDeclarado = value
            End Set
        End Property

        Public Property Divisa() As String
            Get
                Return _Divisa
            End Get
            Set(value As String)
                _Divisa = value
            End Set
        End Property

        Public Property ImporteTotal() As Decimal
            Get
                Return _ImporteTotal
            End Get
            Set(value As Decimal)
                _ImporteTotal = value
            End Set
        End Property

#End Region

    End Class

End Namespace
