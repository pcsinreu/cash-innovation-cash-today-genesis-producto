Namespace NuevoSalidas.Ticket.Bulto.Parametros

    ''' <summary>
    ''' Classe Denominacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Denominacion

#Region "[Atributos]"

        Private _CodDenominacion As String
        Private _NelCantidad As Double
        Private _Importe As Double
        Private _CodigoSimbolo As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoSimbolo() As String
            Get
                Return _CodigoSimbolo
            End Get
            Set(value As String)
                _CodigoSimbolo = value
            End Set
        End Property

        Public Property Importe() As Double
            Get
                Return _Importe
            End Get
            Set(value As Double)
                _Importe = value
            End Set
        End Property

        Public Property CodDenominacion() As String
            Get
                Return _CodDenominacion
            End Get
            Set(value As String)
                _CodDenominacion = value
            End Set
        End Property

        Public Property NelCantidad() As Double
            Get
                Return _NelCantidad
            End Get
            Set(value As Double)
                _NelCantidad = value
            End Set
        End Property

#End Region

    End Class

End Namespace
