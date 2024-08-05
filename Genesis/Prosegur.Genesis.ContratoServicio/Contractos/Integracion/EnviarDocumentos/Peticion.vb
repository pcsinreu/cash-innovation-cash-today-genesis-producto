Imports System.Xml.Serialization

Namespace Contractos.Integracion.EnviarDocumentos

    <XmlType(Namespace:="urn:EnviarDocumentos.Entrada")>
    <XmlRoot(Namespace:="urn:EnviarDocumentos.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property ActualIds As List(Of String)

    End Class

End Namespace
