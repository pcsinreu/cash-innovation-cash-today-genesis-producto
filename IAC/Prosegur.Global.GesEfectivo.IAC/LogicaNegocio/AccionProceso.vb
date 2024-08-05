Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Proceso
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad


Public Class AccionProceso

    ''' <summary>
    '''Função que faz a busca dos procesos.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>    
    Public Function GetProceso(peticion As ContractoServicio.Proceso.GetProceso.Peticion) As ContractoServicio.Proceso.GetProceso.Respuesta

        Dim objRespuesta As New ContractoServicio.Proceso.GetProceso.Respuesta

        Try

            ValidaControle(peticion.CodigoCliente, "016_msg_cod_Cliente")

            objRespuesta.Procesos = AccesoDatos.Proceso.GetProceso(peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    '''Função que faz a busca dos procesos.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>    
    Public Function GetProcesoSapr(peticion As ContractoServicio.Proceso.GetProceso.Peticion) As ContractoServicio.Proceso.GetProceso.Respuesta

        Dim objRespuesta As New ContractoServicio.Proceso.GetProceso.Respuesta

        Try
            'ValidaControle(peticion.CodigoCliente, "016_msg_cod_Cliente")

            objRespuesta.ProcesosSapr = AccesoDatos.Proceso.GetProcesoSapr()
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Faz a busca do procesos no bd de acordo com os parametros especificados.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 - Criado
    ''' </history>
    Public Function GetProcesoDetail(peticion As ContractoServicio.Proceso.GetProcesoDetail.Peticion) As ContractoServicio.Proceso.GetProcesoDetail.Respuesta

        Dim objRespuesta As New ContractoServicio.Proceso.GetProcesoDetail.Respuesta

        Try

            objRespuesta.Procesos = ObtenerProceso(peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Busca os procesos e retorna uma coleção de procesos.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 - Criado
    ''' </history>
    Private Function ObtenerProceso(peticion As GetProcesoDetail.Peticion) As GetProcesoDetail.ProcesoColeccion

        Dim oidProceso As String = String.Empty
        Dim oidProceSubc As String = String.Empty
        Dim oidMedioPago As String = String.Empty
        Dim objColProceso As New GetProcesoDetail.ProcesoColeccion
        Dim objprocesoDetail As GetProcesoDetail.Proceso

        For Each objProceso As GetProcesoDetail.PeticionProceso In peticion.PeticionProcesos

            'Valida os campos obrigatorios
            If String.IsNullOrEmpty(objProceso.IdentificadorProceso) Then
                ValidaControle(objProceso.CodigoCliente, "016_msg_cod_Cliente")
                ValidaControle(objProceso.CodigoDelegacion, "016_msg_cod_Delegacion")
                ValidaControle(objProceso.CodigoSubcanal, "016_msg_cod_SubCanal")
            End If

            'If Not String.IsNullOrEmpty(objProceso.IdentificadorProceso) Then
            '    'busca o proceso e preenche o objeto proceso
            '    objprocesoDetail = AccesoDatos.Proceso.GetProcesoDetailByOid(objProceso.IdentificadorProceso, oidProceSubc)
            'Else
            'busca o proceso e preenche o objeto proceso
            objprocesoDetail = AccesoDatos.Proceso.GetProcesoDetail(objProceso, oidProceSubc)
            'End If

            'se o IndicadorMedioPago for verdadeiro vai preencher a coleção de divisas e medio pago do proceso se não
            'a coleção de agrupações que sera preenchida.
            If objprocesoDetail.IndicadorMediosPago = True Then

                objprocesoDetail.DivisasProceso = AccesoDatos.DivisaPorProceso.BuscaDivisaProceso(objProceso.IdentificadorProceso)
                objprocesoDetail.MediosDePagoProceso = AccesoDatos.MedioPagoPorProceso.BuscaMedioPagoProceso(objProceso.IdentificadorProceso, oidProceSubc)

            Else

                objprocesoDetail.AgrupacionesProceso = AccesoDatos.AgrupacionPorProceso.BuscaAgrupacionProceso(objProceso.IdentificadorProceso)

            End If

            'adiciona o ojetoproceso na coleção de proceso
            objColProceso.Add(objprocesoDetail)

        Next

        Return objColProceso

    End Function

    ''' <summary>
    ''' Valida se o controle foi preenchido.
    ''' </summary>
    ''' <param name="controle"></param>
    ''' <param name="chave"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 17/03/2009 - Criado
    ''' </history>
    Private Sub ValidaControle(controle As String, chave As String)

        If controle = String.Empty AndAlso controle = Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir(chave))
        End If

    End Sub

    ''' <summary>
    ''' Set processo
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <param name="objtransacion">Se for passada uma transação por referencia, os comandos serão adicionados mas a transação
    ''' não será executada.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  13/01/2011  criado
    ''' </history>
    Public Function SetProceso(peticion As ContractoServicio.Proceso.SetProceso.Peticion, Optional ByRef objtransacion As Transacao = Nothing) As ContractoServicio.Proceso.SetProceso.Respuesta

        Dim objRespuesta As New ContractoServicio.Proceso.SetProceso.Respuesta

        Try

            'Declaração de variáveis
            Dim objHashInfGeneral As String
            Dim objHashInfTolerancia As String
            Dim oidProceso As String = String.Empty
            Dim bolProcesoVigente As Boolean = Nothing
            'Dim oidProcesoListaB As String = String.Empty
            Dim ListaOidProcesoPservicio As New List(Of String)

            ' por default, executa a transação (se nenhuma transação foi passada por referencia)
            Dim ejecutaTransacion As Boolean = True

            'Validação de campos obrigatorios
            ValidaControle(peticion.Proceso.CodigoDelegacion, "016_msg_cod_Delegacion")
            ValidaControle(peticion.Proceso.Cliente.Codigo, "016_msg_cod_Cliente")

            If peticion.Proceso.SubCanal.Count = 0 OrElse _
                peticion.Proceso.SubCanal Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("016_msg_cod_SubCanal"))
            End If

            If objtransacion Is Nothing Then
                'Cria um objTransacion
                objtransacion = New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
            Else
                ' se foi passada alguma transação por referencia, ela não deve ser executada
                ejecutaTransacion = False
            End If

            ' Verifica se o processo está vigente
            If peticion.Proceso.Vigente Then

                'Gera chave Hash
                objHashInfGeneral = AccesoDatos.Util.CheckSumInfGeneralSetProceso(RetornaObjInfGeneral(peticion.Proceso))
                objHashInfTolerancia = AccesoDatos.Util.CheckSumInfTolerancias(RetornaObjInfTolerancia(peticion.Proceso))

                'Verifica se o proceso existe
                AccesoDatos.Proceso.VerificaProcesoExiste(objHashInfGeneral, objHashInfTolerancia, oidProceso, bolProcesoVigente)

                If String.IsNullOrEmpty(oidProceso) Then

                    'Insere um novo proceso
                    InsereProceso(objHashInfGeneral.ToString, objHashInfTolerancia.ToString, oidProceso, peticion, objtransacion)

                Else
                    'Modifica o procesO
                    ModificaProceso(bolProcesoVigente, oidProceso, peticion, objtransacion)

                End If

                Dim ListaOidProcesoPorPServicioProcesosInativos As New List(Of String)
                Dim oidListaAProcesoPServicio As New List(Of String)
                Dim ListaCompletaProcesoPorPServicioProcesosInativos As New List(Of String)
                'Retorna Lista A que tem todos os procesos por ponto de servico para combinação 
                'cliente/subcliente/ponto-servico cujo proceso seja diferente do proceso otido
                'AccesoDatos.ProcesoPorPServicio.RetornaOidProcesoPorPServicioInativo(peticion, ListaOidProcesoPservicio)

                Dim oidProcesoPServicioModificado As String = String.Empty

                'Verifica se a coleção de subcliente está vazia
                If peticion.Proceso.Cliente.SubClientes IsNot Nothing AndAlso _
                peticion.Proceso.Cliente.SubClientes.Count > 0 Then

                    'Percorre a coleção de subcliente
                    For Each objSubCliente In peticion.Proceso.Cliente.SubClientes
                        'Verifica se a coleção de punto de servicio esta vazia
                        If objSubCliente.PuntosServicio IsNot Nothing AndAlso objSubCliente.PuntosServicio.Count > 0 Then
                            'Percorre a coleção de punto de servicio.
                            For Each objPuntoServicio In objSubCliente.PuntosServicio
                                'Chama o metodo modifica processo referencia
                                oidProcesoPServicioModificado = ModificaProcessoPorPServicio(peticion, oidProceso, objSubCliente.Codigo, objPuntoServicio.Codigo, objtransacion)
                                ListaOidProcesoPservicio.Add(oidProcesoPServicioModificado)

                                'Recupera os elementos da tabela GEPR_TPROCESO_POR_PSERVICIO para o cliente, sub-cliente, pto_servicio em questão
                                'que não pertencem ao proceso_por_pservicio inserido/alterado.

                                'percorre a coleção de subcanal
                                For Each objsubCanal In peticion.Proceso.SubCanal

                                    ListaOidProcesoPorPServicioProcesosInativos = AccesoDatos.ProcesoPorPServicio.RetornaOidProcesoPorPServicioInativo( _
                                                                           peticion.Proceso.Cliente.Codigo, _
                                                                           objSubCliente.Codigo, _
                                                                           objPuntoServicio.Codigo, _
                                                                           objsubCanal.Codigo, _
                                                                           peticion.Proceso.CodigoDelegacion, _
                                                                           oidProcesoPServicioModificado)

                                    If ListaOidProcesoPorPServicioProcesosInativos IsNot Nothing AndAlso _
                                    ListaOidProcesoPorPServicioProcesosInativos.Count > 0 Then

                                        ListaCompletaProcesoPorPServicioProcesosInativos.AddRange(ListaOidProcesoPorPServicioProcesosInativos)
                                    End If

                                Next

                            Next
                        Else

                            'Chama o metodo modifica processo referencia
                            oidProcesoPServicioModificado = ModificaProcessoPorPServicio(peticion, oidProceso, objSubCliente.Codigo, String.Empty, objtransacion)
                            ListaOidProcesoPservicio.Add(oidProcesoPServicioModificado)

                            'Recupera os elementos da tabela GEPR_TPROCESO_POR_PSERVICIO para o cliente, sub-cliente, pto_servicio em questão
                            'que não pertencem ao proceso_por_pservicio inserido/alterado.
                            For Each objsubCanal In peticion.Proceso.SubCanal

                                ListaOidProcesoPorPServicioProcesosInativos = AccesoDatos.ProcesoPorPServicio.RetornaOidProcesoPorPServicioInativo( _
                                                                   peticion.Proceso.Cliente.Codigo, _
                                                                   objSubCliente.Codigo, _
                                                                   String.Empty, _
                                                                    objsubCanal.Codigo, _
                                                                    peticion.Proceso.CodigoDelegacion, _
                                                                   oidProcesoPServicioModificado)

                                If ListaOidProcesoPorPServicioProcesosInativos IsNot Nothing AndAlso _
                                    ListaOidProcesoPorPServicioProcesosInativos.Count > 0 Then

                                    ListaCompletaProcesoPorPServicioProcesosInativos.AddRange(ListaOidProcesoPorPServicioProcesosInativos)

                                End If

                            Next

                        End If

                    Next

                Else
                    'Chama o metodo modifica processo referencia
                    oidProcesoPServicioModificado = ModificaProcessoPorPServicio(peticion, oidProceso, String.Empty, String.Empty, objtransacion)
                    ListaOidProcesoPservicio.Add(oidProcesoPServicioModificado)

                    'Recupera os elementos da tabela GEPR_TPROCESO_POR_PSERVICIO para o cliente, sub-cliente, pto_servicio em questão
                    'que não pertencem ao proceso_por_pservicio inserido/alterado.
                    For Each objsubCanal In peticion.Proceso.SubCanal
                        ListaOidProcesoPorPServicioProcesosInativos = AccesoDatos.ProcesoPorPServicio.RetornaOidProcesoPorPServicioInativo( _
                                                             peticion.Proceso.Cliente.Codigo, _
                                                             String.Empty, _
                                                             String.Empty, _
                                                              objsubCanal.Codigo, _
                                                             peticion.Proceso.CodigoDelegacion, _
                                                             oidProcesoPServicioModificado)

                        If ListaOidProcesoPorPServicioProcesosInativos IsNot Nothing AndAlso _
                        ListaOidProcesoPorPServicioProcesosInativos.Count > 0 Then

                            ListaCompletaProcesoPorPServicioProcesosInativos.AddRange(ListaOidProcesoPorPServicioProcesosInativos)
                        End If

                    Next

                End If

                For Each OidProcesoPorPServicioProcesosInativos In ListaCompletaProcesoPorPServicioProcesosInativos

                    'Faz a Baja logica do proceso por subcanal e a baja fisica dos termino dos medios pago por proceso
                    BajaListaProcesoPorPServicioAntigos(peticion.Proceso.SubCanal, OidProcesoPorPServicioProcesosInativos, objtransacion, peticion.CodigoUsuario)

                    'Percorre a lista de string e verifica na tabela por ProcesoPorPservicio se existe proceso vigente para os oid's da lista de string.
                    AccesoDatos.ProcesoPorPServicio.BajaProcesoPservicioPorSubCanalVigente(OidProcesoPorPServicioProcesosInativos, objtransacion, peticion.CodigoUsuario)

                Next

                'Se não obtido o proceso por ponto servico para combinação cliente/subcliente/ponto-servico 
                'If ListaOidProcesoPservicio Is Nothing OrElse ListaOidProcesoPservicio.Count = 0 Then
                'ListaOidProcesoPservicio = AccesoDatos.ProcesoPorPServicio.BuscaOidPtoServicio(oidProceso)
                'End If

                'Percorre lista de oidPtoServicio(listaB)
                For Each OidProcesoPservicio As String In ListaOidProcesoPservicio

                    'Percorre a lista de subcanais
                    For Each objsubCanal As ContractoServicio.Proceso.SetProceso.SubCanal In peticion.Proceso.SubCanal

                        Dim oidProcesoSubcanal As String
                        'Verifica se existe uma referencia de oid_proceso_por_pservicio para cada um dos subcanais do objeto peticion
                        oidProcesoSubcanal = AccesoDatos.ProcesoPorSubCanal.VerificaReferenciaProcesoSubCanal(objsubCanal.Codigo, _
                                                                                         OidProcesoPservicio)

                        If oidProcesoSubcanal <> String.Empty Then

                            'Atualiza o Proceso por subcanal
                            AccesoDatos.ProcesoPorSubCanal.ActualizaObservacionProcesoSubCanal(oidProcesoSubcanal, peticion.Proceso.Observacion, objtransacion)

                            'Faz uma baja fisica nos terminos por medio pago.
                            'exclui os registros da tabela GEPR_TTERMINO_MED_PAGO_POR_PRO de acordo com o oid_proceso_subcanal
                            AccesoDatos.TerminoMedioPago.BajaFisicaTerminoMedioPago(oidProcesoSubcanal, objtransacion)

                        Else
                            'Faz uma alta de proceso por subcanal.
                            'cria um registro na tabela proceso_subcanal baseado no OidProcesoPservicio e no objsubCanal.Codigo
                            oidProcesoSubcanal = AccesoDatos.ProcesoPorSubCanal.AltaProcesoSubCanal(OidProcesoPservicio, peticion.Proceso.Observacion, peticion.CodigoUsuario, objtransacion, objsubCanal.Codigo)

                        End If

                        'Insere os terminos de pago para o proceso.

                        For Each objMedioPago In peticion.Proceso.MediosPagoProceso

                            For Each objTermino In objMedioPago.TerminosMedioPago

                                'Insere os terminos de pago para o proceso subcanal.

                                'Busca o oid do Termino
                                Dim oidTermino As String = AccesoDatos.TerminoMedioPago.BuscaOidTermino(objTermino.Codigo, objMedioPago.Codigo, objMedioPago.CodigoTipoMedioPago, objMedioPago.CodigoIsoDivisa)

                                'Faz a alta de termino medio pago por proceso
                                AccesoDatos.TerminoMedioPago.AltaTerminoMedioPagoPorProceso(peticion.Proceso.CodigoDelegacion, _
                                                                                            oidProcesoSubcanal, _
                                                                                            peticion.CodigoUsuario, _
                                                                                            objtransacion, _
                                                                                            oidTermino, _
                                                                                            objTermino.EsObligatorioTerminoMedioPago)

                            Next
                        Next

                    Next
                Next

            Else

                Dim codSubCliente As String = String.Empty
                Dim codPtoServicio As String = String.Empty
                Dim codSubCanal As String = peticion.Proceso.SubCanal(0).Codigo


                If peticion.Proceso.Cliente.SubClientes.Count > 0 Then
                    codSubCliente = peticion.Proceso.Cliente.SubClientes(0).Codigo
                    If peticion.Proceso.Cliente.SubClientes(0).PuntosServicio.Count > 0 Then
                        codPtoServicio = peticion.Proceso.Cliente.SubClientes(0).PuntosServicio(0).Codigo
                    End If
                End If

                oidProceso = AccesoDatos.Proceso.BuscaOidProceso(peticion.Proceso.Cliente.Codigo, _
                                                                 codSubCliente, _
                                                                 peticion.Proceso.CodigoDelegacion, _
                                                                 codPtoServicio, codSubCanal)

                'se o peticion o vigente for false. faz a baja logica no proceso.
                'Verifica se o oidProceso esta vazio
                If oidProceso IsNot Nothing AndAlso oidProceso <> String.Empty Then

                    ActualizarProceso(peticion, oidProceso, objtransacion)

                    Dim oidProcesoPServico As String = AccesoDatos.ProcesoPorPServicio.BuscaOidProcesoPServicio(oidProceso, _
                                                                                                                peticion.Proceso.Cliente.Codigo, _
                                                                                                                codSubCliente, codPtoServicio)

                    'verifica se o oidProcesoPServicio esta vazio.
                    If oidProcesoPServico IsNot Nothing AndAlso oidProcesoPServico <> String.Empty Then
                        AccesoDatos.ProcesoPorPServicio.BajaProcesoPorPuntoServicio(oidProcesoPServico, peticion.CodigoUsuario, objtransacion)
                    End If

                End If

            End If

            If ejecutaTransacion Then
                'Realiza Transação
                objtransacion.RealizarTransacao()
            End If

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Modifica o porcesso por punto servicio se ele não existir insere um novo.
    ''' </summary>
    ''' <param name="bolprocesoVigente"></param>
    ''' <param name="oidProceso"></param>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Private Sub ModificaProceso(bolprocesoVigente As Boolean, _
                                oidProceso As String, _
                                Peticion As ContractoServicio.Proceso.SetProceso.Peticion, _
                                ByRef objTransacion As Transacao)

        'Verifica se o proceso não esta vigente
        If Not bolprocesoVigente Then

            AccesoDatos.Proceso.ActualizarProcesoParaVigente(oidProceso, objTransacion, Peticion.CodigoUsuario)

        End If

        If Peticion.Proceso.IndicadorMediosPago Then

            'Exclui todas as divisas associadas ao processo
            AccesoDatos.DivisaPorProceso.BajaDivisaPorProceso(oidProceso, objTransacion)

            'Insere as divisas por proceso pois a ordem pode ter sido alterada
            AltaDivisa(Peticion.Proceso.DivisaProceso, oidProceso, Peticion.Proceso.CodigoDelegacion, Peticion.CodigoUsuario, objTransacion)

        End If

    End Sub

    ''' <summary>
    ''' Verifica se o processo existe referência com algum outro se não existir insere um novo se não modifica
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <param name="oidProceso"></param>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [anselmo.gois] 20/03/2009
    ''' </history>
    Private Function ModificaProcessoPorPServicio(peticion As SetProceso.Peticion, oidProceso As String, _
                                              codSubcliente As String, codPuntoServicio As String, _
                                              ByRef objTransacion As Transacao) As String


        Dim StrOidProcesoPorPservicio As String

        'Verifica se o proceso existe atraves dos parametros oidprocesso, codcliente, codSubcliente e codPutnoservicio (LISTA B)
        StrOidProcesoPorPservicio = AccesoDatos.Proceso.VerificaProcesoExisteByClienteSubClientePtoServicio(oidProceso, peticion.Proceso.Cliente.Codigo, _
                                                                                   peticion.Proceso.CodigoDelegacion, codSubcliente, _
                                                                                   codPuntoServicio)

        If StrOidProcesoPorPservicio <> String.Empty Then

            'Atualiza a tabela proceso por punto servicio atualiza o cliente faturação, o oidIac e o bolvigente atualiza para
            'verdadeiro
            AccesoDatos.ProcesoPorPServicio.ActualizarProcesoPorPServicio(StrOidProcesoPorPservicio, peticion.Proceso, objTransacion)

        Else
            'Cria um registro na tabela proceso por punto servicio com a combinação cliente, subcliente, puntoServicio
            'delegação e proceso.

            'Busca oid do iac, cliente, clienteFacturacion
            Dim oidIac As String = AccesoDatos.Iac.BuscaOidIac(peticion.Proceso.CodigoIac)
            Dim oidIacBulto As String = AccesoDatos.Iac.BuscaOidIac(peticion.Proceso.CodigoIACBulto)
            Dim oidIacRemesa As String = AccesoDatos.Iac.BuscaOidIac(peticion.Proceso.CodigoIACRemesa)

            Dim oidCliente As String = AccesoDatos.Cliente.BuscarOidCliente(peticion.Proceso.Cliente.Codigo)
            Dim oidClienteFacturacion As String = AccesoDatos.Cliente.BuscarOidCliente(peticion.Proceso.CodigoClienteFacturacion)
            Dim oidSubCliente As String = String.Empty
            Dim oidPuntoServicio As String = String.Empty

            If codSubcliente <> String.Empty Then
                'Busca o oidSubCliente
                oidSubCliente = AccesoDatos.SubCliente.BuscarOidSubCliente(codSubcliente, oidCliente)
            End If

            If codPuntoServicio <> String.Empty Then
                'Busca o oidPuntoServicio
                oidPuntoServicio = AccesoDatos.PuntoServicio.BuscaOidPuntoServicio(codPuntoServicio, oidSubCliente)

            End If

            StrOidProcesoPorPservicio = AccesoDatos.ProcesoPorPServicio.AltaProcesoPorPServicio(oidProceso, oidCliente, oidSubCliente, oidPuntoServicio, _
                                                                                oidIac, oidIacBulto, oidIacRemesa, oidClienteFacturacion, _
                                                                                peticion.Proceso.CodigoDelegacion, _
                                                                                peticion.CodigoUsuario, peticion.Proceso.Vigente, objTransacion)

        End If

        Return StrOidProcesoPorPservicio
    End Function

    ''' <summary>
    ''' Retorna ObjCheckSumInfGeneral
    ''' </summary>
    ''' <param name="objProceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/03/2009 - Criado
    ''' </history>
    Private Function RetornaObjInfGeneral(objProceso As SetProceso.Proceso) As CheckSumInfGeneralSetProceso.CheckSumInfGeneralSetProceso

        'Declaração de variáveis
        Dim objInfGeneral As New CheckSumInfGeneralSetProceso.CheckSumInfGeneralSetProceso
        Dim objAgrupacionInfGeneral As CheckSumInfGeneralSetProceso.Agrupacion
        Dim objMedioPagoInfgGeneral As CheckSumInfGeneralSetProceso.MedioPago
        Dim objDivisaInfGeneral As CheckSumInfGeneralSetProceso.Divisa

        objInfGeneral.Agrupaciones = New CheckSumInfGeneralSetProceso.AgrupacionColeccion
        objInfGeneral.MediosPago = New CheckSumInfGeneralSetProceso.MedioPagoColeccion
        objInfGeneral.Divisas = New CheckSumInfGeneralSetProceso.DivisaColeccion

        'Percorre uma coleção de agrupacionProceso e preenche uma coleção de agrupacionInfGeneral
        For Each objAgrupacionProceso As SetProceso.AgrupacionProceso In objProceso.AgrupacionesProceso

            objAgrupacionInfGeneral = New CheckSumInfGeneralSetProceso.Agrupacion
            objAgrupacionInfGeneral.Descripcion = objAgrupacionProceso.Codigo
            objInfGeneral.Agrupaciones.Add(objAgrupacionInfGeneral)
        Next

        'Percorre uma coleção de medioPagoProceso e preenche uma coleção de medioPagoInfGeneral
        For Each objMedioPagoProcesoas As SetProceso.MedioPagoProceso In objProceso.MediosPagoProceso

            objMedioPagoInfgGeneral = New CheckSumInfGeneralSetProceso.MedioPago
            objMedioPagoInfgGeneral.Descripcion = objMedioPagoProcesoas.Codigo
            objInfGeneral.MediosPago.Add(objMedioPagoInfgGeneral)

        Next

        'Percorre uma coleção de divisas e preenche uma coleção de divisaInfGeneral
        For Each objDivisaProceso As SetProceso.DivisaProceso In objProceso.DivisaProceso

            objDivisaInfGeneral = New CheckSumInfGeneralSetProceso.Divisa
            objDivisaInfGeneral.Codigo = objDivisaProceso.Codigo
            objInfGeneral.Divisas.Add(objDivisaInfGeneral)

        Next

        'Popula um objInfGeneral
        objInfGeneral.Delegacion = objProceso.CodigoDelegacion
        objInfGeneral.DesProducto = objProceso.CodigoProducto
        objInfGeneral.DesModalidad = objProceso.CodigoTipoProcesado
        objInfGeneral.BolMedioPago = objProceso.IndicadorMediosPago
        objInfGeneral.BolCuentaChequeTotal = objProceso.ContarChequesTotal
        objInfGeneral.BolCuentaTicketTotal = objProceso.ContarTicketsTotal
        objInfGeneral.BolCuentaOtrosTotal = objProceso.ContarOtrosTotal
        objInfGeneral.BolCuentaTarjetasTotal = objProceso.ContarTarjetasTotal
        objInfGeneral.CodigoIacParcial = objProceso.CodigoIac
        objInfGeneral.CodigoIACBulto = objProceso.CodigoIACBulto
        objInfGeneral.CodigoIACRemesa = objProceso.CodigoIACRemesa

        Return objInfGeneral

    End Function

    ''' <summary>
    ''' Retorna ObjCheckSumInfTolerancias
    ''' </summary>
    ''' <param name="objProceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/03/2009 - Criado
    ''' </history>
    Private Function RetornaObjInfTolerancia(objProceso As SetProceso.Proceso) As CheckSumInfTolerancias.CheckSumInfTolerancias

        'Declaração de variaveis
        Dim ObjInfTolerancias As New CheckSumInfTolerancias.CheckSumInfTolerancias
        Dim objMedioPagoInfTolerancia As CheckSumInfTolerancias.MedioPago
        Dim objAgrupacionInfToleracnia As CheckSumInfTolerancias.Agrupacion
        Dim objDivisaInfTolerancia As CheckSumInfTolerancias.Divisa

        'instacia uma nova coleção de medio pago e agrupações
        ObjInfTolerancias.MediosPago = New CheckSumInfTolerancias.MedioPagoColeccion
        ObjInfTolerancias.Agrupaciones = New CheckSumInfTolerancias.AgrupacionColeccion
        ObjInfTolerancias.Divisas = New CheckSumInfTolerancias.DivisaColeccion

        'popula uma coleção de mediopagoinfgeneral
        For Each objMedioPagoProceso As SetProceso.MedioPagoProceso In objProceso.MediosPagoProceso

            objMedioPagoInfTolerancia = New CheckSumInfTolerancias.MedioPago
            objMedioPagoInfTolerancia.IndentificadorMedioPago = objProceso.IndicadorMediosPago
            objMedioPagoInfTolerancia.ToleranciaParcialMin = objMedioPagoProceso.ToleranciaParcialMin
            objMedioPagoInfTolerancia.ToleranciaParcialMax = objMedioPagoProceso.ToleranciaParcialMax
            objMedioPagoInfTolerancia.ToleranciaBultoMin = objMedioPagoProceso.ToleranciaBultoMin
            objMedioPagoInfTolerancia.ToleranciaBultoMax = objMedioPagoProceso.ToleranciaBultolMax
            objMedioPagoInfTolerancia.ToleranciaRemesaMin = objMedioPagoProceso.ToleranciaRemesaMin
            objMedioPagoInfTolerancia.ToleranciaRemesaMax = objMedioPagoProceso.ToleranciaRemesaMax

            ObjInfTolerancias.MediosPago.Add(objMedioPagoInfTolerancia)
        Next

        'popula uma coleção de agrupaçõesinfgeneral
        For Each objAgrupacionProceso As SetProceso.AgrupacionProceso In objProceso.AgrupacionesProceso

            objAgrupacionInfToleracnia = New CheckSumInfTolerancias.Agrupacion
            objAgrupacionInfToleracnia.Descripcion = objAgrupacionProceso.Codigo
            objAgrupacionInfToleracnia.ToleranciaParcialMin = objAgrupacionProceso.ToleranciaParcialMin
            objAgrupacionInfToleracnia.ToleranciaParcialMax = objAgrupacionProceso.TolerenciaParcialMax
            objAgrupacionInfToleracnia.ToleranciaBultoMin = objAgrupacionProceso.ToleranciaBultoMin
            objAgrupacionInfToleracnia.ToleranciaBultoMax = objAgrupacionProceso.ToleranciaBultoMax
            objAgrupacionInfToleracnia.ToleranciaRemesaMin = objAgrupacionProceso.ToleranciaRemesaMin
            objAgrupacionInfToleracnia.ToleranciaRemesaMax = objAgrupacionProceso.ToleranciaRemesaMax

            ObjInfTolerancias.Agrupaciones.Add(objAgrupacionInfToleracnia)
        Next

        'popula uma coleção de divisaInfGeneral
        For Each objDivisaProceso As SetProceso.DivisaProceso In objProceso.DivisaProceso

            objDivisaInfTolerancia = New CheckSumInfTolerancias.Divisa
            objDivisaInfTolerancia.Codigo = objDivisaProceso.Codigo
            objDivisaInfTolerancia.ToleranciaParcialMin = objDivisaProceso.ToleranciaParcialMin
            objDivisaInfTolerancia.ToleranciaParcialMax = objDivisaProceso.ToleranciaParcialMax
            objDivisaInfTolerancia.ToleranciaBultoMin = objDivisaProceso.ToleranciaBultoMin
            objDivisaInfTolerancia.ToleranciaBultoMax = objDivisaProceso.ToleranciaBultolMax
            objDivisaInfTolerancia.ToleranciaRemesaMin = objDivisaProceso.ToleranciaRemesaMin
            objDivisaInfTolerancia.ToleranciaRemesaMax = objDivisaProceso.ToleranciaRemesaMax

            ObjInfTolerancias.Divisas.Add(objDivisaInfTolerancia)
        Next

        ObjInfTolerancias.BolMedioPago = objProceso.IndicadorMediosPago

        Return ObjInfTolerancias
    End Function

    ''' <summary>
    ''' Faz a inserção de proceso, medio pago por proceso, agrupacion por proceso e divisa por proceso
    ''' </summary>
    ''' <param name="objInfGeneral"></param>
    ''' <param name="objInfTolerancia"></param>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Private Sub InsereProceso(objInfGeneral As String, _
                              objInfTolerancia As String, _
                              ByRef oidProceso As String, _
                              peticion As ContractoServicio.Proceso.SetProceso.Peticion, ByRef objTransacion As Transacao)

        'Busca o oid do tipo procesado e do producto
        Dim oidTipoProcesado As String = AccesoDatos.TiposProcesado.BuscaOidTipoProcesado(peticion.Proceso.CodigoTipoProcesado)
        Dim oidProducto As String = AccesoDatos.Producto.BuscarOidProducto(peticion.Proceso.CodigoProducto)

        'Insere um novo proceso e retorna seu id
        oidProceso = AccesoDatos.Proceso.AltaProceso(objInfGeneral, objInfTolerancia, oidTipoProcesado, oidProducto, peticion, objTransacion)

        If peticion.Proceso.IndicadorMediosPago = True Then

            'insere medio pago por proceso
            AltaMedioPago(peticion.Proceso.MediosPagoProceso, oidProceso, peticion.Proceso.CodigoDelegacion, peticion.CodigoUsuario, objTransacion)

            'Insere divisas por proceso
            AltaDivisa(peticion.Proceso.DivisaProceso, oidProceso, peticion.Proceso.CodigoDelegacion, peticion.CodigoUsuario, objTransacion)

        Else

            'insere agrupaciones por proceso
            AltaAgrupaciones(peticion.Proceso.AgrupacionesProceso, oidProceso, peticion.Proceso.CodigoDelegacion, peticion.CodigoUsuario, objTransacion)

        End If

    End Sub

    ''' <summary>
    ''' Faz uma alta de agrupaciones
    ''' </summary>
    ''' <param name="objColAgrupaciones"></param>
    ''' <param name="oidProceso"></param>
    ''' <param name="codDelegacion"></param>
    ''' <param name="codUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Private Sub AltaAgrupaciones(objColAgrupaciones As ContractoServicio.Proceso.SetProceso.AgrupacionProcesoColeccion, _
                                 oidProceso As String, codDelegacion As String, codUsuario As String, ByRef objTransacion As Transacao)

        Dim oidAgrupacion As String = String.Empty

        'Verifica para ver se a existe agrupaciones.
        If objColAgrupaciones IsNot Nothing AndAlso objColAgrupaciones.Count > 0 Then

            For Each objAgrupacion As ContractoServicio.Proceso.SetProceso.AgrupacionProceso In objColAgrupaciones

                'Busca o oidAgrupacion
                oidAgrupacion = AccesoDatos.Agrupacion.ObterOidAgrupacion(objAgrupacion.Codigo)

                'Insere uma nova agrupação por proceso
                AccesoDatos.AgrupacionPorProceso.AltaAgrupacionPorProceso(oidProceso, oidAgrupacion, codDelegacion, _
                                                                          codUsuario, objAgrupacion, objTransacion)
            Next

        End If
    End Sub

    ''' <summary>
    ''' Faz uma alta de medio pago
    ''' </summary>
    ''' <param name="objColMedioPago"></param>
    ''' <param name="oidProceso"></param>
    ''' <param name="codDelegacion"></param>
    ''' <param name="codUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Private Sub AltaMedioPago(objColMedioPago As ContractoServicio.Proceso.SetProceso.MedioPagoProcesoColeccion, _
                                 oidProceso As String, codDelegacion As String, codUsuario As String, ByRef objTransacion As Transacao)

        Dim oidMedioPago As String = String.Empty

        'Verifica para ver se a existe medio pago.
        If objColMedioPago IsNot Nothing AndAlso objColMedioPago.Count > 0 Then


            For Each objMedioPago As ContractoServicio.Proceso.SetProceso.MedioPagoProceso In objColMedioPago

                'Busca o oidMedioPago
                oidMedioPago = AccesoDatos.MedioPago.ObterOidMedioPago(objMedioPago.CodigoIsoDivisa, objMedioPago.Codigo, objMedioPago.CodigoTipoMedioPago)

                'Insere um novo medio pago por proceso
                AccesoDatos.MedioPagoPorProceso.AltaMedioPagoPorProceso(oidProceso, oidMedioPago, codDelegacion, _
                                                                        codUsuario, objMedioPago, objTransacion)

            Next

        End If
    End Sub

    ''' <summary>
    ''' Faz uma alta de divisa
    ''' </summary>
    ''' <param name="objColDivisa"></param>
    ''' <param name="oidProceso"></param>
    ''' <param name="codDelegacion"></param>
    ''' <param name="codUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Private Sub AltaDivisa(objColDivisa As ContractoServicio.Proceso.SetProceso.DivisaProcesoColeccion, _
                                 oidProceso As String, codDelegacion As String, codUsuario As String, _
                                 ByRef objTransacion As Transacao)

        Dim oidDivisa As String = String.Empty

        'Verifica para ver se a existe medio pago.
        If objColDivisa IsNot Nothing AndAlso objColDivisa.Count > 0 Then


            For Each objDivisa As ContractoServicio.Proceso.SetProceso.DivisaProceso In objColDivisa

                'Busca o oidMedioPago
                oidDivisa = AccesoDatos.Divisa.ObterOidDivisa(objDivisa.Codigo)

                'Insere um novo medio pago por proceso
                AccesoDatos.DivisaPorProceso.AltaDivisaPorProceso(oidProceso, oidDivisa, codDelegacion, _
                                                                        codUsuario, objDivisa, objTransacion)

            Next

        End If
    End Sub

    ''' <summary>
    ''' Faz a Baja logica do proceso por subcanal e a baja fisica dos termino dos medios pago por proceso
    ''' </summary>
    ''' <param name="objColSubCanal"></param>
    ''' <param name="oidProcesoPorPServicioDesativados"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    Private Sub BajaListaProcesoPorPServicioAntigos(objColSubCanal As ContractoServicio.Proceso.SetProceso.SubCanalColeccion, _
                                                    oidProcesoPorPServicioDesativados As String, ByRef objTransacion As Transacao, codUsuario As String)


        For Each objSubCanal As ContractoServicio.Proceso.SetProceso.SubCanal In objColSubCanal

            'Faz a baja logica do proceso por subcanal
            AccesoDatos.ProcesoPorSubCanal.BajaProcesoSubCanal(objSubCanal.Codigo, oidProcesoPorPServicioDesativados, _
                                                               objTransacion, codUsuario)
            'Baja fisicamente os terminos medio pago
            AccesoDatos.TerminoMedioPago.BajaTerminoMedioPagoPorProceso(objSubCanal.Codigo, oidProcesoPorPServicioDesativados, objTransacion)

        Next

    End Sub

    ''' <summary>
    ''' Faz a alta de terminos por medio pago.
    ''' </summary>
    ''' <param name="codDelegacion"></param>
    ''' <param name="oidProcesoSubCanal"></param>
    ''' <param name="codUsuario"></param>
    ''' <param name="objColMedioPago"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 - Criado
    ''' </history>
    Private Sub AltaTermino(codDelegacion As String, oidProcesoSubCanal As String, codUsuario As String, _
                            objColMedioPago As ContractoServicio.Proceso.SetProceso.MedioPagoProcesoColeccion, _
                            ByRef objTransacion As Transacao)

        Dim oidTermino As String = String.Empty

        'Percorre a coleção de medio pago
        For Each objMedioPago As ContractoServicio.Proceso.SetProceso.MedioPagoProceso In objColMedioPago

            'Percorre a coleção de termino medio pago
            For Each objTermino As ContractoServicio.Proceso.SetProceso.TerminoMedioPago In objMedioPago.TerminosMedioPago
                'Busca o oid do Termino
                oidTermino = AccesoDatos.TerminoIac.BuscaOidTermino(objTermino.Codigo)
                'Faz a alta de termino medio pago por proceso
                AccesoDatos.TerminoMedioPago.AltaTerminoMedioPagoPorProceso(codDelegacion, oidProcesoSubCanal, codUsuario, objTransacion, _
                                                                          oidTermino, objTermino.EsObligatorioTerminoMedioPago)
            Next
        Next

    End Sub

    ''' <summary>
    ''' Atualiza o proceso por subcanal
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <param name="oidProceso"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 24/03/2009 - Criado
    ''' </history>
    Private Sub ActualizarProceso(Peticion As ContractoServicio.Proceso.SetProceso.Peticion, oidProceso As String, _
                                  ByRef objTransacion As Transacao)

        'Verifica se a coleção de subcanal não esta vazia.
        If Peticion.Proceso.SubCanal IsNot Nothing AndAlso Peticion.Proceso.SubCanal.Count > 0 Then
            'Percoore a coleção de subcanal
            For Each objSubcanal As ContractoServicio.Proceso.SetProceso.SubCanal In Peticion.Proceso.SubCanal
                'Atualiza o proceso por subcanal
                AccesoDatos.ProcesoPorSubCanal.ActualizarProcessoSubCanal(oidProceso, Peticion.CodigoUsuario, Peticion.Proceso.Cliente.Codigo, objSubcanal.Codigo, objTransacion, Peticion.Proceso.Cliente.SubClientes)

            Next

        End If

    End Sub

    '''' <summary>
    '''' Se existe canal referenciado ao proceso faz a sua atualização
    '''' </summary>
    '''' <param name="oidProcesoSubCanal"></param>
    '''' <param name="oidProceso"></param>
    '''' <param name="peticion"></param>
    '''' <param name="oidProcessoPorPServicio"></param>
    '''' <remarks></remarks>
    '''' <history>
    '''' [anselmo.gois] 24/03/2009
    '''' </history>
    'Private Sub ModificarProcesoSubCanal(oidProceso As String, _
    '                                          peticion As ContractoServicio.Proceso.SetProceso.Peticion, _
    '                                          ByRef objTransacion As Transacao, _
    '                                          oidProcesoListaB As String, codSubCanal As String)


    '    Dim oidProcesoSubcanal As New List(Of String)
    '    Dim oidPrcPorPServicio As New List(Of String)

    '    'Verifica se existe uma referencia de oid_proceso_por_pservicio para cada um dos subcanail do objeto peticion
    '    AccesoDatos.ProcesoPorSubCanal.VerificaReferenciaProcesoSubCanal(peticion.Proceso.SubCanal, _
    '                                                                     oidProcesoListaB, oidProcesoSubcanal, oidPrcPorPServicio)


    '    Dim cont As Integer = 0
    '    'Verifica se o subcanal esta referenciado ao proceso.
    '    If oidProcesoSubcanal IsNot Nothing AndAlso oidProcesoSubcanal.Count Then

    '        For Each oidsc As String In oidProcesoSubcanal

    '            'Atualiza o Proceso por subcanal
    '            AccesoDatos.ProcesoPorSubCanal.ActualizaReferenciaProcesoSubCanal(oidProceso, peticion.Proceso.SubCanal, _
    '                                                                              peticion.Proceso.Observacion, objTransacion)



    '            'Faz a alta de terminos
    '            AltaTermino(peticion.Proceso.CodigoDelegacion, oidsc, peticion.CodigoUsuario, _
    '                        peticion.Proceso.MediosPagoProceso, objTransacion)

    '            cont += 1
    '        Next

    '    Else

    '        Dim oidSubCanal As String = String.Empty

    '        For Each objSubcanal As ContractoServicio.Proceso.SetProceso.SubCanal In peticion.Proceso.SubCanal

    '            oidSubCanal = AccesoDatos.SubCanal.BuscaOidSubCanal(objSubcanal.Codigo)

    '            'Faz uma alta de proceso por subcanal
    '            AccesoDatos.ProcesoPorSubCanal.AltaProcesoSubCanal(oidProcesoListaB, peticion.Proceso.Observacion, peticion.CodigoUsuario, _
    '                                                               objTransacion, oidSubCanal, peticion.Proceso.Vigente)


    '            'Faz a alta de terminos
    '            AltaTermino(peticion.Proceso.CodigoDelegacion, oidSubCanal, peticion.CodigoUsuario, _
    '                        peticion.Proceso.MediosPagoProceso, objTransacion)
    '        Next


    '    End If

    'End Sub

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' [anselmo.gois] 05/02/2010 - Criado
    Public Function Test() As ContractoServicio.Test.Respuesta
        Dim objRespuesta As New ContractoServicio.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("021_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

End Class
