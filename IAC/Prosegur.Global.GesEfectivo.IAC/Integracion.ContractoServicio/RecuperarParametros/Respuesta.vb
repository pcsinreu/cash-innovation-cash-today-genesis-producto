Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarParametros

    <XmlType(Namespace:="urn:RecuperarParametros")> _
    <XmlRoot(Namespace:="urn:RecuperarParametros")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property DatosPuesto As DatosPuesto

#End Region

    End Class

End Namespace
