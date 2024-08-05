Namespace NuevoSalidas.Ticket.Bulto.Parametros

    ''' <summary>
    ''' Classe Bulto - Contém os dados necessários para impressão de etiquetas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gfraga] 23/08/2010 Criado
    ''' </history>
    Public Class Bulto

#Region "[Atributos]"

        Private _CodCliente As String
        Private _DesCliente As String
        Private _CodSubCliente As String
        Private _DesSubCliente As String
        Private _CodPuntoServicio As String
        Private _DesPuntoServicio As String
        Private _CodRuta As String
        Private _NumBulto As Integer
        Private _TotalBultosRemesa As Integer
        Private _SecNumParada As Integer
        Private _FechaEntrega As DateTime
        Private _DesPrecinto As String
        Private _TotalBulto As String
        Private _TotalRemesa As String
        Private _CodRefCliente As String
        Private _DesRefCliente As String
        Private _NroDocumento As String
        Private _DenominacionBillete As New DenominacionColeccion
        Private _DenominacionMoneda As New DenominacionColeccion
        Private _RemesaTrabajaPorBulto As Boolean

#End Region

#Region "[Propriedades]"

        Public Property NroDocumento() As String
            Get
                Return _NroDocumento
            End Get
            Set(value As String)
                _NroDocumento = value
            End Set
        End Property

        Public Property DesRefCliente() As String
            Get
                Return _DesRefCliente
            End Get
            Set(value As String)
                _DesRefCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodCliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodCliente() As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesCliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesCliente() As String
            Get
                Return _DesCliente
            End Get
            Set(value As String)
                _DesCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodSubCliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodSubCliente() As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesSubCliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesSubCliente() As String
            Get
                Return _DesSubCliente
            End Get
            Set(value As String)
                _DesSubCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodPuntoServicio
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodPuntoServicio() As String
            Get
                Return _CodPuntoServicio
            End Get
            Set(value As String)
                _CodPuntoServicio = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesPuntoServicio
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesPuntoServicio() As String
            Get
                Return _DesPuntoServicio
            End Get
            Set(value As String)
                _DesPuntoServicio = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodRuta
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodRuta() As String
            Get
                Return _CodRuta
            End Get
            Set(value As String)
                _CodRuta = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NumBulto
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NumBulto() As Integer
            Get
                Return _NumBulto
            End Get
            Set(value As Integer)
                _NumBulto = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NumBulto
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property TotalBultosRemesa() As Integer
            Get
                Return _TotalBultosRemesa
            End Get
            Set(value As Integer)
                _TotalBultosRemesa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad SecNumParada
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property SecNumParada() As Integer
            Get
                Return _SecNumParada
            End Get
            Set(value As Integer)
                _SecNumParada = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad FechaEntrega
        ''' </summary>
        ''' <value>DateTime</value>
        ''' <returns>DateTime</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property FechaEntrega() As DateTime
            Get
                Return _FechaEntrega
            End Get
            Set(value As DateTime)
                _FechaEntrega = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesPrecinto
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesPrecinto() As String
            Get
                Return _DesPrecinto
            End Get
            Set(value As String)
                _DesPrecinto = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad TotalBulto
        ''' </summary>
        ''' <value>Decimal</value>
        ''' <returns>Decimal</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property TotalBulto() As String
            Get
                Return _TotalBulto
            End Get
            Set(value As String)
                _TotalBulto = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad TotalRemesa
        ''' </summary>
        ''' <value>Decimal</value>
        ''' <returns>Decimal</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property TotalRemesa() As String
            Get
                Return _TotalRemesa
            End Get
            Set(value As String)
                _TotalRemesa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodRefCliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodRefCliente() As String
            Get
                Return _CodRefCliente
            End Get
            Set(value As String)
                _CodRefCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DenominacionBillete
        ''' </summary>
        ''' <value>DenominacionColeccion</value>
        ''' <returns>DenominacionColeccion</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DenominacionBillete() As DenominacionColeccion
            Get
                Return _DenominacionBillete
            End Get
            Set(value As DenominacionColeccion)
                _DenominacionBillete = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DenominacionMoneda
        ''' </summary>
        ''' <value>DenominacionColeccion</value>
        ''' <returns>DenominacionColeccion</returns>
        ''' <history>[gfraga] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DenominacionMoneda() As DenominacionColeccion
            Get
                Return _DenominacionMoneda
            End Get
            Set(value As DenominacionColeccion)
                _DenominacionMoneda = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad RemesaTrabajaPorBulto
        ''' </summary>
        Public Property RemesaTrabajaPorBulto() As Boolean
            Get
                Return _RemesaTrabajaPorBulto
            End Get
            Set(value As Boolean)
                _RemesaTrabajaPorBulto = value
            End Set
        End Property

#End Region

    End Class

End Namespace

