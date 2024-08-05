Imports System.Xml.Serialization
Imports System.Xml

Namespace CambiaEstadoDocumentoFondosSaldos

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/04/2011 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:CambiaEstadoDocumentoFondosSaldos")> _
    <XmlRoot(Namespace:="urn:CambiaEstadoDocumentoFondosSaldos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property codEstado As String
        Public Property cod_comprobante As String

#End Region

    End Class
End Namespace