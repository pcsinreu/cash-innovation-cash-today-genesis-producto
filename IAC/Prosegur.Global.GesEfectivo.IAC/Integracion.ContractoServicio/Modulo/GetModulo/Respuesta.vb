Imports System.Xml.Serialization
Imports System.Xml

Namespace Modulo.GetModulo

    <XmlType(Namespace:="urn:GetModulo")> _
    <XmlRoot(Namespace:="urn:GetModulo")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _modulos As List(Of Modulo)
        Public Property Modulos() As List(Of Modulo)
            Get
                Return _modulos
            End Get
            Set(value As List(Of Modulo))
                _modulos = value
            End Set
        End Property

    End Class

End Namespace