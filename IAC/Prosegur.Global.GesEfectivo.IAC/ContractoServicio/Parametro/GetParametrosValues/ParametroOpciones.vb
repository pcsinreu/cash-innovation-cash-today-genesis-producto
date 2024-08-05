Namespace Parametro.GetParametrosValues
    <Serializable()> _
    Public Class ParametroOpciones

#Region "Variáveis"
        Private _CodigoOpcion As String
        Private _DescriptionOpcion As String
#End Region
#Region "Propriedades"
        Public Property CodigoOpcion() As String
            Get
                Return _CodigoOpcion
            End Get
            Set(value As String)
                _CodigoOpcion = value
            End Set
        End Property

        Public Property DescriptionOpcion() As String
            Get
                Return _DescriptionOpcion
            End Get
            Set(value As String)
                _DescriptionOpcion = value
            End Set
        End Property
#End Region
    End Class
End Namespace