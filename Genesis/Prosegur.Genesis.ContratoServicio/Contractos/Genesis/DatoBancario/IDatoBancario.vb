Public Interface IDatoBancario

    Function GetDatosBancarios(Peticion As ContractoServicio.DatoBancario.GetDatosBancarios.Peticion) As ContractoServicio.DatoBancario.GetDatosBancarios.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface