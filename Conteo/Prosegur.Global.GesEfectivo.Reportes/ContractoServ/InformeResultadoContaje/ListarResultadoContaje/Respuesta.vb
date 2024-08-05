Imports System.Xml.Serialization
Imports System.Xml

Namespace InformeResultadoContaje.ListarResultadoContaje

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:ListarResultadoContaje")> _
    <XmlRoot(Namespace:="urn:ListarResultadoContaje")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property Remesa As Remesa

#End Region

    End Class
End Namespace