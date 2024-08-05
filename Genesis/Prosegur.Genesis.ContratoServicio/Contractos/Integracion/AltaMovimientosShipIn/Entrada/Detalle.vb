Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosShipIn.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosShipIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipIn.Entrada")>
    <Serializable()>
    Public Class Detalle

        Public Property CodigoCanal As String
        Public Property CodigoSubCanal As String
        Public Property CollectionId As String
        Public Property Importes As List(Of Importe)

    End Class

End Namespace