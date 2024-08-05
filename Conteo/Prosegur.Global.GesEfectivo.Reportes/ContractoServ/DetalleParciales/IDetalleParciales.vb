Public Interface IDetalleParciales

    ''' <summary>
    ''' Interface que deve ser implementada para recuperar os dados do detalle parciales
    ''' </summary>
    ''' <param name="objPeticion">Dados de entrada do relatório</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListarDetalleParciales(objPeticion As ContractoServ.DetalleParciales.GetDetalleParciales.Peticion) As ContractoServ.DetalleParciales.GetDetalleParciales.Respuesta

    ''' <summary>
    ''' Interface Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Test() As ContractoServ.Test.Respuesta

End Interface