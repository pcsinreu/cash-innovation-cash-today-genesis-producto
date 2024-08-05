''' <summary>
''' Interface GrupoCliente
''' </summary>
''' <remarks></remarks>
''' <history>
''' [matheus.araujo] 24/10/2012 Criado
''' </history>
Public Interface IGrupoCliente

    Function Test() As Test.Respuesta

    Function GetGruposCliente(Peticion As GrupoCliente.GetGruposCliente.Peticion) As GrupoCliente.GetGruposCliente.Respuesta

    Function GetGruposClientesDetalle(Peticion As GrupoCliente.GetGruposClientesDetalle.Peticion) As GrupoCliente.GetGruposClientesDetalle.Respuesta

    Function SetGrupoCliente(Peticion As GrupoCliente.SetGruposCliente.Peticion) As GrupoCliente.SetGruposCliente.Respuesta

End Interface
