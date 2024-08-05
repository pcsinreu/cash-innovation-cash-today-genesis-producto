Imports System.Xml.Serialization
Imports System.Xml

Namespace GuardarDatosDocumento

    <XmlType(Namespace:="urn:GuardarDatosDocumento")> _
    <XmlRoot(Namespace:="urn:GuardarDatosDocumento")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _IdDocumento As Integer

        Public Property IdDocumento() As Integer
            Get
                Return _IdDocumento
            End Get
            Set(value As Integer)
                _IdDocumento = value
            End Set
        End Property

    End Class

End Namespace