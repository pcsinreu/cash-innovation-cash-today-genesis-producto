Imports System.Xml.Serialization
Imports System.Xml

Namespace GetPuestos

    <XmlType(Namespace:="urn:GetPuestos")> _
    <XmlRoot(Namespace:="urn:GetPuestos")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"

        Private _CodigoDelegacion As String
        Private _CodigoAplicacion As String
        Private _CodigoPuesto As String
        Private _HostPuesto As String
        Private _BolVigente As Nullable(Of Boolean)
        Private _Permisos As List(Of String)
        Private _BolSoloMecanizado As Boolean

#End Region

#Region "Propriedades"

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        Public Property CodigoPuesto() As String
            Get
                Return _CodigoPuesto
            End Get
            Set(value As String)
                _CodigoPuesto = value
            End Set
        End Property

        Public Property HostPuesto() As String
            Get
                Return _HostPuesto
            End Get
            Set(value As String)
                _HostPuesto = value
            End Set
        End Property

        Public Property BolVigente() As Nullable(Of Boolean)
            Get
                Return _BolVigente
            End Get
            Set(value As Nullable(Of Boolean))
                _BolVigente = value
            End Set
        End Property

        Public Property Permisos() As List(Of String)
            Get
                Return _Permisos
            End Get
            Set(value As List(Of String))
                _Permisos = value
            End Set
        End Property

        Public Property BolSoloMecanizado() As Boolean
            Get
                Return _BolSoloMecanizado
            End Get
            Set(value As Boolean)
                _BolSoloMecanizado = value
            End Set
        End Property
#End Region

    End Class
End Namespace
