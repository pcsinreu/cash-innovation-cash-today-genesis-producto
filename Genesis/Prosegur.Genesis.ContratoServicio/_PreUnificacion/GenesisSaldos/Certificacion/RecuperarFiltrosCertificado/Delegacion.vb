Imports System.Xml.Serialization
Imports System.Xml

Namespace GenesisSaldos.Certificacion.RecuperarFiltrosCertificado

    <XmlType(Namespace:="urn:RecuperarFiltrosCertificado")> _
    <XmlRoot(Namespace:="urn:RecuperarFiltrosCertificado")> _
    <Serializable()> _
    Public Class Delegacion

        Public Identificador As String
        Public Codigo As String
        Public Descripcion As String
        Public Plantas As List(Of Planta)

    End Class

End Namespace
