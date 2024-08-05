Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoAccesoDivisa

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:VerificarCodigoAccesoDivisa")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoAccesoDivisa")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Existe As Boolean

    End Class

End Namespace