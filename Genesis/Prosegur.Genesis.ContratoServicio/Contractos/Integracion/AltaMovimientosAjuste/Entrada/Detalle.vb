Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosAjuste.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosAjuste.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosAjuste.Entrada")>
    <Serializable()>
    Public Class Detalle

        Public Property CodigoCanal As String
        Public Property CodigoSubCanal As String
        Public Property CollectionId As String
        Public Property Importes As List(Of Importe)

    End Class

End Namespace