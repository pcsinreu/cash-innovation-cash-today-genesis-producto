Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.crearDocumentoReenvio

    <XmlType(Namespace:="urn:crearDocumentoReenvio")> _
    <XmlRoot(Namespace:="urn:crearDocumentoReenvio")> _
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseRespuesta

        ''' <summary>
        ''' Código del comprobante generado en Génesis.
        ''' </summary>
        Public Property codigoComprobante As String

    End Class

End Namespace
