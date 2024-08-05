Imports System.Xml.Serialization
Imports System.Xml

Namespace Canal.GetSubCanalesByCertificado
    <XmlType(Namespace:="urn:GetSubCanalesByCertificado")> _
    <XmlRoot(Namespace:="urn:GetSubCanalesByCertificado")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"
        Private _SubCanales As SubCanalColeccion
#End Region

#Region "Propriedades"
        Public Property SubCanales() As SubCanalColeccion
            Get
                Return _SubCanales
            End Get
            Set(value As SubCanalColeccion)
                _SubCanales = value
            End Set
        End Property

#End Region

    End Class
End Namespace
