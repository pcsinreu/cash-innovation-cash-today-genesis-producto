Namespace Contractos.Integracion.IntegracionSistemas.Reintentos
    Public Class Peticion
        Public Property Codigo As String
        Public Property TipoCodigo As TipoCodigo
        Public Property DeviceID As String
        Public Property IdentificadorCliente As String
        Public Property IdentificadorSubCliente As String
        Public Property IdentificadorPuntoServicio As String
        Public Property Estados As List(Of Estados)
        Public Property CodCultura As String
        Public Property CodUsuario As String
        Public Property CodProceso As String
        Public Property MensajeError As String
    End Class

    Public Enum TipoCodigo
        ActualID = 1
        CollectionID = 2
        CodigoExterno = 3
    End Enum

    Public Enum Estados
        EnvioConExito = 1
        EnvioSinExito = 2
        Pendiente = 3
    End Enum
End Namespace

