Imports System.Xml.Serialization
Imports System.Xml

Namespace NuevoSalidas.Bulto.RecuperarBultosRemesas

    <XmlType(Namespace:="urn:RecuperarBultosRemesas")> _
    <XmlRoot(Namespace:="urn:RecuperarBultosRemesas")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[VARIABLES]"

        Private _Remesas As RemesaColeccion

#End Region

#Region "[PROPIEDADES]"

        Public Property Remesas() As RemesaColeccion
            Get
                Return _Remesas
            End Get
            Set(ByVal value As RemesaColeccion)
                _Remesas = value
            End Set
        End Property

#End Region

    End Class

End Namespace