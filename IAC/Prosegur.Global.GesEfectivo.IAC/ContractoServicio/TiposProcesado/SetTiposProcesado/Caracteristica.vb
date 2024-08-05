Namespace TiposProcesado.SetTiposProcesado

    <Serializable()> _
    Public Class Caracteristica

#Region "Variáveis"

        Private _Codigo As String

#End Region

#Region "Propriedades"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

#End Region
    End Class
End Namespace