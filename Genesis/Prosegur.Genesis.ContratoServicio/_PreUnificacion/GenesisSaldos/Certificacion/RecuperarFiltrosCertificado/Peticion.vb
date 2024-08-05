Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos.Certificacion.RecuperarFiltrosCertificado

    <XmlType(Namespace:="urn:RecuperarFiltrosCertificado")> _
    <XmlRoot(Namespace:="urn:RecuperarFiltrosCertificado")> _
    <Serializable()> _
    Public Class Peticion

        Public IdentificadorCertificado As String
        Public CodigoCertificado As String
        Public Delegacion As Clases.Delegacion

    End Class

End Namespace

