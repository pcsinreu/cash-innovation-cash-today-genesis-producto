Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.generarCertificado

    <XmlType(Namespace:="urn:generarCertificado")> _
    <XmlRoot(Namespace:="urn:generarCertificado")> _
    <Serializable()>
    Public Class Peticion

        Public Property TokenAcceso As String
        Public Property IdentificadorAjeno As String
        Public Property FechaCertificacion As DateTime
        Public Property CodigoDelegacion As String
        Public Property CodigoCliente As String
        Public Property Sectores As List(Of Sector)
        Public Property Canales As List(Of Canal)
        Public Property Opciones As Opciones

    End Class
End Namespace
