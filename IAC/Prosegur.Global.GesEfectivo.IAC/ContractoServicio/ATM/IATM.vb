Public Interface IATM

    Function Test() As Test.Respuesta

    Function GetATMDetail(Peticion As ContractoServicio.ATM.GetATMDetail.Peticion) As ContractoServicio.ATM.GetATMDetail.Respuesta

    Function SetATM(Peticion As ContractoServicio.ATM.SetATM.Peticion) As ContractoServicio.ATM.SetATM.Respuesta

    Function GetATMs(Peticion As ContractoServicio.GetATMs.Peticion) As ContractoServicio.GetATMs.Respuesta

End Interface
