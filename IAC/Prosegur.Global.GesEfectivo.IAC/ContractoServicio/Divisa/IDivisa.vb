''' <summary>
''' Interface Divisa
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 27/01/2009 Criado
''' </history>
Public Interface IDivisa

    Function getDivisas(Peticion As ContractoServicio.Divisa.GetDivisas.Peticion) As ContractoServicio.Divisa.GetDivisas.Respuesta

    Function GetDivisasPaginacion(Peticion As ContractoServicio.Divisa.GetDivisasPaginacion.Peticion) As ContractoServicio.Divisa.GetDivisasPaginacion.Respuesta

    Function getDenominacionesByDivisa(Peticion As ContractoServicio.Divisa.GetDenominacionesByDivisa.Peticion) As ContractoServicio.Divisa.GetDenominacionesByDivisa.Respuesta

    Function setDivisaDenominaciones(Peticion As ContractoServicio.Divisa.SetDivisasDenominaciones.Peticion) As ContractoServicio.Divisa.SetDivisasDenominaciones.Respuesta

    Function VerificarCodigoDivisa(Peticion As ContractoServicio.Divisa.VerificarCodigoDivisa.Peticion) As ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta

    Function VerificarDescripcionDivisa(Peticion As ContractoServicio.Divisa.VerificarDescripcionDivisa.Peticion) As ContractoServicio.Divisa.VerificarDescripcionDivisa.Respuesta

    Function VerificarCodigoDenominacion(Peticion As ContractoServicio.Divisa.VerificarCodigoDenominacion.Peticion) As ContractoServicio.Divisa.VerificarCodigoDenominacion.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface