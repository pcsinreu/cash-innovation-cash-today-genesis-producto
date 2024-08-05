Imports System.Xml.Serialization

Namespace Contractos.Integracion.crearDocumentoReenvio

    <Serializable()>
    Public Class Remesa

        Public Property identificador As String

        Public Property codigoExterno As String

        Public Property cuentaOrigen As Cuenta

        Public Property cuentaDestino As Cuenta

        Public Property rowver As Nullable(Of Int64)

        Public Property bultos As List(Of Bulto)

    End Class

End Namespace