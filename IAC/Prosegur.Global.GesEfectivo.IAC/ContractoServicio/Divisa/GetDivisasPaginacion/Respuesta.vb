Imports System.Xml.Serialization
Imports System.Xml

Namespace Divisa.GetDivisasPaginacion

    <XmlType(Namespace:="urn:GetDivisasPaginacion")> _
    <XmlRoot(Namespace:="urn:GetDivisasPaginacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

        Private _Divisas As GetDivisas.DivisaColeccion

        Public Property Divisas() As GetDivisas.DivisaColeccion
            Get
                Return _Divisas
            End Get
            Set(value As GetDivisas.DivisaColeccion)
                _Divisas = value
            End Set
        End Property

    End Class

End Namespace