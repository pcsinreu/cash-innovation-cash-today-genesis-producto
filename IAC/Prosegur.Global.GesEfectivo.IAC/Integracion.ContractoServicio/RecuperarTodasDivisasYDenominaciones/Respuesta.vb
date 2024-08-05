Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarTodasDivisasYDenominaciones

    <XmlType(Namespace:="urn:RecuperarTodasDivisasYDenominaciones")> _
    <XmlRoot(Namespace:="urn:RecuperarTodasDivisasYDenominaciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Divisas As DivisasColeccion
#End Region

#Region "Propriedades"

        Public Property Divisas() As DivisasColeccion
            Get
                Return _Divisas
            End Get
            Set(value As DivisasColeccion)
                _Divisas = value
            End Set
        End Property
#End Region

    End Class
End Namespace