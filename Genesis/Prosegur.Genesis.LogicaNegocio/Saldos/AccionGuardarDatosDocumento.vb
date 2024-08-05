Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.Saldos
Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Global.Saldos.ContractoServicio.Enumeradores

Public Class AccionGuardarDatosDocumento

#Region "[VARIAVEIS]"

    Private _UsuarioAtual As New Negocio.Usuario

#End Region

#Region "[PROPRIEDADES]"

    Private Property UsuarioAtual() As Negocio.Usuario
        Get
            Return _UsuarioAtual
        End Get
        Set(value As Negocio.Usuario)
            _UsuarioAtual = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Metodo responsável por validar e efetuar as inserções na base de dados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/07/2009 Criado
    ''' [vinicius.gama] 13/10/09 Alterado - Novo parametro para Accion, agora o documento pode ser criado e aceito no mesmo processo.
    ''' </history>
    Public Function Ejecutar(Peticion As GuardarDatosDocumento.Peticion) As GuardarDatosDocumento.Respuesta

        Dim objRespuesta As New GuardarDatosDocumento.Respuesta

        ' setar codigo 0 e mensagem erro vazio
        objRespuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
        objRespuesta.MensajeError = String.Empty

        Util.GuardarLogExecucao("Marca Doc. 1.0")

        Try

            ' efetua as validações gerais
            If EfetuarValidacoesGerais(Peticion, objRespuesta) Then

                ' criar objeto documento
                Dim objDocumento As New Negocio.Documento

                Util.GuardarLogExecucao("Marca Doc. 2.0")

                ' obter dados do documento ou do formulário
                If Peticion.Documento.IdDocumento > 0 Then
                    objDocumento.Id = Peticion.Documento.IdDocumento
                    objDocumento.Realizar()
                    If objDocumento.Formulario.ConBultos Then
                        objDocumento.Bultos.Realizar()
                    End If
                Else
                    objDocumento.Formulario.Id = Peticion.Documento.IdCaracteristica
                    objDocumento.Formulario.Realizar()
                End If

                Util.GuardarLogExecucao("Marca Doc. 3.0")

                Dim campoCP = Peticion.Documento.Campos.FirstOrDefault(Function(f) f.Nombre = eCampos.CentroProcesoOrigen)
                Dim cp As Negocio.CentrosProceso = Nothing
                If campoCP IsNot Nothing Then
                    cp = New Negocio.CentrosProceso
                    cp.IdPS = campoCP.Valor
                    cp.Realizar()
                    If cp.Count > 0 Then
                        objDocumento.Fecha = Util.GetDateTime(cp(0).Planta.CodDelegacionGenesis)
                        objDocumento.GMTVeranoAjuste = If(Not String.IsNullOrEmpty(cp(0).Planta.CodDelegacionGenesis), CType(Util.GetGMTVeranoAjuste(cp(0).Planta.CodDelegacionGenesis), Short?), Nothing)
                    End If
                End If

                ' efetuas as validacoes especificas
                If EfetuarValidacoesEspecificas(Peticion, objRespuesta, objDocumento) Then

                    Select Case Peticion.Accion

                        Case Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Aceptar

                            Util.GuardarLogExecucao("Marca Doc. 4.0")

                            AceptarDocumento(objDocumento, Peticion)

                            Util.GuardarLogExecucao("Marca Doc. 4.1")

                        Case Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Actualizar, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Sustituir

                            Util.GuardarLogExecucao("Marca Doc. 5.0")

                            ActualizarSustituirDocumento(objDocumento, Peticion, objRespuesta)

                            Util.GuardarLogExecucao("Marca Doc. 5.1")

                        Case Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Crear, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearImprimir, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearAceptar

                            Util.GuardarLogExecucao("Marca Doc. 6.0")

                            CrearDocumento(objDocumento, Peticion, objRespuesta)

                            Util.GuardarLogExecucao("Marca Doc. 6.1")

                    End Select

                    ' Preencher o objeto resposta
                    If objRespuesta.CodigoError = 0 Then

                        objRespuesta.IdDocumento = objDocumento.Id

                    End If

                End If

            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            Util.GuardarLogExecucao("ERROR: " & ex.ToString)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Efetua as validações obrigatórias.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <param name="objRespuesta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/07/2009 Criado
    ''' </history>
    Private Function EfetuarValidacoesGerais(objPeticion As GuardarDatosDocumento.Peticion, _
                                                    ByRef objRespuesta As GuardarDatosDocumento.Respuesta) As Boolean

        Util.GuardarLogExecucao("Marca Doc. 1.1")

        ' se for um aceite ou uma atualização e o id documento não foi informado
        If (objPeticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Aceptar OrElse objPeticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Actualizar) _
            AndAlso (objPeticion.Documento Is Nothing OrElse objPeticion.Documento.IdDocumento = 0) Then

            ' atualizar retorno do objeto respuesta
            AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                      Traduzir("01_msg_iddocumento_invalido"), objRespuesta)

            ' retorna false sinalizando erro de validação
            Return False

        ElseIf objPeticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Crear OrElse objPeticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearImprimir OrElse objPeticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearAceptar Then

            Util.GuardarLogExecucao("Marca Doc. 1.2")

            If objPeticion.Documento IsNot Nothing AndAlso objPeticion.Documento.IdDocumento > 0 Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_iddocumento_nao_pode_ser_informafo"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            End If

            If objPeticion.Documento Is Nothing OrElse objPeticion.Documento.IdCaracteristica = 0 Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_idformulario_invalido"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            End If

        ElseIf objPeticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Sustituir _
            AndAlso (objPeticion.Documento Is Nothing OrElse objPeticion.Documento.IdOrigen = 0) Then

            Util.GuardarLogExecucao("Marca Doc. 1.3")

            ' atualizar retorno do objeto respuesta
            AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                      Traduzir("01_msg_iddocumentoorigem_invalido"), objRespuesta)

            ' retorna false sinalizando erro de validação
            Return False

        End If

        Util.GuardarLogExecucao("Marca Doc. 1.4")

        ' se usuário foi informado
        If objPeticion.Usuario IsNot Nothing _
                    AndAlso objPeticion.Usuario.Login <> String.Empty _
                    AndAlso objPeticion.Usuario.Clave <> String.Empty Then

            ' obter informações do usuário
            UsuarioAtual.Nombre = objPeticion.Usuario.Login

            UsuarioAtual.Clave = objPeticion.Usuario.Clave
            UsuarioAtual.Ingresar()

            Util.GuardarLogExecucao("Marca Doc. 1.5")

            If UsuarioAtual.Id = -1 Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_dados_login_invalido"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            ElseIf UsuarioAtual.Bloqueado Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_dados_usuario_bloqueado"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            ElseIf UsuarioAtual.Caduco Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_dados_usuario_expirou"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            End If

        Else

            ' atualizar retorno do objeto respuesta
            AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                      Traduzir("01_msg_dados_usuario_invalido"), objRespuesta)

            ' retorna false sinalizando erro de validação
            Return False

        End If

        Util.GuardarLogExecucao("Marca Doc. 1.6")

        ' se chegou até aqui é porque não houve erros nas validações
        Return True

    End Function

    ''' <summary>
    ''' Efetua as validações especiicas
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 20/07/2009 Criado
    ''' </history>
    Private Function EfetuarValidacoesEspecificas(objPeticion As GuardarDatosDocumento.Peticion, _
                                                         ByRef objRespuesta As GuardarDatosDocumento.Respuesta, _
                                                         objDocumento As Negocio.Documento) As Boolean

        Util.GuardarLogExecucao("Marca Doc. 3.1")

        ' verificar se id do grupo foi informado. Não pode ser informado caso seja uma Ata de Processo
        If objDocumento.Formulario.EsActaProceso Then

            If objPeticion.Documento.IdGrupo > 0 Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_dados_usuario_invalido"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            End If

        End If

        Util.GuardarLogExecucao("Marca Doc. 3.2")

        ' Documentos não podem ser informados caso seja uma Ata de Processo ou caso tenha sido informado documento grupo
        If objDocumento.Formulario.EsActaProceso OrElse objPeticion.Documento.IdGrupo > 0 Then

            If objPeticion.Documento.DocumentosAgrupados.Count <> 0 Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_dados_documento_documentos"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            End If

        End If

        Util.GuardarLogExecucao("Marca Doc. 3.3")

        ' Bultos não podem ser informados caso:
        ' - no formulário esteja configurado para o documento não possuir bultos ou
        ' - se o documento enviado é um grupo de documentos
        If Not objDocumento.Formulario.ConBultos OrElse _
           (objPeticion.Documento.DocumentosAgrupados IsNot Nothing AndAlso objPeticion.Documento.DocumentosAgrupados.Count > 0) Then

            If objPeticion.Documento.Bultos.Count <> 0 Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_dados_documento_bultos"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            End If

        Else

            If (Not objPeticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Aceptar) _
                AndAlso (Not objDocumento.Formulario.EsActaProceso) _
                AndAlso (objPeticion.Documento.Bultos.Count = 0) Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_dados_documento_informar_bultos"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            End If

        End If

        Util.GuardarLogExecucao("Marca Doc. 3.4")

        ' Valores não podem ser informados caso no formulário esteja configurado para o documento não possuir valores
        If Not objDocumento.Formulario.ConValores Then

            If objPeticion.Documento.Detalles.Count <> 0 Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_dados_documento_detalles"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            End If

        End If

        Util.GuardarLogExecucao("Marca Doc. 3.5")

        ' Sobres não podem ser informados caso o documento não seja uma Ata
        If Not objDocumento.Formulario.EsActaProceso Then

            If objPeticion.Documento.Sobres.Count <> 0 Then

                ' atualizar retorno do objeto respuesta
                AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                          Traduzir("01_msg_dados_documento_sobres"), objRespuesta)

                ' retorna false sinalizando erro de validação
                Return False

            End If

        End If

        Util.GuardarLogExecucao("Marca Doc. 3.6")

        ' Validacoes especificas por accion
        Select Case objPeticion.Accion

            Case Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Aceptar

                Util.GuardarLogExecucao("Marca Doc. 3.7")

                ' O documento não pode ser um documento agregado
                If objDocumento.Agrupado OrElse objDocumento.Grupo.Id > 0 Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              Traduzir("01_msg_dados_documento_agrupado"), objRespuesta)

                    ' retorna false sinalizando erro de validação
                    Return False

                End If

                ' O status atual do documento deve ser “I” (Impreso)
                If objDocumento.EstadoComprobante.Codigo <> "I" Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              Traduzir("01_msg_dados_documento_estado_impreso"), objRespuesta)

                    ' retorna false sinalizando erro de validação

                    Return False

                End If

                ' O setor para aceite do documento deve ser especificado
                Dim resultCampoDocumento = From ECampo As Negocio.Campo In objDocumento.Formulario.Campos _
                                           Where ECampo.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoDestino.ToString

                Dim resultCampoPeticion = From ECampo As GuardarDatosDocumento.Campo In objPeticion.Documento.Campos _
                                          Where ECampo.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoDestino


                If (resultCampoDocumento Is Nothing OrElse resultCampoDocumento.Count = 0) OrElse (resultCampoPeticion Is Nothing OrElse resultCampoPeticion.Count = 0) Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              Traduzir("01_msg_dados_documento_centro_proceso_destino"), objRespuesta)

                    ' retorna false sinalizando erro de validação

                    Return False

                End If

                Util.GuardarLogExecucao("Marca Doc. 3.8")

            Case Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Actualizar

                Util.GuardarLogExecucao("Marca Doc. 3.9")

                ' O status atual do documento deve ser "P" (En Proceso), "I" (Impreso), "A" (Aceptados)
                If objDocumento.EstadoComprobante.Codigo <> "P" AndAlso objDocumento.EstadoComprobante.Codigo <> "I" AndAlso objDocumento.EstadoComprobante.Codigo <> "A" Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              Traduzir("01_msg_dados_documento_estado_proceso"), objRespuesta)

                    ' retorna false sinalizando erro de validação

                    Return False

                End If

                ' Verifica se o usuario tem permissao no formulario
                If Not VerificaPermissaoUsuario(objDocumento, objPeticion) Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              Traduzir("01_msg_dados_documento_permiso"), objRespuesta)

                    ' retorna false sinalizando erro de validação
                    Return False

                End If

                ' Verifica se todos os campos fixos foram informados
                Dim ListaCampos As List(Of String) = ValidarCamposFixos(objDocumento, objPeticion)

                If ListaCampos.Count > 0 Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              String.Format(Traduzir("01_msg_dados_documento_campo"), ConverteLista(ListaCampos)), objRespuesta)

                    ' retorna false sinalizando erro de validação
                    Return False

                End If

                ' Verifica se as listas sao iguais e se todos os campos extras foram informados
                Dim ListaCamposExtras As List(Of String) = ValidarCamposExtras(objDocumento, objPeticion)

                ' Verifica se existe campos na lista
                If (ListaCamposExtras.Count > 0) Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              String.Format(Traduzir("01_msg_dados_documento_campo_extra"), ConverteLista(ListaCamposExtras)), objRespuesta)

                    ' retorna false sinalizando erro de validação
                    Return False

                End If

                Util.GuardarLogExecucao("Marca Doc. 3.10")

            Case Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Crear, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearImprimir, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearAceptar

                Util.GuardarLogExecucao("Marca Doc. 3.11")

                ' Verifica se todos os campos fixos foram informados
                Dim ListaCampos As List(Of String) = ValidarCamposFixos(objDocumento, objPeticion)

                If ListaCampos.Count > 0 Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              String.Format(Traduzir("01_msg_dados_documento_campo"), ConverteLista(ListaCampos)), objRespuesta)

                    ' retorna false sinalizando erro de validação

                    Return False

                End If

                ' Verifica se todos os campos extra foram informados
                Dim ListaCamposExtras As List(Of String) = ValidarCamposExtras(objDocumento, objPeticion)

                If (ListaCamposExtras.Count > 0) Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              String.Format(Traduzir("01_msg_dados_documento_campo_extra"), ConverteLista(ListaCamposExtras)), objRespuesta)

                    ' retorna false sinalizando erro de validação

                    Return False

                End If

                ' O Usuário deve ter permissão para criação de ata a partir do setor e documento origem informados
                If objDocumento.Formulario.EsActaProceso Then

                    Util.GuardarLogExecucao("Marca Doc. 3.12")

                    If objPeticion.Documento.IdOrigen = 0 Then

                        ' atualizar retorno do objeto respuesta
                        AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                  Traduzir("01_msg_dados_documento_origen"), objRespuesta)

                        ' retorna false sinalizando erro de validação

                        Return False

                    End If

                Else

                    Util.GuardarLogExecucao("Marca Doc. 3.13")

                    If Not VerificaPermissaoUsuario(objDocumento, objPeticion) Then

                        ' atualizar retorno do objeto respuesta
                        AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                  Traduzir("01_msg_dados_documento_permiso"), objRespuesta)

                        ' retorna false sinalizando erro de validação

                        Return False

                    End If

                    Util.GuardarLogExecucao("Marca Doc. 3.14")

                End If

        End Select

        Return True

    End Function

    ''' <summary>
    ''' Atualiza os parametros de retorno do objeto respuesta
    ''' </summary>
    ''' <param name="CodigoError"></param>
    ''' <param name="MensajeError"></param>
    ''' <param name="objRespuesta"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 17/07/2009 Criado
    ''' </history>
    Private Sub AtualizarRetornoRespuesta(CodigoError As Integer, _
                                                 MensajeError As String, _
                                                 ByRef objRespuesta As GuardarDatosDocumento.Respuesta)

        ' setar codigo erro e mensagem erro
        objRespuesta.CodigoError = CodigoError
        objRespuesta.MensajeError = MensajeError

    End Sub


    ''' <summary>
    ''' Valida se os valores dos campos do bulto não foram alterados
    ''' </summary>
    ''' <param name="Documento"></param>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarValoresBultos(Documento As Negocio.Documento, _
                                   Peticion As GuardarDatosDocumento.Peticion) As Boolean

        Dim indice As Integer

        ' Se a quantidade dos bultos é diferente
        If Documento.Bultos.Count <> Peticion.Documento.Bultos.Count Then
            Return False
        End If

        ' Para cada bulto existente no documento
        For indice = 0 To Documento.Bultos.Count - 1

            ' Se o código da bolsa é diferente
            If Documento.Bultos(indice).CodBolsa <> Peticion.Documento.Bultos(indice).CodigoBolsa Then
                Return False
            End If

            ' Se o identificador do destino é diferente
            If Documento.Bultos(indice).Destino.Id <> Peticion.Documento.Bultos(indice).IdDestino Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' Verifica se para cada campo fixo existente no Objeto Documento, existe um campo fixo com mesmo nome em no Objeto Peticion
    ''' </summary>
    ''' <returns>
    ''' Lista de string contendo o Nome dos campos fixos que nao foram encontrados
    ''' </returns>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Function ValidarCamposFixos(Documento As Negocio.Documento, _
                                   Peticion As GuardarDatosDocumento.Peticion) As List(Of String)

        Dim Retorno As New List(Of String)
        Dim CampoNombre As String

        For Each Campo In Documento.Formulario.Campos

            If (Campo.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoDestino.ToString OrElse Campo.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoOrigen.ToString) _
                OrElse (Documento.EsGrupo = False AndAlso Peticion.Documento.DocumentosAgrupados.Count = 0 AndAlso (Campo.Tipo = "A" OrElse Documento.Formulario.BasadoEnExtracto = False)) Then

                CampoNombre = Campo.Nombre

                Dim resultCampo = From ECampo As GuardarDatosDocumento.Campo In Peticion.Documento.Campos _
                                  Where ECampo.Nombre.ToString = CampoNombre

                If resultCampo.Count = 0 OrElse String.IsNullOrEmpty(resultCampo.First.Valor) Then

                    Retorno.Add(Campo.Nombre)

                End If

            End If

        Next

        Return Retorno

    End Function

    ''' <summary>
    ''' Valida se os valores dos campos fixos não foram alterados
    ''' </summary>
    ''' <param name="Documento"></param>
    ''' <param name="Peticion"></param>
    ''' <returns>Retorna os campos fixos alterados</returns>
    ''' <remarks></remarks>
    Private Function ValidarValoresCamposFixos(Documento As Negocio.Documento, _
                                   Peticion As GuardarDatosDocumento.Peticion) As List(Of String)

        Dim Retorno As New List(Of String)
        Dim CampoNombre As String

        For Each Campo In Documento.Formulario.Campos

            If (Campo.Nombre <> Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.ClienteOrigen.ToString AndAlso _
                Campo.Nombre <> Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.ClienteDestino.ToString) Then

                CampoNombre = Campo.Nombre

                Dim resultCampo = From ECampo As GuardarDatosDocumento.Campo In Peticion.Documento.Campos _
                                  Where ECampo.Nombre.ToString = CampoNombre

                If resultCampo.Count > 0 Then

                    Select Case CampoNombre

                        Case Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoDestino.ToString, _
                        Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoOrigen.ToString

                            Dim lstCentroProceso As New Negocio.CentrosProceso
                            lstCentroProceso.IdPS = resultCampo.First.Valor
                            lstCentroProceso.Realizar()

                            If lstCentroProceso.Count > 0 Then

                                If Campo.Valor <> lstCentroProceso.First.Descripcion Then

                                    Retorno.Add(Campo.Nombre)

                                End If

                            End If

                        Case Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.Banco.ToString, _
                        Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.BancoDeposito.ToString

                            Dim lstBanco As New Negocio.Bancos
                            lstBanco.IdPS = resultCampo.First.Valor
                            lstBanco.Realizar()

                            If lstBanco.Count > 0 Then

                                If Campo.Valor <> lstBanco.First.Descripcion Then

                                    Retorno.Add(Campo.Nombre)

                                End If

                            End If

                        Case Else

                            If Campo.Valor <> resultCampo.First.Valor Then

                                Retorno.Add(Campo.Nombre)

                            End If

                    End Select

                End If

            End If

        Next

        Return Retorno

    End Function

    ''' <summary>
    ''' Verifica se para cada campo extra existente no Peticion, existe um com mesmo identificador no objeto
    ''' </summary>
    ''' <returns>
    ''' Lista de string contendo os campos extras informados errado
    ''' </returns>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Function ValidarCamposExtras(objDocumento As Negocio.Documento, _
                                         objPeticion As GuardarDatosDocumento.Peticion) As List(Of String)

        Dim Retorno As New List(Of String)
        Dim CampoNombre As String = String.Empty

        If objDocumento.Formulario.CamposExtra IsNot Nothing Then

            For Each CampoExtra In objDocumento.Formulario.CamposExtra.Where(Function(ce) ce.SeValida = True)

                CampoNombre = CampoExtra.Nombre

                Dim resultCampoExtra = From ECampoExtra As GuardarDatosDocumento.CampoExtra In objPeticion.Documento.CamposExtras _
                                Where ECampoExtra.Nombre = CampoNombre

                If resultCampoExtra.Count = 0 Then

                    Retorno.Add(CampoExtra.Nombre)

                End If

            Next

        End If

        Return Retorno

    End Function

    ''' <summary>
    ''' Verifica se os valores dos campos extras não foram alterados
    ''' </summary>
    ''' <returns>
    ''' Lista de string contendo os campos extras com os valores alterados
    ''' </returns>
    Private Function ValidarCamposExtrasValores(objDocumento As Negocio.Documento, _
                                         objPeticion As GuardarDatosDocumento.Peticion) As List(Of String)

        Dim Retorno As New List(Of String)
        Dim CampoNombre As String = String.Empty

        If objPeticion.Documento.CamposExtras IsNot Nothing Then

            For Each CampoExtra In objPeticion.Documento.CamposExtras

                CampoNombre = CampoExtra.Nombre

                Dim resultCampoExtra = From ECampoExtra As Negocio.CampoExtra In objDocumento.Formulario.CamposExtra _
                                Where ECampoExtra.Nombre = CampoNombre

                If resultCampoExtra.Count > 0 Then

                    If (CampoExtra.Valor <> resultCampoExtra.First.Valor) Then

                        Retorno.Add(CampoExtra.Nombre)

                    End If

                End If

            Next

        End If

        Return Retorno

    End Function

    ''' <summary>
    ''' Verifica se todos os documentos enviados na lista Peticion Documento possuem id correspondente em Reporte Documentos.
    ''' Se o IdDocumento do documento agregado é diferente de 0, esta agrupando. Senao esta atualizando. O IdOrigen e obrigatorio.
    ''' </summary>
    ''' <returns>
    ''' Lista de string contendo o Id dos documentos que nao foram encontrados
    ''' </returns>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Function ValidarDocumentosAgrupados(IdCentroProceso As Integer, _
                                                Peticion As GuardarDatosDocumento.Peticion) As List(Of String)

        Dim Retorno As New List(Of String)

        For Each Documento In Peticion.Documento.DocumentosAgrupados

            If Documento.IdDocumento = 0 Then

                ' verificar se documento existe para o centro de processo informado
                ' caso não exista, deve adicionar para a lista de erros
                If Negocio.Documento.VerificarExistenciaDocumento(Documento.IdOrigen, IdCentroProceso) = 0 Then
                    Retorno.Add(Documento.IdOrigen)
                End If

            End If

        Next

        Return Retorno

    End Function

    ''' <summary>
    ''' Verifica se o usuario possui permissao no formulario
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <history>
    ''' [vinicius.gama] 20/07/2009 Criado
    ''' </history>
    Private Function VerificaPermissaoUsuario(Documento As Negocio.Documento, Peticion As GuardarDatosDocumento.Peticion) As Boolean

        Dim resultCentroProcesoOrigen = From ECampo As GuardarDatosDocumento.Campo In Peticion.Documento.Campos _
                                               Where ECampo.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoOrigen

        Dim IdCentroProceso As Integer

        Dim objCentrosProceso As New Negocio.CentrosProceso
        objCentrosProceso.IdPS = resultCentroProcesoOrigen.First.Valor
        objCentrosProceso.Realizar()

        If objCentrosProceso.Count > 0 Then

            IdCentroProceso = objCentrosProceso.First.Id

            Dim Formularios As New Negocio.Formularios
            Formularios.CentroProceso.Id = IdCentroProceso
            Formularios.UsuarioActual.Id = UsuarioAtual.Id
            Formularios.Realizar()

            If Formularios.Count > 0 Then

                Dim resultFormularioUsuario = From EFormulario As Negocio.Formulario In Formularios _
                                              Where EFormulario.Id = Peticion.Documento.IdCaracteristica

                If resultFormularioUsuario Is Nothing AndAlso resultFormularioUsuario.Count = 0 Then

                    Return False

                End If

            Else

                Return False

            End If

        Else

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' Atualiza o Id do Campo no Documento.
    ''' </summary>
    ''' <returns>
    ''' Retorna True caso o processamento seja concluido com sucesso
    ''' </returns>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Function SetarIdCentroProceso(ByRef Documento As Negocio.Documento, Peticion As GuardarDatosDocumento.Peticion, Campo As Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos, ByRef IdPSCentroProceso As String) As Boolean

        Dim resultDocumentoCentroProceso = From ECampo As Negocio.Campo In Documento.Formulario.Campos _
                                           Where ECampo.Nombre = Campo.ToString

        Dim resultPeticionCentroProceso = From ECampo As GuardarDatosDocumento.Campo In Peticion.Documento.Campos _
                                          Where ECampo.Nombre = Campo

        If resultDocumentoCentroProceso.Count > 0 AndAlso resultPeticionCentroProceso.Count > 0 Then

            IdPSCentroProceso = resultPeticionCentroProceso.First.Valor

            Dim objCentrosProceso As New Negocio.CentrosProceso
            objCentrosProceso.IdPS = IdPSCentroProceso
            objCentrosProceso.Realizar()

            If objCentrosProceso.Count > 0 Then
                resultDocumentoCentroProceso.First.IdValor = objCentrosProceso.First.Id

                Return True

            Else

                Return False

            End If

        Else

            Return True

        End If

    End Function

    ''' <summary>
    ''' Atualiza o Id do Campo no Documento.
    ''' </summary>
    ''' <returns>
    ''' Retorna True caso o processamento seja concluido com sucesso
    ''' </returns>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Function SetarIdCanal(ByRef Documento As Negocio.Documento, Peticion As GuardarDatosDocumento.Peticion, Campo As Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos, ByRef IdPSCanal As String) As Boolean

        Dim resultDocumentoCanal = From ECampo As Negocio.Campo In Documento.Formulario.Campos _
                                   Where ECampo.Nombre = Campo.ToString

        Dim resultPeticionCanal = From ECampo As GuardarDatosDocumento.Campo In Peticion.Documento.Campos _
                                  Where ECampo.Nombre = Campo

        If resultDocumentoCanal.Count > 0 AndAlso resultPeticionCanal.Count > 0 Then

            IdPSCanal = resultPeticionCanal.First.Valor

            Dim objBancos As New Negocio.Bancos
            objBancos.IdPS = IdPSCanal
            objBancos.Realizar()

            If objBancos.Count > 0 Then

                resultDocumentoCanal.First.IdValor = objBancos.First.Id

                Return True

            Else

                Return False

            End If

        Else

            Return True

        End If

    End Function

    ''' <summary>
    ''' Atualiza o Id do Campo no Documento.
    ''' </summary>
    ''' <returns>
    ''' Retorna True caso o processamento seja concluido com sucesso
    ''' </returns>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Function SetarIdCliente(ByRef Documento As Negocio.Documento, Peticion As GuardarDatosDocumento.Peticion, Campo As Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos, ByRef IdPSCliente As String) As Boolean

        Dim resultDocumentoCliente = From ECampo As Negocio.Campo In Documento.Formulario.Campos _
                                     Where ECampo.Nombre = Campo.ToString

        Dim resultPeticionCliente = From ECampo As GuardarDatosDocumento.Campo In Peticion.Documento.Campos _
                            Where ECampo.Nombre = Campo

        If resultPeticionCliente.Count > 0 AndAlso resultDocumentoCliente.Count > 0 Then

            IdPSCliente = resultPeticionCliente.First.Valor

            Dim objClientes As New Negocio.Clientes
            objClientes.IdPS = IdPSCliente
            objClientes.Realizar()

            If objClientes.Count > 0 Then

                resultDocumentoCliente.First.IdValor = objClientes.First.Id

                Return True

            Else

                Return False

            End If

        Else

            Return True

        End If

    End Function

    ''' <summary>
    ''' Atualiza o Numero Externo no Documento.
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Sub SetarNumeroExterno(ByRef Documento As Negocio.Documento, Peticion As GuardarDatosDocumento.Peticion)

        Dim resultDocumentoNumExterno = From ECampo In Documento.Formulario.Campos _
                                            Where ECampo.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.NumExterno.ToString

        Dim resultPeticionNumExterno = From ECampo In Peticion.Documento.Campos _
                                       Where ECampo.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.NumExterno

        If resultDocumentoNumExterno.Count > 0 AndAlso resultPeticionNumExterno.Count > 0 Then

            resultDocumentoNumExterno.First.Valor = resultPeticionNumExterno.First.Valor

        End If

    End Sub

    ''' <summary>
    ''' Executa a accion de Crear o Documento
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Sub CrearDocumento(ByRef objDocumento As Negocio.Documento, ByRef Peticion As GuardarDatosDocumento.Peticion, ByRef objRespuesta As GuardarDatosDocumento.Respuesta)

        objDocumento.Usuario.Id = UsuarioAtual.Id
        'objDocumento.Fecha = DateTime.Now

        If Peticion.Documento.FechaGestion <> DateTime.MinValue Then
            objDocumento.FechaGestion = Peticion.Documento.FechaGestion
        End If

        If Peticion.Documento.IdOrigen > 0 Then
            objDocumento.Origen.Id = Peticion.Documento.IdOrigen
        End If

        objDocumento.IdMovimentacionFondo = Peticion.Documento.IdMovimentacionFondo
        objDocumento.EsSustituto = False
        objDocumento.Legado = Peticion.Documento.Legado

        ' Seta documento grupo, se for o caso
        If (Peticion.Documento.IdGrupo > 0) Then
            objDocumento.EsGrupo = False
            objDocumento.Agrupado = True
            objDocumento.Grupo.Id = Peticion.Documento.IdGrupo
        ElseIf (Peticion.Documento.DocumentosAgrupados.Count > 0) Then
            objDocumento.EsGrupo = True
            objDocumento.Agrupado = False
        Else
            objDocumento.EsGrupo = False
            objDocumento.Agrupado = False
        End If

        PrepararObjetoDocumento(objDocumento, Peticion, objRespuesta)

        ' Executa método para gravar o documento
        If objRespuesta.CodigoError = 0 Then

            Dim ListaRetorno As List(Of String) = Nothing

            ' se for uma acta de processo
            If objDocumento.Formulario.EsActaProceso Then

                ' criar a acta e obter o retorno
                ListaRetorno = objDocumento.CrearActa()

                ' se for para aceptar e não ocorreu nenhum erro
                If Peticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearAceptar _
                    AndAlso ListaRetorno.Count = 0 Then

                    ' imprimir a acta
                    objDocumento.Imprimir()

                End If

            Else

                ' Cria o documento, e caso a accion seja crear y aceptar, ele tambem aceita o documento
                ListaRetorno = objDocumento.Registrar(False)

                If ListaRetorno.Count <> 0 Then

                    ' Atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              ListaRetorno.First, objRespuesta)

                ElseIf Peticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearImprimir Then

                    ' Imprime o documento
                    objDocumento.Imprimir()

                ElseIf Peticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.CrearAceptar Then

                    ' Imprime o documento
                    objDocumento.Imprimir()
                    objDocumento.Realizar()

                    ' Se for um documento que nao é aceitado ao imprimir, ele faz o Aceptar.
                    ' Ex: Re-envio, Acta, etc.
                    If objDocumento.EstadoComprobante.Id = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eEstadoComprobante.Impreso Then
                        objDocumento.UsuarioResolutor.Id = objDocumento.Usuario.Id
                        objDocumento.Aceptar()
                    End If

                End If


            End If

        End If

    End Sub

    ''' <summary>
    ''' Executa a accion de Actualizar ou sustituir o documento
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Sub ActualizarSustituirDocumento(ByRef objDocumento As Negocio.Documento, ByRef Peticion As GuardarDatosDocumento.Peticion, ByRef objRespuesta As GuardarDatosDocumento.Respuesta)

        objDocumento.Usuario.Id = UsuarioAtual.Id
        'objDocumento.Fecha = DateTime.Now

        If Peticion.Documento.FechaGestion <> DateTime.MinValue Then
            objDocumento.FechaGestion = Peticion.Documento.FechaGestion
        End If

        If Peticion.Documento.IdOrigen > 0 Then
            objDocumento.Origen.Id = Peticion.Documento.IdOrigen
        End If

        If (Peticion.Accion = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eAccion.Sustituir) Then
            objDocumento.EsSustituto = True
            objDocumento.Sustituir()
        Else

            PrepararObjetoDocumento(objDocumento, Peticion, objRespuesta)

            If objRespuesta.CodigoError = 0 Then
                objDocumento.Registrar(False)
            End If

        End If

    End Sub

    ''' <summary>
    ''' Metodo que prepara o objeto de documentos
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Sub PrepararObjetoDocumento(ByRef objDocumento As Negocio.Documento, ByRef Peticion As GuardarDatosDocumento.Peticion, ByRef objRespuesta As GuardarDatosDocumento.Respuesta)

        Dim IdPSTemp As String

        ' obter o id do centro de processo origem
        IdPSTemp = String.Empty
        If SetarIdCentroProceso(objDocumento, Peticion, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoOrigen, IdPSTemp) = False Then
            ' atualizar retorno do objeto respuesta
            AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                      String.Format(Traduzir("01_msg_dados_documento_idps_no_encontrado"), IdPSTemp, "CentroProcesoOrigen"), objRespuesta)

            ' retorna false sinalizando erro de validação
            Exit Sub

        End If

        ' obter o id do centro de processo destino
        IdPSTemp = String.Empty
        If SetarIdCentroProceso(objDocumento, Peticion, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoDestino, IdPSTemp) = False Then

            ' atualizar retorno do objeto respuesta
            AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                      String.Format(Traduzir("01_msg_dados_documento_idps_no_encontrado"), IdPSTemp, "CentroProcesoDestino"), objRespuesta)

            ' retorna false sinalizando erro de validação
            Exit Sub

        End If

        ' obter o id do canal
        IdPSTemp = String.Empty
        If SetarIdCanal(objDocumento, Peticion, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.Banco, IdPSTemp) = False Then

            ' atualizar retorno do objeto respuesta
            AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                      String.Format(Traduzir("01_msg_dados_documento_idps_no_encontrado"), IdPSTemp, "Canal"), objRespuesta)

            ' retorna false sinalizando erro de validação
            Exit Sub

        End If

        ' obter o id do canal depósito
        IdPSTemp = String.Empty
        If SetarIdCanal(objDocumento, Peticion, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.BancoDeposito, IdPSTemp) = False Then

            ' atualizar retorno do objeto respuesta
            AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                     String.Format(Traduzir("01_msg_dados_documento_idps_no_encontrado"), IdPSTemp, "CanalDeposito"), objRespuesta)

            ' retorna false sinalizando erro de validação
            Exit Sub

        End If

        ' obter o id do cliente origen
        IdPSTemp = String.Empty
        If SetarIdCliente(objDocumento, Peticion, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.ClienteOrigen, IdPSTemp) = False Then

            ' atualizar retorno do objeto respuesta
            AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                     String.Format(Traduzir("01_msg_dados_documento_idps_no_encontrado"), IdPSTemp, "ClienteOrigen"), objRespuesta)

            ' retorna false sinalizando erro de validação
            Exit Sub

        End If

        ' obter o id do cliente destino
        IdPSTemp = String.Empty
        If SetarIdCliente(objDocumento, Peticion, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.ClienteDestino, IdPSTemp) = False Then

            ' atualizar retorno do objeto respuesta
            AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                     String.Format(Traduzir("01_msg_dados_documento_idps_no_encontrado"), IdPSTemp, "ClienteDestino"), objRespuesta)

            ' retorna false sinalizando erro de validação
            Exit Sub

        End If

        ' setar o numero externo
        SetarNumeroExterno(objDocumento, Peticion)

        ' Cria os campos extras do documento
        SetarCamposExtras(objDocumento, Peticion)

        ' Adicionar os bultos da petição para o documento
        SetarBultos(objDocumento, Peticion)

        ' Adicionar os detalles da petição para o documento
        SetarDetalles(objDocumento, Peticion)

        ' Adicionar os parciais da petição para o documento
        SetarParciais(objDocumento, Peticion)

        If objDocumento.Formulario.BasadoEnReporte AndAlso Not objDocumento.Formulario.EsActaProceso Then

            Dim resultCpOrigen = From ECampo In objDocumento.Formulario.Campos _
                                 Where ECampo.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoOrigen.ToString

            If resultCpOrigen IsNot Nothing AndAlso resultCpOrigen.Count > 0 Then

                ' verificar se existem os documentos agrupados
                Dim DocumentosNaoEncontrados As List(Of String) = ValidarDocumentosAgrupados(resultCpOrigen.First.IdValor, Peticion)

                If DocumentosNaoEncontrados.Count > 0 Then

                    ' atualizar retorno do objeto respuesta
                    AtualizarRetornoRespuesta(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                              String.Format(Traduzir("01_msg_dados_documento_documentos_agrupados"), ConverteLista(DocumentosNaoEncontrados)), objRespuesta)

                    ' retorna false sinalizando erro de validação
                    Exit Sub

                End If

                Dim ListaDocumentosAgrupados As String = String.Empty
                Dim i As Integer = 0

                ' para cada documento agrupado
                For Each Documento In Peticion.Documento.DocumentosAgrupados

                    i += 1
                    ListaDocumentosAgrupados &= Documento.IdOrigen

                    If i < Peticion.Documento.DocumentosAgrupados.Count Then
                        ListaDocumentosAgrupados &= ","
                    End If

                Next

                If Peticion.Documento.DocumentosAgrupados.Count <> 0 Then
                    objDocumento.DocumentosReporteGenerar("Carga", ListaDocumentosAgrupados)
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Adiciona os bultos da petição para o documento
    ''' </summary>
    ''' <param name="Documento"></param>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    Private Sub SetarBultos(ByRef Documento As Negocio.Documento, _
                            Peticion As GuardarDatosDocumento.Peticion)

        If Documento.Formulario.ConBultos Then

            Documento.Bultos.Clear()

            Dim objBulto As Negocio.Bulto = Nothing

            For Each Bulto In Peticion.Documento.Bultos

                objBulto = New Negocio.Bulto

                objBulto.Destino.Id = Bulto.IdDestino
                objBulto.CodBolsa = Bulto.CodigoBolsa
                objBulto.NumPrecinto = Bulto.NumeroPrecinto

                Documento.Bultos.Add(objBulto)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Adiciona os detalles da petição para o documento
    ''' </summary>
    ''' <param name="Documento"></param>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    Private Sub SetarDetalles(ByRef Documento As Negocio.Documento, _
                              Peticion As GuardarDatosDocumento.Peticion)

        If Documento.Formulario.ConValores Then

            Documento.Detalles.Clear()

            Dim objDetalle As Negocio.Detalle = Nothing

            For Each Detalle In Peticion.Documento.Detalles

                objDetalle = New Negocio.Detalle

                objDetalle.Cantidad = Detalle.Cantidad
                objDetalle.Especie.Id = Detalle.IdEspecie
                objDetalle.Importe = Detalle.Importe
                objDetalle.Especie.Moneda.Id = Detalle.IdMoneda

                Documento.Detalles.Add(objDetalle)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Adiciona os parciais da petição para o documento
    ''' </summary>
    ''' <param name="Documento"></param>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    Private Sub SetarParciais(ByRef Documento As Negocio.Documento, _
                              Peticion As GuardarDatosDocumento.Peticion)

        If Documento.Formulario.EsActaProceso Then

            Documento.Sobres.Clear()

            Dim objSobre As Negocio.Sobre = Nothing

            For Each Sobre In Peticion.Documento.Sobres

                objSobre = New Negocio.Sobre

                objSobre.ConDiferencia = Sobre.ConDiferencia
                objSobre.Moneda.Id = Sobre.IdMoneda
                objSobre.Importe = Sobre.Importe
                objSobre.NumSobre = Sobre.NumeroSobre

                Documento.Sobres.Add(objSobre)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Executa a accion de aceptar o documento
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 20/07/09 Criado
    ''' </history>
    Private Sub AceptarDocumento(ByRef Documento As Negocio.Documento, ByRef Peticion As GuardarDatosDocumento.Peticion)

        Dim IdPSCentroProceso As String = String.Empty

        SetarIdCentroProceso(Documento, Peticion, Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoDestino, IdPSCentroProceso)

        Documento.UsuarioResolutor.Id = UsuarioAtual.Id

        Documento.Aceptar()

    End Sub

    ''' <summary>
    ''' Converte uma lista de strings em um string separado por virgulas
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] Criado 20/07/09
    ''' </history>
    Private Function ConverteLista(Lista As List(Of String)) As String

        Dim RetornoStr As String = String.Empty
        Dim i As Integer = 0

        For Each s In Lista
            i += 1
            RetornoStr &= s

            If i < Lista.Count Then
                RetornoStr &= ", "
            End If

        Next

        Return RetornoStr

    End Function

    ''' <summary>
    ''' Retorna o Id do IdPS Centro de Processo Informado
    ''' </summary>
    ''' <param name="IdPSCentroProceso">Id Prosegur do Centro de Proceso</param>
    ''' <returns>Retorna o Id caso exista. Retorna zero caso nao exista ou caso exista mais de uma ocorrencia</returns>
    ''' <remarks></remarks>
    Private Function ObterIdCentroProcesoPorIdPS(IdPSCentroProceso As String) As Integer

        If IdPSCentroProceso <> String.Empty Then

            Dim objCentrosProceso As New Negocio.CentrosProceso
            objCentrosProceso.IdPS = IdPSCentroProceso
            objCentrosProceso.Realizar()

            If objCentrosProceso.Count = 1 Then

                Return objCentrosProceso.First.Id

            Else

                Return 0

            End If

        End If
        Return 0
    End Function

    ''' <summary>
    ''' Adiciona todos campos extras do documento. Caso não seja informado o campo extra sera criado com o valor vazio
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] Criado 04/08/09
    ''' </history>
    Private Sub SetarCamposExtras(ByRef Documento As Negocio.Documento, _
                                  ByRef Peticion As GuardarDatosDocumento.Peticion)

        Dim objCamposExtras As New Negocio.CamposExtra
        Dim objCampoExtra As Negocio.CampoExtra = Nothing
        Dim CampoExtraNombre As String

        For Each CampoExtra In Documento.Formulario.CamposExtra

            CampoExtraNombre = CampoExtra.Nombre

            Dim resultCampoExtra = From ECampoExtra In Peticion.Documento.CamposExtras _
                                   Where ECampoExtra.Nombre = CampoExtraNombre

            objCampoExtra = New Negocio.CampoExtra

            If resultCampoExtra.Count > 0 Then

                objCampoExtra.Id = CampoExtra.Id
                objCampoExtra.Nombre = CampoExtra.Nombre
                objCampoExtra.Valor = resultCampoExtra.First.Valor

                objCamposExtras.Add(objCampoExtra)

            Else

                objCampoExtra.Id = CampoExtra.Id
                objCampoExtra.Nombre = CampoExtra.Nombre
                objCampoExtra.Valor = String.Empty

                objCamposExtras.Add(objCampoExtra)

            End If

        Next

        Documento.Formulario.CamposExtra.Clear()
        Documento.Formulario.CamposExtra = objCamposExtras

    End Sub

#End Region

End Class