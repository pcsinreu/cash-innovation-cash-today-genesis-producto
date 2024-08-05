Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposYValores.GetTiposyValores

    <XmlType(Namespace:="urn:GetTiposyValores")> _
    <XmlRoot(Namespace:="urn:GetTiposyValores")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"

        Private _codTipo As String
        Private _desTipo As String
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

        Public Property DesTipo() As String
            Get
                Return _desTipo
            End Get
            Set(value As String)
                _desTipo = value
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
