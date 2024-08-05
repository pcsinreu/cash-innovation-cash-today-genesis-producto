''' <summary>
''' Interface Direccion
''' </summary>
''' <remarks></remarks>
''' <history>
''' [poncalves] 25/04/2013 Criado
''' </history>
Public Interface IDireccion

    Function Test() As Test.Respuesta

    Function GetDirecciones(Peticion As ContractoServicio.Direccion.GetDirecciones.Peticion) As ContractoServicio.Direccion.GetDirecciones.Respuesta

    Function SetDirecciones(Peticion As ContractoServicio.Direccion.SetDirecciones.Peticion) As ContractoServicio.Direccion.SetDirecciones.Respuesta

End Interface
