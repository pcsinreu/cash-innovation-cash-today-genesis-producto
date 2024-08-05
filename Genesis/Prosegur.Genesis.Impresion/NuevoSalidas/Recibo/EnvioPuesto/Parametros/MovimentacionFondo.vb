Namespace NuevoSalidas.Recibo.EnvioPuesto.Parametros

    ''' <summary>
    ''' Classe MovimentacionFondo - Geração de recibo
    ''' </summary>
    ''' <history>[gfraga] 25/08/2010 Creado</history>
    ''' <remarks></remarks>
    Public Class MovimentacionFondo

        ''' <summary>
        ''' Tipo Movimentacion Fondo
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum TipoMovimentacionFondo
            SolicitudDeFondoTesoro = 1
            EntregaDeValoresASector = 2
            EnvioDeFondosAPuesto = 3
            EntregaDeValoresAlSupervisor = 4
            Armado = 5
        End Enum

#Region "[VARIABLES]"

        Private _OidMovimentacionFondo As String
        Private _CodTicket As Long
        Private _FyhMovimentacion As Date
        Private _CodDelegacion As String
        Private _CodOrigenDinero As String
        Private _CodDestinoDinero As String
        Private _MovimentacionFondoDet As New MovimentacionFondoDetColeccion
        Private _NecTipoMovimentacion As TipoMovimentacionFondo
        Private _DireccionDelegacion As String

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
        ''' Propriedad CodTicket
        ''' </summary>
        ''' <value>Long</value>
        ''' <returns>Long</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodTicket() As Long
            Get
                Return _CodTicket
            End Get
            Set(value As Long)
                _CodTicket = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad FyhMovimentacion
        ''' </summary>
        ''' <value>Date</value>
        ''' <returns>Date</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property FyhMovimentacion() As Date
            Get
                Return _FyhMovimentacion
            End Get
            Set(value As Date)
                _FyhMovimentacion = value
            End Set
        End Property


        ''' <summary>
        ''' Propriedad CodDelegacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodOrigenDinero
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodOrigenDinero() As String
            Get
                Return _CodOrigenDinero
            End Get
            Set(value As String)
                _CodOrigenDinero = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodDestinoDinero
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodDestinoDinero() As String
            Get
                Return _CodDestinoDinero
            End Get
            Set(value As String)
                _CodDestinoDinero = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad MovimentacionFondoDet
        ''' </summary>
        ''' <value>MovimentacionFondoDetColeccion</value>
        ''' <returns>MovimentacionFondoDetColeccion</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property MovimentacionFondoDet() As MovimentacionFondoDetColeccion
            Get
                Return _MovimentacionFondoDet
            End Get
            Set(value As MovimentacionFondoDetColeccion)
                _MovimentacionFondoDet = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NecTipoMovimentacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NecTipoMovimentacion() As TipoMovimentacionFondo
            Get
                Return _NecTipoMovimentacion
            End Get
            Set(value As TipoMovimentacionFondo)
                _NecTipoMovimentacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DireccionDelegacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 25/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DireccionDelegacion() As String
            Get
                Return _DireccionDelegacion
            End Get
            Set(value As String)
                _DireccionDelegacion = value
            End Set
        End Property


#End Region

    End Class

End Namespace
