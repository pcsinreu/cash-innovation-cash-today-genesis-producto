Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosCashOut.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <Serializable()>
    Public Class Detalle

        Public Property CodigoCanal As String
        Public Property CodigoSubCanal As String
        Public Property CollectionId As String
        Public Property Importes As List(Of Importe)

    End Class

End Namespace