
Namespace GetMorfologiaDetail

    ''' <summary>
    ''' Classe Denominacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' </history>
    <Serializable()> _
    Public Class Denominacion

#Region "[Variáveis]"

        Private _codDenominacion As String
        Private _desDenominacion As String
        Private _bolBillete As Boolean
        Private _numValor As Decimal
        
#End Region

#Region "[Propriedades]"

        Public Property CodDenominacion As String
            Get
                Return _codDenominacion
            End Get
            Set(value As String)
                _codDenominacion = value
            End Set
        End Property

        Public Property DesDenominacion As String
            Get
                Return _desDenominacion
            End Get
            Set(value As String)
                _desDenominacion = value
            End Set
        End Property

        Public Property BolBillete As Boolean
            Get
                Return _bolBillete
            End Get
            Set(value As Boolean)
                _bolBillete = value
            End Set
        End Property

        Public Property NumValor As Decimal
            Get
                Return _numValor
            End Get
            Set(value As Decimal)
                _numValor = value
            End Set
        End Property

#End Region

    End Class

End Namespace