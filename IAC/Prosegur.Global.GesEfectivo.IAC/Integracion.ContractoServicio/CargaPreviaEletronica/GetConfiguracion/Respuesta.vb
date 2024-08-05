Imports System.Xml.Serialization

Namespace CargaPreviaEletronica.GetConfiguracion

    <XmlType(Namespace:="urn:GetConfiguracion")> _
    <XmlRoot(Namespace:="urn:GetConfiguracion")> _
    <Serializable()>
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Configuracion_CP As Configuracion_CP


    End Class

End Namespace