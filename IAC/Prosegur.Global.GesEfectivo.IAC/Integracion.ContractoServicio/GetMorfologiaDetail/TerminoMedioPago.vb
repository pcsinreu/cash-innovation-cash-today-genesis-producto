
Namespace GetMorfologiaDetail

    ''' <summary>
    ''' Classe TerminoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 14/02/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class TerminoMedioPago

#Region "[Variáveis]"

        Private _oidTermino As String
        Private _codTermino As String
        Private _desTermino As String
        Private _bolVigente As Boolean
        Private _bolEsObligatorio As Boolean

#End Region

#Region "[Propriedades]"

        Public Property OidTermino As String
            Get
                Return _oidTermino
            End Get
            Set(value As String)
                _oidTermino = value
            End Set
        End Property

        Public Property CodTermino As String
            Get
                Return _codTermino
            End Get
            Set(value As String)
                _codTermino = value
            End Set
        End Property

        Public Property DesTermino As String
            Get
                Return _desTermino
            End Get
            Set(value As String)
                _desTermino = value
            End Set
        End Property

        Public Property BolVigente As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property BolEsObligatorio As Boolean
            Get
                Return _bolEsObligatorio
            End Get
            Set(value As Boolean)
                _bolEsObligatorio = value
            End Set
        End Property

#End Region

    End Class

End Namespace