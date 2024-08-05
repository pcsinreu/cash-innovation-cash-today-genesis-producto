Public Class TotalizadorSaldo

    Public Shared Function RecuperarTotalizadoresSaldos(peticion As ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Peticion) As ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Respuesta

        Dim respuesta As New ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Respuesta

        Try

            respuesta.TotalizadoresSaldos = AccesoDatos.Comon.RecuperarTotalizadoresSaldos(peticion.IdentificadorClienteSaldo, peticion.IdentificadorSubClienteSaldo, peticion.IdentificadorPuntoServicioSaldo,
                                                                                           peticion.IdentificadorClienteMovimiento, peticion.IdentificadorSubClienteMovimiento, peticion.IdentificadorPuntoServicioMovimiento,
                                                                                           peticion.IdentificadorSubCanal)

        Catch ex As Excepcion.NegocioExcepcion
            respuesta.ValidacionesError = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
            respuesta.ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError() With {.codigo = ex.Codigo, .descripcion = ex.Descricao})

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.ValidacionesError = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
            respuesta.ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError() With {.descripcion = ex.Message})

        End Try

        Return respuesta

    End Function

    Public Shared Function RecuperarTotalizadoresSaldosPorCodigo(peticion As ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Peticion) As ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Respuesta

        Dim respuesta As New ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Respuesta

        Try

            respuesta.TotalizadoresSaldos = AccesoDatos.Comon.RecuperarTotalizadoresSaldosPorCodigo(peticion.CodigoClienteSaldo, peticion.CodigoSubClienteSaldo, peticion.CodigoPuntoServicioSaldo,
                                                                                           peticion.CodigoClienteMovimiento, peticion.CodigoSubClienteMovimiento, peticion.CodigoPuntoServicioMovimiento,
                                                                                           peticion.CodigoSubCanal)

        Catch ex As Excepcion.NegocioExcepcion
            respuesta.ValidacionesError = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
            respuesta.ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError() With {.codigo = ex.Codigo, .descripcion = ex.Descricao})

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.ValidacionesError = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
            respuesta.ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError() With {.descripcion = ex.Message})

        End Try

        Return respuesta

    End Function

End Class
