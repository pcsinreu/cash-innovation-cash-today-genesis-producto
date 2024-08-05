Public Interface IMaquina

    Function Test() As Test.Respuesta

    Function GetMaquina(objPeticion As ContractoServicio.Maquina.GetMaquina.Peticion) As ContractoServicio.Maquina.GetMaquina.Respuesta

    Function GetTransacaoMaquina(oidPlanta As String) As ContractoServicio.Maquina.GetMaquinaTransacao.Respuesta

End Interface
