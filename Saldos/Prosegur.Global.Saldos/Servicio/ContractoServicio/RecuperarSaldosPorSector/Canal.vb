Namespace RecuperarSaldosPorSector

    Public Class Canal

#Region "[VARIAVEIS]"

        Private _IdPS As String
        Private _Descripcion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property IdPS() As String
            Get
                Return _IdPS
            End Get
            Set(value As String)
                _IdPS = value
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

#End Region

    End Class

End Namespace