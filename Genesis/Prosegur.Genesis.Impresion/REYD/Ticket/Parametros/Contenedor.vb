Namespace REYD.Ticket.Contenedor.Parametros

    ''' <summary>
    ''' Classe contenedor - Contém os dados necessários para impressão de etiquetas
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Contenedor
#Region "[Atributos]"

        Private _CodCliente As String
        Private _FechaArmado As DateTime
        Private _DesPrecinto As String
        Private _TotalContenedor As Decimal
        Private _UsuarioResponsavelArmarContenedor As String
        Private _CodPuestoContenedor
        Private _DenominacionContenedor As String
        Private _SimboloDivisa As String
        Private _AceptaPico As Boolean

#End Region

#Region "[Propriedades]"

        Public Property UsuarioResponsavelArmarContenedor() As String
            Get
                Return _UsuarioResponsavelArmarContenedor
            End Get
            Set(value As String)
                _UsuarioResponsavelArmarContenedor = value
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
        ''' Propriedad _CodPuestoContenedor
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        Public Property CodPuestoContenedor() As String
            Get
                Return _CodPuestoContenedor
            End Get
            Set(value As String)
                _CodPuestoContenedor = value
            End Set
        End Property


        ''' <summary>
        ''' Propriedad NumBulto
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        Public Property TotalContenedor() As Decimal
            Get
                Return _TotalContenedor
            End Get
            Set(value As Decimal)
                _TotalContenedor = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad FechaEntrega
        ''' </summary>
        ''' <value>DateTime</value>
        ''' <returns>DateTime</returns>
        Public Property FechaArmado() As DateTime
            Get
                Return _FechaArmado
            End Get
            Set(value As DateTime)
                _FechaArmado = value
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

        Public Property DenominacionContenedor() As String
            Get
                Return _DenominacionContenedor
            End Get
            Set(value As String)
                _DenominacionContenedor = value
            End Set
        End Property

        Public Property SimboloDivisa() As String
            Get
                Return _SimboloDivisa
            End Get
            Set(value As String)
                _SimboloDivisa = value
            End Set
        End Property

        Public Property AceptaPico() As Boolean
            Get
                Return _AceptaPico
            End Get
            Set(value As Boolean)
                _AceptaPico = value
            End Set
        End Property


#End Region

    End Class

End Namespace

