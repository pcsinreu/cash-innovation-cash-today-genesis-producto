Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Maquina.GetMaquina

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetMaquina")> _
    <XmlRoot(Namespace:="urn:GetMaquina")> _
    <Serializable()> _
    Public Class Maquina
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _OidMaquina As String
        Private _DeviceID As String
        Private _Descripcion As String
        Private _DesModelo As String
        Private _DesPlanificacion As String
        Private _DesFabricante As String
        Private _BolActivo As Boolean
        Private _FechaValor As Boolean
        Private _FechaVigenciaInicio As DateTime
        Private _FechaVigenciaFin As DateTime
        Private _Cliente As String
        Private _SubCliente As String
        Private _PtoServicio As String
        Private _CodigoCliente As String
        Private _CodigoSubCliente As String
        Private _CodigoPtoServicio As String


#End Region

#Region "[PROPRIEDADE]"

        Public Property OidMaquina As String
            Get
                Return _OidMaquina
            End Get
            Set(value As String)
                _OidMaquina = value
            End Set
        End Property

        Public Property DeviceID As String
            Get
                Return _DeviceID
            End Get
            Set(value As String)
                _DeviceID = value
            End Set
        End Property

        Public Property DesPlanificacion As String
            Get
                Return _DesPlanificacion
            End Get
            Set(value As String)
                _DesPlanificacion = value
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property DesFabricante As String
            Get
                Return _DesFabricante
            End Get
            Set(value As String)
                _DesFabricante = value
            End Set
        End Property

        Public Property DesModelo As String
            Get
                Return _DesModelo
            End Get
            Set(value As String)
                _DesModelo = value
            End Set
        End Property

        Public Property BolActivo As Boolean
            Get
                Return _BolActivo
            End Get
            Set(value As Boolean)
                _BolActivo = value
            End Set
        End Property

      
        Public Property FechaValor As Boolean
            Get
                Return _FechaValor
            End Get
            Set(value As Boolean)
                _FechaValor = value
            End Set
        End Property

        Public Property FechaVigenciaInicio As DateTime
            Get
                Return _FechaVigenciaInicio
            End Get
            Set(value As DateTime)
                _FechaVigenciaInicio = value
            End Set
        End Property

        Public Property FechaVigenciaFin As DateTime
            Get
                Return _FechaVigenciaFin
            End Get
            Set(value As DateTime)
                _FechaVigenciaFin = value
            End Set
        End Property


        Public Property CodigoCliente As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property
        Public Property CodigoSubCliente As String
            Get
                Return _CodigoSubCliente
            End Get
            Set(value As String)
                _CodigoSubCliente = value
            End Set
        End Property

        Public Property CodigoPtoServicio As String
            Get
                Return _CodigoPtoServicio
            End Get
            Set(value As String)
                _CodigoPtoServicio = value
            End Set
        End Property

        Public Property Cliente As String
            Get
                Return _Cliente
            End Get
            Set(value As String)
                _Cliente = value
            End Set
        End Property
        Public Property SubCliente As String
            Get
                Return _SubCliente
            End Get
            Set(value As String)
                _SubCliente = value
            End Set
        End Property

        Public Property PtoServicio As String
            Get
                Return _PtoServicio
            End Get
            Set(value As String)
                _PtoServicio = value
            End Set
        End Property
#End Region

    End Class
End Namespace
