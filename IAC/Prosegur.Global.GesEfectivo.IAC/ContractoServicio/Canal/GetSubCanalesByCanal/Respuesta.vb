Imports System.Xml.Serialization
Imports System.Xml

Namespace Canal.GetSubCanalesByCanal
    <XmlType(Namespace:="urn:GetSubCanalesByCanal")> _
    <XmlRoot(Namespace:="urn:GetSubCanalesByCanal")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"
        Private _Canales As CanalColeccion
#End Region

#Region "Propriedades"
        Public Property Canales() As CanalColeccion
            Get
                Return _Canales
            End Get
            Set(value As CanalColeccion)
                _Canales = value
            End Set
        End Property

#End Region

    End Class
End Namespace
