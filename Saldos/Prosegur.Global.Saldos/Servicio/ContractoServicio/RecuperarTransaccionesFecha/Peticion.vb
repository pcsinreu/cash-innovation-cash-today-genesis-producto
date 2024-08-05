Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarTransaccionesFechas

    ''' <summary>
    ''' Clase Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 12/07/2011 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <Serializable()> _
    Public Class Peticion

#Region "[INICIALIZAÇÃO]"

        Sub New()

            Usuario = New GuardarDatosDocumento.Usuario

        End Sub

#End Region

#Region "[VARIAVEIS]"

        Private _Usuario As GuardarDatosDocumento.Usuario
        Private _TipoNegocio As Enumeradores.TipoNegocio
        Private _FechaTransaccionDesde As DateTime
        Private _FechaTransaccionHasta As DateTime
        Private _CodigoPlanta As String
        Private _CodigoCliente As String
        Private _CodigoSector As String
        Private _CodigoCanal As String
        Private _CodigoMoneda As String
        Private _SaldoDesglosado As Nullable(Of Boolean)
        Private _SoloSaldoDisponible As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"


        ''' <summary>
        ''' Datos del Usuario
        ''' </summary>
        ''' <value>GuardarDatosDocumento.Usuario</value>
        ''' <returns>GuardarDatosDocumento.Usuario</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Usuario() As GuardarDatosDocumento.Usuario
            Get
                Return _Usuario
            End Get
            Set(value As GuardarDatosDocumento.Usuario)
                _Usuario = value
            End Set
        End Property

        ''' <summary>
        ''' Tipo de Negocio
        ''' </summary>
        ''' <value>Enumeradores.TipoNegocio</value>
        ''' <returns>Enumeradores.TipoNegocio</returns>
        ''' <history>[maoliveira] - 14/11/2012 - Creado</history>
        ''' <remarks></remarks>
        Public Property TipoNegocio() As Enumeradores.TipoNegocio
            Get
                Return _TipoNegocio
            End Get
            Set(value As Enumeradores.TipoNegocio)
                _TipoNegocio = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha y Hora Desde
        ''' </summary>
        ''' <value>DateTime</value>
        ''' <returns>DateTime</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property FechaTransaccionDesde() As DateTime
            Get
                Return _FechaTransaccionDesde
            End Get
            Set(value As DateTime)
                _FechaTransaccionDesde = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha y Hora Hasta
        ''' </summary>
        ''' <value>DateTime</value>
        ''' <returns>DateTime</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property FechaTransaccionHasta() As DateTime
            Get
                Return _FechaTransaccionHasta
            End Get
            Set(value As DateTime)
                _FechaTransaccionHasta = value
            End Set
        End Property

        ''' <summary>
        ''' Código de la Planta
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
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
        ''' Código del Centro de Proceso
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoSector() As String
            Get
                Return _CodigoSector
            End Get
            Set(value As String)
                _CodigoSector = value
            End Set
        End Property

        ''' <summary>
        ''' Código del Cliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Código del Canal
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoCanal() As String
            Get
                Return _CodigoCanal
            End Get
            Set(value As String)
                _CodigoCanal = value
            End Set
        End Property

        ''' <summary>
        ''' Código de la Moneda
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 06/11/2012 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoMoneda() As String
            Get
                Return _CodigoMoneda
            End Get
            Set(value As String)
                _CodigoMoneda = value
            End Set
        End Property

        ''' <summary>
        ''' Define si el saldo será o no desglosado por denominación
        ''' </summary>
        ''' <value>Nullable(Of Boolean)</value>
        ''' <returns>Nullable(Of Boolean)</returns>
        ''' <history>[maoliveira] - 06/11/2012 - Creado</history>
        ''' <remarks></remarks>
        Public Property SaldoDesglosado As Nullable(Of Boolean)
            Get
                Return _SaldoDesglosado
            End Get
            Set(value As Nullable(Of Boolean))
                _SaldoDesglosado = value
            End Set
        End Property

        ''' <summary>
        ''' Define si el saldo será disponible, no disponible o los dos
        ''' </summary>
        ''' <value>Nullable(Of Boolean)</value>
        ''' <returns>Nullable(Of Boolean)</returns>
        ''' <history>[maoliveira] - 06/11/2012 - Creado</history>
        ''' <remarks></remarks>
        Public Property SoloSaldoDisponible As Nullable(Of Boolean)
            Get
                Return _SoloSaldoDisponible
            End Get
            Set(value As Nullable(Of Boolean))
                _SoloSaldoDisponible = value
            End Set
        End Property

#End Region

    End Class

End Namespace