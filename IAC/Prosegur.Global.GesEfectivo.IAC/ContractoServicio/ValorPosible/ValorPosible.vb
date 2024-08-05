Namespace ValorPosible

    ''' <summary>
    ''' Classe ValorPosible
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class ValorPosible

        Private _Codigo As String
        Private _Descripcion As String
        Private _Vigente As Nullable(Of Boolean)
        Private _esValorDefecto As Nullable(Of Boolean)

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
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

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _Vigente = value
            End Set
        End Property

        Public Property esValorDefecto() As Nullable(Of Boolean)
            Get
                Return _esValorDefecto
            End Get
            Set(value As Nullable(Of Boolean))
                _esValorDefecto = value
            End Set
        End Property

    End Class

End Namespace