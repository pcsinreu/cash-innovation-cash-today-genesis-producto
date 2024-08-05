Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global.Seguridad.ContractoServicio

Namespace Interfaces.Dashboard
    Public Interface ILoginGlobal
        Function Login(Peticion As Prosegur.Global.Seguridad.ContractoServicio.Login.Peticion) As Prosegur.Global.Seguridad.ContractoServicio.Login.Respuesta
        Function ObtenerDelegaciones(Peticion As ObtenerDelegaciones.Peticion) As ObtenerDelegaciones.Respuesta
        Function ObtenerPermisosUsuario(Peticion As ObtenerPermisosUsuario.Peticion) As ObtenerPermisosUsuario.Respuesta

    End Interface

End Namespace