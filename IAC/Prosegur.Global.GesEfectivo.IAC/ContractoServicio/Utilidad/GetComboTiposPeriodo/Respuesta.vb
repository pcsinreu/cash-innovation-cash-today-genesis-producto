Imports System.Xml.Serialization

Namespace Utilidad.GetComboTiposPeriodo
    <XmlType(Namespace:="urn:GetComboTiposPeriodo")>
    <XmlRoot(Namespace:="urn:GetComboTiposPeriodo")>
    Public Class Respuesta
        Inherits RespuestaGenerico
        Private _tiposDePeriodos As TipoDePeriodoColeccion
        Public Property TiposDePeriodos() As TipoDePeriodoColeccion
            Get
                Return _tiposDePeriodos
            End Get
            Set(ByVal value As TipoDePeriodoColeccion)
                _tiposDePeriodos = value
            End Set
        End Property

        Public Sub New()
            TiposDePeriodos = New TipoDePeriodoColeccion()
        End Sub

    End Class
End Namespace
