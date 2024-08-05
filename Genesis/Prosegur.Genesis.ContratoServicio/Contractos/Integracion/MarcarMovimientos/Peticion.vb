Imports System.Xml.Serialization
Imports System.ComponentModel
Imports Prosegur.Genesis.Comon

Namespace Contractos.Integracion.MarcarMovimientos

    <XmlType(Namespace:="urn:MarcarMovimientos")>
    <XmlRoot(Namespace:="urn:MarcarMovimientos")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest

        '<XmlArray("Movimientos"), XmlArrayItem(GetType(MovimientoEntrada), ElementName:="Movimiento")>
        'Public Property Movimientos As List(Of MovimientoEntrada)

        '  Public Property Accion As Enumeradores.AccionMarcarMovimiento
        Public Property CodigosExterno As List(Of String)

        'Public Property FechaHora As DateTime

        'Public Property SistemaOrigen As String
        'Public Property SistemaDestino As String
        'Public Property Mensaje As String

        <DefaultValue(False)>
        Public Property Acreditar As Boolean

        <DefaultValue(False)>
        Public Property Notificar As Boolean

    End Class

End Namespace