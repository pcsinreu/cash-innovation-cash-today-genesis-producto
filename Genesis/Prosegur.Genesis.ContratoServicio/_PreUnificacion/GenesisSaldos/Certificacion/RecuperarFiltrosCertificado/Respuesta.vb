Imports System.Xml.Serialization
Imports System.Xml

Namespace GenesisSaldos.Certificacion.RecuperarFiltrosCertificado

    <XmlType(Namespace:="urn:RecuperarFiltrosCertificado")> _
    <XmlRoot(Namespace:="urn:RecuperarFiltrosCertificado")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public IdentificadorCertificado As String
        Public CodigoCertificado As String
        Public CodigoExternoCertificado As String
        Public CodigoEstado As String
        Public FechaHoraCertificado As DateTime
        Public IdentificadorCliente As String
        Public CodigoCliente As String
        Public DescripcionCliente As String
        Public TodosSectores As Boolean
        Public TodosSubCanales As Boolean
        Public TodasDelegaciones As Boolean
        Public SubCanales As List(Of RecuperarFiltrosCertificado.SubCanal)
        Public Delegaciones As List(Of RecuperarFiltrosCertificado.Delegacion)
        Public Sectores As List(Of RecuperarFiltrosCertificado.Sector)

    End Class
End Namespace

