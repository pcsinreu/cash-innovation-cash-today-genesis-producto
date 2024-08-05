Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerVersiones

    <Serializable()>
    <XmlType(Namespace:="urn:ObtenerVersiones")>
    <XmlRoot(Namespace:="urn:ObtenerVersiones")>
    <DataContract()>
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region " Variáveis "

        Private _Versiones As VersionColeccion

#End Region

#Region "Propriedades"

        <DataMember()> _
        Public Property Versiones() As VersionColeccion
            Get
                Return _Versiones
            End Get
            Set(value As VersionColeccion)
                _Versiones = value
            End Set
        End Property

#End Region

    End Class

End Namespace