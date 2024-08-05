Imports System.Xml.Serialization
Imports System.Xml

Namespace RespaldoCompleto.GetRespaldosCompletos

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 28/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:RespaldoCompleto")> _
    <XmlRoot(Namespace:="urn:RespaldoCompleto")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _CodigoCliente As String
        Private _CodigoDelegacion As String
        Private _FechaDesde As Date
        Private _FechaHasta As Date
        Private _EsFechaProceso As Integer
        Private _FormatoSalida As Enumeradores.eFormatoSalida
        Private _Procesos As List(Of String)
        Private _EstadoRemesa As String

#End Region

#Region " Propriedades "

        Public Property CodigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property FechaDesde() As Date
            Get
                Return _FechaDesde
            End Get
            Set(value As Date)
                _FechaDesde = value
            End Set
        End Property

        Public Property FechaHasta() As Date
            Get
                Return _FechaHasta
            End Get
            Set(value As Date)
                _FechaHasta = value
            End Set
        End Property

        Public Property EsFechaProceso() As Integer
            Get
                Return _EsFechaProceso
            End Get
            Set(value As Integer)
                _EsFechaProceso = value
            End Set
        End Property

        Public Property FormatoSalida() As Enumeradores.eFormatoSalida
            Get
                Return _FormatoSalida
            End Get
            Set(value As Enumeradores.eFormatoSalida)
                _FormatoSalida = value
            End Set
        End Property

        Public Property Procesos() As List(Of String)
            Get
                Return _Procesos
            End Get
            Set(value As List(Of String))
                _Procesos = value
            End Set
        End Property

        Public Property EstadoRemesa() As String
            Get
                Return _EstadoRemesa
            End Get
            Set(value As String)
                _EstadoRemesa = value
            End Set
        End Property

#End Region

    End Class

End Namespace
