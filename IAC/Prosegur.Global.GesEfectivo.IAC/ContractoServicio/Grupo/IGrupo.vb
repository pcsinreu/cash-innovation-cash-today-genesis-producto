Public Interface IGrupo

    Function Test() As Test.Respuesta

    Function GetATMsbyGrupo(Peticion As ContractoServicio.Grupo.GetATMsbyGrupo.Peticion) As ContractoServicio.Grupo.GetATMsbyGrupo.Respuesta

    Function GetGrupos(Peticion As ContractoServicio.Grupo.GetGrupos.Peticion) As ContractoServicio.Grupo.GetGrupos.Respuesta

    Function SetGrupo(Peticion As ContractoServicio.Grupo.SetGrupo.Peticion) As ContractoServicio.Grupo.SetGrupo.Respuesta

    Function VerificarGrupo(Peticion As ContractoServicio.Grupo.VerificarGrupo.Peticion) As ContractoServicio.Grupo.VerificarGrupo.Respuesta

End Interface
