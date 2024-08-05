Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposYValores.GetTiposyValores

    <XmlType(Namespace:="urn:GetTiposyValores")> _
    <XmlRoot(Namespace:="urn:GetTiposyValores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Tipos As List(Of Tipo)
#End Region

#Region "Propriedades"

        Public Property Tipos() As List(Of Tipo)
            Get
                Return _Tipos
            End Get
            Set(value As List(Of Tipo))
                _Tipos = value
            End Set
        End Property
#End Region

    End Class
End Namespace