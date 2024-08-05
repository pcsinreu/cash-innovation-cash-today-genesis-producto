Namespace PantallaProceso

    ''' <summary>
    ''' Classe utilizada para manter terminos de um médio pago.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 23/03/2009 Criado
    ''' </history>    
    <Serializable()> _
    Public Class TerminoMedioPago

        Private _CodigoTermino As String
        Private _DescripcionTermino As String
        Private _Selecionado As Boolean
        Private _EsObligatorio As Boolean

        Public Property CodigoTermino() As String
            Get
                Return _CodigoTermino
            End Get
            Set(value As String)
                _CodigoTermino = value
            End Set
        End Property

        Public Property DescripcionTermino() As String
            Get
                Return _DescripcionTermino
            End Get
            Set(value As String)
                _DescripcionTermino = value
            End Set
        End Property

        Public Property Selecionado() As Boolean
            Get
                Return _Selecionado
            End Get
            Set(value As Boolean)
                _Selecionado = value
            End Set
        End Property

        Public Property EsObligatorio() As Boolean
            Get
                Return _EsObligatorio
            End Get
            Set(value As Boolean)
                _EsObligatorio = value
            End Set
        End Property
       
    End Class

End Namespace