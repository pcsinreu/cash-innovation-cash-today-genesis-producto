Imports System.Xml.Serialization
Imports System.Xml

Namespace InformeResultadoContaje.BuscarResultadoContaje


    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:BuscarResultadoContaje")> _
    <XmlRoot(Namespace:="urn:BuscarResultadoContaje")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property Remesas As RemesaColeccion

#End Region

    End Class

End Namespace