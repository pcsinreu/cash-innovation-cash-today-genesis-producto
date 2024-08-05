Namespace RecuperarTransaccionDetallada

    ''' <summary>
    ''' Clase Transaccion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 12/07/2011 - Creado
    ''' </history>
    Public Class Transaccion

#Region "[VARIÁVEIS]"

        Private _OidTransaccion As String
        Private _FechaTransaccion As DateTime
        Private _Cliente As Cliente
        Private _Planta As New Planta
        Private _SectorOrigen As New Sector
        Private _SectorDestino As New Sector
        Private _CanalOrigen As New Canal
        Private _CanalDestino As New Canal
        Private _NumeroExterno As String
        Private _Monedas As New Monedas
        Private _CamposExtras As New GuardarDatosDocumento.CamposExtras
        Private _NombreDocumento As String

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Identificador de la Transacción
        ''' </summary>
        ''' <value>Boolean</value>
        ''' <returns>Boolean</returns>
        ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
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
        ''' Fecha y Hora de la Transacción
        ''' </summary>
        ''' <value>DateTime</value>
        ''' <returns>DateTime</returns>
        ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property FechaTransaccion() As DateTime
            Get
                Return _FechaTransaccion
            End Get
            Set(value As DateTime)
                _FechaTransaccion = value
            End Set
        End Property

        ''' <summary>
        ''' Datos del Cliente
        ''' </summary>
        ''' <value>RecuperarTransaccionDetallada.Cliente</value>
        ''' <returns>RecuperarTransaccionDetallada.Cliente</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Cliente() As Cliente
            Get
                Return _Cliente
            End Get
            Set(value As Cliente)
                _Cliente = value
            End Set
        End Property

        ''' <summary>
        ''' Datos de la Planta
        ''' </summary>
        ''' <value>RecuperarTransaccionDetallada.Planta</value>
        ''' <returns>RecuperarTransaccionDetallada.Planta</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Planta() As Planta
            Get
                Return _Planta
            End Get
            Set(value As Planta)
                _Planta = value
            End Set
        End Property

        ''' <summary>
        ''' Datos del Sector Origen
        ''' </summary>
        ''' <value>RecuperarTransaccionDetallada.Sector</value>
        ''' <returns>RecuperarTransaccionDetallada.Sector</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property SectorOrigen() As Sector
            Get
                Return _SectorOrigen
            End Get
            Set(value As Sector)
                _SectorOrigen = value
            End Set
        End Property

        ''' <summary>
        ''' Datos del Sector Destino
        ''' </summary>
        ''' <value>RecuperarTransaccionDetallada.Sector</value>
        ''' <returns>RecuperarTransaccionDetallada.Sector</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property SectorDestino() As Sector
            Get
                Return _SectorDestino
            End Get
            Set(value As Sector)
                _SectorDestino = value
            End Set
        End Property

        ''' <summary>
        ''' Datos del Canal Origen
        ''' </summary>
        ''' <value>RecuperarTransaccionDetallada.Canal</value>
        ''' <returns>RecuperarTransaccionDetallada.Canal</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CanalOrigen() As Canal
            Get
                Return _CanalOrigen
            End Get
            Set(value As Canal)
                _CanalOrigen = value
            End Set
        End Property

        ''' <summary>
        ''' Datos del Canal Destino
        ''' </summary>
        ''' <value>RecuperarTransaccionDetallada.Canal</value>
        ''' <returns>RecuperarTransaccionDetallada.Canal</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CanalDestino() As Canal
            Get
                Return _CanalDestino
            End Get
            Set(value As Canal)
                _CanalDestino = value
            End Set
        End Property

        ''' <summary>
        ''' Número Externo
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
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
        ''' Datos de las Monedas
        ''' </summary>
        ''' <value>RecuperarTransaccionDetallada.Monedas</value>
        ''' <returns>RecuperarTransaccionDetallada.Monedas</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Monedas() As Monedas
            Get
                Return _Monedas
            End Get
            Set(value As Monedas)
                _Monedas = value
            End Set
        End Property

        ''' <summary>
        ''' Coleción de Campos extras
        ''' </summary>
        ''' <value>GuardarDatosDocumento.CamposExtras</value>
        ''' <returns>GuardarDatosDocumento.CamposExtras</returns>
        ''' <history>[maoliveira] - 13/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CamposExtras() As GuardarDatosDocumento.CamposExtras
            Get
                Return _CamposExtras
            End Get
            Set(value As GuardarDatosDocumento.CamposExtras)
                _CamposExtras = value
            End Set
        End Property

        Public Property NombreDocumento() As String
            Get
                Return _NombreDocumento
            End Get
            Set(value As String)
                _NombreDocumento = value
            End Set
        End Property

#End Region

    End Class

End Namespace
