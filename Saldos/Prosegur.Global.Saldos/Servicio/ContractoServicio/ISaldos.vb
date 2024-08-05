Public Interface ISaldos

    Function Test() As Test.Respuesta

    ' Assinatura do método RecuperarCaracteristicasDocumento
    Function RecuperarCaracteristicaDocumento(Peticion As ContractoServicio.RecuperarCaracteristicasDocumento.Peticion) As ContractoServicio.RecuperarCaracteristicasDocumento.Respuesta

    ' Assinatura do método Recuperar
    Function RecuperarDatosDocumento(Peticion As ContractoServicio.RecuperarDatosDocumento.Peticion) As ContractoServicio.RecuperarDatosDocumento.Respuesta

    ' Assinatura do metodo Saldos Realizar
    Function RecuperarSaldosPorSector(Peticion As ContractoServicio.RecuperarSaldosPorSector.Peticion) As ContractoServicio.RecuperarSaldosPorSector.Respuesta

    ' Assinatura do metodo Guardar Documento
    Function AccionGuardarDatosDocmento(Peticion As ContractoServicio.GuardarDatosDocumento.Peticion) As ContractoServicio.GuardarDatosDocumento.Respuesta

    ' Assinatura do metodo Recuperar Transaccion No Migrada
    Function RecuperarTransaccionNoMigrada(objPeticion As ContractoServicio.RecuperarTransaccionNoMigrada.Peticion) As ContractoServicio.RecuperarTransaccionNoMigrada.Respuesta

End Interface