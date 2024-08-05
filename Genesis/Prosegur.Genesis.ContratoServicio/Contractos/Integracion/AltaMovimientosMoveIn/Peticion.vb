Imports System.Xml.Serialization


Namespace Contractos.Integracion.AltaMovimientosMoveIn
    <XmlType(Namespace:="urn:AltaMovimientosMoveIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosMoveIn.Entrada")>
    <Serializable()>
    Public Class Peticion
        Public Property Configuracion As Entrada.Configuracion
        Public Property CodigoPais As String
        Public Property Movimientos As List(Of Entrada.MovimientoMoveIn)
    End Class
End Namespace

