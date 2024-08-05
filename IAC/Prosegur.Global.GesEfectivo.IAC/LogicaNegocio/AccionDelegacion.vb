Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Delegacion
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Genesis

Public Class AccionDelegacion
    Implements ContractoServicio.IDelegacion

    Public Property DarAltaPlanta As Boolean

    ''' <summary>
    ''' Assinatura do método GetCodigoDelegacion
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 18/05/2012 Criado
    ''' </history>
    Public Function GetCodigoDelegacion(objPeticion As ContractoServicio.Delegacion.GetCodigoDelegacion.Peticion) As ContractoServicio.Delegacion.GetCodigoDelegacion.Respuesta Implements ContractoServicio.IDelegacion.GetCodigoDelegacion

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Delegacion.GetCodigoDelegacion.Respuesta

        Try
            ' obtém código da delgacion
            objRespuesta.CodigoDelegacion = AccesoDatos.Delegacion.GetCodigoDelegacion(objPeticion.CodigoAplicacion, objPeticion.HostPuesto)
            If (String.IsNullOrEmpty(objRespuesta.CodigoDelegacion)) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("035_msg_codigo_delegacion_noencontrado"))
            End If
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



    ''' <summary>
    ''' Obtém as delegações. Caso nenhum seja informado, todos registros serão retornados.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/02/2013 Criado
    ''' </history>
    Public Function GetDelegaciones(objPeticion As ContractoServicio.Delegacion.GetDelegacion.Peticion) As ContractoServicio.Delegacion.GetDelegacion.Respuesta Implements ContractoServicio.IDelegacion.GetDelegacion

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacion.Respuesta

        Try
            'If (String.IsNullOrEmpty(objPeticion.OidPais)) Then
            'Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidPais"))
            'End If

            If objPeticion.ParametrosPaginacion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "ParametrosPaginacion"))
            ElseIf (objPeticion.ParametrosPaginacion.RealizarPaginacion AndAlso objPeticion.ParametrosPaginacion.RegistrosPorPagina = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "RegistrosPorPagina"))
            End If

            ' obter delegaciones
            objRespuesta.Delegacion = AccesoDatos.Delegacion.GetDelegacion(objPeticion, objRespuesta.ParametrosPaginacion)
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
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
    ''' Efetua a inserção, exclusão e alteração.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/02/2013 Criado
    ''' [daniel.nunes] 03/09/2013 Alterado
    ''' </history>
    Public Function SetDelegaciones(Peticion As ContractoServicio.Delegacion.SetDelegacion.Peticion) As ContractoServicio.Delegacion.SetDelegacion.Respuesta Implements ContractoServicio.IDelegacion.SetDelegacion

        'Criar objetos
        Dim oidDelegacion As String = String.Empty
        Dim objRespuesta As New ContractoServicio.Delegacion.SetDelegacion.Respuesta
        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

        Try
            If Not Peticion.BolVigente AndAlso Not String.IsNullOrEmpty(Peticion.OidDelegacion) Then

                AccesoDatos.Delegacion.DeleteDelegacion(Peticion)

            ElseIf Peticion.OidDelegacion <> Nothing Then

                ValidaPeticion(Peticion)

                'Efetuar a atualização da Delegacion
                AccesoDatos.Delegacion.AtualizaDelegacion(Peticion)
                oidDelegacion = Peticion.OidDelegacion
                ModificarPlanta(Peticion, AccesoDatos.Delegacion.GetPlanta(Peticion.OidDelegacion))
            ElseIf String.IsNullOrEmpty(Peticion.OidDelegacion) Then

                ValidaPeticion(Peticion)

                'Efetua a inserção no banco de dados
                oidDelegacion = AccesoDatos.Delegacion.AltaDelegacion(Peticion, objTransacao)
                DarAltaPlanta = True

            End If

            If Peticion.BolTotasDelegacionesConfigRegionales Then
                AccesoDatos.Delegacion.ModificarTodasConfiguracionesRegionales(Peticion, objTransacao)
            End If

            AccesoDatos.Delegacion.BajaClienteFacturacion(oidDelegacion, objTransacao)
            For Each item In Peticion.LstClienteFacturacion
                oidDelegacion = AccesoDatos.Delegacion.AltaClienteFacturacion(oidDelegacion, item, objTransacao)
            Next

            If Peticion.PeticionDatosBancarios IsNot Nothing AndAlso Peticion.PeticionDatosBancarios.DatosBancarios.Count > 0 Then

                If Peticion.PeticionDatosBancarios IsNot Nothing AndAlso Peticion.PeticionDatosBancarios.DatosBancarios IsNot Nothing Then
                    Dim objAccionDatoBancario As New AccionDatoBancario

                    Dim objRespuestaDatoBancario = objAccionDatoBancario.SetDatosBancarios(Peticion.PeticionDatosBancarios)
                    If objRespuestaDatoBancario.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                        Throw New Exception("Datos Bancarios: " + objRespuestaDatoBancario.MensajeError)
                    End If
                End If

            End If


            objTransacao.RealizarTransacao()
            If (DarAltaPlanta) Then
                AltaPlanta(Peticion)
            End If
            Dim usuario As String = Peticion.DesUsuarioCreacion
            If Peticion.DesUsuarioCreacion Is Nothing Then
                usuario = Peticion.DesUsuarioModificacion
            End If
            'Grava os Codigos Ajenos
            DefinirCodigoAjeno(Peticion.OidDelegacion, Peticion.CodigoAjeno, usuario)

            objRespuesta.CodigoDelegacion = Peticion.CodDelegacion
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function
    Private Function AltaPlanta(Peticion As ContractoServicio.Delegacion.SetDelegacion.Peticion)
        'Doy de alta una planta asociada a la delegacion automaticamente
        DarAltaPlanta = False
        Dim peticionPlanta = New ContractoServicio.Planta.SetPlanta.Peticion()
        peticionPlanta.OidDelegacion = Peticion.OidDelegacion
        peticionPlanta.CodPlanta = Peticion.CodDelegacion
        peticionPlanta.DesPlanta = Peticion.DesDelegacion
        peticionPlanta.CodigosAjenos = Peticion.CodigoAjeno
        peticionPlanta.DesUsuarioCreacion = Peticion.DesUsuarioCreacion
        peticionPlanta.DesUsuarioModificacion = Peticion.DesUsuarioModificacion
        peticionPlanta.BolActivo = True
        Dim objProxyPlanta As New Comunicacion.ProxyPlanta
        objProxyPlanta.SetPlantas(peticionPlanta)
    End Function

    Private Function ModificarPlanta(Peticion As ContractoServicio.Delegacion.SetDelegacion.Peticion, peticionPlanta As ContractoServicio.Planta.SetPlanta.Peticion)
        If Not String.IsNullOrEmpty(peticionPlanta.OidPlanta) Then
            Dim objProxyPlanta As New Comunicacion.ProxyPlanta
            peticionPlanta.OidDelegacion = Peticion.OidDelegacion
            peticionPlanta.CodPlanta = Peticion.CodDelegacion
            peticionPlanta.DesPlanta = Peticion.DesDelegacion
            peticionPlanta.BolActivo = Peticion.BolVigente
            peticionPlanta.DesUsuarioCreacion = Peticion.DesUsuarioModificacion
            peticionPlanta.DesUsuarioModificacion = Peticion.DesUsuarioModificacion
            For Each codigoAjeno In peticionPlanta.CodigosAjenos
                Dim codigoAjenoDelegacion = Peticion.CodigoAjeno.FirstOrDefault(Function(a) a.CodAjeno = codigoAjeno.CodAjeno)
                If (Not codigoAjenoDelegacion Is Nothing) Then
                    codigoAjeno.DesAjeno = codigoAjenoDelegacion.DesAjeno
                End If
            Next
            objProxyPlanta.SetPlantas(peticionPlanta)
        End If
    End Function

#Region "CODIGO AJENO"

    Private Shared Sub DefinirCodigoAjeno(oidDelegacion As String, codigoAjenoColeccion As IAC.ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase, codigoUsuario As String)

        Dim objAccionAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno

        If codigoAjenoColeccion.Count > 0 Then
            Dim objCodigoAjeno As New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion
            objCodigoAjeno.CodigosAjenos = New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion


            For Each objItem In codigoAjenoColeccion
                Dim objItemCast As New IAC.ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno
                Dim codigotablaGenesis As String = String.Empty

                objItemCast.OidCodigoAjeno = objItem.OidCodigoAjeno
                objItemCast.BolActivo = objItem.BolActivo
                objItemCast.BolDefecto = objItem.BolDefecto
                objItemCast.CodAjeno = objItem.CodAjeno
                objItemCast.CodIdentificador = objItem.CodIdentificador
                objItemCast.DesAjeno = objItem.DesAjeno
                If String.IsNullOrEmpty(objItemCast.DesUsuarioCreacion) Then
                    objItemCast.DesUsuarioCreacion = codigoUsuario
                    objItemCast.GmtCreacion = DateTime.Now
                End If
                objItemCast.DesUsuarioModificacion = codigoUsuario
                objItemCast.GmtModificacion = DateTime.Now
                objItemCast.OidTablaGenesis = oidDelegacion
                codigotablaGenesis = IAC.ContractoServicio.Constantes.COD_DELEGACION
                objItemCast.CodTipoTablaGenesis = (From item In IAC.AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                                   Where item.CodTipoTablaGenesis = codigotablaGenesis
                                                   Select item.Entidade).FirstOrDefault()

                objCodigoAjeno.CodigosAjenos.Add(objItemCast)

            Next

            objAccionAjeno.SetCodigosAjenos(objCodigoAjeno)

        End If

    End Sub

#End Region

    ''' <summary>
    ''' Pegar os dados da delegacion
    ''' </summary>
    ''' <param name="ObjPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 15/02/2013 Criado
    ''' </history>
    Public Function GetDelegacioneDetail(ObjPeticion As ContractoServicio.Delegacion.GetDelegacionDetail.Peticion) As ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta Implements ContractoServicio.IDelegacion.GetDelegacioneDetail
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta

        Try
            ' obter dados da delegação
            objRespuesta.Delegacion = AccesoDatos.Delegacion.GetDadosDelegacion(ObjPeticion.CodigoDelegacione)
            'Recupera os Codigos Ajenos
            objRespuesta.Delegacion(0).CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(objRespuesta.Delegacion(0).OidDelegacion.ToString())
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

    ''' <summary>
    ''' Verifica se o codigo da delegação existe
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 15/02/2013 Criado
    ''' </history>
    Public Function VerificaCodigoDelegacion(Peticion As ContractoServicio.Delegacion.VerificaCodigoDelegacion.Peticion) As ContractoServicio.Delegacion.VerificaCodigoDelegacion.Respuesta Implements ContractoServicio.IDelegacion.VerificaCodigoDelegacion
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Delegacion.VerificaCodigoDelegacion.Respuesta

        Try
            'exewcutar verificação no banco
            objRespuesta.Existe = AccesoDatos.Delegacion.VerificaCodigoDelegacion(Peticion)
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
    ''' Assinatura do método GetCodigoDelegacion
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Claudioniz.Pereira] 06/006/2012 Criado
    ''' </history>
    Public Function GetDelegacionByCertificado(objPeticion As ContractoServicio.Delegacion.GetDelegacionByCertificado.Peticion) As ContractoServicio.Delegacion.GetDelegacionByCertificado.Respuesta Implements ContractoServicio.IDelegacion.GetDelegacionByCertificado
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacionByCertificado.Respuesta

        Try
            ' obtém código da delgacion
            objRespuesta = AccesoDatos.Delegacion.GetDelegacionByCertificado(objPeticion)
            If (objRespuesta.Delegaciones.Count() = 0) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("035_msg_codigo_delegacion_noencontrado"))
            End If
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


    ''' <summary>
    ''' Assinatura do método GetDelegacionByIdentificador
    ''' </summary>
    ''' <param name="identificador"></param>
    ''' <param name="identificadorAjeno"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Claudioniz.Pereira] 06/006/2012 Criado
    ''' </history>
    Public Function GetDelegacionByIdentificador(identificador As String, identificadorAjeno As String) As ContractoServicio.Delegacion.GetDelegacion.Respuesta
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacion.Respuesta

        ' obtém código da delgacion
        Dim obj = AccesoDatos.Delegacion.ObtenerCodigoPorIdentificador(identificador, identificadorAjeno)
        objRespuesta.Delegacion = New GetDelegacion.DelegacionColeccion
        objRespuesta.Delegacion.Add(obj)


        Return objRespuesta
    End Function

    Public Function GetDelegacionByPlanificacion(objPeticion As ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Peticion) As ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta Implements ContractoServicio.IDelegacion.GetDelegacionByPlanificacion
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta

        Try
            ' obtém código da delgacion
            objRespuesta = AccesoDatos.Delegacion.GetDelegacionByPlanificacion(objPeticion)
            If (objRespuesta.Delegacion Is Nothing) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT, Traduzir("035_msg_codigo_delegacion_noencontrado"))
            End If
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


    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IDelegacion.Test
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

    Private Sub ValidaPeticion(ByRef Peticion As SetDelegacion.Peticion)

        Dim data1, data2 As DateTime
        Dim unaDelegacion As ContractoServicio.Delegacion.Delegacion

        'Verificar se o código foi enviado
        If String.IsNullOrEmpty(Peticion.OidPais) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "OidPais"))
        End If

        ''Verifica se a codigo da delegação foi enviada
        If String.IsNullOrEmpty(Peticion.CodDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "CodDelegacion"))
        End If

        'Verifica se a descrição foi enviada
        If String.IsNullOrEmpty(Peticion.DesDelegacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesDelegacion"))
        End If

        'Verifica se o GMT Minutos foi enviada
        If Peticion.NecGmtMinutos Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "NecGmtMinutos"))
        End If

        'Verifica se inicio horario de verão descrição foi enviada
        If Peticion.FyhVeranoInicio Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "FyhVeranoInicio"))
        End If

        'Verifica se o fim do horario de verão foi enviada
        If Peticion.FyhVeranoFin Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "FyhVeranoFin"))
        End If

        'Verifica se os minutos de ajustes foram enviados
        If Peticion.NecVeranoAjuste Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "NecVeranoAjuste"))
        End If

        'Verifica se a zona foi enviada
        If String.IsNullOrEmpty(Peticion.DesZona) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesZona"))
        End If

        If String.IsNullOrEmpty(Peticion.OidDelegacion) Then
            If String.IsNullOrEmpty(Peticion.DesUsuarioCreacion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_id_obligatorio"), "DesUsuarioCreacion"))
            End If
        End If

        data1 = CType(Peticion.FyhVeranoInicio, DateTime)
        data2 = CType(Peticion.FyhVeranoFin, DateTime)

        If (data1 > data2) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("013_msg_erro_Data"), "FyhVeranoInicio", "FyhVeranoFin"))
        End If

        If Not Peticion.BolVigente Then
            If Not String.IsNullOrEmpty(Peticion.OidDelegacion) Then
                If Not AccesoDatos.Delegacion.VerificaUtilizacaoPlanta(Peticion.OidDelegacion) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("013_msg_erro_delegacionUtilizacao"))
                End If
            End If
        End If

        If String.IsNullOrEmpty(Peticion.OidDelegacion) Then
            If Not AccesoDatos.Delegacion.VerificaDelegacaoCadastrada(Peticion.OidPais, Peticion.CodDelegacion, Peticion.CodPais) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("039_msg_DelegacionExistente"))
            End If
        End If

        If Peticion.LstClienteFacturacion.Count > 0 Then
            For Each elemento In Peticion.LstClienteFacturacion
                unaDelegacion = New ContractoServicio.Delegacion.Delegacion()
                If Not AccesoDatos.Delegacion.VerificarClienteCapitalYBancoTesoreriaEnUso(Peticion.OidDelegacion, elemento.OidClienteCapital, elemento.OidSubClienteTesoreria, elemento.OidPtoServicioTesoreria, unaDelegacion) Then
                    Dim mensaje As String = String.Empty
                    Dim codigoCultura As String = If(CulturaSistema IsNot Nothing AndAlso
                                                                             Not String.IsNullOrEmpty(CulturaSistema.Name),
                                                                             CulturaSistema.Name,
                                                                             If(CulturaPadrao IsNot Nothing, CulturaPadrao.Name, Globalization.CultureInfo.CurrentCulture.ToString()))

                    mensaje = String.Format(Prosegur.Genesis.AccesoDatos.Genesis.Diccionario.ObtenerValorDicionarioSimples(codigoCultura, String.Empty, "msgCuentaTesoreriaEnUsoEnDelegacion"), unaDelegacion.Description)


                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, mensaje)
                End If
            Next
        End If


    End Sub

End Class