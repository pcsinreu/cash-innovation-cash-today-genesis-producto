Public Interface IParteDiferencias

    Function Test() As Test.Respuesta

    ''' <summary>
    ''' Interface que deve ser implementada para recuperar os dados de parte de diferencias
    ''' </summary>
    ''' <param name="objPeticion">Dados de entrada do relatório</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListarParteDiferencias(objPeticion As ContractoServ.ParteDiferencias.GetParteDiferencias.Peticion) As ContractoServ.ParteDiferencias.GetParteDiferencias.Respuesta

    ''' <summary>
    ''' Interface que deve ser implementada para recuperar os dados dos documentos de parte de diferencias
    ''' </summary>
    ''' <param name="objPeticion">Dados dos documentos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RecuperarDocumentos(objPeticion As ContractoServ.ParteDiferencias.GetDocumentos.Peticion) As ContractoServ.ParteDiferencias.GetDocumentos.Respuesta

End Interface