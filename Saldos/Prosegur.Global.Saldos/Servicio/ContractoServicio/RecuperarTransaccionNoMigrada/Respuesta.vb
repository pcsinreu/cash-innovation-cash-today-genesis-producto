Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarTransaccionNoMigrada

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [marcel.espiritosanto] - 21/05/2013 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarTransaccionNoMigrada")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionNoMigrada")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Propriedade"

        Public Property HayTransaccionesNoMigradas As Boolean

#End Region

    End Class

End Namespace