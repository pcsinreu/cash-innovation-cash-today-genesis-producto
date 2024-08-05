Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.GrabarAlertaVencimiento

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:GrabarAlertaVencimiento")> _
    <XmlRoot(Namespace:="urn:GrabarAlertaVencimiento")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

    End Class
End Namespace