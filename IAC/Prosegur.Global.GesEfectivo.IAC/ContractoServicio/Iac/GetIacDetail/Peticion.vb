Imports System.Xml.Serialization
Imports System.Xml

Namespace Iac.GetIacDetail

    <XmlType(Namespace:="urn:GetIacDetail")> _
    <XmlRoot(Namespace:="urn:GetIacDetail")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"

        Private _codidoIac As List(Of String)

#End Region

#Region "Propriedades"

        Public Property CodidoIac() As List(Of String)
            Get
                Return _codidoIac
            End Get
            Set(value As List(Of String))
                _codidoIac = value
            End Set
        End Property
#End Region

    End Class
End Namespace

