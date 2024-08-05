Public Interface IInformeResutadoContaje

    Function Test() As Test.Respuesta

    Function ListarResultadoContaje(Peticion As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Peticion) As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Respuesta

    Function BuscarResultadoContaje(Peticion As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Peticion) As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Respuesta

End Interface
