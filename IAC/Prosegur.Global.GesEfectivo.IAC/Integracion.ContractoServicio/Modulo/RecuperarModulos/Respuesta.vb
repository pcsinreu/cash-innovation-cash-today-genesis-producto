Imports System.Xml.Serialization
Imports System.Xml

Namespace Modulo.RecuperarModulos

    <XmlType(Namespace:="urn:RecuperarModulos")> _
    <XmlRoot(Namespace:="urn:RecuperarModulos")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _modulos As List(Of String)
        Public Property Modulos() As List(Of String)
            Get
                Return _modulos
            End Get
            Set(value As List(Of String))
                _modulos = value
            End Set
        End Property

    End Class

End Namespace