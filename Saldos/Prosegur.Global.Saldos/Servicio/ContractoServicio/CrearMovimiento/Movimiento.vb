Namespace CrearMovimiento

    ''' <summary>
    ''' Clase Movimiento
    ''' </summary>
    ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Movimiento

#Region "[INICIALIZAÇÃO]"

        Sub New()
            Me.Desgloses = New Desgloses
        End Sub

#End Region

#Region "[VARIABLES]"

        Private _Accion As Enumeradores.eAccion
        Private _CodigoTipoDocumento As Integer
        Private _CodigoPlanta As String
        Private _CodigoClienteOrigen As String
        Private _codigoClienteDestino As String
        Private _CodigoSectorOrigen As String
        Private _CodigoSectorDestino As String
        Private _CodigoCanalOrigen As String
        Private _CodigoCanalDestino As String
        Private _NumeroExterno As String
        Private _OidTransaccion As String
        Private _Desgloses As Desgloses
        Private _CamposExtras As GuardarDatosDocumento.CamposExtras
        Private _FechaTransaccion As Nullable(Of DateTime)
        



#End Region

#Region "[PROPIEDADES]"

        ''' <summary>
        ''' Código de la Accion
        ''' </summary>
        ''' <value>Enumeradores.eAccion</value>
        ''' <returns>Enumeradores.eAccion</returns>
        ''' <history>[maoliveira] - 21/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Accion() As Enumeradores.eAccion
            Get
                Return _Accion
            End Get
            Set(value As Enumeradores.eAccion)
                _Accion = value
            End Set
        End Property

        ''' <summary>
        ''' Código del Tipo de Documento
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoTipoDocumento() As Integer
            Get
                Return _CodigoTipoDocumento
            End Get
            Set(value As Integer)
                _CodigoTipoDocumento = value
            End Set
        End Property

        ''' <summary>
        ''' Código de la Planta
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoPlanta() As String
            Get
                Return _CodigoPlanta
            End Get
            Set(value As String)
                _CodigoPlanta = value
            End Set
        End Property

        ''' <summary>
        ''' Código del Cliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoClienteOrigen() As String
            Get
                Return _CodigoClienteOrigen
            End Get
            Set(value As String)
                _CodigoClienteOrigen = value
            End Set
        End Property

        ''' <summary>
        ''' Código del Cliente Destino
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 22/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoClienteDestino() As String
            Get
                Return _codigoClienteDestino
            End Get
            Set(value As String)
                _codigoClienteDestino = value
            End Set
        End Property

        ''' <summary>
        ''' Código del Centro de Proceso Origen
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoSectorOrigen() As String
            Get
                Return _CodigoSectorOrigen
            End Get
            Set(value As String)
                _CodigoSectorOrigen = value
            End Set
        End Property

        ''' <summary>
        ''' Código del Centro de Processo Destino
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoSectorDestino() As String
            Get
                Return _CodigoSectorDestino
            End Get
            Set(value As String)
                _CodigoSectorDestino = value
            End Set
        End Property

        ''' <summary>
        ''' Código del Canal
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoCanalOrigen() As String
            Get
                Return _CodigoCanalOrigen
            End Get
            Set(value As String)
                _CodigoCanalOrigen = value
            End Set
        End Property

        ''' <summary>
        ''' Código del Canal Destino
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoCanalDestino() As String
            Get
                Return _CodigoCanalDestino
            End Get
            Set(value As String)
                _CodigoCanalDestino = value
            End Set
        End Property

        ''' <summary>
        ''' Número Externo
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property NumeroExterno() As String
            Get
                Return _NumeroExterno
            End Get
            Set(value As String)
                _NumeroExterno = value
            End Set
        End Property


        ''' <summary>
        ''' Identificación de la transacción en MiAgencia
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property OidTransaccion() As String
            Get
                Return _OidTransaccion
            End Get
            Set(value As String)
                _OidTransaccion = value
            End Set
        End Property

        ''' <summary>
        ''' Coleción de Efectivos
        ''' </summary>
        ''' <value>Desgloses</value>
        ''' <returns>Desgloses</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Desgloses() As Desgloses
            Get
                Return _Desgloses
            End Get
            Set(value As Desgloses)
                _Desgloses = value
            End Set
        End Property

        ''' <summary>
        ''' Coleción de Campos extras
        ''' </summary>
        ''' <value>GuardarDatosDocumento.CamposExtras</value>
        ''' <returns>GuardarDatosDocumento.CamposExtras</returns>
        ''' <history>[maoliveira] - 30/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CamposExtras() As GuardarDatosDocumento.CamposExtras
            Get
                Return _CamposExtras
            End Get
            Set(value As GuardarDatosDocumento.CamposExtras)
                _CamposExtras = value
            End Set
        End Property

        ''' <summary>
        ''' Campo Fecha de la transacción - Inserido para atender a Bug: 0041688
        ''' </summary>
        ''' <value>FechaTransaccion</value>
        ''' <returns>Datetime</returns>
        ''' <history>[aklevanskis] - 07/03/2013 - Creado</history>
        ''' <remarks></remarks>
        Public Property FechaTransaccion() As Nullable(Of DateTime)
            Get
                Return _FechaTransaccion
            End Get
            Set(value As Nullable(Of DateTime))
                _FechaTransaccion = value
            End Set
        End Property

#End Region

    End Class

End Namespace