Namespace RespaldoCompleto.GetRespaldosCompletos

    Public Class InformarcionIAC

#Region " Variáveis "

        Private _Descricao As String = String.Empty
        Private _Valor As Object

#End Region

#Region " Propriedades "

        Public Property Descricao() As String
            Get
                Return _Descricao
            End Get
            Set(value As String)
                _Descricao = value
            End Set
        End Property

        Public Property Valor() As Object
            Get
                Return _Valor
            End Get
            Set(value As Object)
                _Valor = value
            End Set
        End Property

#End Region

    End Class

End Namespace
