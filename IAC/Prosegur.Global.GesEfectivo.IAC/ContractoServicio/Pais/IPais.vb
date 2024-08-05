Public Interface IPais

    Function Test() As Test.Respuesta

    Function GetPais() As ContractoServicio.Pais.GetPais.Respuesta

    Function GetPaisDetail(objPeticion As ContractoServicio.Pais.GetPaisDetail.Peticion) As ContractoServicio.Pais.GetPaisDetail.Respuesta

End Interface
