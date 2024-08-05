''' <summary>
''' Interface Termino
''' </summary>
''' <remarks></remarks>
''' <history>
''' [pda] 09/02/2009 Criado
''' </history>
Public Interface ITermino

    Function getTerminos(Peticion As ContractoServicio.TerminoIac.GetTerminoIac.Peticion) As ContractoServicio.TerminoIac.GetTerminoIac.Respuesta

    Function getTerminoDetail(Peticion As ContractoServicio.TerminoIac.GetTerminoDetailIac.Peticion) As ContractoServicio.TerminoIac.GetTerminoDetailIac.Respuesta

    Function setTermino(Peticion As ContractoServicio.TerminoIac.SetTerminoIac.Peticion) As ContractoServicio.TerminoIac.SetTerminoIac.Respuesta

    Function VerificarCodigoTermino(Peticion As ContractoServicio.TerminoIac.VerificarCodigoTermino.Peticion) As ContractoServicio.TerminoIac.VerificarCodigoTermino.Respuesta

    Function VerificarDescripcionTermino(Peticion As ContractoServicio.TerminoIac.VerificarDescripcionTermino.Peticion) As ContractoServicio.TerminoIac.VerificarDescripcionTermino.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface