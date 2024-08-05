''' <summary>
''' Interface Modulo
''' </summary>
''' <remarks></remarks>

Public Interface IModulo

    Function RecuperarModulos(Peticion As ContractoServicio.Modulo.RecuperarModulo.Peticion) As ContractoServicio.Modulo.RecuperarModulo.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface