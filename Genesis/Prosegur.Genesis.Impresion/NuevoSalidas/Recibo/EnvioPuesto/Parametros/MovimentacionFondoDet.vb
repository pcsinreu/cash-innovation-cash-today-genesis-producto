Namespace NuevoSalidas.Recibo.EnvioPuesto.Parametros

    ''' <summary>
    ''' Classe MovimentacionFondoDet - Envio Posto
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MovimentacionFondoDet

#Region "[VARIABLES]"

        Private _OidMovimentacionFondo As String
        Private _CodDenominacion As String
        Private _CodDivisa As String
        Private _DesDivisa As String
        Private _NelCantidad As Long
        Private _NumImporteDenominacion As Double

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Propriedad OidMovimentacionFondo
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property OidMovimentacionFondo() As String
            Get
                Return _OidMovimentacionFondo
            End Get
            Set(value As String)
                _OidMovimentacionFondo = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodDenominacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodDenominacion() As String
            Get
                Return _CodDenominacion
            End Get
            Set(value As String)
                _CodDenominacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodDivisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodDivisa() As String
            Get
                Return _CodDivisa
            End Get
            Set(value As String)
                _CodDivisa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesDivisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesDivisa() As String
            Get
                Return _DesDivisa
            End Get
            Set(value As String)
                _DesDivisa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NelCantidad
        ''' </summary>
        ''' <value>Long</value>
        ''' <returns>Long</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NelCantidad() As Long
            Get
                Return _NelCantidad
            End Get
            Set(value As Long)
                _NelCantidad = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NumImporteDenominacion
        ''' </summary>
        ''' <value>Long</value>
        ''' <returns>Long</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NumImporteDenominacion() As Double
            Get
                Return _NumImporteDenominacion
            End Get
            Set(value As Double)
                _NumImporteDenominacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace
