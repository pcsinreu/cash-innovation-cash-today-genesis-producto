Imports System.Xml.Serialization

Namespace Utilidad.GetComboTiposCuenta
    <XmlType(Namespace:="urn:GetComboTiposCuenta")>
    <XmlRoot(Namespace:="urn:GetComboTiposCuenta")>
    Public Class Respuesta
        Inherits RespuestaGenerico
        Private _tiposDeCuentas As TipoDeCuentaColeccion
        Public Property TiposDeCuentas() As TipoDeCuentaColeccion
            Get
                Return _tiposDeCuentas
            End Get
            Set(ByVal value As TipoDeCuentaColeccion)
                _tiposDeCuentas = value
            End Set
        End Property

        Public Sub New()
            TiposDeCuentas = New TipoDeCuentaColeccion()
        End Sub

    End Class
End Namespace

