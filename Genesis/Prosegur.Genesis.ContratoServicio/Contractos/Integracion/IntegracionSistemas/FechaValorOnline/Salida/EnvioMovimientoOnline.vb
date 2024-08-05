Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class EnvioMovimientoOnline

        Public Property Delegacion As Delegacion
        Public Property Planta As Planta
        Public Property Maquina As Maquina
        Public Property Cliente As Cliente
        Public Property SubCliente As SubCliente
        Public Property PuntoServicio As PuntoServicio
        Public Property ActualID As String
        Public Property Movimientos As List(Of Movimiento)
        Public Property Planificacion As Planificacion
    End Class
End Namespace