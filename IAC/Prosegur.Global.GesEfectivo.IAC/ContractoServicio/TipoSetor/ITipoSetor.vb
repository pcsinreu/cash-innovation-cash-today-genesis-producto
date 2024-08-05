Public Interface ITipoSetor

    ''' <summary>
    ''' Assinatura dos métodos do WebService
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/03/2013 Alterado
    ''' </history>
    Function GetTiposSectores(objPeticion As ContractoServicio.TipoSetor.GetTiposSectores.Peticion) As ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

    Function SetTiposSectores(objPeticion As ContractoServicio.TipoSetor.SetTiposSectores.Peticion) As ContractoServicio.TipoSetor.SetTiposSectores.Respuesta

    Function GetCaractNoPertenecTipoSector(objPeticion As ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Peticion) As ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface
