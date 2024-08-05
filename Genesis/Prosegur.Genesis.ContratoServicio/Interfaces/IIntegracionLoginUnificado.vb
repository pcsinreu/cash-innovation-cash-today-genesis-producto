Imports Prosegur.Global.Saldos.ContractoServicio

Namespace Interfaces

    Public Interface IIntegracionLoginUnificado

        Function AutenticarUsuarioAplicacionLoginUnificado(Peticion As ContractoServicio.Login.EjecutarLogin.Peticion) As ContractoServicio.Login.AutenticarUsuarioAplicacion.AutenticarUsuarioAplicacionRespuesta

        Function CrearTokenAcceso(Peticion As Genesis.ContractoServicio.Login.CrearTokenAcceso.Peticion) As Genesis.ContractoServicio.Login.CrearTokenAcceso.Respuesta

        Function EjecutarLogin(Peticion As Genesis.ContractoServicio.Login.EjecutarLogin.Peticion) As Genesis.ContractoServicio.Login.EjecutarLogin.Respuesta
    End Interface

End Namespace