Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global

Public Class AccionObtenerVersiones

    Public Shared Function Ejecutar(Peticion As Login.ObtenerVersiones.Peticion) As Login.ObtenerVersiones.Respuesta
        Dim respuesta As New Login.ObtenerVersiones.Respuesta
        Dim objRespuestaObtenerVersiones As Seguridad.ContractoServicio.ObtenerVersiones.Respuesta

        Try

            ' Valida os dados da petição
            ValidarPeticion(Peticion)

            ' obtem as versões
            objRespuestaObtenerVersiones = ObtenerVersiones(Peticion)

            ' Verifica se foi possível recuperar as versões das aplicações
            If objRespuestaObtenerVersiones.Codigo = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' inicializa Versiones
                respuesta.Versiones = New Login.ObtenerVersiones.VersionColeccion()

                ' transforma os valores
                objRespuestaObtenerVersiones.Versiones.ForEach(
                    Sub(objVersion)
                        respuesta.Versiones.Add(New Login.ObtenerVersiones.Version() With
                                                {.CodigoVersion = objVersion.CodigoVersion})
                    End Sub)


            ElseIf objRespuestaObtenerVersiones.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                respuesta.MensajeError = objRespuestaObtenerVersiones.Descripcion
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            ElseIf objRespuestaObtenerVersiones.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                respuesta.MensajeError = objRespuestaObtenerVersiones.Descripcion
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            Else
                respuesta.MensajeError = objRespuestaObtenerVersiones.Descripcion
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            End If

        Catch ex As Excepcion.NegocioExcepcion

            respuesta.CodigoError = ex.Codigo
            respuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString()

        End Try

        Return respuesta

    End Function

    ''' <summary>
    ''' valida parámetro de entrada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende]  23/05/2012  criado
    ''' </history>
    Private Shared Sub ValidarPeticion(Peticion As Login.ObtenerVersiones.Peticion)
        ' Valida se o campo CodigoAplicacion não é nulo ou vazio.
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("gen_msg_obteneraplicacionversion_codigoaplicacion")))
        End If
    End Sub

    ''' <summary>
    ''' Executa serviço ObtenerVersiones
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ObtenerVersiones(Peticion As Login.ObtenerVersiones.Peticion) As Seguridad.ContractoServicio.ObtenerVersiones.Respuesta

        Dim objPeticionObtenerVersiones As New Seguridad.ContractoServicio.ObtenerVersiones.Peticion()
        Dim objProxyLogin As New LoginGlobal.Seguridad()

        objPeticionObtenerVersiones.CodigoAplicacion = Peticion.CodigoAplicacion

        'Obtém versão da aplicação
        Return objProxyLogin.ObtenerVersiones(objPeticionObtenerVersiones)

    End Function

End Class
