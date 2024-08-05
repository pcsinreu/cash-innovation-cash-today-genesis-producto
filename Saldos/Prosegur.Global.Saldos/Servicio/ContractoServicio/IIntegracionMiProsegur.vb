Public Interface IIntegracionMiProsegur

    Function Test() As Test.Respuesta

    ' Assinatura do método CrearMovimiento
    Function CrearMovimiento(Peticion As ContractoServicio.CrearMovimiento.Peticion) As ContractoServicio.CrearMovimiento.Respuesta

    ' Assinatura do método RecuperarSaldos
    Function RecuperarSaldos(Peticion As ContractoServicio.RecuperarSaldos.Peticion) As ContractoServicio.RecuperarSaldos.Respuesta

    ' Assinatura do método RecuperarTransacciones
    Function RecuperarTransaccionesFechas(Peticion As ContractoServicio.RecuperarTransaccionesFechas.Peticion) As ContractoServicio.RecuperarTransaccionesFechas.Respuesta

    ' Assinatura do método RecuperarTransaccionDetallada
    Function RecuperarTransaccionDetallada(Peticion As ContractoServicio.RecuperarTransaccionDetallada.Peticion) As ContractoServicio.RecuperarTransaccionDetallada.Respuesta

    ' Assinatura do método RecuperarRemesasPorGrupo
    Function RecuperarRemesasPorGrupo(Peticion As ContractoServicio.RecuperarRemesasPorGrupo.Peticion) As ContractoServicio.RecuperarRemesasPorGrupo.Respuesta

End Interface