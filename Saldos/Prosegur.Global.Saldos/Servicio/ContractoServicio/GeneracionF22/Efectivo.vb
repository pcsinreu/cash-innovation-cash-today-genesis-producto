Namespace GeneracionF22OLD

    ''' <summary>
    ''' Classe Efectivo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [abueno] 13/07/2010 - Criado
    ''' </history>
    <Serializable()> _
    Public Class Efectivo

#Region "Variáveis"

        Private _CodigoISODivisa As String
        Private _CodigoDenominacion As String
        Private _NelCantidad As Integer
        Private _NumImporte As Decimal

#End Region

#Region "Propriedades"



        ''' <summary>
        ''' Propriedad CodigoISODivisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoISODivisa() As String
            Get
                Return _CodigoISODivisa
            End Get
            Set(value As String)
                _CodigoISODivisa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodigoDenominacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
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
        ''' Propriedad NelCantidad
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <remarks></remarks>
        Public Property NelCantidad() As Integer
            Get
                Return _NelCantidad
            End Get
            Set(value As Integer)
                _NelCantidad = value
            End Set
        End Property


        ''' <summary>
        ''' Propriedad NumImporte
        ''' </summary>
        ''' <value>Decimal</value>
        ''' <returns>Decimal</returns>
        ''' <remarks></remarks>
        Public Property NumImporte() As Decimal
            Get
                Return _NumImporte
            End Get
            Set(value As Decimal)
                _NumImporte = value
            End Set
        End Property


#End Region

    End Class

End Namespace