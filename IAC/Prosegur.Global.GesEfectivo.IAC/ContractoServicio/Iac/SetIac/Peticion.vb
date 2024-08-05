Imports System.Xml.Serialization
Imports System.Xml

Namespace Iac.SetIac

    <XmlType(Namespace:="urn:SetIac")> _
    <XmlRoot(Namespace:="urn:SetIac")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"

        Private _codidoIac As String
        Private _descripcionIac As String
        Private _observacionesIac As String
        Private _vigente As Nullable(Of Boolean)
        Private _esDeclaradoCopia As Boolean
        Private _esInvisible As Boolean
        Private _especificoSaldos As Boolean
        Private _codUsuario As String
        Private _terminosIac As TerminosIacColeccion

#End Region

#Region "Propriedades"

        Public Property CodidoIac() As String
            Get
                Return _codidoIac
            End Get
            Set(value As String)
                _codidoIac = value
            End Set
        End Property

        Public Property EsDeclaradoCopia() As Boolean
            Get
                Return _esDeclaradoCopia
            End Get
            Set(value As Boolean)
                _esDeclaradoCopia = value
            End Set
        End Property

        Public Property ObservacionesIac() As String
            Get
                Return _observacionesIac
            End Get
            Set(value As String)
                _observacionesIac = value
            End Set
        End Property

        Public Property DescripcionIac() As String
            Get
                Return _descripcionIac
            End Get
            Set(value As String)
                _descripcionIac = value
            End Set
        End Property

        Public Property vigente() As Nullable(Of Boolean)
            Get
                Return _vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _vigente = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

        Public Property EsInvisible() As Boolean
            Get
                Return _esInvisible
            End Get
            Set(value As Boolean)
                _esInvisible = value
            End Set
        End Property

        Public Property EspecificoSaldos() As Boolean
            Get
                Return _especificoSaldos
            End Get
            Set(value As Boolean)
                _especificoSaldos = value
            End Set
        End Property

        Public Property TerminosIac() As TerminosIacColeccion
            Get
                Return _terminosIac
            End Get
            Set(value As TerminosIacColeccion)
                _terminosIac = value
            End Set
        End Property
#End Region

    End Class
End Namespace
