Public Interface IIngresoRemesa

    Function Test() As Test.Respuesta

    ' Assinatura do metodo que faz o Ingreso de Remesas
    Function AccionIngresoRemesas(Peticion As ContractoServicio.IngresoRemesas.Peticion) As ContractoServicio.IngresoRemesas.Respuesta

    ' Assinatura do metodo que recupera centro de procesos
    Function AccionRecuperarCentroProceso(Peticion As ContractoServicio.RecuperarCentroProceso.Peticion) As ContractoServicio.RecuperarCentroProceso.Respuesta

End Interface