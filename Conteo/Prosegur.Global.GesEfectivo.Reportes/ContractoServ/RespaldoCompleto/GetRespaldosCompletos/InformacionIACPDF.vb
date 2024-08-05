Namespace RespaldoCompleto.GetRespaldosCompletos

    Public Class InformarcionIACPDF

#Region " Variáveis "

        Private _Parcial As String = String.Empty
        Private _Descricao As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property Parcial() As Object
            Get
                Return _Parcial
            End Get
            Set(value As Object)
                _Parcial = value
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

#End Region

    End Class

End Namespace
