Namespace BilletajeSucursal.GetBilletajesSucursais

    Public Class Respaldo

#Region " Variáveis "

        Private _NumParcialRecontado As Decimal = 0
        Private _NumParcialDeclarado As Decimal = 0

#End Region

#Region " Propriedades "

        Public Property NumParcialRecontado() As Decimal
            Get
                Return _NumParcialRecontado
            End Get
            Set(value As Decimal)
                _NumParcialRecontado = value
            End Set
        End Property

        Public Property NumParcialDeclarado() As Decimal
            Get
                Return _NumParcialDeclarado
            End Get
            Set(value As Decimal)
                _NumParcialDeclarado = value
            End Set
        End Property

#End Region

    End Class

End Namespace
