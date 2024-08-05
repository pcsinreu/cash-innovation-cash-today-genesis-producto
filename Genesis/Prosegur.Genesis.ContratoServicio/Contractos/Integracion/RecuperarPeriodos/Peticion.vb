Namespace Contractos.Integracion.RecuperarPeriodos
    Public Class Peticion
        Public Property FechaInicio As Date?
        Public Property FechaFin As Date?
        Public Property Planificaciones As List(Of String)
        Public Property DeviceIDs As List(Of String)
        Public Property Bancos As List(Of String)
        Public Property EstadosPeriodos As List(Of String)
        Public Property CodUsuario As String
        Public Property CodCultura As String

        Public Sub New()
            Me.Planificaciones = New List(Of String)()
            Me.DeviceIDs = New List(Of String)()
            Me.Bancos = New List(Of String)()
            Me.EstadosPeriodos = New List(Of String)()
            Me.CodCultura = String.Empty
            Me.CodUsuario = String.Empty
        End Sub
    End Class
End Namespace
