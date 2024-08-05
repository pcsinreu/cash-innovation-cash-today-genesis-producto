
Public Interface ISubCliente

    Function Test() As Prosegur.Genesis.ContractoServicio.Test.Respuesta

    Function GetSubClientes(objPeticion As SubCliente.GetSubClientes.Peticion) As SubCliente.GetSubClientes.Respuesta

    Function GetSubClientesDetalle(objPeticion As SubCliente.GetSubClientesDetalle.Peticion) As SubCliente.GetSubClientesDetalle.Respuesta

    Function SetSubClientes(Peticion As ContractoServicio.SubCliente.SetSubClientes.Peticion) As ContractoServicio.SubCliente.SetSubClientes.Respuesta

    Function GetSubclienteByCodigoAjeno(objPeticion As ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Peticion) As ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Respuesta

End Interface
