Namespace RecuperarRemesasPorGrupo

    Public Class Grupo

#Region "[VARIAVEIS]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Vigente As Boolean
        Private _Transacciones As Transacciones

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
        Public Property Vigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
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
        Public Property Transacciones() As Transacciones
            Get
                Return _Transacciones
            End Get
            Set(value As Transacciones)
                _Transacciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace