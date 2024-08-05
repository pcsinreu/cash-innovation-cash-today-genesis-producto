Imports System.Xml.Serialization
Imports System.Xml

Namespace GetParametrosDelegacionPais

    <XmlType(Namespace:="urn:GetParametrosDelegacionPais")> _
    <XmlRoot(Namespace:="urn:GetParametrosDelegacionPais")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property Parametros As ParametroRespuestaColeccion

#End Region

    End Class

End Namespace