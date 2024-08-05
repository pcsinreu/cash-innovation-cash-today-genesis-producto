Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.ObtenerPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.GuardarPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.BorrarPreferenciasAplicacion

Public Interface IPreferencias

    Function ObtenerPreferencias(peticion As ObtenerPreferenciasPeticion) As ObtenerPreferenciasRespuesta

    Function GuardarPreferencias(peticion As GuardarPreferenciasPeticion) As GuardarPreferenciasRespuesta

    Function BorrarPreferenciasAplicacion(peticion As BorrarPreferenciasAplicacionPeticion) As BorrarPreferenciasAplicacionRespuesta

End Interface
