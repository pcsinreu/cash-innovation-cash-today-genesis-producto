Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.AccesoDatos.Constantes
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis

Public Class AccionCodigoAjeno

    ''' <summary>
    ''' Operación para obtener los datos de código Ajeno.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 17/04/2013 Criado
    ''' </history>
    Public Function GetCodigosAjenos(ByRef objPeticion As GetCodigosAjenos.Peticion) As GetCodigosAjenos.Respuesta
        ' criar objeto respuesta
        Dim objRespuesta As New GetCodigosAjenos.Respuesta

        Try

            'Valida petição
            ValidaPeticionGetCodigosAjenos(objPeticion)

            ' obter divisas com as denominaciones
            objRespuesta.EntidadCodigosAjenos = AccesoDatos.CodigoAjeno.GetCodigosAjenos(objPeticion, objRespuesta.ParametrosPaginacion)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Private Sub ValidaPeticionGetCodigosAjenos(ByRef objPeticion As GetCodigosAjenos.Peticion)

        If objPeticion.ParametrosPaginacion Is Nothing Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))

        ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))

        End If

    End Sub

    ''' <summary>
    ''' Operación para grabar los datos de código Ajeno.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 17/04/2013 Criado
    ''' </history>
    Public Function SetCodigosAjenos(ByRef objPeticion As SetCodigosAjenos.Peticion,
                                            Optional ByRef objTransacion As Transacao = Nothing) As SetCodigosAjenos.Respuesta
        ' criar objeto respuesta
        Dim objRespuesta As New SetCodigosAjenos.Respuesta

        Try

            'Valida petição
            ValidaPeticionSetCodigosAjenos(objPeticion, objRespuesta)
            For Each objItem In objPeticion.CodigosAjenos
                Dim objItemLocal = objItem
                ' Verifica se já não existe um erro para este item
                If Not objRespuesta.CodigosAjenos.Exists(Function(f) f.OidTablaGenesis = objItemLocal.OidTablaGenesis AndAlso
                                                      f.CodTipoTablaGenesis = objItemLocal.CodTipoTablaGenesis AndAlso
                                                      f.OidCodigoAjeno = objItemLocal.OidCodigoAjeno AndAlso
                                                      f.CodAjeno = objItemLocal.CodAjeno) Then

                    ' Inserir ou atualizar Codigo Ajeno
                    Dim objItemResp = InsertUpdateSetCodigosAjenos(objItem, objTransacion)

                    If objItemResp IsNot Nothing Then
                        objRespuesta.CodigosAjenos.Add(objItemResp)
                    End If
                End If

            Next
            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Public Sub BorrarCodigosAjenos(oidTablaGenesis As String, nombreTabla As String, objTransacion As Transacao)
        Try
            AccesoDatos.CodigoAjeno.EliminarCodigosAjenos(nombreTabla, oidTablaGenesis, objTransacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub BorrarCodigoAjeno(oidCodigoAjeno As String, objTransacion As Transacao)
        Try
            AccesoDatos.CodigoAjeno.EliminarCodigoAjeno(oidCodigoAjeno, objTransacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function InsertUpdateSetCodigosAjenos(objCodigoAjeno As SetCodigosAjenos.CodigoAjeno,
                                            Optional ByRef objTransacion As Transacao = Nothing) As SetCodigosAjenos.CodigoAjenoRespuesta

        Dim objCodigoAjenoResp As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoRespuesta
        Dim update As Boolean = False

        Try
            ' Inserir ou atualizar Codigo Ajeno
            update = Not String.IsNullOrEmpty(objCodigoAjeno.OidCodigoAjeno)

            Dim objItemResp = AccesoDatos.CodigoAjeno.SetCodigosAjenos(objCodigoAjeno, objTransacion)

            If Not objItemResp AndAlso update Then
                Throw New Exception("NO_ACTUALIZADO-001: No fue possible actualizar.")
            ElseIf Not objItemResp AndAlso Not update Then
                Throw New Exception("NO_ACTUALIZADO-002: No fue possible inserir.")
            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Trata violação de chave AK por item
            If ex.Message.IndexOf(ContractoServicio.Constantes.CONST_ERRO_BANCO_AK_VIOLATION) >= 0 Then
                If ex.Message.IndexOf(Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.Constantes.AK_GEPR_TCODIGO_AJENO_1) >= 0 Then
                    'COD_TIPO_TABLA_GENESIS, COD_IDENTIFICADOR, COD_AJENO
                    objCodigoAjenoResp.CodigoError = 100
                    objCodigoAjenoResp.MensajeError = String.Format(Traduzir("035_msg_004"), objCodigoAjeno.CodIdentificador, objCodigoAjeno.CodAjeno, objCodigoAjeno.CodTipoTablaGenesis)
                ElseIf ex.Message.IndexOf(Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.Constantes.AK_GEPR_TCODIGO_AJENO_2) >= 0 Then
                    'OID_TABLA_GENESIS, COD_IDENTIFICADOR
                    objCodigoAjenoResp.CodigoError = 100
                    objCodigoAjenoResp.MensajeError = String.Format(Traduzir("035_msg_002"), objCodigoAjeno.CodTipoTablaGenesis)
                ElseIf ex.Message.IndexOf(Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.Constantes.AK_GEPR_TCODIGO_AJENO_3) >= 0 Then
                    'OID_TABLA_GENESIS, COD_AJENO
                    objCodigoAjenoResp.CodigoError = 100
                    objCodigoAjenoResp.MensajeError = String.Format(Traduzir("035_msg_003"), objCodigoAjeno.CodTipoTablaGenesis)
                Else
                    objCodigoAjenoResp.CodigoError = 1
                    objCodigoAjenoResp.MensajeError = ex.Message
                End If
            ElseIf ex.Message.IndexOf(Prosegur.Global.GesEfectivo.IAC.ContractoServicio.CodigoAjeno.Constantes.NO_ACTUALIZADO) >= 0 Then
                objCodigoAjenoResp.CodigoError = 100
                objCodigoAjenoResp.MensajeError = Traduzir("035_msg_005")
            Else
                objCodigoAjenoResp.CodigoError = 1
                objCodigoAjenoResp.MensajeError = ex.Message
            End If


        End Try
        'Só retorna OidCodigoAjeno se for ação de update
        objCodigoAjenoResp.OidCodigoAjeno = objCodigoAjeno.OidCodigoAjeno
        objCodigoAjenoResp.OidTablaGenesis = objCodigoAjeno.OidTablaGenesis
        objCodigoAjenoResp.CodTipoTablaGenesis = objCodigoAjeno.CodTipoTablaGenesis
        objCodigoAjenoResp.CodAjeno = objCodigoAjeno.CodAjeno

        Return objCodigoAjenoResp

    End Function

    Private Sub ValidaPeticionSetCodigosAjenos(ByRef objPeticion As SetCodigosAjenos.Peticion, ByRef objRespuesta As SetCodigosAjenos.Respuesta)

        If objPeticion.CodigosAjenos Is Nothing OrElse objPeticion.CodigosAjenos.Count = 0 Then

            'Exceção caso a coleção da mensagem de entrada do serviço esteja nula ou vazia.
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_coleccion_vazia"), "CodigosAjenos"))

        Else
            For Each item In objPeticion.CodigosAjenos
                Dim itemLocal = item
                If objPeticion.CodigosAjenos.Where(Function(f) f.OidTablaGenesis = itemLocal.OidTablaGenesis _
                                                     AndAlso f.CodTipoTablaGenesis = item.CodTipoTablaGenesis _
                                                    AndAlso f.CodAjeno = itemLocal.CodAjeno _
                                                   AndAlso f.BolDefecto = True).ToList.Count > 1 Then

                    'Exceção caso esteja sendo recebido mais de um Codigo Ajeno como defecto
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("035_msg_006"), item.OidTablaGenesis))

                End If
            Next

            'Coleção de respostas
            objRespuesta.CodigosAjenos = New SetCodigosAjenos.CodigoAjenoRespuestaColeccion

            For Each item In objPeticion.CodigosAjenos

                'Valida os campos dos itens de entrada
                Dim camposObligatorios As String = ""

                camposObligatorios &= If(String.IsNullOrEmpty(item.OidTablaGenesis), "OidTablaGenesis, ", "")
                camposObligatorios &= If(String.IsNullOrEmpty(item.CodTipoTablaGenesis), "CodTipoTablaGenesis, ", "")
                camposObligatorios &= If(String.IsNullOrEmpty(item.CodAjeno), "CodAjeno, ", "")
                camposObligatorios &= If(String.IsNullOrEmpty(item.CodIdentificador), "CodIdentificador, ", "")
                camposObligatorios &= If(String.IsNullOrEmpty(item.DesUsuarioModificacion), "DesUsuarioModificacion, ", "")
                If String.IsNullOrEmpty(item.OidCodigoAjeno) Then
                    camposObligatorios &= If(String.IsNullOrEmpty(item.DesUsuarioCreacion), "DesUsuarioCreacion, ", "")
                End If

                Dim respItem As SetCodigosAjenos.CodigoAjenoRespuesta = Nothing
                If camposObligatorios.Length > 0 Then
                    'Trata a msg dos campos validados
                    camposObligatorios = camposObligatorios.Remove(camposObligatorios.Length - 2)

                    'Instancía item de resposta
                    respItem = New SetCodigosAjenos.CodigoAjenoRespuesta

                    'Preenche item de resposta
                    respItem.CodAjeno = item.CodAjeno
                    respItem.CodTipoTablaGenesis = item.CodTipoTablaGenesis
                    respItem.OidCodigoAjeno = item.OidCodigoAjeno
                    respItem.OidTablaGenesis = item.OidTablaGenesis

                    respItem.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT

                    'Adiciona item de resposta a lista de resposta
                    objRespuesta.CodigosAjenos.Add(respItem)

                End If

                ' Define se a mensagem vai ser no plural ou singular
                If camposObligatorios.IndexOf(",") >= 0 Then
                    'Escreve mensagem de resposta no plural caso seja mais de um campo vazio 
                    respItem.MensajeError = String.Format(Traduzir("gen_srv_msg_atributos"), camposObligatorios)
                ElseIf respItem IsNot Nothing Then
                    'Escreve mensagem de resposta no singular caso seja apenas um campo vazio 
                    respItem.MensajeError = String.Format(Traduzir("gen_srv_msg_atributo"), camposObligatorios)
                End If

                'Faz a verificação se o CodAjeno ou CodIdentificador do CodAjeno já existe
                If respItem Is Nothing Then

                    Dim objPeticionVerifica As New VerificarIdentificadorXCodigoAjeno.Peticion
                    objPeticionVerifica.CodAjeno = item.CodAjeno
                    objPeticionVerifica.CodIdentificador = item.CodIdentificador
                    objPeticionVerifica.CodTipoTablaGenesis = item.CodTipoTablaGenesis
                    objPeticionVerifica.OidCodigoAjeno = item.OidCodigoAjeno

                    Dim resp = VerificarIdentificadorXCodigoAjeno(objPeticionVerifica)
                    If resp.CodigoError <> 0 Then
                        'Instancía item de resposta
                        respItem = New SetCodigosAjenos.CodigoAjenoRespuesta

                        respItem.MensajeError = resp.MensajeError
                        respItem.CodigoError = resp.CodigoError
                    End If

                End If

            Next


        End If

    End Sub

    ''' <summary>
    ''' Operación para grabar los datos de código Ajeno.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 17/04/2013 Criado
    ''' </history>
    Public Function VerificarIdentificadorXCodigoAjeno(ByRef objPeticion As VerificarIdentificadorXCodigoAjeno.Peticion) As VerificarIdentificadorXCodigoAjeno.Respuesta
        ' criar objeto respuesta
        Dim objRespuesta As New VerificarIdentificadorXCodigoAjeno.Respuesta
        Dim NomeEntidade As String = String.Empty
        objPeticion.NombreCampoOid = ObtenerNombreCampoOid(objPeticion.CodTipoTablaGenesis)
        objPeticion.NombreCampoCod = ObtenerNombreCampoCodigo(objPeticion.CodTipoTablaGenesis)
        objPeticion.NombreCampoDes = ObtenerNombreCampoDescripcion(objPeticion.CodTipoTablaGenesis)
        Try

            'Valida petição
            ValidaPeticionVerificarIdentificadorXCodigoAjeno(objPeticion)

            Dim objItemResp = AccesoDatos.CodigoAjeno.VerificarIdentificadorXCodigoAjeno(objPeticion)
            If Not String.IsNullOrEmpty(objItemResp.Codigo) Then
                objRespuesta.CodigoError = 100

                NomeEntidade = RetornaDadoTabela(objPeticion.CodTipoTablaGenesis)

                objRespuesta.MensajeError = String.Format(Traduzir("035_msg_004"), objPeticion.CodIdentificador, objPeticion.CodAjeno, If(Not String.IsNullOrEmpty(NomeEntidade), NomeEntidade, objPeticion.CodTipoTablaGenesis), objItemResp.Codigo, objItemResp.Descripcion, NomeEntidade)
            Else
                ' preparar codigos e mensagens do respuesta
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuesta.MensajeError = String.Empty
            End If

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Private Sub ValidaPeticionVerificarIdentificadorXCodigoAjeno(ByRef objPeticion As VerificarIdentificadorXCodigoAjeno.Peticion)

        If objPeticion.CodAjeno Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "CodAjeno"))
        End If
        If objPeticion.CodIdentificador Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "CodIdentificador"))
        End If
        If objPeticion.CodTipoTablaGenesis Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "CodTipoTablaGenesis"))
        End If

    End Sub

    Private Function RetornaDadoTabela(Entidade As String) As String

        Dim mapeo As New IAC.AccesoDatos.MapeoEntidadeCodigoAjeno
        Dim nomeTabela As String = String.Empty

        mapeo = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                 Where item.Entidade = Entidade
                 Select item).FirstOrDefault()

        If mapeo IsNot Nothing Then
            If mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteCliente Then
                nomeTabela = Traduzir("060_msg_nome_entidadeCliente")
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteCanal Then
                nomeTabela = Traduzir("060_msg_nome_entidadeCanal")
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSubcanal Then
                nomeTabela = Traduzir("060_msg_nome_entidadeSubcanal")
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteGrupoCliente Then
                nomeTabela = Traduzir("060_msg_nome_entidadeGrupoCliente")
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstantePlanta Then
                nomeTabela = Traduzir("060_msg_nome_entidadePlanta")
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstantePuntoServicio Then
                nomeTabela = Traduzir("060_msg_nome_entidadePuntoServicio")
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSector Then
                nomeTabela = Traduzir("060_msg_nome_entidadeSector")
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSubcliente Then
                nomeTabela = Traduzir("060_msg_nome_entidadeSubcliente")
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteTipoSector Then
                nomeTabela = Traduzir("060_msg_nome_entidadeTipoSector")
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDelegacion Then
                nomeTabela = Traduzir("060_msg_nome_entidadeDelegacion")
            End If
        End If

        Return nomeTabela
    End Function
    Private Function ObtenerNombreCampoOid(Entidade As String) As String
        Dim mapeo As New IAC.AccesoDatos.MapeoEntidadeCodigoAjeno
        Dim nombreCampoOid As String = String.Empty

        mapeo = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                 Where item.Entidade = Entidade
                 Select item).FirstOrDefault()

        If mapeo IsNot Nothing Then
            If mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteCliente Then
                nombreCampoOid = "OID_CLIENTE"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteCanal Then
                nombreCampoOid = "OID_CANAL"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSubcanal Then
                nombreCampoOid = "OID_SUBCANAL"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteGrupoCliente Then
                nombreCampoOid = "OID_GRUPO_CLIENTE"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstantePlanta Then
                nombreCampoOid = "OID_PLANTA"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstantePuntoServicio Then
                nombreCampoOid = "OID_PTO_SERVICIO"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSector Then
                nombreCampoOid = "OID_SECTOR"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSubcliente Then
                nombreCampoOid = "OID_SUBCLIENTE"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteTipoSector Then
                nombreCampoOid = "OID_TIPO_SECTOR"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDelegacion Then
                nombreCampoOid = "OID_DELEGACION"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDivisa Then
                nombreCampoOid = "OID_DIVISA"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDenominacion Then
                nombreCampoOid = "OID_DENOMINACION"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteMaquina Then
                nombreCampoOid = "OID_MAQUINA"
            End If
        End If

        Return nombreCampoOid
    End Function
    Private Function ObtenerNombreCampoCodigo(Entidade As String) As String
        Dim mapeo As New IAC.AccesoDatos.MapeoEntidadeCodigoAjeno
        Dim nombreCampoOid As String = String.Empty

        mapeo = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                 Where item.Entidade = Entidade
                 Select item).FirstOrDefault()

        If mapeo IsNot Nothing Then
            If mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteCliente Then
                nombreCampoOid = "COD_CLIENTE"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteCanal Then
                nombreCampoOid = "COD_CANAL"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSubcanal Then
                nombreCampoOid = "COD_SUBCANAL"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteGrupoCliente Then
                nombreCampoOid = "COD_GRUPO_CLIENTE"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstantePlanta Then
                nombreCampoOid = "COD_PLANTA"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstantePuntoServicio Then
                nombreCampoOid = "COD_PTO_SERVICIO"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSector Then
                nombreCampoOid = "COD_SECTOR"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSubcliente Then
                nombreCampoOid = "COD_SUBCLIENTE"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteTipoSector Then
                nombreCampoOid = "COD_TIPO_SECTOR"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDelegacion Then
                nombreCampoOid = "COD_DELEGACION"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDivisa Then
                nombreCampoOid = "COD_ISO_DIVISA"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDenominacion Then
                nombreCampoOid = "COD_DENOMINACION"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteMaquina Then
                nombreCampoOid = "OID_MAQUINA"
            End If
        End If

        Return nombreCampoOid
    End Function
    Private Function ObtenerNombreCampoDescripcion(Entidade As String) As String
        Dim mapeo As New IAC.AccesoDatos.MapeoEntidadeCodigoAjeno
        Dim nombreCampoOid As String = String.Empty

        mapeo = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                 Where item.Entidade = Entidade
                 Select item).FirstOrDefault()

        If mapeo IsNot Nothing Then
            If mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteCliente Then
                nombreCampoOid = "DES_CLIENTE"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteCanal Then
                nombreCampoOid = "DES_CANAL"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSubcanal Then
                nombreCampoOid = "DES_SUBCANAL"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteGrupoCliente Then
                nombreCampoOid = "DES_GRUPO_CLIENTE"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstantePlanta Then
                nombreCampoOid = "DES_PLANTA"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstantePuntoServicio Then
                nombreCampoOid = "DES_PTO_SERVICIO"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSector Then
                nombreCampoOid = "DES_SECTOR"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteSubcliente Then
                nombreCampoOid = "DES_SUBCLIENTE"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteTipoSector Then
                nombreCampoOid = "DES_TIPO_SECTOR"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDelegacion Then
                nombreCampoOid = "DES_DELEGACION"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDivisa Then
                nombreCampoOid = "DES_DIVISA"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteDenominacion Then
                nombreCampoOid = "DES_DENOMINACION"
            ElseIf mapeo.Entidade = IAC.AccesoDatos.Constantes.ConstanteMaquina Then
                nombreCampoOid = "COD_IDENTIFICACION"
            End If
        End If

        Return nombreCampoOid
    End Function
    ''' <summary>
    ''' Operación para obtener los datos de código Ajeno
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAjenoByClienteSubClientePuntoServicio(objPeticion As GetAjenoByClienteSubClientePuntoServicio.Peticion) As GetAjenoByClienteSubClientePuntoServicio.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New GetAjenoByClienteSubClientePuntoServicio.Respuesta

        Try

            Dim objPeticionCodigoAjeno As New GetCodigosAjenos.Peticion
            objPeticionCodigoAjeno.CodigosAjeno = New GetCodigosAjenos.CodigoAjeno

            If Not String.IsNullOrEmpty(objPeticion.CodigoCliente) AndAlso String.IsNullOrEmpty(objPeticion.CodigoSubCliente) AndAlso String.IsNullOrEmpty(objPeticion.CodigoPuntoServicio) Then
                objPeticionCodigoAjeno.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TCLIENTE"
                Dim objPeticionCliente As New IAC.ContractoServicio.Cliente.GetClientesDetalle.Peticion With {.CodCliente = objPeticion.CodigoCliente,
                                                                                                              .ParametrosPaginacion = New Genesis.Comon.Paginacion.ParametrosPeticionPaginacion With {.RealizarPaginacion = False}}
                Dim objCliente As New ContractoServicio.Cliente.GetClientesDetalle.Respuesta
                objCliente.Clientes = AccesoDatos.Cliente.GetClienteByCodigo(objPeticionCliente, objCliente.ParametrosPaginacion)
                If objCliente IsNot Nothing AndAlso objCliente.Clientes IsNot Nothing AndAlso objCliente.Clientes.Count > 0 Then
                    objPeticionCodigoAjeno.CodigosAjeno.OidTablaGenesis = objCliente.Clientes(0).OidCliente
                End If
            End If

            If Not String.IsNullOrEmpty(objPeticion.CodigoCliente) AndAlso Not String.IsNullOrEmpty(objPeticion.CodigoSubCliente) AndAlso String.IsNullOrEmpty(objPeticion.CodigoPuntoServicio) Then
                objPeticionCodigoAjeno.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TSUBCLIENTE"
                Dim objPeticionSubCliente As New IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Peticion With {.CodCliente = objPeticion.CodigoCliente,
                                                                                                                       .CodSubCliente = objPeticion.CodigoSubCliente,
                                                                                                                       .ParametrosPaginacion = New Paginacion.ParametrosPeticionPaginacion With {.RealizarPaginacion = False}}
                Dim objSubCliente As New ContractoServicio.SubCliente.GetSubClientesDetalle.Respuesta
                objSubCliente.SubClientes = AccesoDatos.SubCliente.GetSubClientesDetalle(objPeticionSubCliente, objRespuesta.ParametrosPaginacion)
                If objSubCliente IsNot Nothing AndAlso objSubCliente.SubClientes IsNot Nothing AndAlso objSubCliente.SubClientes.Count > 0 Then
                    objPeticionCodigoAjeno.CodigosAjeno.OidTablaGenesis = objSubCliente.SubClientes(0).OidSubCliente
                End If
            End If

            If Not String.IsNullOrEmpty(objPeticion.CodigoCliente) AndAlso Not String.IsNullOrEmpty(objPeticion.CodigoSubCliente) AndAlso Not String.IsNullOrEmpty(objPeticion.CodigoPuntoServicio) Then
                objPeticionCodigoAjeno.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO"
                Dim objPeticionPuntoServicio As New IAC.ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Peticion With {.CodCliente = objPeticion.CodigoCliente,
                                                                                                                               .CodSubcliente = objPeticion.CodigoSubCliente, .CodPtoServicio = objPeticion.CodigoPuntoServicio,
                                                                                                                               .ParametrosPaginacion = New Paginacion.ParametrosPeticionPaginacion With {.RealizarPaginacion = False}}
                Dim objPuntoServicio As New ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Respuesta
                objPuntoServicio.PuntoServicio = AccesoDatos.PuntoServicio.GetPuntoServicioDetalle(objPeticionPuntoServicio, objRespuesta.ParametrosPaginacion)
                If objPuntoServicio IsNot Nothing AndAlso objPuntoServicio.PuntoServicio IsNot Nothing AndAlso objPuntoServicio.PuntoServicio.Count > 0 Then
                    objPeticionCodigoAjeno.CodigosAjeno.OidTablaGenesis = objPuntoServicio.PuntoServicio(0).OidPuntoServicio
                End If
            End If

            objPeticionCodigoAjeno.CodigosAjeno.BolDefecto = IIf(objPeticion.ValorPadron, True, Nothing)
            objPeticionCodigoAjeno.CodigosAjeno.BolActivo = True
            objPeticionCodigoAjeno.ParametrosPaginacion = New Genesis.Comon.Paginacion.ParametrosPeticionPaginacion With {.RealizarPaginacion = False}

            objRespuesta.Ajenos = ConverteCodigoAjeno(AccesoDatos.CodigoAjeno.GetCodigosAjenos(objPeticionCodigoAjeno, objRespuesta.ParametrosPaginacion))

            objPeticionCodigoAjeno.CodigosAjeno.BolDefecto = objPeticion.ValorPadron

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Private Function ConverteCodigoAjeno(objColCodigoAjenos As GetCodigosAjenos.EntidadCodigosAjenoColeccion) As ContractoServicio.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.AjenoColeccion

        Dim objColRetorno As ContractoServicio.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.AjenoColeccion = Nothing

        If objColCodigoAjenos IsNot Nothing AndAlso objColCodigoAjenos.Count > 0 Then

            objColRetorno = New ContractoServicio.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.AjenoColeccion

            For Each item In objColCodigoAjenos

                For Each codigo In item.CodigosAjenos

                    objColRetorno.Add(New ContractoServicio.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.Ajeno With {
                                  .BolDefecto = codigo.BolDefecto,
                                  .CodAjeno = codigo.CodAjeno,
                                  .DesAjeno = codigo.DesAjeno})

                Next

            Next

        End If

        Return objColRetorno

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
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
