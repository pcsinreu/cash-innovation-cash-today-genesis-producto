Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization

Namespace Contractos.Integracion.crearDocumentoFondos

    <XmlType(Namespace:="urn:crearDocumentoFondos")> _
    <XmlRoot(Namespace:="urn:crearDocumentoFondos")> _
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseRespuesta

        ''' <summary>
        ''' Datos del documento generado, en caso de ser generado el documento se enviará este elemento.
        ''' </summary>
        Public Property documento As Comon.Documento
        Public Function ToXML() As String
            Dim sw1 = New StringWriter()
            Dim xs1 As New XmlSerializer(GetType(Respuesta))
            xs1.Serialize(New XmlTextWriter(sw1), Me)
            Return sw1.ToString
        End Function
    End Class

End Namespace
