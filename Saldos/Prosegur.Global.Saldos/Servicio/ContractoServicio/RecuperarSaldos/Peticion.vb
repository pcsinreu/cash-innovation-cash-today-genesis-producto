Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarSaldos

    ''' <summary>
    ''' Clase Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 07/07/2011 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarSaldos")> _
    <XmlRoot(Namespace:="urn:RecuperarSaldos")> _
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
        Private _FechaHoraSaldoDesde As DateTime
        Private _FechaHoraSaldoHasta As DateTime
        Private _CodigoPlanta As String
        Private _CodigoMoneda As String
        Private _CodigoCliente As String
        Private _CodigoSector As String
        Private _CodigoCanal As String
        Private _SaldoDesglosado As Boolean = False
        Private _SoloSaldoDisponible As Nullable(Of Boolean) = Nothing

#End Region

#Region "[PROPRIEDADES]"


        ''' <summary>
        ''' Datos del Usuario
        ''' </summary>
        ''' <value>GuardarDatosDocumento.Usuario</value>
        ''' <returns>GuardarDatosDocumento.Usuario</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
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
        ''' <history>[maoliveira] - 19/11/2012 - Creado</history>
        ''' <remarks></remarks>
        Public Property TipoNegocio() As Enumeradores.TipoNegocio
            Get
                Return _TipoNegocio
            End Get
            Set(value As Enumeradores.TipoNegocio)
                _TipoNegocio = value
            End Set
        End Property

        Public Property FechaHoraSaldoDesde() As DateTime
            Get
                Return _FechaHoraSaldoDesde
            End Get
            Set(value As DateTime)
                _FechaHoraSaldoDesde = value
            End Set
        End Property

        Public Property FechaHoraSaldoHasta() As DateTime
            Get
                Return _FechaHoraSaldoHasta
            End Get
            Set(value As DateTime)
                _FechaHoraSaldoHasta = value
            End Set
        End Property

        ''' <summary>
        ''' Código de la Planta
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
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
        ''' Código de la Moneda
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
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
        ''' Código del Centro de Proceso
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
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
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
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
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
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
        ''' Saldo Desglosado
        ''' </summary>
        ''' <value>Boolean</value>
        ''' <returns>Boolean</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property SaldoDesglosado() As Boolean
            Get
                Return _SaldoDesglosado
            End Get
            Set(value As Boolean)
                _SaldoDesglosado = value
            End Set
        End Property

        ''' <summary>
        ''' Salo Saldo Disponible
        ''' </summary>
        ''' <value>Boolean</value>
        ''' <returns>Boolean</returns>
        ''' <history>[maoliveira] - 26/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property SoloSaldoDisponible() As Nullable(Of Boolean)
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