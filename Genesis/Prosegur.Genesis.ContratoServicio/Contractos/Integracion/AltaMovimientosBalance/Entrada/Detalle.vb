Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosBalance.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <Serializable()>
    Public Class Detalle

        Public Property CodigoCanal As String
        Public Property CodigoSubCanal As String
        Public Property CollectionId As String
        Public Property Importes As List(Of Importe)

    End Class

End Namespace