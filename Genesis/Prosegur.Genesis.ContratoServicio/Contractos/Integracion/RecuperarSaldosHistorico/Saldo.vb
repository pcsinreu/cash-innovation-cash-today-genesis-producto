Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico
    <Serializable()>
    Public Class Saldo
        Public Property Cliente As Entidad
        Public Property SubCliente As Entidad
        Public Property PuntoServicio As Entidad
        Public Property Canales As List(Of Canal)
        Public Sub New()
            Cliente = New Entidad
            SubCliente = New Entidad
            PuntoServicio = New Entidad
            Canales = New List(Of Canal)
        End Sub
    End Class
End Namespace