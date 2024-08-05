Namespace RespaldoCompleto.GetRespaldosCompletos

    Public Class Observacion

#Region " Variáveis "

        Private _Parcial As String = String.Empty
        Private _Descripcion As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property Parcial() As String
            Get
                Return _Parcial
            End Get
            Set(value As String)
                _Parcial = value
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
