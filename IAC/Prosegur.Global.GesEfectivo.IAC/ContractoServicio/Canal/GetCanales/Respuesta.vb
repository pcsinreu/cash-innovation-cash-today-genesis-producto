Imports System.Xml.Serialization
Imports System.Xml

Namespace Canal.GetCanales

    <XmlType(Namespace:="urn:GetCanales")> _
    <XmlRoot(Namespace:="urn:GetCanales")> _
    <Serializable()> _
Public Class Respuesta
        Inherits RespuestaGenerico

        Private _Canales As CanalColeccion

        Public Property Canales() As CanalColeccion
            Get
                Return _Canales
            End Get
            Set(value As CanalColeccion)
                _Canales = value
            End Set
        End Property


    End Class

End Namespace