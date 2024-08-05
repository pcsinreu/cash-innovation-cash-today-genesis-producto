Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo

    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarClienteTotalizadorSaldo")> _
    <XmlRoot(Namespace:="urn:RecuperarClienteTotalizadorSaldo")> _
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property ClienteSaldo As Prosegur.Genesis.Comon.Clases.Cliente

    End Class

End Namespace