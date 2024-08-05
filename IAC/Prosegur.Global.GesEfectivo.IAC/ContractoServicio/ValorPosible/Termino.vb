Namespace ValorPosible

    ''' <summary>
    ''' Classe Termino
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class Termino

#Region "[Variáveis]"

        Private _Codigo As String
        Private _ValoresPosibles As ValorPosibleColeccion

#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property ValoresPosibles() As ValorPosibleColeccion
            Get
                Return _ValoresPosibles
            End Get
            Set(value As ValorPosibleColeccion)
                _ValoresPosibles = value
            End Set
        End Property

#End Region

    End Class

End Namespace