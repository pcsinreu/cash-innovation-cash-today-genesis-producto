Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransacciones
    Public Class RespuestaDetalle

        Public Property Cuentas As List(Of RespuestaDetalleCuenta)
        Public Property Documentos As List(Of RespuestaDetalleDocumento)
    End Class
End Namespace
