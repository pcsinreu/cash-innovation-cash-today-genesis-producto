Namespace RecuperarRemesasPorGrupo

    Public Class Moneda

#Region "[VARIAVEIS]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Importe As Decimal
        Private _Especies As Especies

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
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
        Public Property Especies() As Especies
            Get
                Return _Especies
            End Get
            Set(value As Especies)
                _Especies = value
            End Set
        End Property

#End Region

    End Class

End Namespace