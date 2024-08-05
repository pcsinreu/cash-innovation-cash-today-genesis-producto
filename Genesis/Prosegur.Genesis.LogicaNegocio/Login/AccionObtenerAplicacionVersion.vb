Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global

Public Class AccionObtenerAplicacionVersion

    Public Shared Function Ejecutar(Peticion As Login.ObtenerAplicacionVersion.Peticion) As Login.ObtenerAplicacionVersion.Respuesta

        Dim respuesta As New Login.ObtenerAplicacionVersion.Respuesta
        Dim objRespuestaObtenerAplicacionVersion As Seguridad.ContractoServicio.ObtenerAplicacionVersion.Respuesta

        Try

            ' Valida os dados da petição
            ValidarPeticion(Peticion)

            ' efetua login do usuário e retorna permissões
            objRespuestaObtenerAplicacionVersion = ObtenerAplicacionVersion(Peticion)

            ' Verifica se foi possível recuperar as versões das aplicações
            If objRespuestaObtenerAplicacionVersion.Codigo = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' inicializa aplicacionversion
                respuesta.AplicacionVersion = New Login.ObtenerAplicacionVersion.AplicacionVersion()

                ' preenche mensagem de resposta
                With respuesta
                    .AplicacionVersion.OidAplicacion = objRespuestaObtenerAplicacionVersion.AplicacionVersion.OidAplicacion
                    .AplicacionVersion.OidAplicacionVersion = objRespuestaObtenerAplicacionVersion.AplicacionVersion.OidAplicacionVersion
                    .AplicacionVersion.OidVersionPadre = objRespuestaObtenerAplicacionVersion.AplicacionVersion.OidVersionPadre
                    .AplicacionVersion.FechaVersion = objRespuestaObtenerAplicacionVersion.AplicacionVersion.FechaVersion
                    .AplicacionVersion.ContenidoVersion = objRespuestaObtenerAplicacionVersion.AplicacionVersion.ContenidoVersion
                    .AplicacionVersion.CodigoVersion = objRespuestaObtenerAplicacionVersion.AplicacionVersion.CodigoVersion
                    .AplicacionVersion.ArchivoVersion = objRespuestaObtenerAplicacionVersion.AplicacionVersion.ArchivoVersion
                End With

            ElseIf objRespuestaObtenerAplicacionVersion.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                respuesta.MensajeError = objRespuestaObtenerAplicacionVersion.Descripcion
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            ElseIf objRespuestaObtenerAplicacionVersion.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                respuesta.MensajeError = objRespuestaObtenerAplicacionVersion.Descripcion
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            Else
                respuesta.MensajeError = objRespuestaObtenerAplicacionVersion.Descripcion
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
    Private Shared Sub ValidarPeticion(Peticion As Login.ObtenerAplicacionVersion.Peticion)
        ' Valida se o campo CodigoAplicacion não é nulo ou vazio.
        If String.IsNullOrEmpty(Peticion.CodigoAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("gen_msg_obteneraplicacionversion_codigoaplicacion")))
        End If

        ' Valida se o campo CodigoVersion não é nulo ou vazio.
        If String.IsNullOrEmpty(Peticion.CodigoVersion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("gen_msg_obteneraplicacionversion_codigouversion")))
        End If
    End Sub

    ''' <summary>
    ''' Executa serviço ObtenerAplicacionVersion
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende]  23/05/2012  criado
    ''' </history>
    Private Shared Function ObtenerAplicacionVersion(Peticion As Login.ObtenerAplicacionVersion.Peticion) As Seguridad.ContractoServicio.ObtenerAplicacionVersion.Respuesta

        Dim objPeticionObtenerAplicacionVersion As New Seguridad.ContractoServicio.ObtenerAplicacionVersion.Peticion()
        Dim objProxyLogin As New LoginGlobal.Seguridad()

        objPeticionObtenerAplicacionVersion.CodigoAplicacion = Peticion.CodigoAplicacion
        objPeticionObtenerAplicacionVersion.CodigoVersion = Peticion.CodigoVersion

        'Obtém versão da aplicação
        Return objProxyLogin.ObtenerAplicacionVersion(objPeticionObtenerAplicacionVersion)

    End Function

End Class
