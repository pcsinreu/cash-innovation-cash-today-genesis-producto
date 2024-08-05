Public Interface IPlanta

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Interface Planta
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 19/02/2012 Criado
    ''' </history>
    Function GetPlanta(objPeticion As ContractoServicio.Planta.GetPlanta.Peticion) As ContractoServicio.Planta.GetPlanta.Respuesta

    Function SetPlanta(objPeticion As ContractoServicio.Planta.SetPlanta.Peticion) As ContractoServicio.Planta.SetPlanta.Respuesta

    Function GetPlantaDetail(ObjPeticion As ContractoServicio.Planta.GetPlantaDetail.Peticion) As ContractoServicio.Planta.GetPlantaDetail.Respuesta

    Function VerificaExistencia(ObjPeticion As ContractoServicio.Planta.VerificaExistencia.Peticion) As ContractoServicio.Planta.VerificaExistencia.Respuesta

    Function VerificaCodigoPlanta(objPeticion As ContractoServicio.Planta.VerificaCodigoPlanta.Peticion) As ContractoServicio.Planta.VerificaCodigoPlanta.Respuesta
End Interface

