Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConsultarSaldos

    <XmlType(Namespace:="urn:ConsultarSaldos")> _
    <XmlRoot(Namespace:="urn:ConsultarSaldos")> _
    <Serializable()>
    Public Class Divisa

        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Disponible As Boolean
        Public Property ImporteEfectivo As Decimal
        Public Property Denominaciones As List(Of Denominacion)
        Public Property ImporteTotalEfectivo As Decimal
        Public Property ImporteMedioPago As Decimal
        Public Property MediosPago As List(Of MedioPago)
        Public Property ImporteTotalMedioPago As Decimal

    End Class

End Namespace