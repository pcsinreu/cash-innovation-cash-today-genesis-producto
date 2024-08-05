Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Genesis

Public Class AccionTipoSetor
    Implements ContractoServicio.ITipoSetor

    ''' <summary>
    ''' Assinatura do método GetTiposSectores
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2012 Criado
    ''' </history>
    Public Function GetTiposSectores(objPeticion As ContractoServicio.TipoSetor.GetTiposSectores.Peticion) As ContractoServicio.TipoSetor.GetTiposSectores.Respuesta Implements ContractoServicio.ITipoSetor.GetTiposSectores

        'Cria objeto de Resposta
        Dim objRespuesta As New ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

        Try

            If objPeticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            ' obter Tipo Setor
            objRespuesta.TipoSetor = AccesoDatos.TipoSetor.GetTiposSectores(objPeticion, objRespuesta.ParametrosPaginacion)
            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
            objRespuesta.Resultado = 0
        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.Resultado = 1
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
            objRespuesta.Resultado = 1
        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Assinatura do método SetTiposSectores
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2012 Criado
    ''' </history>
    Public Function SetTiposSectores(objPeticion As ContractoServicio.TipoSetor.SetTiposSectores.Peticion) As ContractoServicio.TipoSetor.SetTiposSectores.Respuesta Implements ContractoServicio.ITipoSetor.SetTiposSectores

        'CRIAR OBJETOS
        Dim objRespuesta As New ContractoServicio.TipoSetor.SetTiposSectores.Respuesta
        Dim objResposta As New IAC.LogicaNegocio.AccionCodigoAjeno
        Dim OidTipoSector As String = Nothing
        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

        Try

            Dim objTipoExistente As New ContractoServicio.TipoSetor.VerificaTipoSectores.Peticion
            objTipoExistente.Codigo = objPeticion.codTipoSector

            ' VALIDAÇÕES DOS DADOS
            If String.IsNullOrEmpty(objPeticion.codTipoSector) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codTipoSector"))
            End If

            If String.IsNullOrEmpty(objPeticion.desTipoSector) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "desTipoSector"))
            End If

            If objPeticion.codCaractTipoSector.Count = 0 Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codCaractTipoSector"))
            End If

            For Each caracteristicas As ContractoServicio.TipoSetor.SetTiposSectores.Caracteristica In objPeticion.codCaractTipoSector
                If caracteristicas Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codCaractTipoSector"))
                End If
            Next

            For Each caracteristicas As ContractoServicio.TipoSetor.SetTiposSectores.Caracteristica In objPeticion.codCaractTipoSector
                If String.IsNullOrEmpty(caracteristicas.codCaractTipoSector) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codCaractTipoSector"))
                End If
            Next

            ' VALIDANDO SE A TIPO SECTOR É NOVA OU SE ESTA ATUALIZANDO UM TIPO SECTOR EXISTENTE
            If objPeticion.oidTipoSector <> Nothing Then

                ' VERIFICANDO SE TIPO SECTOR ESTA ATIVO
                If objPeticion.bolActivo = False Then
                    If Not AccesoDatos.TipoSetor.VerificaTipoSectorUtilizadoSetor(objPeticion.oidTipoSector) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("059_msg_erro_tipoSectorUtilizadoSector"))
                    End If
                End If

                ' EFETUA A ATUALIZACAO DO TIPO SECTOR
                AccesoDatos.TipoSetor.AtualizarTipoSetor(objPeticion, objTransacao)

                If objPeticion.CodigosAjenos.Count > 0 Then

                    Dim objCodigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion = PreencheCodigoAjeno(objPeticion)

                    objRespuesta.CodigosAjenos = objResposta.SetCodigosAjenos(objCodigoAjeno, objTransacao)

                End If

            Else
                ' EFETUA GRAVAÇÃO NO BANCO DE DADOS DO IAC
                OidTipoSector = AccesoDatos.TipoSetor.SetTiposSectores(objPeticion, objTransacao)

                If objPeticion.CodigosAjenos.Count > 0 Then

                    Dim objCodigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion = PreencheCodigoAjeno(objPeticion, OidTipoSector)

                    objRespuesta.CodigosAjenos = objResposta.SetCodigosAjenos(objCodigoAjeno, objTransacao)

                End If

            End If

            ' REALIZA TRANSAÇÃO
            objTransacao.RealizarTransacao()

            objRespuesta.codTipoSector = objPeticion.codTipoSector
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.codTipoSector = objPeticion.codTipoSector
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.codTipoSector = objPeticion.codTipoSector
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try
        Return objRespuesta
    End Function

    Public Function PreencheCodigoAjeno(ByRef objPeticion As ContractoServicio.TipoSetor.SetTiposSectores.Peticion,
                                    Optional OidTipoSector As String = Nothing) As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion

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
            objItemCast.DesUsuarioCreacion = objPeticion.desUsuarioCreacion
            objItemCast.DesUsuarioModificacion = objPeticion.desUsuarioModificacion
            objItemCast.GmtCreacion = objPeticion.gmtCreacion
            objItemCast.GmtModificacion = objPeticion.gmtModificacion
            If String.IsNullOrEmpty(OidTipoSector) Then
                objItemCast.OidTablaGenesis = objPeticion.oidTipoSector
            Else
                objItemCast.OidTablaGenesis = OidTipoSector
            End If
            codigotablaGenesis = ContractoServicio.Constantes.COD_TIPO_SECTOR
            objItemCast.CodTipoTablaGenesis = (From item In AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                               Where item.CodTipoTablaGenesis = codigotablaGenesis
                                               Select item.Entidade).FirstOrDefault()

            objCodigoAjeno.CodigosAjenos.Add(objItemCast)

        Next

        Return objCodigoAjeno

    End Function

    ''' <summary>
    ''' Operación que permite realizar la consulta de las características que no pertenecen al tipo sector.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 04/03/2013 Criado
    ''' </history>
    Public Function GetCaractNoPertenecTipoSector(objPeticion As ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Peticion) As ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Respuesta Implements ContractoServicio.ITipoSetor.GetCaractNoPertenecTipoSector

        Dim objRespuesta As New ContractoServicio.TipoSetor.GetCaractNoPertenecTipoSector.Respuesta
        Dim objResposta As New ContractoServicio.TipoSetor.GetTiposSectores.Respuesta
        Dim objPeticao As New ContractoServicio.TipoSetor.GetTiposSectores.Peticion

        Try

            objPeticao.codTipoSector = objPeticion.codTipoSector

            objResposta.TipoSetor = AccesoDatos.TipoSetor.GetTiposSectores(objPeticao, objResposta.ParametrosPaginacion)

            If String.IsNullOrEmpty(objPeticion.codTipoSector) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "codCaractTipoSector"))
            End If

            objRespuesta.TipoSetorNoPerte = AccesoDatos.TipoSetor.GetCaractNoPertenecTipoSector(objResposta.TipoSetor(0).oidTipoSector, objPeticion.codTipoSector)
            objRespuesta.Resultado = 0
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = String.Empty
        Catch ex As Excepcion.NegocioExcepcion
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
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
    ''' [danielnunes] 17/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoSetor.Test
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
