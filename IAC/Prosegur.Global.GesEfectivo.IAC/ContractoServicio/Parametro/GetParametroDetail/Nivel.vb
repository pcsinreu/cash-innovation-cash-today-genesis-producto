Namespace Parametro.GetParametroDetail
    <Serializable()> _
    Public Class Nivel

#Region "Variáveis"
        Private _CodigoNivel As String
        Private _DescripcionNivel As String
#End Region

#Region "Propriedades"
        Public Property CodigoNivel() As String
            Get
                Return _CodigoNivel
            End Get
            Set(value As String)
                _CodigoNivel = value
            End Set
        End Property

        Public Property DescripcionNivel() As String
            Get
                Return _DescripcionNivel
            End Get
            Set(value As String)
                _DescripcionNivel = value
            End Set
        End Property
#End Region
    End Class
End Namespace
