Namespace DetalleParciales.GetDetalleParciales

    Public Class IAC

#Region " Variáveis "

        Private _CodigoTermino As String = String.Empty
        Private _Descricao As String = String.Empty
        Private _Valor As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property CodigoTermino() As String
            Get
                Return _CodigoTermino
            End Get
            Set(value As String)
                _CodigoTermino = value
            End Set
        End Property

        Public Property Descricao() As String
            Get
                Return _Descricao
            End Get
            Set(value As String)
                _Descricao = value
            End Set
        End Property

        Public Property Valor() As String
            Get
                Return _Valor
            End Get
            Set(value As String)
                _Valor = value
            End Set
        End Property

#End Region

    End Class

End Namespace
