Namespace IngresoContado

    ''' <summary>
    ''' Classe BilleteFalso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class BilleteFalso

#Region "Variáveis"

        Private _CodigoDenominacion As String
        Private _NumeroSerie As String
        Private _NumeroPlancha As String
        Private _Observaciones As String

#End Region

#Region "Propriedades"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoDenominacion() As String
            Get
                Return _CodigoDenominacion
            End Get
            Set(value As String)
                _CodigoDenominacion = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property NumeroSerie() As String
            Get
                Return _NumeroSerie
            End Get
            Set(value As String)
                _NumeroSerie = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property NumeroPlancha() As String
            Get
                Return _NumeroPlancha
            End Get
            Set(value As String)
                _NumeroPlancha = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Observaciones() As String
            Get
                Return _Observaciones
            End Get
            Set(value As String)
                _Observaciones = value
            End Set
        End Property

#End Region


    End Class

End Namespace