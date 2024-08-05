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
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _OidMaquina As String
        Private _OidDelegacion As String
        Private _OidPlanta As String
        Private _OidClientes As List(Of String)
        Private _OidSubClientes As List(Of String)
        Private _OidPuntoServicio As List(Of String)
        Private _DeviceID As String
        Private _Descripcion As String
        Private _OidModelo As String
        Private _OidFabricante As String
        Private _Vigente As Nullable(Of Boolean)

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

        Public Property OidDelegacion As String
            Get
                Return _OidDelegacion
            End Get
            Set(value As String)
                _OidDelegacion = value
            End Set
        End Property

        Public Property OidPlanta As String
            Get
                Return _OidPlanta
            End Get
            Set(value As String)
                _OidPlanta = value
            End Set
        End Property

        Public Property OidClientes As List(Of String)
            Get
                Return _OidClientes
            End Get
            Set(value As List(Of String))
                _OidClientes = value
            End Set
        End Property

        Public Property OidSubClientes As List(Of String)
            Get
                Return _OidSubClientes
            End Get
            Set(value As List(Of String))
                _OidSubClientes = value
            End Set
        End Property

        Public Property OidPuntoServicio As List(Of String)
            Get
                Return _OidPuntoServicio
            End Get
            Set(value As List(Of String))
                _OidPuntoServicio = value
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

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property OidFabricante As String
            Get
                Return _OidFabricante
            End Get
            Set(value As String)
                _OidFabricante = value
            End Set
        End Property

        Public Property OidModelo As String
            Get
                Return _OidModelo
            End Get
            Set(value As String)
                _OidModelo = value
            End Set
        End Property

        Public Property BolVigente() As Nullable(Of Boolean)
            Get
                Return _Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _Vigente = value
            End Set
        End Property

#End Region
    End Class
End Namespace
