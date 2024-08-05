Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos

    <XmlType(Namespace:="urn:RecuperarRemesasPorIdentificadorCodigoExternos")> _
    <XmlRoot(Namespace:="urn:RecuperarRemesasPorIdentificadorCodigoExternos")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property IdentificadoresRemesas As List(Of String)

    End Class

End Namespace
