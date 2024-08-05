Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposYValores.GetValores

    <XmlType(Namespace:="urn:GetValores")> _
    <XmlRoot(Namespace:="urn:GetValores")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"

        Private _codTipo As String
        Private _codValor As String
        Private _desValor As String
        Private _bolActivo As Nullable(Of Boolean)

#End Region

#Region "Propriedades"

        Public Property CodTipo() As String
            Get
                Return _codTipo
            End Get
            Set(value As String)
                _codTipo = value
            End Set
        End Property

        Public Property CodValor() As String
            Get
                Return _codValor
            End Get
            Set(value As String)
                _codValor = value
            End Set
        End Property

        Public Property DesValor() As String
            Get
                Return _desValor
            End Get
            Set(value As String)
                _desValor = value
            End Set
        End Property

        Public Property BolActivo() As Nullable(Of Boolean)
            Get
                Return _bolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _bolActivo = value
            End Set
        End Property
#End Region

    End Class
End Namespace

