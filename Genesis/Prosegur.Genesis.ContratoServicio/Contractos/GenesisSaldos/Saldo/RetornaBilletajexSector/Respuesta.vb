Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.Saldo.RetornaBilletajexSector

    <XmlType(Namespace:="urn:RetornaBilletajexSector")> _
    <XmlRoot(Namespace:="urn:RetornaBilletajexSector")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaBilletajexSector")> _
    <XmlRoot(Namespace:="urn:RetornaBilletajexSector")> _
    <Serializable()>
    Public Class Dados
        Public DescricaoDivisa As String
        Public DescricaoDenominacao As String
        Public TipoValor As String
        Public Disponivel As Boolean?
        Public Saldo As Decimal?
    End Class

End Namespace