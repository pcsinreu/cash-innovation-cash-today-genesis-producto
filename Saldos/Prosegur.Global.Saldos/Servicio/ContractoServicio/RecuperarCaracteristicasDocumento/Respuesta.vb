Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarCaracteristicasDocumento

    <XmlType(Namespace:="urn:RecuperarCaracteristicasDocumento")> _
    <XmlRoot(Namespace:="urn:RecuperarCaracteristicasDocumento")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _Caracteristica As Caracteristica

        Public Property Caracteristica() As Caracteristica
            Get
                Return _Caracteristica
            End Get
            Set(value As Caracteristica)
                _Caracteristica = value
            End Set
        End Property

    End Class

End Namespace