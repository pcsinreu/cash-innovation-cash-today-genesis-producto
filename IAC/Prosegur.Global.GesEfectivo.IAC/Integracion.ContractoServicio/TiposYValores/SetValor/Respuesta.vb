Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposYValores.SetValor

    <XmlType(Namespace:="urn:SetValor")> _
    <XmlRoot(Namespace:="urn:SetValor")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _codValor As String
#End Region

#Region "Propriedades"

        Public Property CodValor() As String
            Get
                Return _codValor
            End Get
            Set(value As String)
                _codValor = value
            End Set
        End Property
#End Region

    End Class
End Namespace