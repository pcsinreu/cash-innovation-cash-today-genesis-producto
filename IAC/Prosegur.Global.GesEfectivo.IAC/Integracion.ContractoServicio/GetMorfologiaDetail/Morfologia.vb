
Namespace GetMorfologiaDetail

    ''' <summary>
    ''' Classe Morfologia
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 20/12/2010 Criado
    ''' </history>
    <Serializable()> _
    Public Class Morfologia

#Region "[Variáveis]"

        Private _codMorfologia As String
        Private _desMorfologia As String
        Private _bolVigente As Boolean
        Private _fyhActualizacion As DateTime
        Private _necModalidadRecogida As Integer
        Private _componentes As List(Of Componente)

#End Region

#Region "[Propriedades]"

        Public Property CodMorfologia() As String
            Get
                Return _codMorfologia
            End Get
            Set(value As String)
                _codMorfologia = value
            End Set
        End Property

        Public Property DesMorfologia() As String
            Get
                Return _desMorfologia
            End Get
            Set(value As String)
                _desMorfologia = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property NecModalidadRecogida() As Integer
            Get
                Return _necModalidadRecogida
            End Get
            Set(value As Integer)
                _necModalidadRecogida = value
            End Set
        End Property

        Public Property Componentes() As List(Of Componente)
            Get
                Return _componentes
            End Get
            Set(value As List(Of Componente))
                _componentes = value
            End Set
        End Property

        Public Property FyhActualizacion() As DateTime
            Get
                Return _fyhActualizacion
            End Get
            Set(value As DateTime)
                _fyhActualizacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace