Public Interface ISetor

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Assinatura dos métodos do WebService
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/03/2013 Alterado
    ''' </history>
    Function getSectores(objPeticion As ContractoServicio.Setor.GetSectores.Peticion) As ContractoServicio.Setor.GetSectores.Respuesta

    Function GetSectoresIAC(objPeticion As ContractoServicio.Sector.GetSectoresIAC.Peticion) As ContractoServicio.Sector.GetSectoresIAC.Respuesta

    Function setSectores(ObjPeticion As ContractoServicio.Setor.SetSectores.Peticion) As ContractoServicio.Setor.SetSectores.Respuesta

    Function getSetorDetail(objPeticion As ContractoServicio.Setor.GetSectoresDetail.Peticion) As ContractoServicio.Setor.GetSectoresDetail.Respuesta

    Function GetSectoresTesoro(objPeticion As ContractoServicio.Sector.GetSectoresTesoro.Peticion) As ContractoServicio.Sector.GetSectoresTesoro.Respuesta
End Interface
