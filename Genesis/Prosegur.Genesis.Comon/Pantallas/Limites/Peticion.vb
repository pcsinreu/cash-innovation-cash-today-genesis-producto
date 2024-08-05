Namespace Pantallas.Limites
    Public Class Peticion
        Public Property FechaInicio As Date?
        Public Property FechaFin As Date?
        Public Property OidPlanificaciones As List(Of String)
        Public Property OidDeviceIDs As List(Of String)
        Public Property OidBancos As List(Of String)
        Public Property OidEstadosPeriodos As List(Of String)
        Public Property CodUsuario As String
        Public Property CodCultura As String

        Public Sub New()
            Me.OidPlanificaciones = New List(Of String)()
            Me.OidDeviceIDs = New List(Of String)()
            Me.OidBancos = New List(Of String)()
            Me.OidEstadosPeriodos = New List(Of String)()
            Me.CodCultura = String.Empty
            Me.CodUsuario = String.Empty
        End Sub
    End Class

End Namespace