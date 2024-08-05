Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.generarCertificado

    <XmlType(Namespace:="urn:generarCertificado")> _
    <XmlRoot(Namespace:="urn:generarCertificado")> _
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseRespuesta

        Public Property CodErr As String
        Public Property MenErr As String
        Public Property CodCer As String
        Public Property EstCer As String
        Public Property FecCer As DateTime
        Public Property Docs As List(Of Doc)
        Public Property TimeZone As String

    End Class
End Namespace

