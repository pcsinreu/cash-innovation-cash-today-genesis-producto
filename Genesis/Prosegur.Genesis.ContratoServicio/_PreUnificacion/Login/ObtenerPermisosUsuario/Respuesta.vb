Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
   <XmlType(Namespace:="urn:ObtenerPermisosUsuario")> _
   <XmlRoot(Namespace:="urn:ObtenerPermisosUsuario")> _
   <DataContract()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region " Variáveis "

        Private _Continentes As New ContinenteColeccion

#End Region

#Region "Propriedades"

        <DataMember()> _
        Public Property Continentes() As ContinenteColeccion
            Get
                Return _Continentes
            End Get
            Set(value As ContinenteColeccion)
                _Continentes = value
            End Set
        End Property


#End Region

    End Class

End Namespace