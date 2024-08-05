Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoAccesoDenominacion

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:VerificarCodigoAccesoDenominacion")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoAccesoDenominacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Existe As Boolean

    End Class

End Namespace