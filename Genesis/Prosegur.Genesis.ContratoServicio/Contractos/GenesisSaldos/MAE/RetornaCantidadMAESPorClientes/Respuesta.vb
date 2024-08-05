Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes

    <XmlType(Namespace:="urn:RetornaCantidadMAESPorClientes")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadMAESPorClientes")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property Dados As List(Of Dados)

    End Class

    <XmlType(Namespace:="urn:RetornaCantidadMAESPorClientes")> _
    <XmlRoot(Namespace:="urn:RetornaCantidadMAESPorClientes")> _
    <Serializable()>
    Public Class Dados
        Public Cliente As String
        Public Cantidad As Integer
    End Class

End Namespace