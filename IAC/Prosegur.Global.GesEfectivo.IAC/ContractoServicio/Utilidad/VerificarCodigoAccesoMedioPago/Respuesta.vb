Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoAccesoMedioPago

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:VerificarCodigoAccesoMedioPago")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoAccesoMedioPago")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Existe As Boolean

    End Class

End Namespace