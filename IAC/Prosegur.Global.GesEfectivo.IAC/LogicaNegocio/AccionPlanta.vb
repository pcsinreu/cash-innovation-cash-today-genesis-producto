Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Planta
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionPlanta
    Implements ContractoServicio.IPlanta

    ''' <summary>
    ''' Assinatura do método GetPlanta - Retorna a pesquisa de Plantas
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Public Function GetPlantas(objPeticion As ContractoServicio.Planta.GetPlanta.Peticion) As ContractoServicio.Planta.GetPlanta.Respuesta Implements ContractoServicio.IPlanta.GetPlanta

        'CRIA O OBJETO DE RESPOSTA
        Dim objRespuesta As New ContractoServicio.Planta.GetPlanta.Respuesta
        Try
            If (String.IsNullOrEmpty(objPeticion.oidDelegacion)) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "oidDelegacion"))
            End If

            If objPeticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            'OBTER PLANTAS
            objRespuesta.Planta = AccesoDatos.Planta.GetPlanta(objPeticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        Catch ex As Excepcion.NegocioExcepcion
            'CASO OCORRA ALGUMA EXEÇÃO, TRATA O OBJETO RESPUESTA DA FORMA ADEQUADA
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' ASSINATURA DO METODO DE GETPLANTADETAIL - PEGAR OS DETALHES DA PLANTA
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Public Function GetPlantaDetail(objPeticion As ContractoServicio.Planta.GetPlantaDetail.Peticion) As ContractoServicio.Planta.GetPlantaDetail.Respuesta Implements ContractoServicio.IPlanta.GetPlantaDetail

        'CRIAR O OBJETO DE RESPOSTA
        Dim objRespuesta As New ContractoServicio.Planta.GetPlantaDetail.Respuesta

        Try
            'OBTER OS DETALHES
            objRespuesta.Planta = AccesoDatos.Planta.GetDadosPlanta(objPeticion.OidPlanta)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' ASSINATURA DO METODO SETPLANTAS EFETUA A INCLUSÃO, EXCLUSÃO E ALTERAÇÃO
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' [daniel.nunes] 03/09/2013 - Atualizado
    ''' </history>
    Public Function SetPlantas(objPeticion As ContractoServicio.Planta.SetPlanta.Peticion) As ContractoServicio.Planta.SetPlanta.Respuesta Implements ContractoServicio.IPlanta.SetPlanta

        'CRIAR O OBJETO DE RESPOSTA
        Dim objRespuesta As New ContractoServicio.Planta.SetPlanta.Respuesta
        Dim objResposta As New IAC.LogicaNegocio.AccionCodigoAjeno
        Dim objRespostaImporteMaximo As New IAC.LogicaNegocio.AccionImporteMaximo
        Dim oidPlanta As String = Nothing
        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

        Try

            ' PREENCHE OBJETO PARA VALIDAR SE PLANTA JÁ EXISTE
            Dim objExistente As New ContractoServicio.Planta.VerificaExistencia.Peticion
            objExistente.CodDelegacion = objPeticion.OidDelegacion
            objExistente.CodPlanta = objPeticion.CodPlanta

            ' VALIDANDO DADOS
            If String.IsNullOrEmpty(objPeticion.GmtModificacion.ToString) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "GmtModificacion"))
            End If

            If String.IsNullOrEmpty(objPeticion.DesUsuarioCreacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesUsuarioCreacion"))
            End If

            If String.IsNullOrEmpty(objPeticion.DesUsuarioModificacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesUsuarioModificacion"))
            End If

            If (String.IsNullOrEmpty(objPeticion.DesPlanta)) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesPlanta"))
            End If

            If (String.IsNullOrEmpty(objPeticion.OidDelegacion)) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidDelegacion"))
            End If

            If (String.IsNullOrEmpty(objPeticion.CodPlanta)) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodPlanta"))
            End If

            ' VERIFICANDO UTILIZAÇÃO DO SECTOR
            If Not String.IsNullOrEmpty(objPeticion.OidPlanta) Then
                If Not objPeticion.BolActivo Then
                    If Not AccesoDatos.Planta.VerificaUtilizacaoSector(objPeticion.OidPlanta) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("053_msg_erro_Erro_PlantaUtilizada"))
                    End If
                End If
            End If

            ' VERIFICANDO SE A DELEGAÇÃO ESTA ATIVA
            If Not AccesoDatos.Planta.VerificaActivoDelegacion(objPeticion.OidDelegacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("053_msg_erro_Erro_DelegacionDesativada"))
            End If

            ' VALIDANDO SE A PLANTA É NOVA OU SE ESTA ATUALIZANDO UMA PLANTA EXISTENTE
            If objPeticion.OidPlanta <> Nothing Then

                'EFETUA A ATUALIZACAO DA PLANTA
                AccesoDatos.Planta.AtualizacionPlanta(objPeticion, objTransacao)

                If objPeticion.CodigosAjenos.Count > 0 Then

                    Dim objCodigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion = PreencheCodigoAjeno(objPeticion)

                    objRespuesta.CodigosAjenos = objResposta.SetCodigosAjenos(objCodigoAjeno, objTransacao)

                End If

                If objPeticion.ImporteMaximo.Count > 0 Then

                    Dim objImporteMaximo As ContractoServicio.ImporteMaximo.SetImporteMaximo.Peticion = PreencheImporteMaximo(objPeticion)

                    objRespuesta.ImportesMaximos = objRespostaImporteMaximo.SetImporteMaximo(objImporteMaximo, objTransacao)

                End If

            Else

                ' VERIFICA SE A PLANTA JÀ EXISTE
                If AccesoDatos.Planta.VerificaCodigoDelegacionPlanta(objExistente) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("053_msg_erro_PlantaExistente"))
                End If

                ' EFETUA A INCLUSÃO NO BANCO DE DADOS DO IAC
                Dim objTransacaoPlanta As New Transacao(Constantes.CONEXAO_GE)
                oidPlanta = AccesoDatos.Planta.AltaPlanta(objPeticion, objTransacaoPlanta)
                objTransacaoPlanta.RealizarTransacao()

                ' VERIFICA SE CONTÉM CÓDIGOS AJENOS
                If objPeticion.CodigosAjenos.Count > 0 Then

                    Dim objCodigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion = PreencheCodigoAjeno(objPeticion, oidPlanta)

                    objRespuesta.CodigosAjenos = objResposta.SetCodigosAjenos(objCodigoAjeno, objTransacao)

                End If

                ' VERIFICA SE CONTÉM IMPORTE MAXIMO
                If objPeticion.ImporteMaximo.Count > 0 Then

                    Dim objImporteMaximo As ContractoServicio.ImporteMaximo.SetImporteMaximo.Peticion = PreencheImporteMaximo(objPeticion, oidPlanta)

                    objRespuesta.ImportesMaximos = objRespostaImporteMaximo.SetImporteMaximo(objImporteMaximo, objTransacao)

                End If

            End If

            ' RELIZA A TRANSAÇÃO
            objTransacao.RealizarTransacao()

            ' PREENCHE OBJETO DE RESPOSTA SEM ERROS
            objRespuesta.CodigoPlanta = objPeticion.CodPlanta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoPlanta = objPeticion.CodPlanta
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            objRespuesta.CodigoPlanta = objPeticion.CodPlanta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta

    End Function

    Private Function PreencheImporteMaximo(ByRef objPeticion As ContractoServicio.Planta.SetPlanta.Peticion,
                                                 Optional oidPlanta As String = Nothing) As ContractoServicio.ImporteMaximo.SetImporteMaximo.Peticion

        Dim objImporteMaximo As New ContractoServicio.ImporteMaximo.SetImporteMaximo.Peticion

        objImporteMaximo.ImportesMaximo = New ContractoServicio.ImporteMaximo.SetImporteMaximo.ImporteMaximoColeccion

        For Each objItem In objPeticion.ImporteMaximo
            Dim objItemCast As New ContractoServicio.ImporteMaximo.SetImporteMaximo.ImporteMaximo
            Dim codigotablaGenesis As String = String.Empty

            objItemCast.OidImporteMaximo = objItem.OidImporteMaximo
            If oidPlanta IsNot Nothing Then
                objItemCast.oidPlanta = oidPlanta
            Else
                objItemCast.oidPlanta = objPeticion.OidPlanta
            End If

            objItemCast.Cliente = objItem.Cliente
            objItemCast.Divisa = objItem.Divisa
            objItemCast.Canal = objItem.Canal
            objItemCast.Sector = objItem.Sector
            objItemCast.SubCanal = objItem.SubCanal
            objItemCast.ValorMaximo = objItem.ValorMaximo
            objItemCast.BolVigente = objItem.BolVigente
            objItemCast.DesUsuarioCreacion = objPeticion.DesUsuarioCreacion
            objItemCast.DesUsuarioModificacion = objPeticion.DesUsuarioModificacion
            objItemCast.GmtCreacion = objPeticion.GmtCreacion
            objItemCast.GmtModificacion = objPeticion.GmtModificacion
            objImporteMaximo.ImportesMaximo.Add(objItemCast)

        Next

        Return objImporteMaximo

    End Function

    Public Function PreencheCodigoAjeno(ByRef objPeticion As ContractoServicio.Planta.SetPlanta.Peticion,
                                        Optional OidPlanta As String = Nothing) As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion

        Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion

        objCodigoAjeno.CodigosAjenos = New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion

        For Each objItem In objPeticion.CodigosAjenos
            Dim objItemCast As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno
            Dim codigotablaGenesis As String = String.Empty

            objItemCast.OidCodigoAjeno = objItem.OidCodigoAjeno
            objItemCast.BolActivo = objItem.BolActivo
            objItemCast.BolDefecto = objItem.BolDefecto
            objItemCast.CodAjeno = objItem.CodAjeno
            objItemCast.CodIdentificador = objItem.CodIdentificador
            objItemCast.DesAjeno = objItem.DesAjeno
            objItemCast.DesUsuarioCreacion = objPeticion.DesUsuarioCreacion
            objItemCast.DesUsuarioModificacion = objPeticion.DesUsuarioModificacion
            objItemCast.GmtCreacion = objPeticion.GmtCreacion
            objItemCast.GmtModificacion = objPeticion.GmtModificacion
            If String.IsNullOrEmpty(OidPlanta) Then
                objItemCast.OidTablaGenesis = objPeticion.OidPlanta
            Else
                objItemCast.OidTablaGenesis = OidPlanta
            End If
            codigotablaGenesis = ContractoServicio.Constantes.COD_PLANTA
            objItemCast.CodTipoTablaGenesis = (From item In AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                               Where item.CodTipoTablaGenesis = codigotablaGenesis
                                               Select item.Entidade).FirstOrDefault()

            objCodigoAjeno.CodigosAjenos.Add(objItemCast)

        Next

        Return objCodigoAjeno

    End Function

    ''' <summary>
    ''' Assinatura do método VerificaExistencia
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Public Function VerificaExistencia(objPeticion As ContractoServicio.Planta.VerificaExistencia.Peticion) As ContractoServicio.Planta.VerificaExistencia.Respuesta Implements ContractoServicio.IPlanta.VerificaExistencia

        'CRIA OBJETO DE RESPOSTA
        Dim objRespuesta As New ContractoServicio.Planta.VerificaExistencia.Respuesta

        Try

            'VERIFICA SE OS PARAMETROS FORAM ENVIADOS
            If String.IsNullOrEmpty(objPeticion.CodDelegacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("052_msg_erro_PlantaCodDelegacion"))
            End If

            If String.IsNullOrEmpty(objPeticion.CodPlanta) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("049_msg_erro_PlantaCodigo"))
            End If

            objRespuesta.Existe = AccesoDatos.Planta.VerificaCodigoDelegacionPlanta(objPeticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Verifica se o codigo da Planta existe
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 15/02/2013 Criado
    ''' </history>
    Public Function VerificaCodigoPlanta(Peticion As ContractoServicio.Planta.VerificaCodigoPlanta.Peticion) As ContractoServicio.Planta.VerificaCodigoPlanta.Respuesta Implements ContractoServicio.IPlanta.VerificaCodigoPlanta
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Planta.VerificaCodigoPlanta.Respuesta

        Try
            'exewcutar verificação no banco
            objRespuesta.Existe = AccesoDatos.Planta.VerificaCodigoPlanta(Peticion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try
        Return objRespuesta
    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IPlanta.Test
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