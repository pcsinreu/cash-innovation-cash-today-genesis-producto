Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPaises

    <Serializable()>
    <XmlType(Namespace:="urn:ObtenerPaises")>
    <XmlRoot(Namespace:="urn:ObtenerPaises")>
    <DataContract()>
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region " Variables "

        Private _paises As PaisColeccion

#End Region

#Region "Propriedades"

        <DataMember()>
        Public Property Paises() As PaisColeccion
            Get
                Return _paises
            End Get
            Set(value As PaisColeccion)
                _paises = value
            End Set
        End Property

#End Region

    End Class

End Namespace