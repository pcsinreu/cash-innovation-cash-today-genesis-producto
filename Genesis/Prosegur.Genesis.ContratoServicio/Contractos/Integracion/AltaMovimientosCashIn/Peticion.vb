Imports System.Xml.Serialization


Namespace Contractos.Integracion.AltaMovimientosCashIn
    <XmlType(Namespace:="urn:AltaMovimientosCashIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashIn.Entrada")>
    <Serializable()>
    Public Class Peticion
        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoCashIn)
    End Class
End Namespace

