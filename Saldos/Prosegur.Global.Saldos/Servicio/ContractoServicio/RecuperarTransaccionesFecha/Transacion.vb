Namespace RecuperarTransaccionesFechas

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
        Private _NumExterno As String
        Private _NombreDocumento As String
        Private _Disponible As Nullable(Of Boolean)
        Private _Cliente As Cliente
        Private _Planta As New Planta
        Private _SectorOrigen As New Sector
        Private _SectorDestino As New Sector
        Private _CanalOrigen As New Canal
        Private _CanalDestino As New Canal
        Private _Monedas As New Monedas

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
        ''' Fecha y Hora de la Campo FECHAGESTION del PD_DOCUMENTOCABECERA
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
        ''' Número externo
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 06/11/2012 - Creado</history>
        ''' <remarks></remarks>
        Public Property NumExterno() As String
            Get
                Return _NumExterno
            End Get
            Set(value As String)
                _NumExterno = value
            End Set
        End Property

        ''' <summary>
        ''' Nombre del documento
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 06/11/2012 - Creado</history>
        ''' <remarks></remarks>
        Public Property NombreDocumento() As String
            Get
                Return _NombreDocumento
            End Get
            Set(value As String)
                _NombreDocumento = value
            End Set
        End Property

        ''' <summary>
        ''' Define si la moneda está o no disponible
        ''' </summary>
        ''' <value>Boolean</value>
        ''' <returns>Boolean</returns>
        ''' <history>[maoliveira] - 06/11/2012 - Creado</history>
        ''' <remarks></remarks>
        Public Property Disponible() As Nullable(Of Boolean)
            Get
                Return _Disponible
            End Get
            Set(value As Nullable(Of Boolean))
                _Disponible = value
            End Set
        End Property

        ''' <summary>
        ''' Datos del Cliente
        ''' </summary>
        ''' <value>RecuperarTransacciones.Cliente</value>
        ''' <returns>RecuperarTransacciones.Cliente</returns>
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
        ''' <value>RecuperarTransacciones.Planta</value>
        ''' <returns>RecuperarTransacciones.Planta</returns>
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
        ''' <value>RecuperarTransacciones.Sector</value>
        ''' <returns>RecuperarTransacciones.Sector</returns>
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
        ''' <value>RecuperarTransacciones.Sector</value>
        ''' <returns>RecuperarTransacciones.Sector</returns>
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
        ''' <value>RecuperarTransacciones.Canal</value>
        ''' <returns>RecuperarTransacciones.Canal</returns>
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
        ''' <value>RecuperarTransacciones.Canal</value>
        ''' <returns>RecuperarTransacciones.Canal</returns>
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
        ''' Datos de las Monedas
        ''' </summary>
        ''' <value>RecuperarTransacciones.Monedas</value>
        ''' <returns>RecuperarTransacciones.Monedas</returns>
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

#End Region

    End Class

End Namespace
