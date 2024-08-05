
Public Interface ICliente

    Function Test() As Prosegur.Genesis.ContractoServicio.Test.Respuesta

    Function GetClientes(objPeticion As Cliente.GetClientes.Peticion) As Cliente.GetClientes.Respuesta

    Function GetClientesDetalle(objPeticion As Cliente.GetClientesDetalle.Peticion) As Cliente.GetClientesDetalle.Respuesta

    Function SetClientes(Peticion As ContractoServicio.Cliente.SetClientes.Peticion) As ContractoServicio.Cliente.SetClientes.Respuesta

    Function GetClienteByCodigoAjeno(objPeticion As Cliente.GetClienteByCodigoAjeno.Peticion) As Cliente.GetClienteByCodigoAjeno.Respuesta

End Interface
