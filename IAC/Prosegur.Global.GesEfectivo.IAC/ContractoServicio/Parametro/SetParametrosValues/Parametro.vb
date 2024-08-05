Namespace Parametro.SetParametrosValues
    <Serializable()> _
    Public Class Parametro

#Region "Variáveis"
        Private _CodigoParametro As String
        Private _ValorParametro As String
#End Region

#Region "Propriedades"
        Public Property CodigoParametro() As String
            Get
                Return _CodigoParametro
            End Get
            Set(value As String)
                _CodigoParametro = value
            End Set
        End Property

        Public Property ValorParametro() As String
            Get
                Return _ValorParametro
            End Get
            Set(value As String)
                _ValorParametro = value
            End Set
        End Property
#End Region
    End Class
End Namespace