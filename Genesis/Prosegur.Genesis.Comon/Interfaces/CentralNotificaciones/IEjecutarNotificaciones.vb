Namespace Interfaces.CentralNotificaciones
    Public Interface IEjecutarNotificaciones
        Sub EjecutarAccion(accion As Acciones, parametros As Object)

    End Interface

    Public Enum Acciones
        AccionCambiarRemesaSupervisor
    End Enum

End Namespace
