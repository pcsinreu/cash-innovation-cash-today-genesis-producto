Imports Prosegur.Global.Saldos
Imports Prosegur.Global.Saldos.ContractoServicio

Public Class AccionRecuperarCaracteristicaDocumento

    ''' <summary>
    ''' Método principal responsável por obter dados do formulario 
    ''' e chamar os metodos que converte para o objeto respuesta
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 14/07/2009 Criado
    ''' </history>
    Public Function Ejecutar(Peticion As RecuperarCaracteristicasDocumento.Peticion) As RecuperarCaracteristicasDocumento.Respuesta

        Dim objRespuesta As New RecuperarCaracteristicasDocumento.Respuesta

        Try

            ' se informou o identificador do formulário
            If Peticion.IdFormulario <> 0 Then

                ' obter dados do formulário
                Dim objFormulario As New Negocio.Formulario
                objFormulario.Id = Peticion.IdFormulario
                objFormulario.Realizar()

                ' preencher objeto respuesta
                Me.PreencherRespuesta(objRespuesta, objFormulario)

            End If

            'Caso não ocorra exceção, retorna o objrespuesta com codigo 0 e mensagem erro vazio
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Preenche o objeto respuesta com os dados do objeto formulario
    ''' </summary>
    ''' <param name="objRespuesta"></param>
    ''' <param name="objFormulario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 14/07/2009 Criado
    ''' </history>
    Private Sub PreencherRespuesta(ByRef objRespuesta As RecuperarCaracteristicasDocumento.Respuesta, _
                                   objFormulario As Negocio.Formulario)

        ' criar nova caracteristica
        objRespuesta.Caracteristica = New RecuperarCaracteristicasDocumento.Caracteristica
        objRespuesta.Caracteristica.Descripcion = objFormulario.Descripcion
        objRespuesta.Caracteristica.SeImprime = objFormulario.SeImprime
        objRespuesta.Caracteristica.EsInterplantas = objFormulario.Interplantas
        objRespuesta.Caracteristica.EsSustituible = objFormulario.Sustituible
        objRespuesta.Caracteristica.EsActaProceso = objFormulario.EsActaProceso
        objRespuesta.Caracteristica.EsConValores = objFormulario.ConValores
        objRespuesta.Caracteristica.EsConBultos = objFormulario.ConBultos
        objRespuesta.Caracteristica.EsBasadoEnReporte = objFormulario.BasadoEnReporte
        objRespuesta.Caracteristica.Campos = ConverterCampos(objFormulario.Campos)
        objRespuesta.Caracteristica.CamposExtra = ConverterCamposExtras(objFormulario.CamposExtra)

        ' obter tipo de distinguir por nivel
        If objFormulario.DistinguirPorNivel Then
            If objFormulario.Matrices Then
                objRespuesta.Caracteristica.DistinguirPorNivel = Enumeradores.eDistinguirPorNivel.SoloMatrices
            Else
                objRespuesta.Caracteristica.DistinguirPorNivel = Enumeradores.eDistinguirPorNivel.SoloSectores
            End If
        Else
            objRespuesta.Caracteristica.DistinguirPorNivel = Enumeradores.eDistinguirPorNivel.NoDistinguir
        End If

        ' obter tipo de permite creacion
        If objFormulario.SoloIndividual AndAlso objFormulario.SoloEnGrupo Then
            objRespuesta.Caracteristica.PermiteCreacion = Enumeradores.ePermiteCreacion.Ambos
        ElseIf Not objFormulario.SoloIndividual Then
            objRespuesta.Caracteristica.PermiteCreacion = Enumeradores.ePermiteCreacion.SoloEnGrupo
        Else
            objRespuesta.Caracteristica.PermiteCreacion = Enumeradores.ePermiteCreacion.SoloIndividual
        End If

        ' obter tipo de baseado en saldos
        If objFormulario.BasadoEnSaldos Then
            If objFormulario.SoloSaldoDisponible Then
                objRespuesta.Caracteristica.BasadoEnSaldos = Enumeradores.eBasadoEnSaldos.EsBasadoSoloEnSaldoDisponible
            Else
                objRespuesta.Caracteristica.BasadoEnSaldos = Enumeradores.eBasadoEnSaldos.EsBasadoEnSaldos
            End If
        Else
            objRespuesta.Caracteristica.BasadoEnSaldos = Enumeradores.eBasadoEnSaldos.NoEsBasadoEnSaldos
        End If

    End Sub

    ''' <summary>
    ''' Converte a lista de campos do objeto formulario para o objeto caracteristica do respuesta
    ''' </summary>
    ''' <param name="objCampos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 14/07/2009 Criado
    ''' </history>
    Private Function ConverterCampos(objCampos As Negocio.Campos) As RecuperarCaracteristicasDocumento.Campos

        Dim objCamposRespuesta As New RecuperarCaracteristicasDocumento.Campos

        If objCampos IsNot Nothing AndAlso objCampos.Count > 0 Then

            Dim objCampoRespuesta As RecuperarCaracteristicasDocumento.Campo = Nothing

            For Each objCampo As Negocio.Campo In objCampos

                ' criar novo campo respuesta
                objCampoRespuesta = New RecuperarCaracteristicasDocumento.Campo
                objCampoRespuesta.Etiqueta = objCampo.Etiqueta
                objCampoRespuesta.Identificador = objCampo.Id
                objCampoRespuesta.Nombre = objCampo.Nombre
                objCampoRespuesta.Tipo = objCampo.Tipo

                ' adicionar para coleção
                objCamposRespuesta.Add(objCampoRespuesta)

            Next

        End If

        Return objCamposRespuesta

    End Function

    ''' <summary>
    ''' Converte lista de campos extras do formulario para o objeto resposta
    ''' </summary>
    ''' <param name="objCamposExtras"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 14/07/2009 Criado
    ''' </history>
    Private Function ConverterCamposExtras(objCamposExtras As Negocio.CamposExtra) As RecuperarCaracteristicasDocumento.CamposExtras

        Dim objCamposExtrasRespuesta As New RecuperarCaracteristicasDocumento.CamposExtras

        If objCamposExtras IsNot Nothing AndAlso objCamposExtras.Count > 0 Then

            Dim objCampoExtraRespuesta As RecuperarCaracteristicasDocumento.CampoExtra = Nothing

            For Each objCampoExtra As Negocio.CampoExtra In objCamposExtras

                ' criar novo campo respuesta
                objCampoExtraRespuesta = New RecuperarCaracteristicasDocumento.CampoExtra
                objCampoExtraRespuesta.SeValida = objCampoExtra.SeValida
                objCampoExtraRespuesta.Identificador = objCampoExtra.Id
                objCampoExtraRespuesta.Nombre = objCampoExtra.Nombre

                ' preencher campo extra
                objCampoExtraRespuesta.TipoCampoExtra.Id = objCampoExtra.TipoCampoExtra.Id
                objCampoExtraRespuesta.TipoCampoExtra.Descripcion = objCampoExtra.TipoCampoExtra.Descripcion
                objCampoExtraRespuesta.TipoCampoExtra.Codigo = objCampoExtra.TipoCampoExtra.Codigo

                ' adicionar para coleção
                objCamposExtrasRespuesta.Add(objCampoExtraRespuesta)

            Next

        End If

        Return objCamposExtrasRespuesta

    End Function

End Class