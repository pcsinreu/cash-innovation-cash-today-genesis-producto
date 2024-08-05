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
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property DatosDocumento As DatosDocumento
        Public Property Usuario As Usuario

#End Region

    End Class
End Namespace