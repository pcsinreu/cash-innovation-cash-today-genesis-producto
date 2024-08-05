Namespace RespaldoCompleto.GetRespaldosCompletos

    Public Class Sobre

#Region " Variáveis "

        Private _ParcialesContados As Decimal = 0
        Private _ParcialesIngresados As Nullable(Of Decimal) = Nothing
        Private _Parcial As String = String.Empty
        Private _Sucursal As String = String.Empty
        Private _DescricionSucursal As String = String.Empty
        Private _F22 As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property ParcialesContados() As Decimal
            Get
                Return _ParcialesContados
            End Get
            Set(value As Decimal)
                _ParcialesContados = value
            End Set
        End Property

        Public Property ParcialesIngresados() As Nullable(Of Decimal)
            Get
                Return _ParcialesIngresados
            End Get
            Set(value As Nullable(Of Decimal))
                _ParcialesIngresados = value
            End Set
        End Property

        Public Property Parcial As String
            Get
                Return _Parcial
            End Get
            Set(value As String)
                _Parcial = value
            End Set
        End Property

        Public Property Sucursal() As String
            Get
                Return _Sucursal
            End Get
            Set(value As String)
                _Sucursal = value
            End Set
        End Property

        Public Property DescricionSucursal() As String
            Get
                Return _DescricionSucursal
            End Get
            Set(value As String)
                _DescricionSucursal = value
            End Set
        End Property

        Public Property F22() As String
            Get
                Return _F22
            End Get
            Set(value As String)
                _F22 = value
            End Set
        End Property

#End Region

    End Class

End Namespace
