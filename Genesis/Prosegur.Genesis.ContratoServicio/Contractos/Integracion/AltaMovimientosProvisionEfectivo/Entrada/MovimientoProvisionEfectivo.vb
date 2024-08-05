Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosProvisionEfectivo.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosProvisionEfectivo.Entrada")>
    <Serializable()>
    Public Class MovimientoProvisionEfectivo

        Public Property CodigoBancoCapital As String
        Public Property FechaHora As DateTime
        Public Property FechaContable As DateTime?
        Public Property ActualId As String
        Public Property CollectionId As String
        Public Property CamposExtras As List(Of CampoExtra)
        Public Property Importes As List(Of Importe)

    End Class
End Namespace