Public Interface IPuesto

    Function Test() As Test.Respuesta

    Function GetPuestos(Peticion As ContractoServicio.Puesto.GetPuestos.Peticion) As ContractoServicio.Puesto.GetPuestos.Respuesta

    Function GetPuestoDetail(Peticion As ContractoServicio.Puesto.GetPuestoDetail.Peticion) As ContractoServicio.Puesto.GetPuestoDetail.Respuesta

    Function SetPuesto(Peticion As ContractoServicio.Puesto.SetPuesto.Peticion) As ContractoServicio.Puesto.SetPuesto.Respuesta

End Interface
