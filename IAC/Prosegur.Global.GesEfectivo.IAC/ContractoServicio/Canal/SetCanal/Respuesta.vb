Imports System.Xml.Serialization
Imports System.Xml

Namespace Canal.SetCanal
    <XmlType(Namespace:="urn:SetCanal")> _
    <XmlRoot(Namespace:="urn:SetCanal")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _respuestaCanales As RespuestaCanalColeccion
        Private _codigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta

        Public Property RespuestaCanales() As RespuestaCanalColeccion
            Get
                Return _respuestaCanales
            End Get
            Set(value As RespuestaCanalColeccion)
                _respuestaCanales = value
            End Set
        End Property

        Public Property CodigosAjenos() As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta
            Get
                Return _codigoAjeno
            End Get
            Set(value As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta)
                _codigoAjeno = value
            End Set
        End Property
    End Class
End Namespace