Public Interface ICliente

    ' Assinatura do metodo que guarda o cliente
    Function AccionGuardarCliente(Peticion As ContractoServicio.GuardarCliente.Peticion) As ContractoServicio.GuardarCliente.Respuesta

    Function Test() As ContractoServicio.Test.Respuesta

End Interface
