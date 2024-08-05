Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos

    <Serializable()>
    Public Class Saldo

        Public Property Cliente As Comon.Entidad

        Public Property SubCliente As Comon.Entidad

        Public Property PuntoServicio As Comon.Entidad

        Public Property CodigoIsoDivisa As String

        Public Property Movimientos As List(Of Movimiento)

        Public Property Divisas As List(Of Divisa)
    End Class

End Namespace