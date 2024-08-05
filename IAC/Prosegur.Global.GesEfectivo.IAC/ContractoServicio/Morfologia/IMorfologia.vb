Public Interface IMorfologia

    Function Test() As Test.Respuesta

    Function GetMorfologia(Peticion As ContractoServicio.Morfologia.GetMorfologia.Peticion) As ContractoServicio.Morfologia.GetMorfologia.Respuesta

    Function SetMorfologia(Peticion As ContractoServicio.Morfologia.SetMorfologia.Peticion) As ContractoServicio.Morfologia.SetMorfologia.Respuesta

    Function VerificarMorfologia(Peticion As ContractoServicio.Morfologia.VerificarMorfologia.Peticion) As ContractoServicio.Morfologia.VerificarMorfologia.Respuesta

End Interface
