Imports System.Xml.Serialization

Namespace Comum

    <XmlType(Namespace:="urn:Comum")> _
    <XmlRoot(Namespace:="urn:Comum")> _
    <Serializable()> _
    Public Class ATM

        Public Property CodigoCliente As String
        Public Property DescripcionCliente As String
        Public Property CodigoSubcliente As String
        Public Property DescripcionSubcliente As String
        Public Property CodigoPuntoServicio As String
        Public Property DescripcionPuntoServicio As String

    End Class

End Namespace


