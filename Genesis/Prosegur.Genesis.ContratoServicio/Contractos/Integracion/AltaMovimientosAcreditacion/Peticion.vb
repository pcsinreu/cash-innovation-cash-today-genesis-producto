Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosAcreditacion

    <XmlType(Namespace:="urn:AltaMovimientosAcreditacion.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosAcreditacion.Entrada")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoAcreditacion)

    End Class

End Namespace