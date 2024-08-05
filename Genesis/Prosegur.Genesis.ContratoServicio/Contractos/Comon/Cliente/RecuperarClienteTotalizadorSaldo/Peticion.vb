Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo

    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarClienteTotalizadorSaldo")> _
    <XmlRoot(Namespace:="urn:RecuperarClienteTotalizadorSaldo")> _
    Public Class Peticion

        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property CodigoSubCanal As String

    End Class

End Namespace