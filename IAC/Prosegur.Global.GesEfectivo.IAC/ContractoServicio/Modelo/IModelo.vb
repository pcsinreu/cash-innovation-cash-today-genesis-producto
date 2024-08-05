Public Interface IModelo

    Function Test() As Test.Respuesta

    Function GetModelo(objPeticion As ContractoServicio.Modelo.GetModelo.Peticion) As ContractoServicio.Modelo.GetModelo.Respuesta

End Interface
