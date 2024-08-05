Imports System.Xml.Serialization
Imports System.ComponentModel
Imports Prosegur.Genesis.Comon

Namespace Contractos.Integracion.ActualizarMovimientos

    <XmlType(Namespace:="urn:ActualizarMovimientos")>
    <XmlRoot(Namespace:="urn:ActualizarMovimientos")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest

        <XmlArray("Movimientos"), XmlArrayItem(GetType(MovimientoEntrada), ElementName:="Movimiento")>
        Public Property Movimientos As List(Of MovimientoEntrada)

        Public Property Accion As Enumeradores.AccionActualizarMovimiento
        'Public Property CodigosExterno As List(Of String)

        Public Property FechaHora As DateTime

        Public Property SistemaOrigen As String
        Public Property SistemaDestino As String
        Public Property Mensaje As String

        '<DefaultValue(False)>
        'Public Property Acreditar As Boolean

        '<DefaultValue(False)>
        'Public Property Notificar As Boolean

    End Class

End Namespace