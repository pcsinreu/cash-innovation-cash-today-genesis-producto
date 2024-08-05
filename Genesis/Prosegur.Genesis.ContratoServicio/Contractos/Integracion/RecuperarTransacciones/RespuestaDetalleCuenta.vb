Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransacciones
    Public Class RespuestaDetalleCuenta

        Public Property Cliente As String
        Public Property Subcliente As String
        Public Property PuntoServicio As String
        Public Property Maquina As String
        Public Property CodExterno As String
        Public Property Fechagestion As DateTime
        Public Property FechaCriacion As DateTime
        Public Property Acreditado As Boolean
        Public Property Notificado As Boolean
        Public Property Teller As String
        Public Property TipoMovimiento As String
        Public Property Formulario As String
        Public Property OidCuenta As String
        Public Property Descricao As String
        Public Property Valores As List(Of RespuestaDetalleValores)
        Public Property Totales As List(Of RespuestaDetalleTotales)


    End Class
End Namespace
