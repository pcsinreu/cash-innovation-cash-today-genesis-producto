Imports System.Xml.Serialization
Imports System.Xml

Namespace SetCliente

    <XmlType(Namespace:="urn:SetCliente")> _
    <XmlRoot(Namespace:="urn:SetCliente")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _respuestaClientes As RespuestaClienteColeccion

        Public Property RespuestaClientes() As RespuestaClienteColeccion
            Get
                Return _respuestaClientes
            End Get
            Set(value As RespuestaClienteColeccion)
                _respuestaClientes = value
            End Set
        End Property

    End Class

End Namespace