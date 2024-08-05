Imports System.Xml.Serialization
Imports System.Xml

Imports System.Runtime.Serialization

Namespace Login.ObtenerDelegaciones

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerDelegaciones")> _
    <XmlRoot(Namespace:="urn:ObtenerDelegaciones")> _
    <DataContract()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region " Variáveis "

        Private _Delegaciones As DelegacionColeccion

#End Region

#Region "Propriedades"

        <DataMember()> _
        Public Property Delegaciones() As DelegacionColeccion
            Get
                Return _Delegaciones
            End Get
            Set(value As DelegacionColeccion)
                _Delegaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace