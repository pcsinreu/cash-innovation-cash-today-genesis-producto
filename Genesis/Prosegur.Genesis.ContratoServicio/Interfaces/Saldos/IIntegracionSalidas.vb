Public Interface IIntegracionSalidas

    Function Test() As Test.Respuesta

    ' Assinatura do método RecuperarCaracteristicasDocumento
    Function GeneracionF22(Peticion As Legado.GeneracionF22.Peticion) As Legado.GeneracionF22.Respuesta

    ' Assinatura do método CreacionMifIntersector
    Function CreacionMifIntersector(Peticion As Prosegur.Global.Saldos.ContractoServicio.CreacionMifIntersector.Peticion) As Prosegur.Global.Saldos.ContractoServicio.CreacionMifIntersector.Respuesta

    'Assinatura do método CambiaEstadoDocumentoFondosSaldos
    Function CambiaEstadoDocumentoFondosSaldos(Peticion As Prosegur.Global.Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Peticion) As Prosegur.Global.Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Respuesta

End Interface
