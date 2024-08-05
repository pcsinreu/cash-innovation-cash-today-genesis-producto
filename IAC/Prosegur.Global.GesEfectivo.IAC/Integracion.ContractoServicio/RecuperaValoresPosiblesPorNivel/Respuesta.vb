Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperaValoresPosiblesPorNivel

    <XmlType(Namespace:="urn:RecuperaValoresPosiblesPorNivel")> _
    <XmlRoot(Namespace:="urn:RecuperaValoresPosiblesPorNivel")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property Terminos As TerminoRespostaColeccion

#End Region

    End Class

End Namespace