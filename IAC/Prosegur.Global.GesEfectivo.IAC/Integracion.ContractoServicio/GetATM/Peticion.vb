Imports System.Xml.Serialization
Imports System.Xml

Namespace GetATM

    <XmlType(Namespace:="urn:GetATM")> _
    <XmlRoot(Namespace:="urn:GetATM")> _
    <Serializable()> _
    Public Class Peticion

        Public Property CodigoCliente As String
        Public Property CodigoSubcliente As String
        Public Property CodigoPuntoServicio As String
        Public Property CodigoClienteFaturacion As String
        Public Property FechaServicio As Date

    End Class

End Namespace




