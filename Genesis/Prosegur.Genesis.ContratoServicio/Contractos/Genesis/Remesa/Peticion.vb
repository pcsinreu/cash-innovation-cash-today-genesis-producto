Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos

    <XmlType(Namespace:="urn:RecuperarRemesasPorIdentificadorCodigoExternos")> _
    <XmlRoot(Namespace:="urn:RecuperarRemesasPorIdentificadorCodigoExternos")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property IdentificadoresExternos As List(Of String)
        Public Property CodigosExternos As List(Of String)

    End Class

End Namespace
